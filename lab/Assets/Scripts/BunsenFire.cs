using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BunsenFire : MonoBehaviour
{
    private bool isOpened = false; // indicate if gas switch is opened
    private Vector3 rotationVector;
    private Vector3 startRotationVector;
    [SerializeField] private GameObject _gasSwitch;
    [SerializeField] private ParticleSystem _fireParticleSystem;
    [NonSerialized] public bool IsOn = false; // indicate if there is fire 
    [SerializeField] private AudioClip _gasLeakageClip;
    [SerializeField] private AudioClip _flameClip;
    private AudioSource audioSource;

    void Awake()
    {
        startRotationVector = _gasSwitch.transform.rotation.eulerAngles;
        rotationVector = _gasSwitch.transform.rotation.eulerAngles;
        rotationVector.y = -90;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if(!isOpened)
            _gasSwitch.transform.rotation = Quaternion.Euler(rotationVector);
        else
            _gasSwitch.transform.rotation = Quaternion.Euler(startRotationVector);

        isOpened = !isOpened;

        if (!isOpened)
        {
            _fireParticleSystem.Stop();
            IsOn = false;
            audioSource.Stop();
        }
        else
        {
            audioSource.clip = _gasLeakageClip;
            audioSource.Play();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter") && isOpened)
        {
            _fireParticleSystem.Play();
            IsOn = true;
            audioSource.clip = _flameClip;
            audioSource.Play();
        }
    }
}
