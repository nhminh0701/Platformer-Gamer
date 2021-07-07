using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Stationary region parameters within Camera")]
    [SerializeField] float areaWidth;
    [SerializeField] float areaHeight;
    [SerializeField] float camSpeed;

    Transform player;
    Camera myCamera;

    Vector3 camPos;
    Vector3 targetPos;
    float distanceX;
    float distanceY;
    float targetXPos;
    float targetYPos;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    private void SetUpCamera()
    {
        player = FindObjectOfType<Player>().transform;

        if (!player)
        {
            Debug.LogError("No Player Object existed on Scene!!!");
            return;
        }

        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FollowObject(player);
    }

    private void FollowObject(Transform player)
    {
        // Check horizontal axis if player excess cameraboundary
        distanceX = player.position.x - myCamera.transform.position.x;
        if (Mathf.Abs(distanceX) > areaWidth)
        {
            targetXPos = myCamera.transform.position.x + areaWidth * Mathf.Sign(distanceX);
        }
        else
        {
            targetXPos = myCamera.transform.position.x;
        }

        // Check vertical axis if player excess cameraboundary
        distanceY = player.position.y - myCamera.transform.position.y;
        if (Mathf.Abs(distanceY) > areaHeight)
        {
            targetYPos = myCamera.transform.position.y + areaHeight * Mathf.Sign(distanceY);
        }
        else
        {
            targetYPos = myCamera.transform.position.y;
        }

        targetPos = new Vector3(targetXPos, targetYPos, myCamera.transform.position.z);

        // Using Lerp function to move smothly by frame
        camPos = Vector3.Lerp(myCamera.transform.position, targetPos, camSpeed*Time.deltaTime);
        myCamera.transform.position = camPos;
    }




}
