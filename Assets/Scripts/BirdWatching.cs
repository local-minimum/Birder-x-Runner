using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdWatching : MonoBehaviour {

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

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }

    private void Update()
    {
        if (obsStarted)
        {
            obsTimeSoFar += (hovered ? hoverSpeedup : 1f) * Time.deltaTime;
            obsProgressImage.fillAmount = Mathf.Clamp01(obsTimeSoFar / obsDuration);
            obsAlertImage.enabled = false;
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
