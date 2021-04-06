using UnityEngine;

public class Staircase : MonoBehaviour
{
    public GameObject currentLevel;
    public GameObject nextLevel;

    private void Start()
    {
        nextLevel.SetActive(false);
    }

    public void UseStairs()
    {
        currentLevel.SetActive(false);
        nextLevel.SetActive(true);
    }
}
