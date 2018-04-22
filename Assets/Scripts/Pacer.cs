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

    [SerializeField]
    float stepLength = 1.2f;

    private void Start()
    {
        uiPaceMeter.cadance = cadance;
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

    [SerializeField] float missedStepFactor;

    private void UiPaceMeter_OnStep(int steps, Leg leg)
    {
        if (steps > lastStep + 1)
        {
            cadance -= cadanceDecrease.Evaluate(cadance) * missedStepFactor;
            cadance = Mathf.Max(cadance, 0);
            Debug.Log(string.Format("CADANCE MISSED STEP {0}", cadance));
            uiPaceMeter.cadance = cadance;
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

    [SerializeField] AnimationCurve cadanceIncrease;
    [SerializeField] AnimationCurve cadanceDecrease;
    [SerializeField] float fumbleFactor = 10;

    void UpdatePace(PaceEffect adjustment)
    {
        switch(adjustment)
        {
            case PaceEffect.Increase:
                cadance += cadanceIncrease.Evaluate(cadance);
                break;
            case PaceEffect.Decrease:
                cadance -= cadanceDecrease.Evaluate(cadance);
                break;
            case PaceEffect.Fumble:
                cadance -= cadanceDecrease.Evaluate(cadance) * fumbleFactor;
                break;
        }
        cadance = Mathf.Max(cadance, 0);
        Debug.Log(string.Format("CADANCE {0} {1}", adjustment, cadance));
        uiPaceMeter.cadance = cadance;
    }

    public float Speed
    {
        get
        {
            return cadance * stepLength / 60;
        }
    }
}
