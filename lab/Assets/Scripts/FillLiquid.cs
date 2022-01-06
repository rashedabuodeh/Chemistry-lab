using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLiquid : MonoBehaviour
{
    [SerializeField] private Material water, Lugol, Starch;
    private MeshRenderer meshRenderer;
    private const string waterTag = "Water";
    private const string starchTag = "Starch";
    private const string lugolTag = "Lugol";

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider");
        switch (other.tag)
        {
            case waterTag:
                meshRenderer.material = water;
                    break;
            case starchTag:
                meshRenderer.material = Starch;
                    break;
            case lugolTag:
                meshRenderer.material = Lugol;
                    break;
            default:
                break;
        }
    }
}
