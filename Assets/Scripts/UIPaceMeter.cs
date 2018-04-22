using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Leg { Left, Right };
public enum PaceEffect { Increase, Same, Decrease, Fumble };

public delegate void StepEvent(int steps, Leg leg);
public delegate void MidStep(int steps);

public class UIPaceMeter : MonoBehaviour
{
    public event StepEvent OnStep;
    public event MidStep OnMidStep;

    [SerializeField] Image[] loweringPaceImages;
    [SerializeField] Image[] samePaceImages;
    [SerializeField] Image[] increasingPaceImages;

    [SerializeField] RectTransform paceMarker;

    [HideInInspector]
    public float cadance = 70;

    float stepMarkerYmin;
    float stepMarkerYmax;

    [SerializeField] RunController rc;
    [SerializeField] UIRunPulse pulseMeter;

    float position = 0.5f;
    float direction = 1f;

    float runStart;
    int steps = 0;

    private void Start()
    {
        float difficulty = cadanceDifficulty;
        loweringThreshold = loweringTarget.Evaluate(difficulty);
        sameThreshold = sameTarget.Evaluate(difficulty);
        increasingThreshold = increasingTarget.Evaluate(difficulty);
        SetZoneImages();
        GetMarkerYSize();
        runStart = Time.timeSinceLevelLoad;
    }

    [HideInInspector]
    public bool running = false;

    private void Update()
    {
        if (!running)
        {
            return;
        }
        SetZoneThresholds();
        SetZoneImages();
        SetCadancePosition();
    }

    [SerializeField] AnimationCurve loweringTarget;
    [SerializeField] AnimationCurve sameTarget;
    [SerializeField] AnimationCurve increasingTarget;
    
    float loweringThreshold;
    float sameThreshold;
    float increasingThreshold;
    [SerializeField, Range(0, 2f)]
    float attack = 0.1f;

    void SetZoneThresholds()
    {
        float difficulty = cadanceDifficulty;
        loweringThreshold = Mathf.Lerp(loweringThreshold, loweringTarget.Evaluate(difficulty), attack * Time.deltaTime);
        sameThreshold = Mathf.Lerp(sameThreshold, sameTarget.Evaluate(difficulty), attack * Time.deltaTime);
        increasingThreshold = Mathf.Lerp(increasingThreshold, increasingTarget.Evaluate(difficulty), attack * Time.deltaTime);
    }

    void SetZoneImages()
    {
        for (int i = 0; i < 2; i++)
        {
            loweringPaceImages[i].fillAmount = loweringThreshold;
            samePaceImages[i].fillAmount = sameThreshold;
            increasingPaceImages[i].fillAmount = increasingThreshold;
        }
    }

    [SerializeField] AnimationCurve bpmToDifficulty;
    [SerializeField] AnimationCurve cadanceToDifficulty;

    float cadanceDifficulty
    {
        get
        {
            float slope = rc.Slope;
            if (slope > Mathf.PI)
            {
                slope = 2 * Mathf.PI - slope;
            }
            return 1 + slope + bpmToDifficulty.Evaluate(pulseMeter.Pulse) + cadanceToDifficulty.Evaluate(cadance);
        }
    }

    float cadanceProgress
    {
        get
        {
            return cadance * Time.deltaTime / 60;
        }
    }

    float previousPosition;

    void SetCadancePosition()
    {
        float progress = cadanceProgress;
        position += direction * progress;
        if (Mathf.Sign(previousPosition - 0.5f) != Mathf.Sign(position - 0.5f))
        {
            if (OnMidStep != null) OnMidStep(steps);
        }
        if (direction > 0 && position > 1f)
        {
            position = 1f;
            direction = -1f;
            steps++;
            if (OnStep != null) OnStep(steps, Leg.Right);
        }
        else if (direction < 0 && position < 0f)
        {
            position = 0f;
            direction = 1;
            steps++;
            if (OnStep != null) OnStep(steps, Leg.Left);
        }
        paceMarker.anchorMin = new Vector2(position, stepMarkerYmin);
        paceMarker.anchorMax = new Vector2(position, stepMarkerYmax);
        if (position != 0.5f) previousPosition = position;
    }

    void GetMarkerYSize()
    {
        stepMarkerYmin = paceMarker.anchorMin.y;
        stepMarkerYmax = paceMarker.anchorMax.y;
    }

    public PaceEffect Score(Leg foot)
    {
        if (foot == Leg.Left && position > 0.5f || foot == Leg.Right && position < 0.5f)
        {
            return PaceEffect.Same;
        }
        float difficulty = cadanceDifficulty;
        if (foot == Leg.Left)
        {
            if (position < increasingThreshold)
            {
                return PaceEffect.Increase;
            } else if (position < sameThreshold)
            {
                return PaceEffect.Same;
            } else if (position < loweringThreshold)
            {
                return PaceEffect.Decrease;
            }
        } else
        {
            if (position > 1 - increasingThreshold)
            {
                return PaceEffect.Increase;
            } else if (position > 1 - sameThreshold)
            {
                return PaceEffect.Same;
            } else if (position > 1 - loweringThreshold)
            {
                return PaceEffect.Decrease;
            }
        }
        return PaceEffect.Fumble;
    }

    public int Steps
    {
        get
        {
            if (direction > 0 && position > 0.5f)
            {
                return steps + 1;
            } else if (direction < 0 && position < 0.5f)
            {
                return steps + 1;
            }
            return steps;
        }
    }
}
