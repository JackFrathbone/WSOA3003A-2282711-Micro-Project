using TMPro;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    public string actionName;
    private string actionDescription;

    public bool useOnSelf;

    //The stats
    public int chanceToHit;
    public int chanceToWound;
    public int vulnerabilityMod;
    public int comboMod;

    private Battle_Manager battleManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Battle_Manager>();
        GetComponentInChildren<TextMeshProUGUI>().text = actionName;

        if (!useOnSelf)
        {
        actionDescription = "Hit Bonus: +" + chanceToHit.ToString() + "\n" + "Wound Bonus: +" + chanceToWound.ToString() + "\n" + "Vuln Increase: +" + vulnerabilityMod.ToString() + "\n" + "Combo Increase: +" + comboMod.ToString();
        }
        else
        {
            actionDescription = "Hit Bonus: +" + chanceToHit.ToString() + "\n" + "Wound Bonus: +" + chanceToWound.ToString() + "\n" + "Vuln Increase: " + vulnerabilityMod.ToString() + "\n" + "Combo Increase: " + comboMod.ToString();
        }
    }

    public void ClickToDisplay()
    {
        battleManager.descriptionBox.DisplayDescription(actionName, actionDescription);
        battleManager.selectedAction = this;
    }
}
