using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightpain : MonoBehaviour
{
    public AudioClip KnightPain;
    AudioSource audioSource;

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
                audioSource.clip = KnightPain;
                audioSource.Play();
            }
        }
    }
}
