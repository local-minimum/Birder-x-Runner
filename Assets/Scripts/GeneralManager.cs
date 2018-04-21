using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static bool IsPersonalRecord(float time)
    {
        return instance.bestTime <= 0 || time < instance.bestTime;
    }

    public static void SetRecordRecording(List<KeyValuePair<float, Vector2>> run, float time)
    {
        instance.bestTime = time;
        instance.bestRun.Clear();
        instance.bestRun.AddRange(run);
    }

    public static bool HasRecording()
    {
        return instance.bestTime > 0;
    }

    public static Vector2 GetBestRunPosition(float currentTime)
    {
        List<KeyValuePair<float, Vector2>> bestRun = instance.bestRun;
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
    List<KeyValuePair<float, Vector2>> bestRun = new List<KeyValuePair<float, Vector2>>();

    [SerializeField]
    float bestTime;

}
