using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public event RunEvent OnRunGoal;

    bool playerInGoal = false;

    [SerializeField]
    RunController rc;

	void Update () {
        if (!playerInGoal && rc.transform.position.x > transform.position.x)
        {
            rc.enabled = false;
            playerInGoal = true;
            if (OnRunGoal != null) OnRunGoal(RunPhase.Goal, Time.time);
        }		
	}

}
