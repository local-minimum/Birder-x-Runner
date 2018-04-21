using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour {

    [SerializeField] Pacer pacer;

    float speed;

    float deltaY;

    private void Start()
    {
        deltaY = transform.position.y - GetGroundElevation(transform.position);
        Debug.Log(deltaY);
    }

    void Update () {
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
