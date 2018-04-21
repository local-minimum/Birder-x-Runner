using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRunProgress : MonoBehaviour {

    [SerializeField]
    float totalDistance = 22.3f;

    [SerializeField]
    RunController rc;

    [SerializeField]
    RectTransform runner;

    private void OnEnable()
    {
        rc.OnGPS += HandleGPS;
    }

    private void OnDisable()
    {
        rc.OnGPS -= HandleGPS;
    }

    bool hasStarted = false;
    float distance = 0;
    Vector2 lastPos;
    float minAnchorY;
    float maxAnchorY;

    void Start()
    {
        minAnchorY = runner.anchorMin.y;
        maxAnchorY = runner.anchorMax.y;
    }

    private void HandleGPS(Vector2 pos)
    {
        if (hasStarted)
        {
            distance += Vector2.Distance(lastPos, pos);
            lastPos = pos;
            float progress = Mathf.Clamp01(distance / totalDistance);
            runner.anchorMax = new Vector2(progress, maxAnchorY);
            runner.anchorMin = new Vector2(progress, minAnchorY);
        }
        else
        {
            lastPos = pos;
            hasStarted = true;
        }

    }
}
