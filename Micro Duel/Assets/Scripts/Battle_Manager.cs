using UnityEngine;
using TMPro;
using System.Collections;

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

    private void Start()
    {
        currentState = "PlayerTurn";
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
            }
            else if (targetedPart == "Torso")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
            }
            else if (targetedPart == "Arms")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
            }
            else if (targetedPart == "Legs")
            {
                GameObject textLog = Instantiate(eventText, eventParent.transform);
                textLog.GetComponent<TextMeshProUGUI>().text = EnemyHitCalculator(usedAction, targetedPart);
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

        if (hitRoll <= (usedAction.chanceToHit + enemyStats.currentVulnerability + (playerStats.combo / 100 * usedAction.chanceToHit) + (hitPenatly / 100 * usedAction.chanceToHit) + (hitBonus / 100 * usedAction.chanceToHit)))
        {
            int woundRoll = Random.Range(0, 101);
            if (woundRoll <= usedAction.chanceToWound + (woundBonus / 100 * usedAction.chanceToWound))
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
    }

    IEnumerator EnemyTurnWait()
    {
        yield return new WaitForSeconds(5);
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
}
