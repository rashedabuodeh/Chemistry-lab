using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingEffect : MonoBehaviour
{
    [SerializeField] private Material _heatedMixedStarchLugol;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private BunsenFire _bunsenFire;


    private void OnEnable()
    {
        UIManager.Instance.timerFinishedEvent += OnTimerFinish;
    }

    private void OnDisable()
    {
        UIManager.Instance.timerFinishedEvent -= OnTimerFinish;
    }

    private void OnTimerFinish()
    {
        if(_bunsenFire.IsOn)
        {
            _meshRenderer.material = _heatedMixedStarchLugol; 
        }
    }

}
