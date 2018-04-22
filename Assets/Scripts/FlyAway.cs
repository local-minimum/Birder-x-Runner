using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour {

    [SerializeField] GameObject bird;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (!escaping && collision.tag == "Player")
        {
            StartCoroutine(Escape());
        }
    }
    [SerializeField] float flightTime;
    [SerializeField] AnimationCurve elevation;
    [SerializeField] AnimationCurve horizontal;

    bool escaping = false;
    IEnumerator<WaitForSeconds> Escape()
    {
        escaping = true;
        float start = Time.time;
        float duration = 0;
        Vector3 refPos = bird.transform.localPosition;
        while (duration < flightTime)
        {
            duration = Time.time - start;
            bird.transform.localPosition = new Vector3(
                refPos.x + horizontal.Evaluate(duration),
                refPos.y + elevation.Evaluate(duration),
                refPos.z
            );
            yield return new WaitForSeconds(0.02f);
        }
        bird.SetActive(false);
    }
}
