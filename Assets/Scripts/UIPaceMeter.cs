using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Leg { Left, Right };
public enum PaceEffect { Increase, Same, Decrease, Fumble };

public delegate void StepEvent(int steps, Leg leg);

public class UIPaceMeter : MonoBehaviour
{
    public event StepEvent OnStep;

    [SerializeField] Image[] loweringPaceImages;
    [SerializeField] Image[] samePaceImages;
    [SerializeField] Image[] increasingPaceImages;

    [SerializeField] RectTransform paceMarker;

    [HideInInspector]
    public float pace = 70;

    float paceMarkerYmin;
    float paceMarkerYmax;

    [SerializeField, Range(0, 0.5f)]
    float loweringThreshold = 0.4f;

    [SerializeField, Range(0, 0.5f)]
    float sameThreshold = 0.3f;

    [SerializeField, Range(0, 0.5f)]
    float increasingThreshold = 0.15f;


    float position = 0.5f;
    float direction = 1f;

    float runStart;
    int steps = 0;

    private void Start()
    {
        SetZoneImages();
        GetMarkerYSize();
        runStart = Time.timeSinceLevelLoad;
    }

    public bool running = false;

    private void Update()
    {
        if (!running)
        {
            return;
        }
        SetZoneImages();
        SetPacePosition();
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

    float paceProgress
    {
        get
        {
            return pace * Time.deltaTime / 60;
        }
    }

    void SetPacePosition()
    {
        float progress = paceProgress;
        position += direction * progress;
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
        paceMarker.anchorMin = new Vector2(position, paceMarkerYmin);
        paceMarker.anchorMax = new Vector2(position, paceMarkerYmax);
    }

    void GetMarkerYSize()
    {
        paceMarkerYmin = paceMarker.anchorMin.y;
        paceMarkerYmax = paceMarker.anchorMax.y;
    }

    public PaceEffect Score(Leg foot)
    {
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
            return steps;
        }
    }
}
