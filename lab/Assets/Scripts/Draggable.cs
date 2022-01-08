using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 rotationVector;

    [SerializeField] private float speed = 1;
    private bool notHome = false;
    private bool Reached = false; // used for tubes when moving them to the fire
    [SerializeField] private int _tubeNum = 0;

    Vector3 pos1ToReach = new Vector3(0, 0.1f, -0.189f);
    Vector3 pos2ToReach = new Vector3(0, 0.1f, -0.1618f);

    public enum TypeOfDraggable
    {
        dropper, tube, Lighter
    }
    public TypeOfDraggable typeOfDraggable;

    ParticleSystem fireParticleSystem;

    private void OnEnable()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = -90;

        if(typeOfDraggable == TypeOfDraggable.Lighter)
            fireParticleSystem = transform.GetComponentInChildren<ParticleSystem>();
    }
  
    private void OnMouseDown()
    {
        Reached = false;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        if (typeOfDraggable == TypeOfDraggable.Lighter)
            fireParticleSystem.Play();
    }
    private void OnMouseUp()
    {
        if (Reached) return;

        transform.rotation = startRot;

        notHome = true;

        if (typeOfDraggable == TypeOfDraggable.Lighter)
            fireParticleSystem.Stop();
    }

    private void OnMouseDrag()
    {
        if (Reached) return;

        if (typeOfDraggable == TypeOfDraggable.dropper)
            transform.rotation = Quaternion.Euler(rotationVector);

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        transform.position = curPosition;
    }
    private void Update()
    {
        if (Reached) return;

        // return the object to the start position
        if (notHome)
            transform.position = Vector3.Lerp(transform.position, startPos, speed * Time.deltaTime);
        // when it reach starting position stop moving
        if (Vector3.Distance(transform.position, startPos) < 0.001f)
            notHome = false;                 
    }
    private void OnTriggerEnter(Collider other)
    {
        if(typeOfDraggable == TypeOfDraggable.tube)
        {
            if (other.CompareTag("HotWater"))
            {
                Reached = true;
                if(_tubeNum == 0)
                    transform.localPosition = pos1ToReach;
                else
                    transform.localPosition = pos2ToReach;
            }
        }
    }
}