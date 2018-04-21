using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunPhase { Start, Goal };

public delegate void RunEvent(RunPhase phase, float time);

public class Start : MonoBehaviour {

    public event RunEvent OnRunStart;

    bool started = false;

    void Update () {
		if (!started && Input.anyKeyDown)
        {
            started = true;
            if (OnRunStart != null) OnRunStart(RunPhase.Start, Time.time);            
        }
	}
}
