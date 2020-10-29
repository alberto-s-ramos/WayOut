using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player, centerPoint;
    private Transform parent;
    private float mouseX, mouseY;     private float mouseSensivity = 6.75f;     public float cameraHeight;

    float minDistance = 1.0f;
    float maxDistance = 7.0f;
    float smooth = 10.0f;
    private Vector3 dollyDir;
    private float distance;

    private GameObject gameManager;

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
        parent = transform.parent;
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (gameManager.GetComponent<Menu>().hasStarted() && !gameManager.GetComponent<Menu>().hasFinished())
        {

            transform.localEulerAngles = new Vector3(14, 0, transform.localEulerAngles.z);

            if (Input.GetMouseButton(1))             {
               Cursor.visible = false;
               Cursor.lockState = CursorLockMode.Confined;
               mouseX += Input.GetAxis("Mouse X")*mouseSensivity;                mouseY -= Input.GetAxis("Mouse Y")*mouseSensivity;
            }

            else if (!Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
               mouseX = centerPoint.eulerAngles.y;
            }

            mouseY = Mathf.Clamp(mouseY, -30f, 60f);
            transform.LookAt(centerPoint);

            centerPoint.position = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z);
            centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

            /*
            * Collision detection
            */

            Vector3 desiredCameraPos = centerPoint.TransformPoint(dollyDir * maxDistance);
            RaycastHit hit;

            if (Physics.Linecast(centerPoint.position, desiredCameraPos, out hit))
            {
              distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
            }
            else
            {
              distance = maxDistance;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        }

    }

 
}
