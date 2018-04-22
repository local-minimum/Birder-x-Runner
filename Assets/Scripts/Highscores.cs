using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscores : MonoBehaviour {

    [SerializeField] Text btnText;

    private void Start()
    {
        btnText.text = PlayerPrefs.GetString("Highscore.NextSceneName", "Menu");    
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Highscore.NextScene", "Scenes/menu"));
    }
}
