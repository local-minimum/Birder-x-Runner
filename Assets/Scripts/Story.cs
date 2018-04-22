using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour {

    [SerializeField]
    Text txt;

    [SerializeField]
    string birdingStory = "";

    [SerializeField]
    string runningStory = "";

    int step = 0;

    private void Start()
    {
        txt.text = birdingStory;
    }

    void Update () {
        if (Input.anyKeyDown)
        {
            if (step == 0)
            {
                GetComponent<Animator>().SetTrigger("Swap");
                step += 1;
                ShowRunningStory();
            }
            else
            {
                SceneManager.LoadScene("Scenes/menu");
            }
        }
	}

    public void ShowRunningStory()
    {
        txt.text = runningStory;
    }
}
