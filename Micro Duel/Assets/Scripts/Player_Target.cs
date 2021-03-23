using UnityEngine;

public class Player_Target : MonoBehaviour
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
        if (battleManager.selectedAction != null)
        {
            battleManager.PlayerAttackOnSelf();
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
            if (battleManager.playerStats.headWound == true)
            {
                statusEffets = "The head has been wounded and makes the player more vulnerable";
            }
        }
        else if (descriptionLabel == "Torso")
        {
            if (battleManager.playerStats.torsoWound == true)
            {
                statusEffets = "The torso has been wounded and has increased the chance of the player being wounded again";
            }
        }
        else if (descriptionLabel == "Arms")
        {
            if (battleManager.playerStats.armWound == true)
            {
                statusEffets = "The arms have been wounded and have decreased the chance to hit of the player";
            }
        }
        else if (descriptionLabel == "Legs")
        {
            if (battleManager.playerStats.legWound == true)
            {
                statusEffets = "The legs have been wounded and have increased the chance of the player being hit";
            }
        }

        battleManager.descriptionBox.DisplayDescription(descriptionLabel, statusEffets);
    }
}
