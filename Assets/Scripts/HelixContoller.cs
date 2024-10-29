using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixContoller : MonoBehaviour
{
    private Vector2 lastPosition;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.localEulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;
            if (lastPosition == Vector2.zero)
            {
                lastPosition = currentTapPosition;
            }
            float distance = lastPosition.x - currentTapPosition.x;
            lastPosition = currentTapPosition;

            transform.Rotate(Vector3.up * distance);

        }

        if (Input.GetMouseButtonUp(0))
        {
            lastPosition = Vector2.zero;
        }
    }
}
