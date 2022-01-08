using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BunsenFire : MonoBehaviour
{
    private bool isOpened = false;
    private Vector3 rotationVector;
    private Vector3 startRotationVector;
    [SerializeField] private GameObject _gasSwitch;
    [SerializeField] private ParticleSystem _fireParticleSystem;
    [NonSerialized] public bool IsOn = false ;

    void Awake()
    {
        startRotationVector = _gasSwitch.transform.rotation.eulerAngles;
        rotationVector = _gasSwitch.transform.rotation.eulerAngles;
        rotationVector.y = -90;
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter") && isOpened)
        {
            _fireParticleSystem.Play();
            IsOn = true;
        }
    }
}
