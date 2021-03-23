using UnityEngine;
using TMPro;

public class Description_Box : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI bodyText;

    private void Start()
    {
       ClearDescription();
    }

    public void DisplayDescription(string label, string body)
    {
        labelText.text = label;
        bodyText.text = body;
    }

    public void ClearDescription()
    {
        labelText.text = "Description";
        bodyText.text = "";
    }
}
