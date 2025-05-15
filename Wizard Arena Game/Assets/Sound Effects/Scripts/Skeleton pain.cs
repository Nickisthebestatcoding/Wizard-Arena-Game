using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPain : MonoBehaviour
{
    public AudioClip BoneCrack;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Wizard attack"))
        {
            if (audioSource != null)
            {
                audioSource.clip = BoneCrack;
                audioSource.Play();
            }
        }
    }
}
