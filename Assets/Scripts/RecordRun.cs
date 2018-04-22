using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordRun : MonoBehaviour {

    List<KeyValuePair<float, Vector2>> currentRun = new List<KeyValuePair<float, Vector2>>();

	[SerializeField] Start start;
    [SerializeField] Goal goal;
    [SerializeField] RunController runController;

    float currentStartTime;
    float currentGoalTime;

    private void OnEnable()
    {
        start.OnRunStart += HandleRunEvent;
        goal.OnRunGoal += HandleRunEvent;
        runController.OnGPS += HandleGPS;
    }

    private void OnDisable()
    {
        start.OnRunStart -= HandleRunEvent;
        goal.OnRunGoal -= HandleRunEvent;
        runController.OnGPS -= HandleGPS;
    }

    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Start)
        {
            currentStartTime = time;
            bool show = GeneralManager.HasRun();
            SpriteRenderer[] rends = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < rends.Length; i++) rends[i].enabled = show;
        }
        else if (phase == RunPhase.Goal)
        {
            currentGoalTime = time;
            float currentTime = currentGoalTime - currentStartTime;
            if (GeneralManager.IsPersonalRunRecord(currentTime))
            {
                GeneralManager.SetRunRecording(currentRun);
            }
            currentRun.Clear();
            SpriteRenderer[] rends = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < rends.Length; i++) rends[i].enabled = false;
        }
    }

    private void HandleGPS(Vector2 pos)
    {
        if (currentStartTime != 0)
        {
            currentRun.Add(new KeyValuePair<float, Vector2>(Time.time - currentStartTime, pos));
        }
        
    }

    private void Update()
    {
        if (currentStartTime > 0 && currentGoalTime == 0 && GeneralManager.HasRunRecord())
        {
            SetShadowPosition();
        } else
        {
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Scenes/1k");
        }
    }

    void SetShadowPosition()
    {
        float curProgress = Time.time - currentStartTime;
        transform.position = GeneralManager.GetBestRunPosition(curProgress);
    }
}


