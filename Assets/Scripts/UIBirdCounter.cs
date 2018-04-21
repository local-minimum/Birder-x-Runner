using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBirdCounter : MonoBehaviour {

    [SerializeField]
    Text counter;

    BirdWatching[] birds;

    Dictionary<string, int> observations = new Dictionary<string, int>();

    private void OnEnable()
    {
        if (birds == null) birds = GameObject.FindObjectsOfType<BirdWatching>();
        for (int i=0; i<birds.Length; i++)
        {
            birds[i].OnBirdWatched += HandleObservation;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].OnBirdWatched -= HandleObservation;
        }
    }

    private void HandleObservation(string species)
    {
        if (!observations.ContainsKey(species))
        {
            observations[species] = 1;
        } else
        {
            observations[species] += 1;
        }
        counter.text = observations.Keys.Count.ToString("000");
    }
}
