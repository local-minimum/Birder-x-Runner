using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRerun : MonoBehaviour {

    [SerializeField] Pacer pacer;
    [SerializeField] Goal goal;
    [SerializeField] GameObject rerun;
    [SerializeField, Range(0, 4)] float triggerShowSpeed = 0.5f;
    bool reachedGoal = false;

    private void OnEnable()
    {
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        goal.OnRunGoal -= HandleRunEvent;
    }

    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Goal)
        {
            reachedGoal = true;
            StartCoroutine(ShowUI(2f));
        }
    }

    bool showing = false;

    IEnumerator<WaitForSeconds> ShowUI(float delay)
    {
        showing = true;
        yield return new WaitForSeconds(delay);
        if (showing) rerun.SetActive(true);
        while (showing)
        {
            yield return new WaitForSeconds(1f);
        }
        rerun.SetActive(false);
    }

    private void Start()
    {
        rerun.SetActive(false);
    }

    private void Update()
    {
        if (reachedGoal) return;
        bool showing = pacer.Speed < triggerShowSpeed;
        if (showing && !this.showing)
        {
            StartCoroutine(ShowUI(0));            
        }
        this.showing = showing;
    }
}
