using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D PlayerController;
    public UIView transitionView;
    public GameObject sprite;
    public Light2D flashlight;
    public bool isInputAvailable = true;
    public float MoveSpeed = 5;
    public float raycastDistance = 0.05f;
    public float horizontalDirection;

    private float colliderOffset = 3.2f;

    void Start()
    {
        flashlight = GetComponentsInChildren<Light2D>()[1];
        anim = GetComponentInChildren<Animator>();
        transitionView = GameObject.Find("View - Transition").GetComponent<UIView>();
        transitionView.Hide();
        colliderOffset = gameObject.GetComponent<BoxCollider2D>().size.x / 2;
    }

    void Update()
    {
        if (isInputAvailable)
        {
            horizontalDirection = Input.GetAxis("Horizontal");
            if (horizontalDirection != 0)
            {
                flashlight.enabled = true;
                if (horizontalDirection > 0)
                {
                    sprite.transform.localScale = new Vector3(3, 3, 1);
                }
                else
                {
                    sprite.transform.localScale = new Vector3(-3, 3, 1);
                }
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
                flashlight.enabled = false;
            }
        }
    }

    private void FixedUpdate()
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



    //Death states
    public void PressureFailure()
    {
        StartCoroutine(PressureRoutine());
    }
    public void PowerFailure()
    {
        StartCoroutine(PowerRoutine());
    }

    IEnumerator PressureRoutine()
    {
        transitionView.Show();
        //Play sound & clear progress
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Start");
    }

    IEnumerator PowerRoutine()
    {
        transitionView.Show();
        //Play sound & clear progress
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Start");
    }
}
