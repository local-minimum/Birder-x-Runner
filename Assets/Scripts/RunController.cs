using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GPSEvent(Vector2 pos);

public class RunController : MonoBehaviour {
    struct ElevationSlope
    {
        public float elevation;
        public float slope;

        public ElevationSlope(float elevation, float slope)
        {
            this.elevation = elevation;
            this.slope = slope;
        }
    }

    public event GPSEvent OnGPS;

    [SerializeField] Pacer pacer;

    [SerializeField, Range(0, 1)] float gpsFrequency = 0.1f;

    float speed;

    float deltaY;

    private void Start()
    {
        deltaY = transform.position.y - GetGround(transform.position).elevation;
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
        ElevationSlope elevationSlope = GetGround(pos);
        pos.x += Mathf.Cos(elevationSlope.slope) * speed * Time.deltaTime;
        elevationSlope = GetGround(pos);
        pos.y = elevationSlope.elevation + deltaY;
        transform.position = pos;
    }

    [SerializeField] float rayU = 0.1f;

    ElevationSlope GetGround (Vector3 pos)
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(pos, Vector3.down);
        RaycastHit2D hitBehind = Physics2D.Raycast(pos, Vector3.down + Vector3.left * rayU);
        RaycastHit2D hitForward = Physics2D.Raycast(pos, Vector3.down + Vector3.right * rayU);
        return new ElevationSlope(
            GetGroundElevation(hitCenter, hitBehind, hitForward),
            GetGroundSlope(hitCenter, hitBehind, hitForward));
    }

    float GetGroundElevation(RaycastHit2D hitCenter, RaycastHit2D hitBehind, RaycastHit2D hitForward)
    {
        if (hitCenter)
        {
            return hitCenter.point.y;
        }
        else if (hitBehind && hitForward)
        {
            return Vector2.Lerp(hitBehind.point, hitForward.point, 0.5f).y;
        }
        else if (hitBehind)
        {
            return hitBehind.point.y;
        }
        else if (hitForward)
        {
            return hitForward.point.y;
        }
        else
        {
            Debug.LogWarning("Could not find ground");
            return transform.position.y;
        }
    }

    float GetGroundSlope(RaycastHit2D hitCenter, RaycastHit2D hitBehind, RaycastHit2D hitForward)
    {
        if (hitBehind && hitForward)
        {
            return Mathf.Atan2(hitForward.point.y - hitBehind.point.y, hitForward.point.x - hitBehind.point.x);
        } else if (hitCenter && hitForward)
        {
            return Mathf.Atan2(hitForward.point.y - hitCenter.point.y, hitForward.point.x - hitCenter.point.x);
        } else if (hitCenter && hitBehind)
        {
            return Mathf.Atan2(hitCenter.point.y - hitBehind.point.y, hitCenter.point.x - hitBehind.point.x);
        }

        Debug.LogWarning("Could not find enough ground to calculate slope");
        return 0;
    }
}
