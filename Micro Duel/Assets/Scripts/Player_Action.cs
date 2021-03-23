using TMPro;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    public string actionName;
    public string actionDescription;

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
    }

    public void ClickToDisplay()
    {
        battleManager.descriptionBox.DisplayDescription(actionName, actionDescription);
        battleManager.selectedAction = this;
    }
}
