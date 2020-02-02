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
    public SaveState save;
    public AudioClip pressureLose;
    public AudioClip powerLose;

    public bool isInputAvailable = true;
    public float MoveSpeed = 5;
    public float raycastDistance = 0.05f;
    public float horizontalDirection;

    void Start()
    {
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        flashlight = GetComponentsInChildren<Light2D>()[1];
        anim = GetComponentInChildren<Animator>();
        transitionView = GameObject.Find("View - Transition").GetComponent<UIView>();
        transitionView.Hide();
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
                    sprite.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    sprite.transform.localScale = new Vector3(-1, 1, 1);
                }
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
                flashlight.enabled = false;
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            horizontalDirection = 0;
            flashlight.enabled = false;
        }
    }

    public void ChangeInput()
    {
        if (isInputAvailable)
        {
            isInputAvailable = false;
        }
        else
        {
            isInputAvailable = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y + 1f);
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
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(pressureLose, 1);
        transitionView.Show();
        yield return new WaitForSeconds(4f);
        save.Clear();
        SceneManager.LoadScene("GameOpen");
    }

    IEnumerator PowerRoutine()
    {
        transitionView.Show();
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(powerLose, 1);
        yield return new WaitForSeconds(33f);
        save.Clear();
        SceneManager.LoadScene("GameOpen");
    }
}
