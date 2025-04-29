using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizardpain : MonoBehaviour
{
    public AudioClip WizardPain;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(WizardPain);

        }
    }
}