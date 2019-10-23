using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Stationary region parameters within Camera")]
    [SerializeField] float areaWidth;
    [SerializeField] float areaHeight;

    Transform player;
    Camera myCamera;
    Vector3 cameraPos;

    Vector3 playerPositionInScreen;

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
        cameraPos = myCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowObject(player);
    }

    private void FollowObject(Transform targetPosition)
    {
        AdjustXDirection(targetPosition);
        AdjustYDirection(targetPosition);
    }

    private void AdjustYDirection(Transform targetPosition)
    {
        float distanceYDir = targetPosition.position.y - cameraPos.y;

        if (Mathf.Abs(distanceYDir) > areaHeight)
        {
            cameraPos.y = Mathf.Clamp(cameraPos.y, targetPosition.position.y - areaHeight, targetPosition.position.y + areaHeight);

            myCamera.transform.position = cameraPos;
        }
    }

    private void AdjustXDirection(Transform targetPosition)
    {
        float distanceXDir = targetPosition.position.x - cameraPos.x;

        if (Mathf.Abs(distanceXDir) > areaWidth)
        {
            cameraPos.x = Mathf.Clamp(cameraPos.x, targetPosition.position.x - areaWidth, targetPosition.position.x + areaWidth);

            myCamera.transform.position = cameraPos;
        }
    }
}
