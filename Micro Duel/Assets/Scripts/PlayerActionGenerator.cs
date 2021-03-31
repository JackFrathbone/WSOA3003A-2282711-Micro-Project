using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerActionGenerator : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject parent;

    public void GenerateActions(ActorActions action, BattleManager battleManager)
    {
        GameObject actionButton = Instantiate(textPrefab, parent.transform);
        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = action.name;
        actionButton.GetComponentInChildren<ActionButton>().action = action;
        actionButton.GetComponentInChildren<ActionButton>().battleManager = battleManager;

        if(action.CheckUseOnSelf() == true)
        {
            actionButton.GetComponent<Image>().color = Color.red;
        }
    }
}
