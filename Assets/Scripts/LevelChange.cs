﻿using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public UIView transitionView;
    public UIView label;
    public int sceneToLoad;
    public GameObject player;
    public float activationDistance;
    // Start is called before the first frame update
    void Start()
    {
        transitionView.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance)
        {
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