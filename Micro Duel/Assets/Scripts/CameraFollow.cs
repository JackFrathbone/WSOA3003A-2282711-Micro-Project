using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform cameraFollow;
    public Vector3 offset;

    private void Update()
    {
        cameraFollow.position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, 0f);
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
}
