using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setting : MonoBehaviour
{
    //production
    [HideInInspector] public bool SlowmotionAfterReceive;
    [HideInInspector] public bool LerpingCamera;
    [HideInInspector] public float CameraRotateSpeed;
    [HideInInspector] public bool CameraFollowBall;
    //spike
    [HideInInspector] public float SpikeAngleX;
    //serve
    [HideInInspector] public float ServeBallAngleX;
    [HideInInspector] public float ThrowUpForce1Adjustment;
    [HideInInspector] public float ThrowUpForce2Adjustment;
    [HideInInspector] public float ThrowUpForce3Adjustment;

    private void Start()
    {
        //temp
        SlowmotionAfterReceive = true;
        LerpingCamera = true;
        CameraRotateSpeed = 1f;
        CameraFollowBall = false;
        SpikeAngleX = -10f;
        ServeBallAngleX = -20f;
        ThrowUpForce1Adjustment = 0.1f;
        ThrowUpForce2Adjustment = 0f;
        ThrowUpForce3Adjustment = 1.5f;
    }
}
