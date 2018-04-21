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
        } else if (phase == RunPhase.Goal)
        {
            currentGoalTime = time;
            float currentTime = currentGoalTime - currentStartTime;
            if (GeneralManager.IsPersonalRecord(currentTime))
            {
                GeneralManager.SetRecordRecording(currentRun, currentTime);
            }
            currentRun.Clear();
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
        if (currentStartTime > 0 && currentGoalTime == 0 && GeneralManager.HasRecording())
        {
            SetShadowPosition();
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


