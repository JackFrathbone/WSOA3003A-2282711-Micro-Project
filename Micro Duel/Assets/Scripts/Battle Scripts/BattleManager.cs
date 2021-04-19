using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    private string currentTurn;
    public GameManager gameManager;

    //The Two Opponents
    public ActorStats playerStats;
    public ActorStats enemyStats;
    private EnemyLogic enemyLogic;

    public PlayerActionGenerator actionGenerator;

    private ActorActions currentPlayerAction;
    private ActorActions currentEnemyAction;

    //GUI Elements
    [SerializeField] TextMeshProUGUI _descriptionLabel;
    [SerializeField] TextMeshProUGUI _descriptionBody;

    [SerializeField] TextMeshProUGUI _playerStatBody;

    [SerializeField] TextMeshProUGUI _playerWoundLabel;
    [SerializeField] TextMeshProUGUI _playerComboLabel;
    [SerializeField] TextMeshProUGUI _playerRiskLabel;

    [SerializeField] TextMeshProUGUI _enemyWoundLabel;
    [SerializeField] TextMeshProUGUI _enemyComboLabel;
    [SerializeField] TextMeshProUGUI _enemyRiskLabel;

    [SerializeField] TextMeshProUGUI _chanceTooltip;
    [SerializeField] TextMeshProUGUI _woundToolTip;

    private ActionLog actionLog;

    [SerializeField] List<BodyPart> enemyBodyParts = new List<BodyPart>();
    [SerializeField] List<BodyPart> playerBodyParts = new List<BodyPart>();

    private void Start()
    {
        actionLog = GetComponent<ActionLog>();
        gameManager = GetComponent<GameManager>();
    }

    public void StartBattle(ActorStats p1, ActorStats p2)
    {
        gameManager.RemoveOverlays();

        playerStats = p1;
        enemyStats = p2;
        enemyLogic = gameObject.GetComponent<EnemyLogic>();
        actionLog = GetComponent<ActionLog>();

        playerStats.SetBodyPartOwner(playerBodyParts);
        enemyStats.SetBodyPartOwner(enemyBodyParts);

        UpdateStats();

        //Determines which player goes first
        if (playerStats.initiativeSkill > enemyStats.initiativeSkill)
        {
            currentTurn = "Player";
            actionLog.AddToActionLog("Player goes first", Color.green);
        }
        else if(enemyStats.initiativeSkill > playerStats.initiativeSkill)
        {
            currentTurn = "Enemy";
            actionLog.AddToActionLog("Enemy goes first", Color.red);
            enemyLogic.BeginTurn(this);
        }
        else if(enemyStats.initiativeSkill == playerStats.initiativeSkill)
        {
            int splitChance = Random.Range(0, 2);
            if (splitChance == 0)
            {
                currentTurn = "Player";
                actionLog.AddToActionLog("Player goes first", Color.green);
            }
            else
            {
                currentTurn = "Enemy";
                actionLog.AddToActionLog("Enemy goes first", Color.red);
                enemyLogic.BeginTurn(this);
            }
        }

        //Clears action log
        foreach (Transform child in actionLog.eventParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Clears action buttons
        foreach (Transform child in actionGenerator.parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Generate action buttons for the player
        foreach (ActorActions action in playerStats.actions)
        {
            actionGenerator.GenerateActions(action, this);
        }

        //Generate Player Stats Display
        _playerStatBody.text = playerStats.GetStatDescription(enemyStats);
    }

    public void ActivateAction(string label, string body, ActorActions action)
    {
        _descriptionLabel.text = label;
        _descriptionBody.text = body;

        currentPlayerAction = action;
    }

    public void ActivateBodyPart(BodyPart bodyPart)
    {
        if (bodyPart.partOwner == playerStats)
        {
            if (currentTurn == "enemy" && currentEnemyAction != null)
            {
                //Enemy attacks player
                UseActionOnOther(enemyStats, playerStats, currentEnemyAction, bodyPart);
            }
            else if (currentPlayerAction == null)
            {
                _descriptionLabel.text = bodyPart.partType.ToString();
                _descriptionBody.text = bodyPart.GetPartDescription();
            }
        }
        else if (bodyPart.partOwner == enemyStats)
        {
            if (currentTurn == "Player" && currentPlayerAction != null)
            {
                //player attacks enemy
                UseActionOnOther(playerStats, enemyStats, currentPlayerAction, bodyPart);
            }
            else if (currentPlayerAction == null)
            {
                _descriptionLabel.text = bodyPart.partType.ToString();
                _descriptionBody.text = bodyPart.GetPartDescription();
            }
        }
    }

    public void DeselectAction()
    {
        if (currentPlayerAction != null)
        {
            currentPlayerAction = null;
            _descriptionLabel.text = "Description";
            _descriptionBody.text = "";
        }
    }

    public void NextTurn()
    {
        if (currentTurn == "Player")
        {
            currentTurn = "Enemy";
            enemyLogic.BeginTurn(this);
        }
        else if(currentTurn == "Enemy")
        {
            currentTurn = "Player";
        }
    }

    public void ActorDeath(ActorStats actorStats)
    {
        currentTurn = "Over";

        string condition = "";

        if (actorStats == playerStats)
        {
            condition = "Lose";
            actionLog.AddToActionLog("The Player has died", Color.red);
        }
        else
        {
            condition = "Win";
            actionLog.AddToActionLog("The Enemy has died", Color.green);
        }

        StartCoroutine(EndMatch(condition));
    }

    private void UpdateStats()
    {
        _playerWoundLabel.text = "Wounds: " + playerStats.currentWounds.ToString() + "/" + playerStats.health;
        _playerRiskLabel.text = "Risk: " + playerStats.currentRisk.ToString() + "%";
        _playerComboLabel.text = "Combo: " + playerStats.currentCombo.ToString() + "%";

        _enemyWoundLabel.text = "Wounds: " + enemyStats.currentWounds.ToString() + "/" + enemyStats.health;
        _enemyRiskLabel.text = "Risk: " + enemyStats.currentRisk.ToString() + "%";
        _enemyComboLabel.text = "Combo: " + enemyStats.currentCombo.ToString() + "%";
    }

    private void UpdateActionLog(ActorStats actorStats, ActorActions action, string outcome)
    {
        if (actorStats == playerStats)
        {
            actionLog.AddToActionLog("Player used " + action.name + outcome, Color.green);
        }
        else
        {
            actionLog.AddToActionLog("Enemy used " + action.name + outcome, Color.red);
        }


    }

    public void UseActionOnSelf(ActorStats actorStats, ActorActions action)
    {
        if (actorStats == playerStats && currentTurn != "Player")
        {
            return;
        }
        if (actorStats == enemyStats && currentTurn != "Enemy")
        {
            return;
        }

        actorStats.SetRisk(action.GetRiskChange());
        actorStats.SetCombo(action.GetComboChange());
        UpdateStats();
        UpdateActionLog(actorStats, action, "");

        NextTurn();
    }

    public void UseActionOnOther(ActorStats actorStats, ActorStats opponentStats, ActorActions action, BodyPart bodyPart)
    {
        if (CalculateHit(actorStats, opponentStats, action))
        {
            if (CalculateWound(actorStats, opponentStats, action))
            {
                UpdateActionLog(actorStats, action, " against the " + bodyPart.partType + " and struck a wound");
                opponentStats.AddWound(bodyPart);
                bodyPart.DisplayWound();
            }
            else
            {
                UpdateActionLog(actorStats, action, " against the " + bodyPart.partType + " and hit but didn't wound");
            }
        }
        else
        {
            UpdateActionLog(actorStats, action, " against the " + bodyPart.partType + " and missed");
        }

        actorStats.SetRisk(action.GetRiskChange());
        actorStats.SetCombo(action.GetComboChange());
        UpdateStats();
        NextTurn();
    }

    public bool CalculateHit(ActorStats actorStats, ActorStats opponentStats, ActorActions action)
    {
        if (Random.Range(0, 101) <= GetHitChance(actorStats, opponentStats, action))
        {
            return true;
        }
        else return false;
    }

    public bool CalculateWound(ActorStats actorStats, ActorStats opponentStats, ActorActions action)
    {
        if (Random.Range(0, 101) <= GetWoundChance(actorStats, opponentStats, action))
        {
            return true;
        }
        else return false;
    }

    public int GetHitChance(ActorStats p1, ActorStats p2, ActorActions action)
    {
        int hitChance = (action.GetHitBonus() + p2.currentRisk + (p1.attackSkill - p2.defenceSkill));

        if(p2.currentRisk == 100)
        {
            hitChance = 100;
        }

        //Gives an extra five chance to hit if enemy legs are wounded
        if (p2.bodyParts[3].isWounded)
        {
            hitChance += 5;
        }

        //Reduces chance to hit if your arms are wounded
        if (p1.bodyParts[2].isWounded)
        {
            hitChance -= 5;
        }

        return hitChance;
    }

    public int GetWoundChance(ActorStats p1, ActorStats p2, ActorActions action)
    {
        int woundChance = (action.GetWoundBonus() + p1.currentCombo + (p1.attackSkill - p2.defenceSkill));

        if (p2.currentRisk == 100)
        {
            woundChance = 100;
        }

        //Gives an extra five chance to hit if enemy legs are wounded
        if (p2.bodyParts[1].isWounded)
        {
            woundChance += 5;
        }

        //Reduces chance to hit if your arms are wounded
        if (p1.bodyParts[0].isWounded)
        {
            woundChance -= 5;
        }

        return woundChance;
    }

    public void ShowChanceTooltip(BodyPart bodyPart)
    {
        if (currentPlayerAction != null)
        {
            _chanceTooltip.gameObject.SetActive(true);
            _woundToolTip.gameObject.SetActive(true);
            _chanceTooltip.text = "Hit: " + GetHitChance(playerStats, enemyStats, currentPlayerAction).ToString() + "%" + "\n" + "Wound: " + GetWoundChance(playerStats, enemyStats, currentPlayerAction).ToString() + "%";
            _woundToolTip.text = "Wound Effect: " + "\n" + bodyPart.GetPartWoundingDescription();
        }
    }

    public void HideChanceToolTip()
    {
        _chanceTooltip.gameObject.SetActive(false);
        _woundToolTip.gameObject.SetActive(false);
    }

    public void ResetMatch()
    {
        playerStats.ResetStats();
        enemyStats.ResetStats();
        StartBattle(playerStats, enemyStats);
    }

    public void ClearMatch()
    {
        playerStats.ResetStats();
        enemyStats.ResetStats();

        enemyStats.gameObject.SetActive(false);

        enemyStats = null;
    }

    private string LevelUpPlayer()
    {
        //adds the stats for the level up and outputs a string for the display
        string _levelUpText = "";

        playerStats.attackSkill += 5;
        playerStats.defenceSkill += 5;

        _levelUpText = "You have gained +5 attack" + "\n" + "You have gained +5 defence" + "\n";

        if(playerStats.health < enemyStats.health)
        {
            _levelUpText += "You gained +" + (enemyStats.health - playerStats.health).ToString() + " health";
            playerStats.health = enemyStats.health;
        }

        return _levelUpText;
    }

    private IEnumerator EndMatch(string condition)
    {
        yield return new WaitForSeconds(2f);
        if (condition == "Win")
        {
            gameManager.WinMatch(LevelUpPlayer());
        }
        else if(condition == "Lose")
        {
            gameManager.LoseMatch();
        }
    }
}
