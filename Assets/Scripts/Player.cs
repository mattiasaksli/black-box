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
    AudioManager AM;

    public bool isInputAvailable = true;
    public bool levelChanging = false;
    public float MoveSpeed = 5;
    public float raycastDistance = 0.05f;
    public float horizontalDirection;
    bool mobile = false;

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        flashlight = GetComponentsInChildren<Light2D>()[1];
        anim = GetComponentInChildren<Animator>();
        transitionView = GameObject.Find("View - Transition").GetComponent<UIView>();
        transitionView.Hide();
        if (Application.isMobilePlatform)
        {
            mobile = true;
        }
    }

    void Update()
    {
        if (isInputAvailable)
        {
            anim.SetBool("Flashlight", true);
            if (mobile)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x > Screen.currentResolution.width * 0.6f)
                    {
                        horizontalDirection = 1;
                    }
                    else if (touch.position.x < Screen.currentResolution.width * 0.4f)
                    {
                        horizontalDirection = -1;
                    }
                }
                else
                {
                    horizontalDirection = 0;
                }
            }
            else
            {
                horizontalDirection = Input.GetAxis("Horizontal");
            }

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
            }
        }
        else
        {
            if (!levelChanging)
            {
                anim.SetBool("Flashlight", false);
            }
            anim.SetBool("Walk", false);
            horizontalDirection = 0;
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

    private void LateUpdate()
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
        transitionView.Show();
        AM.PlaySound(pressureLose, 1);
        AM.GetComponents<AudioSource>()[0].Stop();
        AM.GetComponents<AudioSource>()[1].Stop();
        yield return new WaitForSeconds(pressureLose.length);
        SceneManager.LoadScene("GameOpen");
    }

    IEnumerator PowerRoutine()
    {
        transitionView.Show();
        AM.PlaySound(powerLose, 1);
        AM.GetComponents<AudioSource>()[0].Stop();
        AM.GetComponents<AudioSource>()[1].Stop();
        yield return new WaitForSeconds(powerLose.length);
        SceneManager.LoadScene("GameOpen");
    }
}
