using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunClock : MonoBehaviour {

    [SerializeField] Start start;
    [SerializeField] Goal goal;
    [SerializeField] Text clock;
    [SerializeField] UIRecords uiRecords;

    private void OnEnable()
    {
        start.OnRunStart += HandleRunEvent;
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        start.OnRunStart -= HandleRunEvent;
        goal.OnRunGoal -= HandleRunEvent;
    }

    float startTime;
    float endTime;

    void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Start)
        {
            startTime = time;
        } else if (phase == RunPhase.Goal)
        {
            endTime = time;
            float runTime = endTime - startTime;
            if (GeneralManager.HasRunRecord())
            {
                
                uiRecords.ShowRunnerResult(runTime, GeneralManager.GetRunTimeRecord());
                
            }
            else
            {
                uiRecords.ShowRunnerResult(runTime);
            }
            if (GeneralManager.IsPersonalRunRecord(runTime))
            {
                GeneralManager.SetRunRecord(runTime);
            }
            GeneralManager.RecentRunTime = runTime;
        }
    }

	void Update () {
        clock.text = GetFormattedDuration(time);
	}

    float time
    {
        get
        {
            if (startTime == 0)
            {
                return 0;
            } else if (endTime == 0)
            {
                return Time.time - startTime;
            } else
            {
                return endTime - startTime;
            }
        }
    }

    string GetFormattedDuration(float duration)
    {
        int minutes = Mathf.FloorToInt(duration / 60);
        float seconds = duration - minutes;
        return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00.00"));
    }
}
