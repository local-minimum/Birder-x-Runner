using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void BirdWatchingEvent(string species);

public class BirdWatching : MonoBehaviour {

    public event BirdWatchingEvent OnBirdWatched;

    [SerializeField]
    string species;

    [SerializeField]
    Image obsAlertImage;

    [SerializeField]
    Image obsProgressImage;

    [SerializeField, Range(1, 2f)]
    float hoverSpeedup = 1.25f;

    [SerializeField, Range(0, 10)]
    float obsDuration = 3f;

    float obsTimeSoFar = 0f;

    bool obsStarted = false;
    bool hovered = false;
    bool visible;

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }

    private void OnBecameVisible()
    {
        visible = true;    
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

    bool completed = false;

    private void Update()
    {
        if (!visible || completed)
        {
            return;
        }

        if (obsStarted)
        {
            obsTimeSoFar += (hovered ? hoverSpeedup : 1f) * Time.deltaTime;
            float fill = Mathf.Clamp01(obsTimeSoFar / obsDuration);
            obsProgressImage.fillAmount = fill;
            obsAlertImage.enabled = false;
            if (fill == 1f)
            {
               if (OnBirdWatched != null) OnBirdWatched(species);
                obsProgressImage.enabled = false;
                completed = true;
            }
        } else if (hovered && Input.GetMouseButtonDown(0))
        {
            obsStarted = true;
            obsAlertImage.enabled = true;
        } else
        {
            obsAlertImage.enabled = hovered;
        }
    }
}
