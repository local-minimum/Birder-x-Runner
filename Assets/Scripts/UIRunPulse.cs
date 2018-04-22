using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunPulse : MonoBehaviour {

    [SerializeField] RunController rc;
    [SerializeField] Pacer pacer;
    [SerializeField] Text txt;
    [SerializeField] Start start;
    [SerializeField] Goal goal;

    [SerializeField]
    AnimationCurve effortToTargetPulse;

    float pulse;

    [SerializeField, Range(0, .5f)]
    float loweringAttack = 0.4f;

    [SerializeField, Range(0, .5f)]
    float increasingAttack = 1;

    [SerializeField]
    float climbEffortFactor = 20;

    [SerializeField]
    float descentEffortFactor = 5;

    private void Start()
    {
        pulse = effortToTargetPulse.Evaluate(0);
        StartCoroutine(SetBPMText());
    }

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

    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Start)
        {
            running = true;
        } else if (phase == RunPhase.Goal)
        {
            running = false;
        }
    }

    bool running;
    float startOverEffort = -1;
    
    void Update () {
        float targetPulse = effortToTargetPulse.Evaluate(GetEffort());
        float t = 1;
        if (targetPulse > pulse)
        {
            if (startOverEffort < 0)
            {
                startOverEffort = Time.time;
                t = increasingAttack;
            } else
            {
                t = increasingAttack * (1 + Time.time - startOverEffort);
            }
            
        }
        else
        {
            startOverEffort = -1;
            t = loweringAttack;
        }

        pulse = Mathf.Lerp(pulse, targetPulse, t * Time.deltaTime);
	}

    float GetEffort()
    {
        if (!running) return 0;
        float slope = rc.Slope;
        float speed = pacer.Speed;
        float climb = Mathf.Sin(slope);
        float factor = 0;
        if (climb < 0) {
            factor = descentEffortFactor;
        } else
        {
            factor = climbEffortFactor;
        }
        return (speed + Mathf.Abs(climb) * factor);
    }

    [SerializeField, Range(0.5f, 3f)]
    float updateFreq = 1.4f;

    IEnumerator<WaitForSeconds> SetBPMText()
    {
        while (true)
        {
            txt.text = string.Format("{0}bpm", pulse.ToString("000"));
            yield return new WaitForSeconds(updateFreq);
        }
    }
}
