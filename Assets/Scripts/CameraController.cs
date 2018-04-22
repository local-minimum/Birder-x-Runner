using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    AnimationCurve speedToViewSize;

    [SerializeField]
    Pacer pacer;

    [SerializeField, Range(0, 5)]
    float attack;

    [SerializeField]
    AnimationCurve xOffset;

    Camera cam;

    [SerializeField] AnimationCurve yOffset;
    float zOffset;

    private void Start()
    {
        cam = GetComponent<Camera>();
        zOffset = transform.localPosition.z;
    }

    private void Update()
    {
        //Debug.Log(pacer.Speed);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, speedToViewSize.Evaluate(pacer.Speed), attack * Time.deltaTime);
        Vector3 localPos = transform.localPosition;
        localPos.x = Mathf.Lerp(localPos.x, xOffset.Evaluate(cam.orthographicSize), attack * Time.deltaTime);
        localPos.y = Mathf.Lerp(localPos.y, yOffset.Evaluate(cam.orthographicSize), attack * Time.deltaTime);
        localPos.z = zOffset;
        transform.localPosition = localPos;
    }
}
