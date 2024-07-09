using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MapCamera : MonoBehaviour
{
    [Header("Camera")]
    public GameObject virCamera;

    [Header("Positions")]
    Vector3 touchPos;
    Vector3 mousePos;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchVec3 = touch.position;
            touchVec3.z = -10.0f;

            if (touchVec3 != touchPos && touch.phase == TouchPhase.Moved)
            {
                virCamera.transform.position += touchPos - touchVec3;

                return;
            }

            touchPos = touch.position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mouseVec3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseVec3.z = -10.0f;

            virCamera.transform.position += mousePos - mouseVec3;
        }
    }
}
