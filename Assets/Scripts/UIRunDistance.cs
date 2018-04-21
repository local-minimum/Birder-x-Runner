using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunDistance : MonoBehaviour {

    [SerializeField] RunController rc;
    [SerializeField] Text txt;

    Vector2 lastPos;
    float distance;
    bool hasStarted = false;

    private void OnEnable()
    {
        rc.OnGPS += Rc_OnGPS;
    }

    private void OnDisable()
    {
        rc.OnGPS -= Rc_OnGPS;
    }

    private void Rc_OnGPS(Vector2 pos)
    {
        if (hasStarted)
        {
            distance += Vector2.Distance(lastPos, pos);
            lastPos = pos;
            txt.text = string.Format("{0}m", distance.ToString("000.0"));
        } else
        {
            lastPos = pos;
            hasStarted = true;
        }        
    }
}
