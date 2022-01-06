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

    private void OnEnable()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = -90;
    }
  
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseUp()
    {
        transform.rotation = startRot;

        notHome = true;
    }

    private void OnMouseDrag()
    {
        transform.rotation = Quaternion.Euler(rotationVector);

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        transform.position = curPosition;
    }
    private void Update()
    {
        // Moves the object to start position
        if (notHome)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, speed * Time.deltaTime);
        }
        // when it reach starting position stop moving
        if (Vector3.Distance(transform.position, startPos) < 0.01f)
        {
            notHome = false;                 
        }
    }
}