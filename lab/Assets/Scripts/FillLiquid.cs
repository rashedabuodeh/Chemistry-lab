using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLiquid : MonoBehaviour
{
    public enum TypeOfLiquid
    {
        Water, Lugol, Starch
    }
    public TypeOfLiquid typeOfLiquid; // to use for each dropper one liquid

    [SerializeField] private Material _water, _lugol, _starch;
    private Material basicMaterial; // whne there is no liquid in the dropper
    private MeshRenderer meshRenderer;
    private const string waterTag = "Water";
    private const string starchTag = "Starch";
    private const string lugolTag = "Lugol";

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        basicMaterial = meshRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collider");
        switch (other.tag)
        {
            case waterTag:
                if (typeOfLiquid == TypeOfLiquid.Water)
                    meshRenderer.material = _water;
                else
                    Debug.Log("miss use");
                    break;
            case starchTag:
                if (typeOfLiquid == TypeOfLiquid.Starch)
                    meshRenderer.material = _starch;
                else
                    Debug.Log("miss use"); break;
            case lugolTag:
                if (typeOfLiquid == TypeOfLiquid.Lugol)
                    meshRenderer.material = _lugol;
                else
                    Debug.Log("miss use"); break;
            default:
                    Debug.Log("entered the wrong collider"); break;
                break;
        }
    }
}
