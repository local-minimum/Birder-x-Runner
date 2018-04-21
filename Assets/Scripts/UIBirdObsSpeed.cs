using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBirdObsSpeed : MonoBehaviour {

    [SerializeField] Text spedometer;
    [SerializeField] Start start;
    [SerializeField] Goal goal;

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
        if (startTime != 0) spedometer.text = GetFormattedSpeed(time);
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
        }
    }

    void Update()
    {
        if (startTime == 0)
        {
            return;
        }
        spedometer.text = GetFormattedSpeed(time);
    }


    string GetFormattedSpeed(float time)
    {
        return (observations * 60 / time ).ToString("00.0");
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
