using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    [SerializeField] Transform background1;
    [SerializeField] Transform background2;

    [SerializeField] Transform thisBG;
    [SerializeField] Transform newBG;
    Transform tempPos;
    Transform myCamera;
    float distance;

    private void Start()
    {
        myCamera = Camera.main.transform;
        SetBG();

    }

    // Update is called once per frame
    void Update()
    {
        MoveBG();
    }

    private void MoveBG()
    {
        distance = myCamera.position.x - thisBG.position.x;

        if (Mathf.Abs(distance) > 4)
        {
            Vector3 thisBGPos = thisBG.position;
            newBG.position = thisBGPos + new Vector3(23, 0, 0) * Math.Sign(distance);

            SwapBG();
        }
    }

    private void SwapBG()
    {
        tempPos = thisBG;
        thisBG = newBG;
        newBG = tempPos;
    }

    private void SetBG()
    {
        if (background1.position.x - myCamera.position.x < background1.position.y - myCamera.position.x)
        {
            thisBG = background1;
            newBG = background2;
        } else
        {
            thisBG = background2;
            newBG = background1;
        }

        distance = myCamera.position.x - thisBG.position.x;

        if (Mathf.Abs(distance) > 5)
        {
            thisBG.position = new Vector3(myCamera.position.x, myCamera.position.y, thisBG.position.z);
        }
    }
}
