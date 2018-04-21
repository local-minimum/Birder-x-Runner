using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    AnimationCurve speedToViewSize;

    [SerializeField]
    Pacer pacer;

    [SerializeField, Range(0, 1)]
    float attack;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        //Debug.Log(pacer.Speed);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, speedToViewSize.Evaluate(pacer.Speed), attack * Time.deltaTime);
    }
}
