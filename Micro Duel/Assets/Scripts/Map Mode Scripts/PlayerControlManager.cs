using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Move(Vector2 newPos)
    {
        gameObject.transform.position = newPos;
    }

    public void StartBattle(ActorStats enemyStats)
    {
        gameManager.ActivateBattle(enemyStats);
    }

    public void UseStaircase(Staircase staircase)
    {
        staircase.UseStairs();
    }
}
