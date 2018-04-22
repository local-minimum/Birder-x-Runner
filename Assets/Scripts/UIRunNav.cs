using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRunNav : MonoBehaviour {

    [SerializeField] GameObject nav;
    [SerializeField, Range(0, 10)] float delay;
    [SerializeField] Goal goal;

    void Start () {
        nav.SetActive(false);	
	}

    private void OnEnable()
    {
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        goal.OnRunGoal -= HandleRunEvent;
    }

    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Goal)
        {
            StartCoroutine(ShowNav());
        }
    }

    IEnumerator<WaitForSeconds> ShowNav()
    {
        yield return new WaitForSeconds(delay);
        nav.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/menu");
    }

    public void LoadHighscore()
    {
        PlayerPrefs.SetString("Highscore.NextScene", "Scenes/1k");
        SceneManager.LoadScene("Scenes/highscore");
    }
}
