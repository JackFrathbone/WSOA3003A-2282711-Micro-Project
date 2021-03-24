using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Battle_Manager : MonoBehaviour
{
    public Description_Box descriptionBox;
    public Player_Action selectedAction;

    public string currentState;

    //For the event log
    public GameObject eventParent;
    public GameObject eventText;

    public Player_Stats playerStats;
    public Player_Stats enemyStats;

    private Enemy_Turn_Logic turnLogic;

    private Color enemyColor = Color.red;
    private Color playerColor = Color.green;

    public GameObject overlay;
    public GameObject winScreen;
    public GameObject loseScreen;

    private void Start()
    {
        currentState = "PlayerTurn";

        turnLogic = GetComponent<Enemy_Turn_Logic>();
    }

    public void PlayerAttackOnEnemy(string targetedPart)
    {
        if (currentState == "PlayerTurn")
        {
            Player_Action usedAction = selectedAction;

            if (targetedPart == "Head")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = playerColor;
            }
            else if (targetedPart == "Torso")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = playerColor;
            }
            else if (targetedPart == "Arms")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = playerColor;
            }
            else if (targetedPart == "Legs")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = playerColor;
            }

            playerStats.UpdateVulnerability(usedAction.vulnerabilityMod);
            playerStats.UpdateCombo(usedAction.comboMod);

            DeselectAction();
            EnemyTurnStart();
        }
    }

    public void PlayerAttackOnSelf()
    {
        if (currentState == "PlayerTurn")
        {
            Player_Action usedAction = selectedAction;

            if (usedAction.useOnSelf == true)
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = "The player uses " + usedAction.actionName;
                textLog.GetComponent<TextMeshProUGUI>().color = playerColor;

                playerStats.UpdateVulnerability(usedAction.vulnerabilityMod);
                playerStats.UpdateCombo(usedAction.comboMod);

                DeselectAction();
                EnemyTurnStart();
            }
        }

    }

    private string EnemyHitCalculator(Player_Action usedAction, string targetedPart)
    {
        string result = null;
        int woundBonus = 0;
        int hitBonus = 0;
        int hitPenatly = 0;

        //Roll to hit
        int hitRoll = Random.Range(0, 101);
        if (enemyStats.torsoWound)
        {
            woundBonus = 5;
        }

        if (enemyStats.legWound)
        {
            hitBonus = 5;
        }

        if (playerStats.armWound)
        {
            hitPenatly = -5;
        }

        if (hitRoll <= (usedAction.chanceToHit + enemyStats.currentVulnerability + (hitPenatly / 100 * usedAction.chanceToHit) + (hitBonus / 100 * usedAction.chanceToHit)))
        {
            int woundRoll = Random.Range(0, 101);
            if (woundRoll <= usedAction.chanceToWound + playerStats.combo +(woundBonus / 100 * usedAction.chanceToWound))
            {
                result = " and wounds";
                enemyStats.UpdateWound(targetedPart);
            }
            else
            {
                result = " and hits but doesn't wound";
            }
        }
        else
        {
            result = " and misses";
        }

        return "Player used " + usedAction.actionName + " against the " + targetedPart + result;
    }

    private void EnemyTurnStart()
    {
        currentState = "EnemyTurn";
        StartCoroutine(EnemyTurnWait());
    }

    public void EnemyAttackOnPlayer(Player_Action action, string targetedPart)
    {
        if (currentState == "EnemyTurn")
        {
            Player_Action usedAction = action;

            if (targetedPart == "Head")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = PlayerHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = enemyColor;
            }
            else if (targetedPart == "Torso")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = PlayerHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = enemyColor;
            }
            else if (targetedPart == "Arms")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = PlayerHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = enemyColor;
            }
            else if (targetedPart == "Legs")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = PlayerHitCalculator(usedAction, targetedPart);
                textLog.GetComponent<TextMeshProUGUI>().color = enemyColor;
            }

            enemyStats.UpdateVulnerability(usedAction.vulnerabilityMod);
            enemyStats.UpdateCombo(usedAction.comboMod);
        }
    }

    public void EnemyAttackOnSelf(Player_Action action)
    {
        if (currentState == "EnemyTurn")
        {
            Player_Action usedAction = action;

            GameObject textLog = Instantiate(eventText, eventParent.transform);
            textLog.GetComponent<TextMeshProUGUI>().text = "The enemy uses " + usedAction.actionName;
            textLog.GetComponent<TextMeshProUGUI>().color = enemyColor;

            enemyStats.UpdateVulnerability(usedAction.vulnerabilityMod);
            enemyStats.UpdateCombo(usedAction.comboMod);
        }
    }

    private string PlayerHitCalculator(Player_Action usedAction, string targetedPart)
    {
        string result = null;
        int woundBonus = 0;
        int hitBonus = 0;
        int hitPenatly = 0;

        //Roll to hit
        int hitRoll = Random.Range(0, 101);
        if (playerStats.torsoWound)
        {
            woundBonus = 5;
        }

        if (playerStats.legWound)
        {
            hitBonus = 5;
        }

        if (enemyStats.armWound)
        {
            hitPenatly = -5;
        }

        if (hitRoll <= (usedAction.chanceToHit + playerStats.currentVulnerability + (hitPenatly / 100 * usedAction.chanceToHit) + (hitBonus / 100 * usedAction.chanceToHit)))
        {
            int woundRoll = Random.Range(0, 101);
            if (woundRoll <= usedAction.chanceToWound + enemyStats.combo + (woundBonus / 100 * usedAction.chanceToWound))
            {
                result = " and wounds";
                playerStats.UpdateWound(targetedPart);
            }
            else
            {
                result = " and hits but doesn't wound";
            }
        }
        else
        {
            result = " and misses";
        }

        return "Enemy used " + usedAction.actionName + " against the " + targetedPart + result;
    }

    IEnumerator EnemyTurnWait()
    {
        yield return new WaitForSeconds(2);
        turnLogic.StartTurn(this);
        yield return new WaitForSeconds(1);
        currentState = "PlayerTurn";
    }

    public void DeselectAction()
    {
        if (selectedAction != null)
        {
            selectedAction = null;
            descriptionBox.ClearDescription();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndMatch(Player_Stats origin)
    {
        if(origin.playerType == "Player")
        {
            Lose();
        }
        else
        {
            Win();
        }
    }

    private void Win()
    {
        overlay.SetActive(true);
        winScreen.SetActive(true);
    }

    private void Lose()
    {
        overlay.SetActive(true);
        loseScreen.SetActive(true);
    }
}
