using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isInputAvailable = true;
    public float MoveSpeed = 5;
    public Rigidbody2D PlayerController;
    public float raycastDistance = 1;
    private float colliderOffset = 3.2f;

    void Start()
    {
        colliderOffset = gameObject.GetComponent<BoxCollider2D>().size.x / 2;
    }

    void Update()
    {
        if (isInputAvailable)
        {
            float horizontalDirection = Input.GetAxis("Horizontal");

            if (horizontalDirection != 0)
            {
                float sideOffset = colliderOffset * (horizontalDirection < 0 ? -1 : 1);

                Vector2 origin = new Vector2(transform.position.x + sideOffset, transform.position.y);
                Vector2 dir = new Vector2(horizontalDirection < 0 ? -1 : 1, 0);

                Debug.DrawRay(origin, dir * raycastDistance);

                RaycastHit2D hit = Physics2D.Raycast(origin, dir, raycastDistance, LayerMask.GetMask("Default"));
                if (hit.collider == null)
                {
                    gameObject.transform.position += (new Vector3(horizontalDirection, 0, 0) * MoveSpeed * Time.deltaTime);
                }

                else if (hit.collider.tag != "Level")
                {
                    gameObject.transform.position += (new Vector3(horizontalDirection, 0, 0) * MoveSpeed * Time.deltaTime);
                }
            }


        }
    }
}
