using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void LoadStory()
    {
        SceneManager.LoadScene("Scenes/story");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Scenes/tutorial");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Scenes/credits");
    }

    public void LoadHighscore()
    {
        SceneManager.LoadScene("Scenes/highscore");
    }

    public void Load1k()
    {
        SceneManager.LoadScene("Scenes/1k");
    }
}
