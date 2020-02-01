using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public UIView transitionView;
    public UIView label;
    public string sceneToLoad;
    public GameObject player;
    public float activationDistance;
    public SaveState save;
    public string key;

    void Start()
    {
        transitionView = GameObject.Find("View - Transition").GetComponent<UIView>();
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance)
        {
            if (!key.Equals(""))
            {
                if (!save.flags[key])
                {
                    this.enabled = false;
                }
            }
            label.Show();
            if (Input.GetKeyDown(KeyCode.F))
            {
                label.Hide();
                StartCoroutine(LevelTransition());
            }
        }
        else
        {
            label.Hide();
        }
    }

    IEnumerator LevelTransition()
    {
        transitionView.Show();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
