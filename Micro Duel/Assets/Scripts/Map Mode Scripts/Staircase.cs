using UnityEngine;

public class Staircase : MonoBehaviour
{
    public GameObject currentLevel;
    public GameObject nextLevel;

    public bool startOff;

    private void Start()
    {
        if (startOff)
        {
            currentLevel.SetActive(false);
        }
    }

    public void UseStairs()
    {
        //currentLevel.SetActive(false);
        Destroy(currentLevel);
        nextLevel.SetActive(true);
    }
}
