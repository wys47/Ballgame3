using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    private Vector3 ShowMapPos = new Vector3(0, 2.28f, -3.72f);
    private float CamDefaultDis = -2;

    [HideInInspector]public Transform CameraHolderTr;
    [HideInInspector]public Transform CamTr;

    private Transform CamTargetObj;
    private Vector3 TargetPos;
    private float CamLocalPos;
    private string CamMode;
    private int LockCamPos;
    private int LockCamRot;//0. 안잠금, 1. 전부 잠금, 2. x축 잠금, 3. y축 잠금

    private setting settingCS;

    void Start()
    {
        CameraHolderTr = GameObject.Find("camholder").GetComponent<Transform>();
        CamTr = GameObject.Find("main camera").GetComponent<Transform>();
        settingCS = GameObject.Find("SettingCS").GetComponent<setting>();

        CamMode = "ShowMap";
    }

    void Update()
    {
        if (CamMode == "ShowMap")
        {
            if (LockCamPos == 0)
            {
                if (settingCS.LerpingCamera) CameraHolderTr.position = Vector3.Lerp(CameraHolderTr.position, ShowMapPos, 0.2f);
                else CameraHolderTr.position = ShowMapPos;
            }
            if (LockCamRot == 0) CameraHolderTr.rotation = Quaternion.Euler(10, 0, 0);
        }
        if (CamMode == "ThirdPerson")
        {
            if (LockCamPos == 0)
            {
                if (settingCS.LerpingCamera) CameraHolderTr.position = Vector3.Lerp(CameraHolderTr.position, CamTargetObj.position + Vector3.up * 0.5f, 0.2f);
                else CameraHolderTr.position = CamTargetObj.position + Vector3.up * 0.5f;
            }

            float x = 0;
            float y = CameraHolderTr.eulerAngles.y;
            if (LockCamRot != 2)
            {
                x = CameraHolderTr.eulerAngles.x - Input.GetAxis("Mouse y") * settingCS.CameraRotateSpeed;
                if (x > 90) x -= 360;
            }
            if (LockCamRot != 3) y = CameraHolderTr.eulerAngles.y + Input.GetAxis("Mouse x") * settingCS.CameraRotateSpeed;
            if (LockCamRot != 1) CameraHolderTr.rotation = Quaternion.Euler(Mathf.Clamp(x, -80, 80), y, 0);

            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput > 0) CamLocalPos += 0.1f;
            else if (wheelInput < 0) CamLocalPos -= 0.1f;
            if (Input.GetMouseButtonUp(2)) CamLocalPos = CamDefaultDis;

            CamTr.localPosition = new Vector3(0, 0, CamLocalPos);
            Vector3 CamPos = CamTr.position;
            Vector3 CamHolderPos = CameraHolderTr.position;
            Vector3 NewCamPos = Vector3.zero;
            float ratio = 0;
            if (CamPos.x > 3.8f)
            {
                ratio = (3.8f - CamHolderPos.x) / (CamPos.x - CamHolderPos.x);
                NewCamPos.x = 3.8f;
                NewCamPos.y = CamHolderPos.y - (CamHolderPos.y - CamPos.y) * ratio;
                NewCamPos.z = CamHolderPos.z - (CamHolderPos.z - CamPos.z) * ratio;

                CamTr.position = NewCamPos;
                CamPos = NewCamPos;
            }
            if (CamPos.x < -3.8f)
            {
                ratio = (3.8f + CamHolderPos.x) / (CamHolderPos.x - CamPos.x);
                NewCamPos.x = -3.8f;
                NewCamPos.y = CamHolderPos.y - (CamHolderPos.y - CamPos.y) * ratio;
                NewCamPos.z = CamHolderPos.z - (CamHolderPos.z - CamPos.z) * ratio;

                CamTr.position = NewCamPos;
                CamPos = NewCamPos;
            }
            if (CamPos.y > 5.5f)
            {
                ratio = (5.5f - CamHolderPos.y) / (CamPos.y - CamHolderPos.y);
                NewCamPos.x = CamHolderPos.x - (CamHolderPos.x - CamPos.x) * ratio;
                NewCamPos.y = 5.5f;
                NewCamPos.z = CamHolderPos.z - (CamHolderPos.z - CamPos.z) * ratio;

                CamTr.position = NewCamPos;
                CamPos = NewCamPos;
            }
            if (CamPos.y < 0.2f)
            {
                ratio = (CamHolderPos.y - 0.2f) / (CamHolderPos.y - CamPos.y);
                NewCamPos.x = CamHolderPos.x - (CamHolderPos.x - CamPos.x) * ratio;
                NewCamPos.y = 0.2f;
                NewCamPos.z = CamHolderPos.z - (CamHolderPos.z - CamPos.z) * ratio;

                CamTr.position = NewCamPos;
                CamPos = NewCamPos;
            }
            if (CamPos.z > 7.4f)
            {
                ratio = (7.4f - CamHolderPos.z) / (CamPos.z - CamHolderPos.z);
                NewCamPos.x = CamHolderPos.x - (CamHolderPos.x - CamPos.x) * ratio;
                NewCamPos.y = CamHolderPos.y - (CamHolderPos.y - CamPos.y) * ratio;
                NewCamPos.z = 7.4f;

                CamTr.position = NewCamPos;
                CamPos = NewCamPos;
            }
            if (CamPos.z < -7.4f)
            {
                ratio = (CamHolderPos.z + 7.4f) / (CamHolderPos.z - CamPos.z);
                NewCamPos.x = CamHolderPos.x - (CamHolderPos.x - CamPos.x) * ratio;
                NewCamPos.y = CamHolderPos.y - (CamHolderPos.y - CamPos.y) * ratio;
                NewCamPos.z = -7.4f;

                CamTr.position = NewCamPos;
            }
        }
    }

    public void SetCamVar(string ChangeModeTo = "none", Transform ObjTr = null, int LockPos = 0, int LockRot = 0, int RotXZero = 0, int RotateY180 = 0)
    {
        if (ChangeModeTo != "none") CamMode = ChangeModeTo;
        if (ObjTr != null) CamTargetObj = ObjTr.GetComponent<Transform>();
        LockCamPos = LockPos;
        LockCamRot = LockRot;
        if (RotXZero != 0)
        {
            Vector3 Angles = CameraHolderTr.eulerAngles;
            CameraHolderTr.rotation = Quaternion.Euler(0, Angles.y, Angles.z);
        }
        if (RotateY180 != 0) CameraHolderTr.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void BeforeChangeGameStat(int ChangeGameStatTo)
    {
        if (ChangeGameStatTo == 1)
        {
            CamMode = "ShowMap";
            CamTargetObj = null;
        }
        if (ChangeGameStatTo == 2)
        {
            LockCamPos = 0;
            LockCamRot = 0;
            CamTr.localPosition = new Vector3(0, 0, -2);
            CamLocalPos = -2;
        }
        if (ChangeGameStatTo == 3)
        {

        }
        if (ChangeGameStatTo == 4)
        {
            CamMode = "ShowMap";
            CamTargetObj = null;
        }
    }
}
