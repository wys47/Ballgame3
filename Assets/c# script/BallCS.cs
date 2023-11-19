using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCS : MonoBehaviour
{
    private int NoneSpinPercentage = 8;
    private float NoneSpinShakePower = 0.3f;
    private float NoneSpinSlowDown = 0.99f;
    private float NoneSpinGravityManipulator = 1.3f;

    private float DownSpinDownForce = 0.000015f;
    private float DownSpinVelocityPow = 4.3f;

    private WaitForSeconds WaitMicroSec = new WaitForSeconds(0.1f);

    public Rigidbody BallRigid;
    public Transform BallTr;
    public GameObject Trail;
    public Transform BallGizmo;

    [HideInInspector] public string BallState;
    private float GravityAxisY;
    private bool[] BallStateCorutineActivedCount = new bool[3];
    [HideInInspector] public int ShowBallArrivalPos;

    public Ref referee;

    private void Start()
    {
        GravityAxisY = Physics.gravity.y;
    }

    private void Update()
    {
        if (referee.GameStat == 3)
        {
            float timescale = Time.timeScale;
            if (BallState == "nonespin")
            {
                if (Random.Range(1, (int)(NoneSpinPercentage / timescale)) == 1)
                {
                    Vector3 BallAngle = BallTr.eulerAngles;
                    BallTr.forward = BallRigid.velocity.normalized;
                    BallTr.Rotate(0, 0, Random.Range(1, 361));
                    BallRigid.AddForce(BallTr.up * NoneSpinShakePower * Random.Range(0.5f, 2f) / timescale);
                    BallTr.eulerAngles = BallAngle;
                }
                BallRigid.velocity *= NoneSpinSlowDown / Mathf.Pow(NoneSpinSlowDown, 1 - Time.timeScale);
            }
            if (BallState == "downspin" && !BallStateCorutineActivedCount[2])
            {
                StartCoroutine(DownSpinAddForce());
                BallStateCorutineActivedCount[2] = true;
            }

            if (ShowBallArrivalPos == 2)
            {
                BallGizmo.position = BallArrivalPos(0.1f, -1);
                ShowBallArrivalPos = 0;
            }
            else if (ShowBallArrivalPos == 1) ShowBallArrivalPos = 2;
        }
        if (BallRigid.velocity.magnitude > 10) Trail.SetActive(true);
        else Trail.SetActive(false);
    }

    public void ChangeBallState(string BallStateName)
    {
        BallState = BallStateName;
        switch (BallStateName)
        {
            case "nonespin": BallStateCorutineActivedCount[1] = false; break;
            case "downspin": BallStateCorutineActivedCount[2] = false; break;
        }
    }

    IEnumerator DownSpinAddForce()
    {
        while (BallState == "downspin")
        {
            yield return WaitMicroSec;
            Vector3 Vel = BallRigid.velocity;
            Vel -= Vector3.up * Vel.y;
            BallRigid.velocity += Vector3.down * Mathf.Pow(Vector3.Magnitude(Vel), DownSpinVelocityPow) * DownSpinDownForce;
        }
    }

    public float BallArrivalTime(float PosY, int UpOrDown, float VelY)
    {
        float TargetPosY = PosY - BallTr.position.y;
        float Gravity = GravityAxisY;

        if (BallState == "nonespin") Gravity *= NoneSpinGravityManipulator;
        if (BallState == "downspin")
        {
            Vector3 Vel = BallRigid.velocity;
            Vel -= Vector3.up * Vel.y;
            Gravity -= Mathf.Pow(Vector3.Magnitude(Vel), DownSpinVelocityPow) * DownSpinDownForce * 10;
        }

        return (-VelY + UpOrDown * Mathf.Sqrt(VelY * VelY + 2 * Gravity * TargetPosY)) / Gravity;
    }
    public Vector3 BallArrivalPos(float PosY, int UpOrDown)
    {
        float TargetPosY = PosY - BallTr.position.y;
        Vector3 Vel = BallRigid.velocity;
        float Time = BallArrivalTime(PosY, UpOrDown, Vel.y);

        Vector3 ArrivalPos = BallTr.position + new Vector3(Vel.x * Time, TargetPosY, Vel.z * Time);

        return ArrivalPos;
    }
    public Vector3 BallArrivalPower(Vector3 Pos, int UpOrDown, float TopHight)
    {
        Vector3 BallPos = BallTr.position;
        float VelY;
        float Time;
        float Gravity = GravityAxisY;

        if (BallState == "nonespin") Gravity *= NoneSpinGravityManipulator;
        if (BallState == "downspin")
        {
            Vector3 Vel = BallRigid.velocity;
            Vel -= Vector3.up * Vel.y;
            Gravity -= Mathf.Pow(Vector3.Magnitude(Vel), DownSpinVelocityPow) * DownSpinDownForce * 10;
        }

        VelY = Mathf.Sqrt(- 2 * Gravity * (TopHight - BallPos.y));
        Time = BallArrivalTime(Pos.y, UpOrDown, VelY);
        
        return new Vector3((Pos.x - BallPos.x) / Time, VelY, (Pos.z - BallPos.z) / Time);
    }

    public void BeforeChangeGameStat(int ChangeGameStatTo)
    {
        if (ChangeGameStatTo == 2)
        {
            ChangeBallState("normal");
            BallStateCorutineActivedCount[1] = false;
            BallStateCorutineActivedCount[2] = false;
            ShowBallArrivalPos = 0;
        }
        if (ChangeGameStatTo == 4) ChangeBallState("normal");
    }
    public void AfterChangeGameStat(int ChangedGameStat)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeBallState("normal");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ballnet_up")
        {
            BallRigid.velocity *= 0.4f;
            BallRigid.velocity += Vector3.up * 3;
            ChangeBallState("normal");
        }
        if (other.tag == "ballnet_down")
        {
            BallRigid.velocity *= 0.2f;
            ChangeBallState("normal");
        }
    }
}
