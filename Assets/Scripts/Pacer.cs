using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : MonoBehaviour {
    [SerializeField] UIPaceMeter uiPaceMeter;

    [SerializeField]
    KeyCode leftKey;

    [SerializeField]
    KeyCode rightKey;
    [SerializeField] Start start;
    [SerializeField] Goal goal;

    float cadance = 80f;
    int lastStep = 0;

    float stepLength = 1.2f;

    private void Start()
    {
        uiPaceMeter.pace = cadance;
    }

    private void OnEnable()
    {
        uiPaceMeter.OnStep += UiPaceMeter_OnStep;
        start.OnRunStart += HandleRunEvent;
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        uiPaceMeter.OnStep -= UiPaceMeter_OnStep;
        start.OnRunStart -= HandleRunEvent;
        goal.OnRunGoal -= HandleRunEvent;
    }

    bool running = false;
    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Start)
        {
            running = true;
            uiPaceMeter.running = true;
        } else
        {
            running = false;
            uiPaceMeter.running = false;
        }
    }

    private void UiPaceMeter_OnStep(int steps, Leg leg)
    {
        if (steps > lastStep + 1)
        {
            cadance = Mathf.Clamp(Mathf.Min(cadance - 10f, cadance * 0.7f), 40, 250);
            Debug.Log(string.Format("CADANCE MISSED STEP {0}", cadance));
            uiPaceMeter.pace = cadance;
        }
    }

    void Update () {
        if (!running)
        {
            return;
        }

        if (Input.GetKeyDown(leftKey))
        {
            UpdatePace(uiPaceMeter.Score(Leg.Left));
            lastStep = uiPaceMeter.Steps;
        } else if (Input.GetKeyDown(rightKey))
        {
            UpdatePace(uiPaceMeter.Score(Leg.Right));
            lastStep = uiPaceMeter.Steps;
        }
    }

    void UpdatePace(PaceEffect adjustment)
    {
        switch(adjustment)
        {
            case PaceEffect.Increase:
                cadance += 5f;
                break;
            case PaceEffect.Decrease:
                cadance -= 5f;
                break;
            case PaceEffect.Fumble:
                cadance *= 0.4f;
                break;
        }
        cadance = Mathf.Clamp(cadance, 40, 250);
        Debug.Log(string.Format("CADANCE {0} {1}", adjustment, cadance));
        uiPaceMeter.pace = cadance;
    }

    public float Speed
    {
        get
        {
            return cadance * stepLength / 60;
        }
    }
}
