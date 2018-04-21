using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GPSEvent(Vector2 pos);

public class RunController : MonoBehaviour {

    public event GPSEvent OnGPS;

    [SerializeField] Pacer pacer;

    [SerializeField, Range(0, 1)] float gpsFrequency = 0.1f;

    float speed;

    float deltaY;

    private void Start()
    {
        deltaY = transform.position.y - GetGroundElevation(transform.position);
        Debug.Log(deltaY);
    }

    [SerializeField]
    Start start;
    [SerializeField]
    Goal goal;

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
            StartCoroutine(gps());
        } else if(phase == RunPhase.Goal)
        {
            if (OnGPS != null) OnGPS(transform.position);
            running = false;
        }
    }

    bool running = false;

    IEnumerator<WaitForSeconds> gps()
    {
        while(running)
        {
            if (OnGPS != null) OnGPS(transform.position);
            yield return new WaitForSeconds(gpsFrequency);
        }
    }

    void Update () {
        if (running)
        {
            SetNewPosition();
        }
	}

    void SetNewPosition()
    {
        speed = pacer.Speed;
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        pos.y = GetGroundElevation(pos) + deltaY;
        transform.position = pos;
    }

    [SerializeField] float rayU = 0.1f;

    float GetGroundElevation (Vector3 pos)
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(pos, Vector3.down);
        RaycastHit2D hitBehind = Physics2D.Raycast(pos, Vector3.down + Vector3.left * rayU);
        RaycastHit2D hitForward = Physics2D.Raycast(pos, Vector3.down + Vector3.right * rayU);
        Vector2 refPoint;
        if (hitCenter)
        {
            refPoint = hitCenter.point;
        } else if (hitBehind && hitForward)
        {
            refPoint = Vector2.Lerp(hitBehind.point, hitForward.point, 0.5f);
        } else if (hitBehind)
        {
            refPoint = hitBehind.point;
        } else if (hitForward)
        {
            refPoint = hitForward.point;
        } else
        {
            Debug.LogWarning("Could not find ground");
            return transform.position.y;
        }
        return refPoint.y;
    }
}
