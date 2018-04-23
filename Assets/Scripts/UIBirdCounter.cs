using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBirdCounter : MonoBehaviour {

    [SerializeField] Text counter;
    [SerializeField] Goal goal;
    [SerializeField] UIRecords uiRecords;

    BirdWatching[] birds;

    Dictionary<string, int> observations = new Dictionary<string, int>();

    private void OnEnable()
    {
        if (birds == null) birds = GameObject.FindObjectsOfType<BirdWatching>();
        for (int i=0; i<birds.Length; i++)
        {
            birds[i].OnBirdWatched += HandleObservation;
        }
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].OnBirdWatched -= HandleObservation;
        }
        goal.OnRunGoal -= HandleRunEvent;
    }

    bool observing = true;
    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Goal)
        {
            observing = false;
            if (GeneralManager.HasBirdingRecord())
            {
                uiRecords.ShowBirderResult(score, GeneralManager.GetBirdingRecord());
            } else
            {
                uiRecords.ShowBirderResult(score);
            }
            if (GeneralManager.IsPersonalBirdingRecord(score))
            {
                GeneralManager.SetBirdingRecord(score);
            }
            GeneralManager.RecentRunBirding = score;
        }
    }

    private void HandleObservation(string species)
    {
        if (!observing)
        {
            return;
        }

        if (!observations.ContainsKey(species))
        {
            observations[species] = 1;
        } else
        {
            observations[species] += 1;
        }
        counter.text = score.ToString("000");
    }

    public int score
    {
        get
        {
            int score = 0;
            foreach (int count in observations.Values)
            {
                score += 5 + 3 * Mathf.Clamp(count - 1, 0, 1) + 2 * Mathf.Clamp(count - 2, 0, 2) + Mathf.Max(0, count - 4);
            }
            return score;
        }
    }
}
