using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCS : MonoBehaviour
{
    //Default
    private float BallSpinPower = 2f;
    //RunFunction()
    private float RunSpeed_1 = 0.5f;
    private float RunSpeed_2 = 2f;
    private float RunSpeed_3 = 1f;
    private float BaseRunSpeed = 1f;
    private float RunMinusStamina = 0.0000001f;
    //JumpFunction
    private float JumpPower_1 = 7f;
    private float JumpPower_2 = 13f;
    private float JumpPower_3 = 18f;
    private float BaseJumpPower = 10f;
    private float JumpMinusStamina = 0.1f;
    //BallHitFution, ReceiveFunction
    private float BaseMinusStamina = 1f;
    private float ServeHitMinusStamina = 1f;
    private float SpikeHitMinusStamina = 1f;
    private float BaseBallHitPower = 0.5f;
    private float servePower1 = 2.1f;
    private float servePower2 = 2.3f;
    private float servePower3 = 2.8f;
    
    private float Serve2NetHightPlusRotX = 0.9f;
    private float Serve3NetHightPlusRotX = 2.3f;
    private float Serve3NetHightMinusPowerManipulator = 0.19f;
    private float SpikePower1 = 2.5f;
    private float SpikePower2 = 2.7f;
    private float SpikePower3 = 3f;
    private float HandBallDisManipulatorPower = 2f;
    private float BaseRotateAngle = 0.03f;
    private float BaseReceiveTurnPower = 0.1f;
    private float HandBallDisManipulatorRotate = 1f;
    private float BaseServe1AngleX = 27f;
    private float Serve1HitPowerDiffManipulator = 0.1f;
    private float SpikeNetHightPlusRotX = 0.2f;
    //serve
    private float Serve1DragDis = 26f;
    private float Serve3DragDis = 198f;
    private float ThrowForwardForce1 = -0.12f;
    private float ThrowUpForce1 = 1.9f;
    private float ThrowForwardForce2 = 0.6f;
    private float ThrowUpForce2 = 2.8f;
    private float ThrowForwardForce3 = 0.7f;
    private float ThrowUpForce3 = 3f;
    private float ServeHandBallDis1 = 0.6f;
    private float ServeHandBallDis2 = 0.5f;
    private float PlusMental = 5f;
    private float ServeRandomRotYRange = 10f;
    //block
    private float BlockJumpMinusStamina = 1f;
    private float BlockPlusMental = 5f;
    private float BlockMinusMental = 5f;
    private float BasicReflexTime = 0.4f;
    private float ReflexLvManipulator = 0.1f;
    private float BlockHight = 4f;
    //receive
    private float ReceiveMinusStamina = 1f;
    private float ReceiveUpManipulator = 1.5f;
    private float LongReceiveManipulator = 2f;
    private float ReceiveBallSpeedManipulator = 0.2f;
    private float[] Receive_Hight = new float[5];
    private float OverDisPlus = 0.5f;
    private float ReceiveUpBallMagnitude = 5f;
    private float ReceiveCloseDis = 0.9f;
    private float ReceiveLongDis = 2f;
    private float ReceiveBallHandDis = 0.7f;
    private float ReceiveMotionStartTime = 0.22f;
    private float ChangeReceiveModeTime = 0.6f;
    private float CloseReceivePosX = 0.11f;
    private float ReceiveFailUpForce = 2f;
    private float ReceivePlusMental = 5f;
    private float ReceiveMinusMental = 5f;
    //attack
    private float SpikeJumpForwardlength = 60f;
    private float SpikeDistance1 = 0.8f;
    private float SpikeDistance2 = 0.3f;
    private float SpikeStreightBallRotY = 20;
    private float SpikeRunningPowerManipulator = 1.3f;
    private Vector3 LeftSpikeDefaultPos = new Vector3(-3.1f, 0, -1.6f);
    private Vector3 LeftBackDefaultPos = new Vector3(-1, 0, -2.4f);
    private Vector3 RightSpikeDefaultPos = new Vector3(3.1f, 0, -1.6f);
    private Vector3 RightBackDefaultPos = new Vector3(1, 0 -2.4f);
    private Vector3 QuikDefaultPos = new Vector3(-0.3f, 0, -1.2f);
    private float LeftSpikeRunStartTime = 0.12f;
    private float RightSpikeRunStartTime = 0.05f;
    private float LeftBackSpikeRunStartTime = 0.27f;
    private float RightBackSpikeRunStartTime = 0.2f;
    private float QuikSpikeRunStartTime = 0.6f;
    private float SpikeRandomRotYRange = 5f;
    //blockhand
    private float BlockPowerManipulator = 0.2f;
    private float BlockHandBallVelocityManipulator = 1f;
    //setup
    private float SetupMinusStamina = 1f;
    private float BaseSetupTurnPower = 0.1f;
    private float SetupMinusDis = 0.3f;
    private float SetupMotionStartTime = 0.22f;
    private float SetupCloseDis = 0.9f;
    private float SetupManipulator = 0.6f;
    private float SetupPosBaseHight = 0.002f;
    private float LeftSetupHight = 2.5f;
    private float RightSetupHight = 2.7f;
    private float QuikSetupHight = 1.8f;
    private float LeftBackSetupHight = 2f;
    private float RightBackSetupHight = 2.2f;

    private WaitForSeconds Wait3Sec = new WaitForSeconds(3);
    private WaitForSeconds WaitPoint2Sec = new WaitForSeconds(0.2f);
    private WaitForSeconds WaitBeforeAnimEnd = new WaitForSeconds(1f);
    private WaitForSeconds WaitForBlockReflex;

    public GameObject gameObj;
    public Transform gameObjTr;
    public Rigidbody gameObjRigid;
    public Transform ModelTr;
    public Animator anim;
    public Transform HandTr;
    public GameObject LeftArmColl;
    public GameObject RightArmColl;
    public Transform PosBallTr;
    private Transform BallTr;
    private Rigidbody BallRigid;
    private GameObject SetterLineGizmo;
    private GameObject SetterPointGizmo;
    private Transform DebugGizmo;

    [HideInInspector] public float Hight;
    [HideInInspector] public int LeftHandedOrRightHanded;
    [HideInInspector] public int JumpPowerLv;
    private float JumpPowerLvManipulator = 0.1f;
    [HideInInspector] public int SteminaLv;
    private float StaminaLvManipulator = 0.1f;
    [HideInInspector] public int MentalLv;
    [HideInInspector] public int PowerLv;
    private float PowerLvManipulator = 0.4f;
    [HideInInspector] public int DelicacyLv;
    private float ProficiencyLvManipulator = 0.1f;
    [HideInInspector] public int IntelligenceLv;
    [HideInInspector] public int QuicknessLv;
    private float QuicknessLvManipulator = 0.3f;

    [HideInInspector] public int ServeLv;
    [HideInInspector] public int FloatServeLv;
    [HideInInspector] public int SpikeServeLv;
    [HideInInspector] public int ReceiveLv;
    [HideInInspector] public int BlockLv;//레벨이 상승할수록 커밋블록은 더 정확해지고, 리드블록의 반응속도는 더 빨라진다. 노하우가 쌓인다고 보면 됌.
    [HideInInspector] public int SetupLv;//2레벨부터 속공 가능, 3레벨부터 점프토스 및 투어택 가능
    [HideInInspector] public int SpikeLv;//2레벨부터 스파이크 치는 방향 조절 가능, 페인트 가능, 3레벨부터 백어택 가능, 미들은 백어택 불가

    [HideInInspector] public int[] ServeStrategy = new int[11];
    [HideInInspector] public int[] ReceiveStrategy = new int[11];
    [HideInInspector] public int[] BlockStrategy = new int[11];
    [HideInInspector] public int[] SetupStrategy = new int[11];
    [HideInInspector] public int[] SpikeStrategy = new int[11];

    [HideInInspector] public bool IsOp;
    private int OpManipulator;
    private bool Impact;
    private bool AnimEnd;
    private int ItsTime;//-1.false, 0.wait, 1. true
    private int[] WASD_Button_Pushed_Number = new int[5];
    private bool[] ClickActivatedInSameTouchCount = new bool[6];//0.좌클, 1.우클, 2.W, 3.A, 4.S, 5.D
    [HideInInspector] public bool Serve_IsPlayerCtrl;
    private int Serve_Process;
    private Vector2 Serve_MouseOriginPos;
    private int Serve_Mode;
    [HideInInspector] public bool MyBall;
    [HideInInspector] public string AttackType;
    [HideInInspector] public Vector3 SpikeRunTargetPos;
    [HideInInspector] public int Receive_Mode;
    [HideInInspector] public int Setup_Mode;
    [HideInInspector] public bool SetupJumpToss = false;
    private float ReceiveModeManipulator;
    private float SetupModeManipulator;
    private Vector3 SetupPlayerCtrlTargetPos;
    private int SetupPlayerCtrlUpOrDown;
    private float SetupPlayerCtrlMaxHight;
    private string SelectedAttackType;
    [HideInInspector] public int Spike_Process;
    private bool Spike_Running;
    private float Spike_AngleY;
    private int IsAnimPlaying;//0. 정지, 1. 재생중, 2. 공중에 뜸
    [HideInInspector] public int BlockMode;//아직 세터가 공을 올리기 전 어떻게 할지 정하는 변수 1. 팀 스크립트대로, 2. 집중마크모드
    private int BlockTriggered;

    [HideInInspector] public string CurrentPosition;
    [HideInInspector] public bool IsCurrentlyPlaying;
    [HideInInspector] public int StandingPositionNumber;
    [HideInInspector] public float Stemina;
    [HideInInspector] public float Mental;
    [HideInInspector] private float Condition;
    private float ConditionManipulator = 0.005f;

    //setting
    [HideInInspector] public int PowerLvToStaminaLv = 0;

    private Ref referee;
    private TeamCS teamCS;
    private UI UICS;
    private setting settingCS;
    private BallCS ballCS;
    private CamCtrl camCtrl;

    private void Start()
    {
        GameObject Ball = GameObject.Find("vollyballpos");
        BallTr = Ball.GetComponent<Transform>();
        BallRigid = Ball.GetComponent<Rigidbody>();
        SetterLineGizmo = GameObject.Find("SetterLineGizmo");
        SetterPointGizmo = GameObject.Find("SetterPointGizmo");
        DebugGizmo = GameObject.Find("DebugGizmo").GetComponent<Transform>();

        referee = GameObject.Find("RefCS").GetComponent<Ref>();
        UICS = GameObject.Find("Canvas").GetComponent<UI>();
        settingCS = GameObject.Find("SettingCS").GetComponent<setting>();
        ballCS = GameObject.Find("vollyballpos").GetComponent<BallCS>();
        camCtrl = GameObject.Find("camholder").GetComponent<CamCtrl>();

        //임시 스탯 {
        ServeLv = 1;
        FloatServeLv = 0;
        SpikeServeLv = 1;
        ReceiveLv = 1;
        BlockLv = 1;
        SetupLv = 2;
        SpikeLv = 3;

        WaitForBlockReflex = new WaitForSeconds(BasicReflexTime - 0.1f * BlockLv);

        Hight = 1;
        LeftHandedOrRightHanded = 1;
        JumpPowerLv = 1;
        SteminaLv = 1;
        MentalLv = 1;
        PowerLv = 1;
        DelicacyLv = 1;
        IntelligenceLv = 1;
        QuicknessLv = 1;
        // }

        if (!IsOp)
        {
            OpManipulator = 1;
            teamCS = GameObject.Find("TeamCSHolder").GetComponent<TeamCS>();
        }
        else
        {
            OpManipulator = -1;
            teamCS = GameObject.Find("OpTeamCSHolder").GetComponent<TeamCS>();
        }

        for (int n = 1; n <= 10; n++) if (ServeStrategy[n] != 0) ServeStrategy[n] = -1;
        for (int n = 1; n <= 10; n++) if (ReceiveStrategy[n] != 0) ReceiveStrategy[n] = -1;
        for (int n = 1; n <= 10; n++) if (BlockStrategy[n] != 0) BlockStrategy[n] = -1;
        for (int n = 1; n <= 10; n++) if (SetupStrategy[n] != 0) SetupStrategy[n] = -1;
        for (int n = 1; n <= 10; n++) if (SpikeStrategy[n] != 0) SpikeStrategy[n] = -1;

        gameObjTr.localScale *= Hight;
        Receive_Hight[1] = 1f * Hight;
        Receive_Hight[2] = 0.4f * Hight;
        Receive_Hight[3] = 0.25f * Hight;
        Receive_Hight[4] = 0.1f * Hight;

        BlockMode = 1;
    }

    void Update()
    {
        if (IsCurrentlyPlaying)
        {
            if (referee.GameStat == 2)
            {
                if ((referee.WhoServe * OpManipulator == 1) && StandingPositionNumber == 1) ServeFunction();
            }
            if (referee.GameStat == 3)
            {
                if (referee.TouchCount == 3 && gameObj.name == "OutsideHitter1" && !IsOp) print(IsAnimPlaying);
                int TC = referee.TouchCount;
                if (MyBall)
                {
                    if (TC * OpManipulator == -3 || (TC * OpManipulator < 0 && ballCS.BallArrivalPos(referee.NetHight + 0.5f, -1).z * OpManipulator < 0)) ReceiveFunction();
                    if (TC * OpManipulator == 1) SetupFunction();
                }
                else
                {
                    if ((TC * OpManipulator == 3 || TC * OpManipulator == -3 || (TC * OpManipulator < 0 && ballCS.BallArrivalPos(referee.NetHight + 0.5f, -1).z * OpManipulator < 0)) && BlockTriggered == 0 && teamCS.MyBallDeclared)
                    {
                        Vector3 StandingPos = Vector3.up * gameObjTr.position.y;
                        Vector3 LookPos = Vector3.up * gameObjTr.position.y;
                        bool move = false;

                        switch (CurrentPosition)
                        {
                            case "S":
                                StandingPos += teamCS.SetterDefaultStandingPos * OpManipulator;
                                LookPos += Vector3.forward * -8 * OpManipulator;
                                move = true;
                                break;
                            case "OP":
                                if (StandingPositionNumber >= 2 && StandingPositionNumber <= 4)
                                {
                                    StandingPos += RightSpikeDefaultPos * OpManipulator;
                                    LookPos += teamCS.SetupPosRightSpike * OpManipulator;
                                }
                                else
                                {
                                    StandingPos += RightBackDefaultPos * OpManipulator;
                                    LookPos += teamCS.SetupPosRightBackSpike * OpManipulator;
                                }
                                move = true;
                                break;
                            case "OH":
                                if (StandingPositionNumber >= 2 && StandingPositionNumber <= 4)
                                {
                                    StandingPos += LeftSpikeDefaultPos * OpManipulator;
                                    LookPos += teamCS.SetupPosLeftSpike * OpManipulator;
                                }
                                else
                                {
                                    StandingPos += LeftBackDefaultPos * OpManipulator;
                                    LookPos += teamCS.SetupPosLeftBackSpike * OpManipulator;
                                }
                                move = true;
                                break;
                            case "MB":
                                StandingPos += QuikDefaultPos * OpManipulator;
                                LookPos += teamCS.SetupPosQuikSpike * OpManipulator;
                                move = true;
                                break;
                        }

                        if (move && IsAnimPlaying == 0)
                        {
                            float dis = Vector3.Distance(StandingPos, gameObjTr.position);
                            if (dis > 0.1f)
                            {
                                gameObjTr.LookAt(StandingPos);
                                RunFunction(2, Vector3.forward);
                                anim.SetBool("run", true);
                            }
                            else
                            {
                                anim.SetBool("run", false);
                                gameObjTr.LookAt(LookPos);
                            }
                        }
                    }
                    else if (TC * OpManipulator > 0)
                    {
                        if (teamCS.Spike_PlayerCtrl)
                        {
                            if (SelectedAttackType != "None") SpikeFunction(true, "spike", SpikeRunTargetPos + Vector3.up * gameObjTr.position.y, false);
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.W))
                                {
                                    SelectedAttackType = "Quik";
                                }
                                if (Input.GetKeyDown(KeyCode.S))
                                {
                                    if (Input.GetKeyDown(KeyCode.A))
                                    {
                                        SelectedAttackType = "LeftBack";
                                    }
                                    if (Input.GetKeyDown(KeyCode.D))
                                    {
                                        SelectedAttackType = "RightBack";
                                    }
                                }
                                else
                                {
                                    if (Input.GetKeyDown(KeyCode.A))
                                    {
                                        SelectedAttackType = "Left";
                                    }
                                    if (Input.GetKeyDown(KeyCode.D))
                                    {
                                        SelectedAttackType = "Right";
                                    }
                                }

                                if (SelectedAttackType != "None")
                                {
                                    referee.ChangeTimeScale(1);

                                    for (int i = 1; i <= 6; i++)
                                    {
                                        if (teamCS.CurrentPlayingPlayerCS[i].AttackType == SelectedAttackType)
                                        {
                                            teamCS.AttackerNumber = i;
                                            teamCS.Change_PlayerCtrl_PlayerNumbur(i);

                                            Vector3 LookPos = Vector3.up * gameObjTr.position.y;

                                            switch (AttackType)
                                            {
                                                case "Left":
                                                    LookPos += teamCS.SetupPosLeftSpike;
                                                    break;
                                                case "Right":
                                                    LookPos += teamCS.SetupPosRightSpike;
                                                    break;
                                                case "LeftBack":
                                                    LookPos += teamCS.SetupPosLeftBackSpike;
                                                    break;
                                                case "RightBack":
                                                    LookPos += teamCS.SetupPosRightBackSpike;
                                                    break;
                                                case "Quik":
                                                    LookPos += teamCS.SetupPosQuikSpike;
                                                    break;
                                            }
                                            gameObjTr.LookAt(LookPos);

                                            camCtrl.SetCamVar("ThirdPerson", teamCS.CurrentPlayingPlayerCS[i].gameObjTr);
                                        }
                                    }
                                }
                            }
                        }

                        if (AttackType != "None" && !(teamCS.Spike_PlayerCtrl && AttackType == SelectedAttackType)) SpikeFunction(false, "spike", SpikeRunTargetPos + Vector3.up * gameObjTr.position.y, true);
                        else
                        {

                        }
                    }
                    else
                    {
                        if (StandingPositionNumber >= 2 && StandingPositionNumber <= 4)
                        {
                            BlockFunction();
                        }
                    }
                }
            }
        }
    }

    private IEnumerator Timer(WaitForSeconds WaitSec)
    {
        ItsTime = 0;
        yield return WaitSec;
        ItsTime = 1;
    }

    bool WASD(int runspeedLv)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WASD_Button_Pushed_Number[1]++;
            ModelTr.Rotate(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.W)) RunFunction(runspeedLv, Vector3.forward);
        if (Input.GetKeyUp(KeyCode.W) && WASD_Button_Pushed_Number[1] == 1)
        {
            WASD_Button_Pushed_Number[1]--;
            ModelTr.Rotate(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            WASD_Button_Pushed_Number[2]++;
            ModelTr.Rotate(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.A)) RunFunction(runspeedLv, Vector3.left);
        if (Input.GetKeyUp(KeyCode.A) && WASD_Button_Pushed_Number[2] == 1)
        {
            WASD_Button_Pushed_Number[2]--;
            ModelTr.Rotate(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            WASD_Button_Pushed_Number[3]++;
            ModelTr.Rotate(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.S)) RunFunction(runspeedLv, Vector3.back);
        if (Input.GetKeyUp(KeyCode.S) && WASD_Button_Pushed_Number[3] == 1)
        {
            WASD_Button_Pushed_Number[3]--;
            ModelTr.Rotate(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            WASD_Button_Pushed_Number[4]++;
            ModelTr.Rotate(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.D)) RunFunction(runspeedLv, Vector3.right);
        if (Input.GetKeyUp(KeyCode.D) && WASD_Button_Pushed_Number[4] == 1)
        {
            WASD_Button_Pushed_Number[4]--;
            ModelTr.transform.Rotate(0, -90, 0);
        }

        if (WASD_Button_Pushed_Number[1] + WASD_Button_Pushed_Number[2] + WASD_Button_Pushed_Number[3] + WASD_Button_Pushed_Number[4] > 0)
        {
            gameObjTr.rotation = Quaternion.Euler(0, camCtrl.CameraHolderTr.eulerAngles.y, 0);
            return true;
        }
        else
        {
            anim.SetFloat("anim play speed", 0f);
            return false;
        }
    }

    float SpikerMaxHight(PlayerCS SpikePlayer)
    {
        return SpikePlayer.Hight + SetupPosBaseHight * JumpPower_3 * (BaseJumpPower + SpikePlayer.JumpPowerLv * JumpPowerLvManipulator + 50 * ConditionManipulator);
    }
    void RunFunction(int runspeedLv, Vector3 dir, bool IsSpeedDefealt = false)
    {
        float runspeed = 0;
        switch (runspeedLv)
        {
            case 1: runspeed = RunSpeed_1; break;
            case 2: runspeed = RunSpeed_2; break;
            case 3: runspeed = RunSpeed_3; break;
        }

        float ChangingSpeed;
        float DefealtSpeed = BaseRunSpeed + 1 * QuicknessLvManipulator + 100 * ConditionManipulator;
        if (!IsSpeedDefealt) ChangingSpeed = BaseRunSpeed + QuicknessLv * QuicknessLvManipulator + Condition * ConditionManipulator;
        else ChangingSpeed = DefealtSpeed;
        anim.SetFloat("anim play speed", ChangingSpeed / DefealtSpeed);
        gameObjTr.Translate(dir * runspeed * ChangingSpeed * Time.unscaledDeltaTime * Time.timeScale);
        Stemina -= RunMinusStamina * runspeed * (1 - SteminaLv * StaminaLvManipulator);
        Condition = Stemina * 0.5f + Mental * 0.5f;
    }
    void JumpFunction(int JumpPower, bool SpikeRunning = false)
    {
        float jumppower = 0;
        switch (JumpPower)
        {
            case 1: jumppower = JumpPower_1; break;
            case 2: jumppower = JumpPower_2; break;
            case 3: jumppower = JumpPower_3; break;
        }

        if (SpikeRunning) gameObjRigid.AddForce(gameObjTr.forward * SpikeJumpForwardlength / Time.timeScale);
        gameObjRigid.AddForce(Vector3.up * jumppower * (BaseJumpPower + JumpPowerLv * JumpPowerLvManipulator + Condition * ConditionManipulator) / Time.timeScale);
        Stemina -= JumpMinusStamina * jumppower * (1 - SteminaLv * StaminaLvManipulator);
        Condition = Stemina * 0.5f + Mental * 0.5f;
    }
    void BallHitFunction(string mode, float AvailableHitDistance, int Option = 0)
    {
        float MinusStamina = 0;
        float dis = Vector3.Distance(BallTr.position, HandTr.position);

        float BallHitPower = BaseBallHitPower + (PowerLv - PowerLvToStaminaLv) * PowerLvManipulator + Condition * ConditionManipulator + (AvailableHitDistance - dis) * HandBallDisManipulatorPower;
        float CommonBallHitPower = BaseBallHitPower + PowerLvManipulator + 100 * ConditionManipulator + AvailableHitDistance * 0.5f * HandBallDisManipulatorPower;

        float TurnAngle = BaseRotateAngle * dis;

        Vector2 Rot = Vector2.zero;
        Vector3 BallRot = BallTr.eulerAngles;

        BallTr.position = HandTr.position;
        switch (mode)
        {
            case "serve":
                MinusStamina = ServeHitMinusStamina;
                BallHitPower *= servePower1;
                BallRigid.AddTorque(BallTr.right * BallSpinPower);

                if (Serve_IsPlayerCtrl)
                {
                    Rot = camCtrl.CameraHolderTr.rotation.eulerAngles;
                    BallTr.rotation = Quaternion.Euler(Rot.x + settingCS.ServeBallAngleX, Rot.y, 0);
                }
                else
                {
                    BallTr.rotation = Quaternion.Euler(-(BaseServe1AngleX + (CommonBallHitPower - BallHitPower) * Serve1HitPowerDiffManipulator), gameObjTr.eulerAngles.y + Random.Range(-ServeRandomRotYRange, ServeRandomRotYRange), 0);
                }

                TurnAngle *= 2 - (DelicacyLv + 5) * ProficiencyLvManipulator;
                break;
            case "floatserve":
                MinusStamina = ServeHitMinusStamina;
                BallHitPower *= servePower2;
                ballCS.ChangeBallState("nonespin");

                if (Serve_IsPlayerCtrl)
                {
                    Rot = camCtrl.CameraHolderTr.rotation.eulerAngles;
                    BallTr.rotation = Quaternion.Euler(Rot.x + settingCS.ServeBallAngleX, Rot.y, 0);
                }
                else
                {
                    BallTr.LookAt(new Vector3(BallTr.position.x - BallTr.position.z * Mathf.Tan((gameObjTr.eulerAngles.y + Random.Range(-ServeRandomRotYRange, ServeRandomRotYRange)) * Mathf.PI / 180), referee.NetHight + Serve2NetHightPlusRotX, 0));
                }

                TurnAngle *= 2 - (DelicacyLv + FloatServeLv) * ProficiencyLvManipulator;
                break;
            case "spikeserve":
                MinusStamina = SpikeHitMinusStamina;
                BallHitPower *= servePower3;
                BallRigid.AddTorque(BallTr.right * BallSpinPower);
                ballCS.ChangeBallState("downspin");

                if (Serve_IsPlayerCtrl) BallTr.rotation = Quaternion.Euler(camCtrl.CameraHolderTr.rotation.eulerAngles.x + settingCS.ServeBallAngleX, Spike_AngleY, 0);
                else BallTr.LookAt(new Vector3(BallTr.position.x - BallTr.position.z * Mathf.Tan(Spike_AngleY * Mathf.PI / 180), referee.NetHight + Serve3NetHightPlusRotX - BallHitPower * Serve3NetHightMinusPowerManipulator, 0));

                TurnAngle *= 2 - (DelicacyLv + SpikeServeLv) * ProficiencyLvManipulator;
                break;
            case "spike":
                MinusStamina = SpikeHitMinusStamina;
                switch (Option)
                {
                    case 1: BallHitPower *= SpikePower1; break;
                    case 2: BallHitPower *= SpikePower2; break;
                    case 3: BallHitPower *= SpikePower3; break;
                }
                if (Spike_Running) BallHitPower *= SpikeRunningPowerManipulator;
                BallRigid.AddTorque(BallTr.right * BallSpinPower);
                ballCS.ChangeBallState("normal");

                if (teamCS.PlayerCtrl_PlayerNumber == StandingPositionNumber) BallTr.rotation = Quaternion.Euler(camCtrl.CameraHolderTr.rotation.eulerAngles.x + settingCS.SpikeAngleX, Spike_AngleY, 0);
                else BallTr.LookAt(new Vector3(BallTr.position.x - BallTr.position.z * Mathf.Tan(Spike_AngleY * Mathf.PI / 180), referee.NetHight + SpikeNetHightPlusRotX, 0));

                TurnAngle *= 2 - (DelicacyLv + SpikeLv) * ProficiencyLvManipulator;
                break;
        }
        
        if (TurnAngle < 0) print("TurnAngleMinus");
        //BallTr.Rotate(Random.Range(-TurnAngle, TurnAngle), Random.Range(-TurnAngle, TurnAngle), 0);
        BallRigid.velocity = Vector3.zero;
        BallRigid.AddForce(BallTr.forward * BallHitPower / Time.timeScale);
        BallTr.eulerAngles = BallRot;
        ballCS.ShowBallArrivalPos = 1;
        Stemina -= BaseMinusStamina * MinusStamina * (1 - (SteminaLv + PowerLvToStaminaLv) * StaminaLvManipulator);
        Condition = Condition = Stemina * 0.5f + Mental * 0.5f;
        if (!IsOp)
        {
            if (settingCS.CameraFollowBall) camCtrl.SetCamVar("ThirdPerson", BallTr);
            referee.PlusTouchCount(1);
        }
        else referee.PlusTouchCount(-1);
    }

    void ServeFunction()
    {
        if (Serve_Process == 0)
        {
            if (Serve_IsPlayerCtrl)
            {
                WASD(1);
                UICS.Serve_ShowMouseOriginPos();
                if (Input.GetMouseButtonDown(0))
                {
                    camCtrl.SetCamVar("none", null, 0, 1);
                    Serve_MouseOriginPos = Input.mousePosition;
                    gameObjTr.Rotate(0, camCtrl.CameraHolderTr.eulerAngles.y - gameObjTr.eulerAngles.y, 0);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    camCtrl.SetCamVar("none", null, 0, 0);
                    float dis = Vector2.Distance(Serve_MouseOriginPos, Input.mousePosition);
                    anim.SetFloat("anim play speed", 1);
                    if (dis < Serve1DragDis)
                    {
                        anim.SetTrigger("serve 1 1");
                        Serve_Mode = 1;
                    }
                    else if (dis < Serve3DragDis)
                    {
                        anim.SetTrigger("serve 2 1");
                        Serve_Mode = 2;
                    }
                    else
                    {
                        anim.SetTrigger("serve 3");
                        Serve_Mode = 3;
                    }
                    Serve_Process = 1;
                }
            }
            else
            {
                if (Serve_Mode == 1)
                {
                    if (ItsTime == 1)
                    {
                        anim.SetTrigger("serve 1 1");
                        Serve_Process = 1;
                        ItsTime = -1;
                    }
                }
                if (Serve_Mode == 2)
                {
                    if (ItsTime == 1)
                    {
                        anim.SetFloat("anim play speed", 1f);
                        anim.SetTrigger("serve 2 1");
                        Serve_Process = 1;
                        ItsTime = -1;
                    }
                }
                if (Serve_Mode == 3)
                {
                    if (ItsTime == 1)
                    {
                        anim.SetTrigger("serve 3");
                        Serve_Process = 1;
                        ItsTime = -1;
                    }
                }
            }
            BallTr.position = HandTr.position;
        }
        if (Serve_Process == 1)
        {
            if (Impact)
            {
                BallRigid.isKinematic = false;
                float JumpHightManipulator = (BaseJumpPower + JumpPowerLv * JumpPowerLvManipulator + Condition * ConditionManipulator) / (BaseJumpPower + JumpPowerLvManipulator + 100 * ConditionManipulator);
                float Adjustment = 0;
                if (Serve_Mode == 1)
                {
                    if (Serve_IsPlayerCtrl) Adjustment = settingCS.ThrowUpForce1Adjustment;
                    BallRigid.AddForce((gameObjTr.forward * ThrowForwardForce1 + Vector3.up * (ThrowUpForce1 + Adjustment)) / Time.timeScale);
                    Serve_Process = 2;
                }
                if (Serve_Mode == 2)
                {
                    if (Serve_IsPlayerCtrl) Adjustment = settingCS.ThrowUpForce2Adjustment;
                    BallRigid.AddForce((gameObjTr.forward * ThrowForwardForce2 + Vector3.up * (ThrowUpForce2 + Adjustment) * JumpHightManipulator) / Time.timeScale);
                    Serve_Process = 2;
                }
                if (Serve_Mode == 3)
                {
                    if (Serve_IsPlayerCtrl) Adjustment = settingCS.ThrowUpForce3Adjustment;
                    BallRigid.AddForce((gameObjTr.forward * ThrowForwardForce3 + Vector3.up * (ThrowUpForce3 + Adjustment) * JumpHightManipulator) / Time.timeScale);
                    Serve_Process = 6;
                }
                Impact = false;
            }
            else BallTr.position = HandTr.position;
        }
        if (Serve_Process == 2)
        {
            if (Serve_IsPlayerCtrl)
            {
                if (Serve_Mode == 1)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        anim.SetTrigger("serve 1 2");
                        Serve_Process = 3;
                    }
                }
                if (Serve_Mode == 2)
                {
                    if (!AnimEnd)
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            RunFunction(3, Vector3.forward, true);
                        }
                        else anim.SetFloat("anim play speed", 0);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        anim.SetTrigger("serve 2 2");
                        AnimEnd = false;
                        Serve_Process = 3;
                    }
                }
            }
            else
            {
                if (Serve_Mode == 1 && AnimEnd)
                {
                    if (ItsTime == -1) StartCoroutine(Timer(WaitPoint2Sec));
                    else if (ItsTime == 1)
                    {
                        anim.SetTrigger("serve 1 2");
                        Serve_Process = 3;
                        AnimEnd = false;
                        ItsTime = -1;
                    }
                }
                if (Serve_Mode == 2)
                {
                    if (AnimEnd)
                    {
                        anim.SetTrigger("serve 2 2");
                        AnimEnd = false;
                        Serve_Process = 3;
                    }
                    else
                    {
                        RunFunction(3, Vector3.forward, true);
                    }
                }

            }
        }
        if (Serve_Process == 3)
        {
            if (Serve_Mode == 1 && Impact)
            {
                if (Vector3.Distance(BallTr.position, HandTr.position) < ServeHandBallDis1) BallHitFunction("serve", ServeHandBallDis1);
                Impact = false;
            }
            if (Serve_Mode == 2 && Impact)
            {
                JumpFunction(2, true);
                Impact = false;
                Serve_Process = 4;
            }
        }
        if (Serve_Process == 4)
        {
            if (Serve_IsPlayerCtrl)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetTrigger("serve 2 3");
                    Serve_Process = 5;
                }
            }
            else if (AnimEnd)
            {
                anim.SetTrigger("serve 2 3");
                Serve_Process = 5;
            }
        }
        if (Serve_Process == 5)
        {
            if (Impact)
            {
                if (Vector3.Distance(BallTr.position, HandTr.position) < ServeHandBallDis2)
                {
                    Vector2 Rot = camCtrl.CameraHolderTr.rotation.eulerAngles;
                    BallTr.rotation = Quaternion.Euler(Rot.x + settingCS.ServeBallAngleX, Rot.y, 0);
                    BallTr.position = HandTr.position;
                    BallRigid.angularVelocity = Vector3.zero;
                    BallHitFunction("floatserve", ServeHandBallDis2);
                    Serve_Process = 7;
                }
                Impact = false;
            }
        }
        if (Serve_Process == 6)
        {
            if (Serve_IsPlayerCtrl) SpikeFunction(true, "spikeserve", Vector3.zero, false);
            else SpikeFunction(false, "spikeserve", Vector3.zero, false);
        }
    }
    void BlockFunction()
    {
        if (referee.TouchCount * OpManipulator == -1 && IsAnimPlaying == 0)
        {
            float PosX = 0;

            if (teamCS.BlockMode == 2 && BlockMode == 1)
            {
                if (CurrentPosition == "OH") PosX = -1.8f * OpManipulator;
                else if (CurrentPosition == "OP") PosX = 1.8f * OpManipulator;
                else if (CurrentPosition == "MB") PosX = 0;
            }

            Vector3 TargetPos = new Vector3(PosX, gameObjTr.position.y, -0.2f * OpManipulator);

            if (Vector3.Distance(gameObjTr.position, TargetPos) > 0.01f)
            {
                gameObjTr.LookAt(TargetPos);
                RunFunction(2, Vector3.forward);
                anim.SetBool("run", true);
            }
            else
            {
                gameObjTr.rotation = Quaternion.Euler(0, 90 - 90 * OpManipulator, 0);
                anim.SetBool("run", false);
            }
        }
        if ((referee.TouchCount * OpManipulator == -2 || referee.TouchCount * OpManipulator == -3) && ItsTime == 1)
        {
            if (BlockTriggered == 0 && IsAnimPlaying == 0)
            {
                PlayerCS opPlayer = teamCS.opTeamCS.CurrentPlayingPlayerCS[teamCS.opTeamCS.AttackerNumber];
                float SpikerPosX = opPlayer.gameObjTr.position.x - 0.1f * opPlayer.LeftHandedOrRightHanded * OpManipulator;
                Vector3 TargetPos = Vector3.right * SpikerPosX + Vector3.up * gameObjTr.position.y + Vector3.forward * -0.2f * OpManipulator;

                if (opPlayer.gameObjRigid.velocity.y > 0)
                {
                    if (Vector3.Distance(gameObjTr.position, TargetPos) < 1f)
                    {
                        gameObjTr.rotation = Quaternion.Euler(0, 90 - 90 * OpManipulator, 0);
                        anim.SetBool("run", false);

                        BlockTriggered = 1;
                        anim.SetFloat("anim play speed", 1);
                        anim.SetTrigger("block");
                    }
                }
                else
                {
                    if (Vector3.Distance(gameObjTr.position, TargetPos) > 0.1f)
                    {
                        gameObjTr.LookAt(TargetPos);
                        RunFunction(2, Vector3.forward);
                        anim.SetBool("run", true);
                    }
                    else
                    {
                        gameObjTr.rotation = Quaternion.Euler(0, 90 - 90 * OpManipulator, 0);
                        anim.SetBool("run", false);
                    }
                }
            }
            else if (BlockTriggered == 1 && Impact)
            {
                Impact = false;
                JumpFunction(3);
                BlockTriggered = 2;
            }
            else if (BlockTriggered == 2 && Impact)
            {
                Impact = false;
                anim.SetFloat("anim play speed", 0);
                BlockTriggered = 3;

                LeftArmColl.SetActive(true);
                LeftArmColl.transform.rotation = Quaternion.Euler(0, 0, 0);
                RightArmColl.SetActive(true);
                RightArmColl.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (BlockTriggered == 3 && gameObjRigid.velocity.y < -3)
            {
                anim.SetFloat("anim play speed", 1);
                BlockTriggered = 0;

                LeftArmColl.SetActive(false);
                RightArmColl.SetActive(false);
            }
        }
    }
    void ReceiveFunction()
    {
        Vector3 PlayerPos = gameObjTr.position;

        if (IsAnimPlaying == 0)
        {
            if (ReceiveModeManipulator == 0)
            {
                if (teamCS.Receive_PlayerCtrl && StandingPositionNumber == 5)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        anim.SetBool("run", false);
                        gameObjTr.forward = new Vector3(-BallRigid.velocity.x, 0, -BallRigid.velocity.z);
                        ModelTr.localRotation = Quaternion.Euler(0, 0, 0);

                        Vector3 StandardPos = new Vector3(PlayerPos.x, Receive_Hight[2], PlayerPos.z);
                        float ShortestDis = Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[1], -1)) + OverDisPlus;
                        Receive_Mode = 1;

                        float Dis;
                        for (int i = 2; i <= 3; i++)
                        {
                            Dis = Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[i], -1));
                            if (Dis < ShortestDis)
                            {
                                ShortestDis = Dis;
                                Receive_Mode = i;
                            }
                        }
                    }
                    if (Input.GetMouseButton(1))
                    {
                        PosBallTr.position = ballCS.BallArrivalPos(Receive_Hight[Receive_Mode], -1);
                        if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ReceiveCloseDis)
                        {
                            if (Receive_Mode == 1 && Input.GetKey(KeyCode.S))
                            {
                                anim.SetTrigger("receive 7");
                                ReceiveModeManipulator = 1;
                            }
                            if (Receive_Mode == 2 || Receive_Mode == 3)
                            {
                                Vector3 Vel = BallRigid.velocity;
                                Vel -= Vector3.up * Vel.y;
                                if (Vector3.Magnitude(Vel) > ReceiveUpBallMagnitude)
                                {
                                    float PosBallTrLocalPosX = PosBallTr.localPosition.x;
                                    if (Receive_Mode == 2)
                                    {
                                        if (PosBallTrLocalPosX < -CloseReceivePosX && Input.GetKey(KeyCode.A))
                                        {
                                            anim.SetTrigger("receive 4");
                                            ReceiveModeManipulator = 1;
                                        }
                                        else if (PosBallTrLocalPosX > CloseReceivePosX && Input.GetKey(KeyCode.D))
                                        {
                                            anim.SetTrigger("receive 6");
                                            ReceiveModeManipulator = 1;
                                        }
                                        else if (Input.GetKey(KeyCode.W))
                                        {
                                            anim.SetTrigger("receive 5");
                                            ReceiveModeManipulator = 1;
                                        }
                                    }
                                    else
                                    {
                                        if (PosBallTrLocalPosX < -CloseReceivePosX && Input.GetKey(KeyCode.A))
                                        {
                                            anim.SetTrigger("receive 1");
                                            ReceiveModeManipulator = 1;
                                        }
                                        else if (PosBallTrLocalPosX > CloseReceivePosX && Input.GetKey(KeyCode.D))
                                        {
                                            anim.SetTrigger("receive 3");
                                            ReceiveModeManipulator = 1;
                                        }
                                        else if (Input.GetKey(KeyCode.W))
                                        {
                                            anim.SetTrigger("receive 2");
                                            ReceiveModeManipulator = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    anim.SetTrigger("receive 8");
                                    ReceiveModeManipulator = ReceiveUpManipulator;
                                }
                            }
                        }
                        else if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ReceiveLongDis && Input.GetKey(KeyCode.W))
                        {
                            gameObjTr.LookAt(new Vector3(PosBallTr.position.x, gameObjTr.position.y, PosBallTr.position.z));
                            anim.SetTrigger("receive 0");
                            ReceiveModeManipulator = LongReceiveManipulator;
                        }
                    }
                    else
                    {
                        if (WASD(2)) anim.SetBool("run", true);
                        else anim.SetBool("run", false);
                    }
                }
                else
                {
                    Vector3 BallPos = BallTr.position;

                    if (Receive_Mode == 0)
                    {
                        Vector3 StandardPos = new Vector3(PlayerPos.x, Receive_Hight[2], PlayerPos.z);
                        float ShortestDis = Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[1], -1)) + OverDisPlus;
                        Receive_Mode = 1;

                        float Dis;
                        for (int i = 2; i <= 3; i++)
                        {
                            Dis = Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[i], -1));
                            if (Dis < ShortestDis)
                            {
                                ShortestDis = Dis;
                                Receive_Mode = i;
                            }
                        }
                    }
                    else
                    {
                        Vector3 BallArrivalPos = ballCS.BallArrivalPos(Receive_Hight[Receive_Mode], -1);
                        Vector3 PlayerMovePos = new Vector3(BallArrivalPos.x, PlayerPos.y, BallArrivalPos.z);
                        float ArrivalTime = ballCS.BallArrivalTime(BallArrivalPos.y, -1, BallRigid.velocity.y);

                        if (ArrivalTime > ReceiveMotionStartTime)
                        {
                            if (Vector3.Distance(PlayerMovePos, PlayerPos) > 0.2f)
                            {
                                if (gameObjTr.position.z * OpManipulator < -0.2f)
                                {
                                    anim.SetBool("run", true);
                                    gameObjTr.LookAt(PlayerMovePos);
                                    RunFunction(2, Vector3.forward);
                                }
                                else anim.SetBool("run", false);
                            }
                            else
                            {
                                if (ArrivalTime > ChangeReceiveModeTime) Receive_Mode = 2;
                                else
                                {
                                    gameObjTr.LookAt(new Vector3(BallPos.x, gameObjTr.position.y, BallPos.z));
                                    anim.SetBool("run", false);
                                }
                            }
                        }
                        else if (BallPos.z * OpManipulator < 0 && BallPos.z * OpManipulator > -4 && BallPos.x * OpManipulator > -2 && BallPos.x * OpManipulator < 2)
                        {
                            PosBallTr.position = ballCS.BallArrivalPos(Receive_Hight[Receive_Mode], -1);
                            if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ReceiveCloseDis)
                            {
                                gameObjTr.forward = new Vector3(-BallRigid.velocity.x, 0, -BallRigid.velocity.z);
                                ReceiveModeManipulator = 1;

                                if (Receive_Mode == 1) anim.SetTrigger("receive 7");
                                if (Receive_Mode == 2 || Receive_Mode == 3)
                                {
                                    Vector3 Vel = BallRigid.velocity;
                                    Vel -= Vector3.up * Vel.y;
                                    if (Vector3.Magnitude(Vel) > ReceiveUpBallMagnitude)
                                    {
                                        float PosBallTrLocalPosX = PosBallTr.localPosition.x;
                                        if (Receive_Mode == 2)
                                        {
                                            if (PosBallTrLocalPosX < -CloseReceivePosX) anim.SetTrigger("receive 4");
                                            else if (PosBallTrLocalPosX > CloseReceivePosX) anim.SetTrigger("receive 6");
                                            else anim.SetTrigger("receive 5");
                                        }
                                        else
                                        {
                                            if (PosBallTrLocalPosX < -CloseReceivePosX) anim.SetTrigger("receive 1");
                                            else if (PosBallTrLocalPosX > CloseReceivePosX) anim.SetTrigger("receive 3");
                                            else anim.SetTrigger("receive 2");
                                        }
                                    }
                                    else
                                    {
                                        anim.SetTrigger("receive 8");
                                        ReceiveModeManipulator = ReceiveUpManipulator;
                                    }
                                }
                            }
                            else if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ReceiveLongDis)
                            {
                                gameObjTr.LookAt(new Vector3(PosBallTr.position.x, gameObjTr.position.y, PosBallTr.position.z));
                                anim.SetTrigger("receive 0");
                                ReceiveModeManipulator = LongReceiveManipulator;
                            }
                        }
                        else
                        {
                            anim.SetBool("run", false);
                            print("아웃!");
                        }
                    }
                }
            }
            else if (ReceiveModeManipulator > 0)
            {
                Stemina -= BaseMinusStamina * ReceiveMinusStamina * ReceiveModeManipulator * (1 - (SteminaLv + PowerLvToStaminaLv) * StaminaLvManipulator);
                Condition = Condition = Stemina * 0.5f + Mental * 0.5f;
                ReceiveModeManipulator *= -1;
            }
        }

        if (Impact)
        {
            print("Impact!");
            Impact = false;
            MyBall = false;
            float dis = Vector3.Distance(BallTr.position, HandTr.position);

            if (dis < ReceiveBallHandDis * -ReceiveModeManipulator)
            {
                BallTr.position = HandTr.position;
                if (ballCS.BallState == "nonespin") dis *= 2;
                float TurnPower = BaseReceiveTurnPower * -ReceiveModeManipulator * (BallRigid.velocity.magnitude * ReceiveBallSpeedManipulator + dis * HandBallDisManipulatorRotate + 1 - (DelicacyLv + ReceiveLv) * ProficiencyLvManipulator);

                ballCS.ChangeBallState("normal");
                if (TurnPower < 0) print("TurnPower Minus");

                if (TurnPower < 3)
                {
                    BallRigid.velocity = ballCS.BallArrivalPower(new Vector3(0.3f * OpManipulator + Random.Range(-TurnPower, TurnPower), 1 + Random.Range(-TurnPower, TurnPower), -0.6f * OpManipulator + Random.Range(-TurnPower, TurnPower)), -1, 2.5f);
                    BallRigid.angularVelocity = Vector3.zero;
                }
                else
                {
                    print("Receive Fail:" + TurnPower);
                    Vector3 Vel = BallRigid.velocity;
                    Vel -= Vector3.up * Vel.y;
                    if (Vector3.Magnitude(Vel) > ReceiveUpBallMagnitude)
                    {
                        BallRigid.velocity *= 0.5f;
                        BallRigid.AddForce(Vector3.up * ReceiveFailUpForce / Time.timeScale);
                    }
                    else
                    {
                        BallRigid.velocity *= 0.1f;
                        BallRigid.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) / Time.timeScale);
                    }
                }

                if (!IsOp)
                {
                    referee.PlusTouchCount(1);
                    if (settingCS.CameraFollowBall) camCtrl.SetCamVar("ThirdPerson", BallTr);
                    teamCS.Change_PlayerCtrl_PlayerNumbur(0);
                }
                else referee.PlusTouchCount(-1);
                ReceiveModeManipulator = 0;
            }
        }
    }
    void SetupFunction()
    {
        Vector3 PlayerPos = gameObjTr.position;

        if (IsAnimPlaying == 0)
        {
            if (SetupModeManipulator == 0)
            {
                if (teamCS.Setup_PlayerCtrl)
                {
                    if (teamCS.AttackerNumber == 0)
                    {
                        //string SelectedAttackType = "None";

                        if (Input.GetKeyDown(KeyCode.W))
                        {
                            SelectedAttackType = "Quik";
                            SetupPlayerCtrlMaxHight = QuikSetupHight;
                        }
                        if (Input.GetKeyDown(KeyCode.S))
                        {
                            if (Input.GetKeyDown(KeyCode.A))
                            {
                                SelectedAttackType = "LeftBack";
                                SetupPlayerCtrlMaxHight = LeftBackSetupHight;
                            }
                            if (Input.GetKeyDown(KeyCode.D))
                            {
                                SelectedAttackType = "RightBack";
                                SetupPlayerCtrlMaxHight = RightBackSetupHight;
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.A))
                            {
                                SelectedAttackType = "Left";
                                SetupPlayerCtrlMaxHight = LeftSetupHight;
                            }
                            if (Input.GetKeyDown(KeyCode.D))
                            {
                                SelectedAttackType = "Right";
                                SetupPlayerCtrlMaxHight = RightSetupHight;
                            }
                        }

                        if (SelectedAttackType != "None")
                        {
                            referee.ChangeTimeScale(1);

                            for (int i = 1; i <= 6; i++)
                            {
                                if (teamCS.CurrentPlayingPlayerCS[i].AttackType == SelectedAttackType)
                                {
                                    teamCS.AttackerNumber = i;
                                }
                            }
                        }
                    }
                    else
                    {
                        SetterLineGizmo.transform.position = gameObjTr.position - Vector3.up * (gameObjTr.position.y - 0.1f);
                        SetterLineGizmo.transform.rotation = Quaternion.Euler(0, camCtrl.CameraHolderTr.eulerAngles.y, 0);

                        if (Setup_Mode == 0)
                        {
                            Vector3 StandardPos = new Vector3(PlayerPos.x, Receive_Hight[1], PlayerPos.z);
                            float ShortestDis = 10000;
                            if (SetupLv > 0) ShortestDis = Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[1], -1)) - SetupMinusDis;
                            Setup_Mode = 1;

                            if (Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ShortestDis) Setup_Mode = 2;
                        }
                        else
                        {
                            Vector3 BallArrivalPos = ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1);
                            Vector3 PlayerMovePos = new Vector3(BallArrivalPos.x, PlayerPos.y, BallArrivalPos.z);
                            float ArrivalTime = ballCS.BallArrivalTime(BallArrivalPos.y, -1, BallRigid.velocity.y);

                            float MotionStartTime = SetupMotionStartTime;
                            if (Setup_Mode == 2) MotionStartTime = ReceiveMotionStartTime;

                            if (ArrivalTime > MotionStartTime + 0.2f)
                            {
                                if (Vector3.Distance(PlayerMovePos, PlayerPos) > 0.1f)
                                {
                                    if (gameObjTr.position.z * OpManipulator < -0.2f)
                                    {
                                        anim.SetBool("run", true);
                                        gameObjTr.LookAt(PlayerMovePos);
                                        RunFunction(2, Vector3.forward);
                                    }
                                    else anim.SetBool("run", false);
                                }
                                else anim.SetBool("run", false);
                            }
                            else
                            {
                                referee.ChangeTimeScale(0.1f);

                                PosBallTr.position = ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1);
                                gameObjTr.eulerAngles = Vector3.up * (camCtrl.CameraHolderTr.eulerAngles.y - 90);

                                anim.SetBool("run", false);

                                if (Input.GetMouseButtonDown(1))
                                {
                                    ClickActivatedInSameTouchCount[1] = true;
                                    camCtrl.SetCamVar("none", null, 0, 1, 1);
                                    SetterPointGizmo.SetActive(true);
                                }
                                if (Input.GetMouseButton(1) && ClickActivatedInSameTouchCount[1])
                                {
                                    RaycastHit hit;
                                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                    Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("raycast_screen"));
                                    SetupPlayerCtrlTargetPos = hit.point;
                                    SetterPointGizmo.transform.position = new Vector3(SetupPlayerCtrlTargetPos.x, 0.1f, SetupPlayerCtrlTargetPos.z);
                                }
                                if (Input.GetMouseButtonUp(1) && ClickActivatedInSameTouchCount[1])
                                {
                                    camCtrl.SetCamVar("none", null, 0, 0);
                                    SetterPointGizmo.SetActive(false);

                                    referee.ChangeTimeScale(1);

                                    if (Setup_Mode == 1)
                                    {
                                        if (SetupLv > 0)
                                        {
                                            if (ArrivalTime < SetupMotionStartTime && Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1)) < SetupCloseDis)
                                            {
                                                SetupModeManipulator = SetupManipulator;
                                                if (SetupPlayerCtrlTargetPos.x < gameObjTr.position.x) anim.SetTrigger("setup 1");
                                                else anim.SetTrigger("setup 2");
                                            }
                                            else Setup_Mode = 2;
                                        }
                                        else Setup_Mode = 2;
                                    }
                                    if (Setup_Mode == 2)
                                    {
                                        if (ArrivalTime < ReceiveMotionStartTime)
                                        {
                                            if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1)) < ReceiveCloseDis)
                                            {
                                                anim.SetTrigger("receive 8");
                                                SetupManipulator = ReceiveUpManipulator;
                                            }
                                            else if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ReceiveLongDis)
                                            {
                                                gameObjTr.LookAt(new Vector3(PosBallTr.position.x, gameObjTr.position.y, PosBallTr.position.z));
                                                anim.SetTrigger("receive 0");
                                                SetupManipulator = LongReceiveManipulator;
                                            }
                                        }
                                    }

                                    if (Input.GetKey(KeyCode.Q)) SetupPlayerCtrlUpOrDown = 1;
                                    else SetupPlayerCtrlUpOrDown = -1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Setup_Mode == 0)
                    {
                        Vector3 StandardPos = new Vector3(PlayerPos.x, Receive_Hight[1], PlayerPos.z);
                        float ShortestDis = 10000;
                        if (SetupLv > 0) ShortestDis = Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[1], -1)) - SetupMinusDis;
                        Setup_Mode = 1;

                        if (Vector3.Distance(StandardPos, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ShortestDis) Setup_Mode = 2;
                    }
                    else
                    {
                        Vector3 BallArrivalPos = ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1);
                        Vector3 PlayerMovePos = new Vector3(BallArrivalPos.x, PlayerPos.y, BallArrivalPos.z);
                        float ArrivalTime = ballCS.BallArrivalTime(BallArrivalPos.y, -1, BallRigid.velocity.y);

                        float MotionStartTime = SetupMotionStartTime;
                        if (Setup_Mode == 2) MotionStartTime = ReceiveMotionStartTime;

                        if (ArrivalTime > MotionStartTime)
                        {
                            if (Vector3.Distance(PlayerMovePos, PlayerPos) > 0.1f)
                            {
                                if (gameObjTr.position.z * OpManipulator < -0.2f)
                                {
                                    anim.SetBool("run", true);
                                    gameObjTr.LookAt(PlayerMovePos);
                                    RunFunction(2, Vector3.forward);
                                }
                                else anim.SetBool("run", false);
                            }
                            else anim.SetBool("run", false);
                        }
                        else
                        {
                            PosBallTr.position = ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1);
                            gameObjTr.rotation = Quaternion.Euler(0, -90 * OpManipulator, 0);

                            if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[Setup_Mode], -1)) < SetupCloseDis)
                            {
                                SetupModeManipulator = 1;

                                int[] AttackPossibleNumber = new int[7];
                                int AttackPossibleCount = 1;
                                for (int i = 1; i <= 6; i++)
                                {
                                    if (StandingPositionNumber != i && teamCS.CurrentPlayingPlayerCS[i].AttackType != "None")
                                    {
                                        AttackPossibleNumber[AttackPossibleCount] = i;
                                        AttackPossibleCount++;
                                    }
                                }

                                teamCS.AttackerNumber = 2;//AttackPossibleNumber[Random.Range(1, AttackPossibleCount)];

                                if (Setup_Mode == 1)
                                {
                                    float posx = gameObjTr.position.x;
                                    switch (teamCS.CurrentPlayingPlayerCS[teamCS.AttackerNumber].AttackType)
                                    {
                                        case "Left":
                                            if (posx > teamCS.SetupPosLeftSpike.x) anim.SetTrigger("setup 1");
                                            else anim.SetTrigger("setup 2");
                                            break;
                                        case "Right":
                                            if (posx < teamCS.SetupPosRightSpike.x) anim.SetTrigger("setup 2");
                                            else anim.SetTrigger("setup 1");
                                            break;
                                        case "Quik":
                                            if (posx > teamCS.SetupPosQuikSpike.x) anim.SetTrigger("setup 1");
                                            else anim.SetTrigger("setup 2");
                                            break;
                                        case "LeftBack":
                                            if (posx > teamCS.SetupPosLeftBackSpike.x) anim.SetTrigger("setup 1");
                                            else anim.SetTrigger("setup 2");
                                            break;
                                        case "RightBack":
                                            if (posx < teamCS.SetupPosRightBackSpike.x) anim.SetTrigger("setup 2");
                                            else anim.SetTrigger("setup 1");
                                            break;
                                    }
                                    SetupModeManipulator = SetupManipulator;
                                }
                                if (Setup_Mode == 2)
                                {
                                    anim.SetTrigger("receive 8");
                                    SetupModeManipulator = ReceiveUpManipulator;
                                }
                            }
                            else if (Vector3.Distance(gameObjTr.position, ballCS.BallArrivalPos(Receive_Hight[2], -1)) < ReceiveLongDis)
                            {
                                gameObjTr.LookAt(new Vector3(PosBallTr.position.x, gameObjTr.position.y, PosBallTr.position.z));
                                anim.SetTrigger("receive 0");
                                SetupModeManipulator = LongReceiveManipulator;
                            }
                        }
                    }
                }
            }
            else if (SetupModeManipulator > 0)
            {
                anim.SetBool("run", false);
                Stemina -= BaseMinusStamina * SetupMinusStamina * SetupModeManipulator * (1 - (SteminaLv + PowerLvToStaminaLv) * StaminaLvManipulator);
                Condition = Condition = Stemina * 0.5f + Mental * 0.5f;
                SetupModeManipulator *= -1;
            }
        }

        if (Impact)
        {
            Impact = false;
            SetterLineGizmo.SetActive(false);
            float dis = Vector3.Distance(BallTr.position, HandTr.position);

            if (dis < ReceiveBallHandDis * -SetupModeManipulator)
            {
                BallTr.position = HandTr.position;
                float TurnPower = BaseSetupTurnPower * -SetupModeManipulator * (dis * HandBallDisManipulatorRotate + 1 - (DelicacyLv + SetupLv) * ProficiencyLvManipulator);

                ballCS.ChangeBallState("normal");
                if (TurnPower < 0) print("TurnPower Minus");

                int UpOrDown = 0;
                float SetupMaxHight = 0;
                Vector3 SetupPos = Vector3.zero;

                if (teamCS.Setup_PlayerCtrl)
                {
                    SetupPos = SetupPlayerCtrlTargetPos;
                    UpOrDown = SetupPlayerCtrlUpOrDown;
                    SetupMaxHight = SetupPlayerCtrlMaxHight;
                }
                else
                {
                    PlayerCS SpikePlayer = teamCS.CurrentPlayingPlayerCS[teamCS.AttackerNumber];
                    SetupPos = Vector3.up * SpikerMaxHight(SpikePlayer);

                    switch (SpikePlayer.AttackType)
                    {
                        case "Left":
                            UpOrDown = -1;
                            SetupPos += teamCS.SetupPosLeftSpike;
                            SetupMaxHight = LeftSetupHight;
                            break;
                        case "Right":
                            UpOrDown = -1;
                            SetupPos += teamCS.SetupPosRightSpike;
                            SetupMaxHight = RightSetupHight;
                            break;
                        case "Quik":
                            UpOrDown = 1;
                            SetupPos += teamCS.SetupPosQuikSpike;
                            SetupMaxHight = QuikSetupHight;
                            break;
                        case "LeftBack":
                            UpOrDown = -1;
                            SetupPos += teamCS.SetupPosLeftBackSpike;
                            SetupMaxHight = LeftBackSetupHight;
                            break;
                        case "RightBack":
                            UpOrDown = -1;
                            SetupPos += teamCS.SetupPosRightBackSpike;
                            SetupMaxHight = RightBackSetupHight;
                            break;
                    }
                    SetupPos = Vector3.right * (SetupPos.x * OpManipulator + Random.Range(-TurnPower, TurnPower)) + Vector3.up * (SetupPos.y + Random.Range(-TurnPower, TurnPower)) + Vector3.forward * (SetupPos.z + Random.Range(-TurnPower, TurnPower));
                }

                BallRigid.velocity = ballCS.BallArrivalPower(SetupPos, UpOrDown, SetupMaxHight);
                BallRigid.angularVelocity = Vector3.zero;

                if (!IsOp)
                {
                    referee.PlusTouchCount(1);
                    if (!teamCS.Spike_PlayerCtrl)
                    {
                        if (settingCS.CameraFollowBall) camCtrl.SetCamVar("ThirdPerson", BallTr);
                        teamCS.Change_PlayerCtrl_PlayerNumbur(0);
                    }
                }
                else referee.PlusTouchCount(-1);
                SetupModeManipulator = 0;
            }
        }
    }
    void SpikeFunction(bool IsPlayerCtrl, string SpikeOrServe, Vector3 RunTargetPos, bool IsJumpPosAvailable)
    {
        if (Spike_Process == 1 && IsAnimPlaying == 0)
        {
            Spike_Running = false;
            if (IsPlayerCtrl)
            {
                if (WASD(2)) anim.SetBool("run", true);
                else anim.SetBool("run", false);

                if (Input.GetMouseButtonUp(0))
                {
                    anim.SetTrigger("spike run");
                    Spike_Process = 2;
                }
            }
            else
            {
                bool NextProcess = true;
                if (AttackType != "None")
                {
                    NextProcess = false;

                    float Time = 0;
                    Vector3 Pos = Vector3.up * gameObjTr.position.y;
                    Vector3 LookPos = Vector3.up * gameObjTr.position.y;

                    switch (AttackType)
                    {
                        case "Left": 
                            Time = LeftSpikeRunStartTime;
                            Pos += LeftSpikeDefaultPos * OpManipulator;
                            LookPos += teamCS.SetupPosLeftSpike;
                            break;
                        case "Right":
                            Time = RightSpikeRunStartTime;
                            Pos += RightSpikeDefaultPos * OpManipulator;
                            LookPos += teamCS.SetupPosRightSpike;
                            break;
                        case "LeftBack": 
                            Time = LeftBackSpikeRunStartTime;
                            Pos += LeftBackDefaultPos * OpManipulator;
                            LookPos += teamCS.SetupPosLeftBackSpike;
                            break;
                        case "RightBack": 
                            Time = RightBackSpikeRunStartTime;
                            Pos += RightBackDefaultPos * OpManipulator;
                            LookPos += teamCS.SetupPosRightBackSpike;
                            break;
                        case "Quik": 
                            Time = QuikSpikeRunStartTime;
                            Pos += QuikDefaultPos * OpManipulator;
                            LookPos += teamCS.SetupPosQuikSpike;
                            break;
                    }

                    if (ballCS.BallArrivalTime(teamCS.SetupHandHight, -1, ballCS.BallRigid.velocity.y) < Time) NextProcess = true;

                    if (Vector3.Distance(Pos, gameObjTr.position) < 0.3f)
                    {
                        anim.SetBool("run", false);
                        gameObjTr.LookAt(LookPos);
                    }
                    else
                    {
                        NextProcess = false;
                        gameObjTr.LookAt(Pos);
                        RunFunction(2, Vector3.forward);
                        anim.SetBool("run", true);
                    }
                }

                if (NextProcess)
                {
                    anim.SetTrigger("spike run");
                    Spike_Process = 2;
                }
            }
        }
        if (Spike_Process == 2)
        {
            if (IsPlayerCtrl)
            {
                if (!AnimEnd)
                {
                    Spike_Running = false;
                    if (Input.GetKey(KeyCode.W))
                    {
                        RunFunction(3, Vector3.forward, true);
                    }
                    else anim.SetFloat("anim play speed", 0);
                }
                else Spike_Running = true;

                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetTrigger("spike jump");
                    AnimEnd = false;
                    Spike_Process = 3;
                }
            }
            else
            {
                if (AnimEnd || (IsJumpPosAvailable && Vector3.Distance(gameObjTr.position, RunTargetPos) < 0.2f))
                {
                    if (AttackType != "None") gameObjTr.LookAt(new Vector3(0, gameObjTr.position.y, 3.5f * OpManipulator));
                    anim.SetTrigger("spike jump");
                    Spike_Process = 3;
                    if (AnimEnd)
                    {
                        Spike_Running = true;
                        AnimEnd = false;
                    }
                }
                else
                {
                    Vector3 RunTartgetPosFinal = RunTargetPos;
                    if (AttackType != "None" && referee.TouchCount == 2 && teamCS.AttackerNumber == StandingPositionNumber)
                    {
                        Vector3 Pos = ballCS.BallArrivalPos(SpikerMaxHight(this), AttackType == "Quik" ? 1 : -1);
                        RunTartgetPosFinal = new Vector3(Pos.x, gameObjTr.position.y, Pos.z);
                    }

                    if (IsJumpPosAvailable) gameObjTr.LookAt(RunTartgetPosFinal);
                    Spike_Running = false;
                    RunFunction(3, Vector3.forward, true);
                }
            }
        }
        if (Spike_Process == 3 && Impact)
        {
            JumpFunction(3, Spike_Running);
            Impact = false;
            Spike_Process = 4;
        }
        if (Spike_Process == 4)
        {
            if (SpikeOrServe == "spikeserve")
            {
                if (IsPlayerCtrl) Spike_AngleY = camCtrl.CameraHolderTr.eulerAngles.y;
                else Spike_AngleY = gameObjTr.eulerAngles.y + Random.Range(-ServeRandomRotYRange, ServeRandomRotYRange);
            }
            if (SpikeOrServe == "spike")
            {
                if (IsPlayerCtrl) Spike_AngleY = camCtrl.CameraHolderTr.eulerAngles.y;
                else Spike_AngleY = gameObjTr.eulerAngles.y + Random.Range(-SpikeRandomRotYRange, SpikeRandomRotYRange);
            }

            if ((IsPlayerCtrl && Input.GetMouseButtonDown(0)) || (!IsPlayerCtrl && gameObjRigid.velocity.y < 0.5f))
            {
                float Diff = Spike_AngleY - gameObjTr.eulerAngles.y;
                if (Diff > 0)
                {
                    if (Diff > 180)
                    {
                        if (360 - Diff > SpikeStreightBallRotY) anim.SetTrigger("spike left");
                        else anim.SetTrigger("spike streight");
                    }
                    else
                    {
                        if (Diff > SpikeStreightBallRotY) anim.SetTrigger("spike right");
                        else anim.SetTrigger("spike streight");
                    }
                }
                else
                {
                    if (Diff < -180)
                    {
                        if (360 + Diff > SpikeStreightBallRotY) anim.SetTrigger("spike right");
                        else anim.SetTrigger("spike streight");
                    }
                    else
                    {
                        if (-Diff > SpikeStreightBallRotY) anim.SetTrigger("spike left");
                        else anim.SetTrigger("spike streight");
                    }
                }

                Spike_Process = 5;
            }
        }
        if (Spike_Process == 5)
        {
            if (Impact)
            {
                Impact = false;
                if (Vector3.Distance(BallTr.position, HandTr.position) < SpikeDistance1)
                {
                    bool hit = false;

                    if (SpikeOrServe == "spikeserve") hit = true;
                    else
                    {
                        if (AttackType != "None")
                        {
                            if (teamCS.AttackerNumber == StandingPositionNumber) hit = true;
                        }
                        else hit = true;
                    }

                    if (hit)
                    {
                        int power = 3;
                        switch (AttackType)
                        {
                            case "None":
                                power = 3;
                                break;
                            case "Left":
                                power = 3;
                                break;
                            case "Right":
                                power = 3;
                                break;
                            case "Quik":
                                power = 1;
                                break;
                            case "LeftBack":
                                power = 2;
                                break;
                            case "RightBack":
                                power = 2;
                                break;
                        }

                        BallHitFunction(SpikeOrServe, SpikeDistance1, power);
                        Spike_Process = 6;
                    }
                }
            }
        }
    }

    public void ImpactEvent(int AnimNum)
    {
        Impact = true;
        IsAnimPlaying = AnimNum;
    }
    public void AnimEndEvent(int Option = 0)
    {
        AnimEnd = true;

        if (IsAnimPlaying != 2)
        {
            StartCoroutine(Timer(WaitBeforeAnimEnd));
            if (ItsTime == 1) anim.SetTrigger("idle");
            if (Option == 2) gameObjTr.Translate(Vector3.forward * 1.2f);
            IsAnimPlaying = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ground" && IsAnimPlaying == 2)
        {
            IsAnimPlaying = 0;
            StartCoroutine(Timer(WaitBeforeAnimEnd));
            if (ItsTime == 1) anim.SetTrigger("idle");
        }
    }

    public void BeforeChangeTouchCount(int ChangeTouchCountTo)
    {
        for (int i = 0; i <= 5; i++) ClickActivatedInSameTouchCount[i] = false;

        if (ChangeTouchCountTo * OpManipulator == -2)
        {
            StartCoroutine(Timer(WaitForBlockReflex));
        }
        if (ChangeTouchCountTo * OpManipulator < 0)
        {
            Receive_Mode = 0;
        }
        if (ChangeTouchCountTo * OpManipulator == 1)
        {
            Setup_Mode = 0;
            if (teamCS.Setup_PlayerCtrl) SetterLineGizmo.SetActive(true);
            if (teamCS.Spike_PlayerCtrl) SelectedAttackType = "None";
        }
        if (ChangeTouchCountTo * OpManipulator == 3)
        {
            AttackType = "None";
        }
    }
    public void BeforeChangeGameStat(int ChangeGameStatTo)
    {
        if (ChangeGameStatTo == 0)
        {
            Stemina = 100;
            Mental = 100;
            Condition = 100;
        }
        if (ChangeGameStatTo == 1)
        {
            ItsTime = -1;
            anim.SetTrigger("idle");
            anim.SetBool("run", false);
        }
        if (ChangeGameStatTo == 2 && IsCurrentlyPlaying)
        {
            ModelTr.localRotation = Quaternion.Euler(0, 0, 0);
            for (int i = 1; i <= 4; i++) WASD_Button_Pushed_Number[i] = 0;
            Serve_Mode = 0;
            Serve_Process = 0;
            Impact = false;
            AnimEnd = false;
            Spike_Process = 1;
            Spike_Running = false;
            ItsTime = -1;
            ReceiveModeManipulator = 0;
            MyBall = false;
            IsAnimPlaying = 0;
            AttackType = "None";
            BlockTriggered = 0;
            LeftArmColl.SetActive(false);
            RightArmColl.SetActive(false);
            camCtrl.SetCamVar("none", null, 0, 0);
            SetterLineGizmo.SetActive(false);
            SetterPointGizmo.SetActive(false);
        }
        if (ChangeGameStatTo == 3 && IsCurrentlyPlaying)
        {

        }
        if (ChangeGameStatTo == 4 && IsCurrentlyPlaying)
        {
            anim.SetBool("run", false);
            anim.SetTrigger("idle");
            SetterLineGizmo.SetActive(false);
        }
    }
    public void AfterChangeGameStat(int ChangedGameStat)
    {
        if (ChangedGameStat == 2 && IsCurrentlyPlaying)
        {
            BallRigid.isKinematic = true;
            if ((referee.WhoServe * OpManipulator == 1) && Serve_Process == 0 && StandingPositionNumber == 1)
            {
                anim.SetTrigger("serve run");
                anim.SetFloat("anim play speed", 0);
                if (!IsOp) camCtrl.SetCamVar("ThirdPerson", gameObjTr);
                if (Serve_IsPlayerCtrl)
                {
                    gameObjTr.position = new Vector3(0, gameObjTr.position.y, -5);
                    teamCS.Change_PlayerCtrl_PlayerNumbur(1);
                }
                else
                {
                    teamCS.Change_PlayerCtrl_PlayerNumbur(0);

                    if (SpikeServeLv == 0 && FloatServeLv == 0) Serve_Mode = 1;
                    if (SpikeServeLv == 0 && FloatServeLv > 0) Serve_Mode = 2;
                    if (SpikeServeLv > 0 && FloatServeLv == 0) Serve_Mode = 3;
                    if (SpikeServeLv > 0 && FloatServeLv > 0)
                    {
                        if (Random.Range(1, 11) == Mathf.RoundToInt(10 * SpikeServeLv / (SpikeServeLv + FloatServeLv))) Serve_Mode = 3;
                        else Serve_Mode = 2;
                    }

                    if (Serve_Mode == 1)
                    {
                        gameObjTr.position = new Vector3(0, gameObjTr.position.y, -5f * OpManipulator);
                        gameObjTr.rotation = Quaternion.Euler(0, 90 - 90 * OpManipulator, 0);
                    }
                    if (Serve_Mode == 2)
                    {
                        gameObjTr.position = new Vector3(0, gameObjTr.position.y, -5f * OpManipulator);
                        gameObjTr.rotation = Quaternion.Euler(0, 90 - 90 * OpManipulator, 0);
                    }
                    if (Serve_Mode == 3)
                    {
                        gameObjTr.position = new Vector3(-2.1f * OpManipulator, gameObjTr.position.y, -5.55f * OpManipulator);
                        gameObjTr.rotation = Quaternion.Euler(0, 100 - 90 * OpManipulator, 0);
                    }

                    StartCoroutine(Timer(Wait3Sec));
                }
            }
        }
    }
}
