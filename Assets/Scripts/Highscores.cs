using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscores : MonoBehaviour {

    [SerializeField] Text btnText;
    [SerializeField] GameObject recentText;
    [SerializeField] GameObject recentInput;
    [SerializeField] GameObject recentSubmit;

    private void Start()
    {
        btnText.text = PlayerPrefs.GetString("Highscore.NextSceneName", "Menu");
        bool showRecent = GeneralManager.CanSubmit;
        recentInput.SetActive(showRecent);
        recentSubmit.SetActive(showRecent);
        recentText.SetActive(showRecent);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Highscore.NextScene", "Scenes/menu"));
    }
}
