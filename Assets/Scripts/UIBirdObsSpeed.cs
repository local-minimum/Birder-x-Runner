using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBirdObsSpeed : MonoBehaviour {

    [SerializeField] Text spedometer;
    [SerializeField] Start start;
    [SerializeField] Goal goal;
    [SerializeField] UIRecords uiRecords;

    BirdWatching[] birds;

    int observations;

    private void OnEnable()
    {
        if (birds == null) birds = GameObject.FindObjectsOfType<BirdWatching>();
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].OnBirdWatched += HandleObservation;
        }
        start.OnRunStart += HandleRunEvent;
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].OnBirdWatched -= HandleObservation;
        }
        start.OnRunStart -= HandleRunEvent;
        goal.OnRunGoal -= HandleRunEvent;
    }

    private void HandleObservation(string species)
    {
        observations += 1;
        if (startTime != 0) spedometer.text = GetFormattedSpeed();
    }

    float startTime;
    float endTime;

    void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Start)
        {
            startTime = time;
        }
        else if (phase == RunPhase.Goal)
        {
            endTime = time;
            float score = this.score;
            if (GeneralManager.HasBirdRunRecord())
            {
                uiRecords.ShowBirdRunnerResult(score, GeneralManager.GetBirdRunRecord());
            } else
            {
                uiRecords.ShowBirdRunnerResult(score);
            }
            if (GeneralManager.IsPersonalBirdRunRecord(score))
            {
                GeneralManager.SetBirdRunRecord(score);
            }
        }
    }

    void Update()
    {
        if (startTime == 0)
        {
            return;
        }
        spedometer.text = GetFormattedSpeed();
    }


    string GetFormattedSpeed()
    {
        return score.ToString("00.0");
    }

    public float score
    {
        get
        {
            float t = time;            
            return t == 0 ? 0 : observations * 60 / t;
        }
    }

    float time
    {
        get
        {
            if (startTime == 0)
            {
                return 0;
            }
            else if (endTime == 0)
            {
                return Time.time - startTime;
            }
            else
            {
                return endTime - startTime;
            }
        }
    }

}
