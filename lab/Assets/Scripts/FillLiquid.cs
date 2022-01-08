using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FillLiquid : MonoBehaviour
{
    public enum TypeOfLiquid
    {
        Water, Starch, Lugol
    }
    public TypeOfLiquid typeOfTakenLiquid; // each dropper is used for one type of liquid

    public enum TypeOfHolder
    {
        dropper, tube
    }
    public TypeOfHolder typeOfHolder;

    [NonSerialized] public MeshRenderer meshRenderer;

    [Header("Materials")]
    [SerializeField] private Material _water;
    [SerializeField] private Material  _starch;
    [SerializeField] private Material  _lugol;
    public Material basicMaterial; // material used when there is no liquid in the dropper

    [SerializeField] private GameObject  _mixedLiquid = null;

    private const string waterTag = "Water";
    private const string starchTag = "Starch";
    private const string lugolTag = "Lugol";

    [NonSerialized] public bool isFilled = false;

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        basicMaterial = meshRenderer.material;
        // add listenter when tiimer finish
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            // filling water logic 
            case waterTag:
                if (typeOfTakenLiquid == TypeOfLiquid.Water)
                {
                    if (typeOfHolder == TypeOfHolder.dropper)
                    {
                        meshRenderer.material = _water; // should make sound for filling
                        isFilled = true;
                    }
                    else
                    {
                        if (other.TryGetComponent<FillLiquid>(out FillLiquid liquid) )
                            if (liquid.isFilled)
                            {
                                liquid.meshRenderer.material = basicMaterial; // take the liquid from the dropper
                                meshRenderer.material = _water; // should make sound for filling
                                liquid.isFilled = false;
                                break;
                            }
                    }
                }
                //else
                    //Debug.Log("miss use");
                break;

            // filling starch logic
            case starchTag:
                if (typeOfTakenLiquid == TypeOfLiquid.Starch)
                {
                    if (typeOfHolder == TypeOfHolder.dropper)
                    {
                        meshRenderer.material = _starch; // should make sound for filling
                        isFilled = true;
                    }
                    else
                    {
                        if (other.TryGetComponent<FillLiquid>(out FillLiquid liquid))
                            if (liquid.isFilled)
                            {
                                liquid.meshRenderer.material = basicMaterial; // take the liquid from the dropper
                                meshRenderer.material = _starch; // should make sound for filling
                                liquid.isFilled = false; 
                            }
                    }
                }
                //else
                    //Debug.Log("miss use");
                break;

            // filling lugol logic
            case lugolTag:
                if (typeOfTakenLiquid == TypeOfLiquid.Lugol)
                {
                    meshRenderer.material = _lugol;
                    isFilled = true;
                }
                else if (typeOfHolder == TypeOfHolder.tube)
                {
                    if (other.TryGetComponent<FillLiquid>(out FillLiquid liquid))
                        if (liquid.isFilled)
                        {
                            liquid.meshRenderer.material = basicMaterial; // take the liquid from the dropper
                            meshRenderer.material = _water; // should make sound for filling
                            liquid.isFilled = false;
                            _mixedLiquid.SetActive(true);
                        }
                }
                //else
                    //Debug.Log("miss use");
                break;

            //default:
            //        Debug.Log("entered the wrong collider"); break;
        }
    }
}
