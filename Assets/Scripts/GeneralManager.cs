using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RunResult
{
    public float time;
    public float birdingSpeed;
    public int birdingScore;

    public RunResult(int birdingScore, float birdingSpeed, float time)
    {
        this.time = time;
        this.birdingScore = birdingScore;
        this.birdingSpeed = birdingSpeed;
    }
}

public class GeneralManager : MonoBehaviour {

    static GeneralManager _instance;
    static GeneralManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<GeneralManager>();
                if (!_instance)
                {
                    var go = new GameObject("General Manager");
                    _instance = go.AddComponent<GeneralManager>();
                }
            }
            return _instance;
        }
    }

    public static bool IsPersonalRunRecord(float time)
    {
        return instance.bestRunTime <= 0 || time < instance.bestRunTime;
    }

    public static bool IsPersonalBirdingRecord(int score)
    {
        return score > 0 && (instance.bestBirdScore <= 0 || score > instance.bestBirdScore);
    }

    public static bool IsPersonalBirdRunRecord(float value)
    {
        return value > 0 && (instance.bestBirdRun < 0 || value > instance.bestBirdRun);
    }

    public static void SetRunRecording(List<KeyValuePair<float, Vector2>> run)
    {
        instance.runRecording.Clear();
        instance.runRecording.AddRange(run);
    }

    public static void SetRunRecord(float record)
    {
        instance.bestRunTime = record;
    }

    public static void SetBirdingRecord(int record)
    {
        instance.bestBirdScore = record;
    }

    public static void SetBirdRunRecord(float record)
    {
        instance.bestBirdRun = record;
    }

    public static bool HasRun()
    {
        return instance.runRecording.Count > 0;
    }

    public static bool HasRunRecord()
    {
        return instance.bestRunTime > 0;
    }

    public static bool HasBirdingRecord()
    {
        return instance.bestBirdScore > 0;
    }

    public static bool HasBirdRunRecord()
    {
        return instance.bestBirdRun > 0;
    }

    public static float GetRunTimeRecord()
    {
        return instance.bestRunTime;
    }

    public static float GetBirdRunRecord()
    {
        return instance.bestBirdRun;
    }

    public static int GetBirdingRecord()
    {
        return instance.bestBirdScore;
    }

    public static Vector2 GetBestRunPosition(float currentTime)
    {
        List<KeyValuePair<float, Vector2>> bestRun = instance.runRecording;
        for (int i = 0, l = bestRun.Count - 2; i < l; i++)
        {
            KeyValuePair<float, Vector2> checkpoint1 = bestRun[i];
            KeyValuePair<float, Vector2> checkpoint2 = bestRun[i + 1];
            if (checkpoint1.Key == currentTime)
            {
                return checkpoint1.Value;
            }
            else if (checkpoint1.Key < currentTime && checkpoint2.Key > currentTime)
            {
                float lerpTime = (currentTime - checkpoint1.Key) / (checkpoint2.Key - checkpoint1.Key);
                return Vector2.Lerp(checkpoint1.Value, checkpoint2.Value, lerpTime);
            }
        }
        return bestRun[bestRun.Count - 1].Value;

    }

    void Awake () {
		if (_instance && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (!_instance)
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
	}

    [SerializeField]
    List<KeyValuePair<float, Vector2>> runRecording = new List<KeyValuePair<float, Vector2>>();

    [SerializeField]
    float bestRunTime;

    [SerializeField]
    int bestBirdScore;

    [SerializeField]
    float bestBirdRun;
    
    static float recentRunTime;
    static int recentRunBirding;
    static float recentRunBirdingXRunning;
    static int submissionStatus = 0;

    public static float RecentRunTime
    {
        set
        {
            recentRunTime = value;
            submissionStatus += 1;
        }

        get
        {
            return recentRunTime;
        }
    }

    public static int RecentRunBirding
    {
        set
        {
            recentRunBirding = value;
            submissionStatus += 1;
        }

        get
        {
            return recentRunBirding;
        }
    }

    public static float RecentRunBirdingXRunning
    {
        set
        {
            recentRunBirdingXRunning = value;
            submissionStatus += 1;
        }

        get
        {
            return recentRunBirdingXRunning;
        }
    }

    public static bool CanSubmit
    {
        get
        {
            return submissionStatus >= 3;
        }
    } 
    
    public static RunResult GetSubmiss()
    {
        submissionStatus = 0;
        return new RunResult(recentRunBirding, recentRunBirdingXRunning, recentRunTime);
    }
}
