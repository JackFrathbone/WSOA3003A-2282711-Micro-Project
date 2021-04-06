using UnityEngine;
using TMPro;

public class ActionLog : MonoBehaviour
{
    public GameObject eventParent;
    public GameObject eventText;

    public void AddToActionLog(string input, Color c)
    {
        GameObject textLog = Instantiate(eventText, eventParent.transform);
        textLog.GetComponent<TextMeshProUGUI>().text = input;
        textLog.GetComponent<TextMeshProUGUI>().color = c;
    }
}
