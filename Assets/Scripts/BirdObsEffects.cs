using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdObsEffects : MonoBehaviour
{
    [SerializeField] BirdWatching bird;

    public void Trigger()
    {
        GetComponent<Animator>().SetTrigger("Obs");
    }

    public void RemoveBird()
    {
        bird.gameObject.SetActive(false);
    }
}
