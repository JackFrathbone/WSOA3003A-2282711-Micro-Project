using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControlManager playerControlManager;
    private SpriteRenderer visual;

    public ActorStats onEnemy;

    public Staircase onStaircase;

    public bool onItem;
    private GameObject item;

    private void Start()
    {
        playerControlManager = GetComponentInParent<PlayerControlManager>();
        visual = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (visual.enabled && onEnemy == null && onStaircase == null && onItem == false)
        {
            playerControlManager.Move(this.transform.position);
        }
        else if (visual.enabled && onEnemy != null)
        {
            playerControlManager.StartBattle(onEnemy);
        }
        else if (visual.enabled && onStaircase != null)
        {
            playerControlManager.UseStaircase(onStaircase, this.transform.position);
        }
        else if (visual.enabled && onItem != false)
        {
            playerControlManager.UseItem();
            playerControlManager.Move(this.transform.position);
            Destroy(item);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            visual.enabled = false;
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            onEnemy = collision.gameObject.GetComponent<ActorStats>();
            visual.enabled = true;
        }
        else if(collision.gameObject.tag == "Staircase")
        {
            onStaircase = collision.gameObject.GetComponent<Staircase>();
            visual.enabled = true;
        }
        else if(collision.gameObject.tag == "Item")
        {
            onItem = true;
            item = collision.gameObject;
            visual.enabled = true;
        }
        else
        {
            visual.enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        visual.enabled = true;
        onEnemy = null;

        onItem = false;
    }
}
