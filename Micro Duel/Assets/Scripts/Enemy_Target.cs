using UnityEngine;

public class Enemy_Target : MonoBehaviour
{
    public string descriptionLabel;

    private string statusEffets = "No wounds or effects";

    private Battle_Manager battleManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Battle_Manager>();
    }

    public void ClickToActivate()
    {
        if(battleManager.selectedAction != null)
        {
            battleManager.PlayerAttackOnEnemy(descriptionLabel);
        }
        else
        {
            ClickToDisplay();
        }
    }

    public void ClickToDisplay()
    {
        if (descriptionLabel == "Head")
        {
            if(battleManager.enemyStats.headWound == true)
            {
                statusEffets = "The head has been wounded and makes the enemy more vulnerable";
            }
        }
        else if (descriptionLabel == "Torso")
        {
            if (battleManager.enemyStats.torsoWound == true)
            {
                statusEffets = "The torso has been wounded and has increased the chance of the enemy being wounded again";
            }
        }
        else if (descriptionLabel == "Arms")
        {
            if (battleManager.enemyStats.armWound == true)
            {
                statusEffets = "The arms have been wounded and have decreased the chance to hit of the enemy";
            }
        }
        else if (descriptionLabel == "Legs")
        {
            if (battleManager.enemyStats.legWound == true)
            {
                statusEffets = "The legs have been wounded and have increased the chance of the enemy being hit";
            }
        }

        battleManager.descriptionBox.DisplayDescription(descriptionLabel, statusEffets);
    }
}
