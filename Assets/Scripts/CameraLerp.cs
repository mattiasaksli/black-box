using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public GameObject player;
    public float smoothTime;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothTime * Time.deltaTime);
    }
}
