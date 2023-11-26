using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamCS : MonoBehaviour
{
    private float PositioningFreeSpace = 0.5f;
    private float BallArrivalPosVelocityManipulator = 0.02f;
    private float MyBallPlusDis = 3f;
    private float StableReceiveDIs = 1f;

    [HideInInspector] public Vector3 SetterDefaultStandingPos = new Vector3(0.2f, 0, -0.3f);
    [HideInInspector] public Vector3 SetupPosLeftSpike = new Vector3(-1.8f, 0, -0.3f);
    [HideInInspector] public Vector3 SetupPosRightSpike = new Vector3(1.8f, 0, -0.3f);
    [HideInInspector] public Vector3 SetupPosQuikSpike = new Vector3(-0.3f, 0, -0.3f);
    [HideInInspector] public Vector3 SetupPosLeftBackSpike = new Vector3(-1, 0, -1f);
    [HideInInspector] public Vector3 SetupPosRightBackSpike = new Vector3(1, 0, -1f);

    private WaitForSeconds WaitNanoSec = new WaitForSeconds(0.01f);

    public PlayerCS[] AllPlayerCS = new PlayerCS[15];
    [HideInInspector] public PlayerCS[] CurrentPlayingPlayerCS = new PlayerCS[7];

    public bool IsOp;
    private int OpManipulator;
    private string FrontOrderOrBackOrder;
    private PlayerCS[] TempPlayerCS = new PlayerCS[7];
    [HideInInspector] public int PlayerCtrl_PlayerNumber;
    [HideInInspector] public bool Receive_PlayerCtrl;
    [HideInInspector] public bool Block_PlayerCtrl;
    [HideInInspector] public bool Setup_PlayerCtrl;
    [HideInInspector] public bool Spike_PlayerCtrl;
    [HideInInspector] public bool MyBallDeclared;
    private int MyBallPlayerNumber;
    [HideInInspector] public int AttackerNumber;
    [HideInInspector] public float SetupHandHight;
    [HideInInspector] public int BlockMode;//1. 커밋블록, 2. 리드블록

    public Ref referee;
    public BallCS ballCS;
    public CamCtrl camCtrl;
    public TeamCS opTeamCS;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 14; i++) AllPlayerCS[i].IsOp = IsOp;
        if (IsOp) Change_PlayerCtrl_PlayerNumbur(0);

        BlockMode = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (referee.GameStat == 3)
        {
            if (referee.TouchCount * OpManipulator < 0 && (referee.TouchCount * OpManipulator == -3 || ballCS.BallArrivalPos(referee.NetHight + 0.5f, -1).z * OpManipulator < 0))
            {
                if (!MyBallDeclared)
                {
                    if (Receive_PlayerCtrl)
                    {
                        if (Input.GetMouseButtonUp(1))
                        {
                            CurrentPlayingPlayerCS[5].MyBall = true;
                            MyBallDeclared = true;
                        }
                        if (Input.GetKeyUp(KeyCode.Q))
                        {
                            MyBallSetFunction(5);
                            MyBallDeclared = true;
                        }
                    }
                    else
                    {
                        MyBallSetFunction();
                        MyBallDeclared = true;
                    }
                }
            }
        }
    }

    private void PlayerPositioningFunction(string mode)
    {
        for (int i = 1; i <= 6; i++) TempPlayerCS[CurrentPlayingPlayerCS[i].StandingPositionNumber] = CurrentPlayingPlayerCS[i].GetComponent<PlayerCS>();
        bool[] PositionFixedPlayer = new bool[7];
        for (int k = 1; k <= 6; k++) PositionFixedPlayer[k] = false;

        if (mode == "servereceive")
        {
            int FrontLineOHNumber = 0;
            for (int i = 2; i <= 4; i++)
            {
                if (TempPlayerCS[i].CurrentPosition == "OH")
                {
                    FrontLineOHNumber = i;
                    TempPlayerCS[i].gameObjTr.position = new Vector3(-1.3f, TempPlayerCS[i].gameObjTr.position.y, -2.7f);
                    PositionFixedPlayer[i] = true;
                }
            }

            int BackLinePlayerNumber = 0;
            int FrontLinePlayerNumber = 0;
            if (TempPlayerCS[5].CurrentPosition == "S" || TempPlayerCS[5].CurrentPosition == "OP")
            {
                BackLinePlayerNumber = 5;

                TempPlayerCS[6].gameObjTr.position = new Vector3(0, TempPlayerCS[6].gameObjTr.position.y, -3);
                PositionFixedPlayer[6] = true;
                TempPlayerCS[1].gameObjTr.position = new Vector3(1.3f, TempPlayerCS[6].gameObjTr.position.y, -3);
                PositionFixedPlayer[1] = true;
            }
            else if (TempPlayerCS[6].CurrentPosition == "S" || TempPlayerCS[6].CurrentPosition == "OP")
            {
                BackLinePlayerNumber = 6;

                TempPlayerCS[5].gameObjTr.position = new Vector3(0, TempPlayerCS[6].gameObjTr.position.y, -3);
                PositionFixedPlayer[5] = true;
                TempPlayerCS[1].gameObjTr.position = new Vector3(1.3f, TempPlayerCS[6].gameObjTr.position.y, -3);
                PositionFixedPlayer[1] = true;
            }
            else if (TempPlayerCS[1].CurrentPosition == "S" || TempPlayerCS[1].CurrentPosition == "OP")
            {
                BackLinePlayerNumber = 1;

                TempPlayerCS[5].gameObjTr.position = new Vector3(0, TempPlayerCS[6].gameObjTr.position.y, -3);
                PositionFixedPlayer[5] = true;
                TempPlayerCS[6].gameObjTr.position = new Vector3(1.3f, TempPlayerCS[6].gameObjTr.position.y, -3);
                PositionFixedPlayer[6] = true;
            }

            if (BackLinePlayerNumber == 1) FrontLinePlayerNumber = 4;
            else FrontLinePlayerNumber = BackLinePlayerNumber - 3;

            if (TempPlayerCS[BackLinePlayerNumber].CurrentPosition == "S")
            {
                if (TempPlayerCS[2].CurrentPosition == "OH")
                {
                    TempPlayerCS[BackLinePlayerNumber].gameObjTr.position = new Vector3(1.75f, TempPlayerCS[BackLinePlayerNumber].gameObjTr.position.y, -3.7f);
                    PositionFixedPlayer[BackLinePlayerNumber] = true;
                }
                else
                {
                    TempPlayerCS[BackLinePlayerNumber].gameObjTr.position = new Vector3(0, TempPlayerCS[BackLinePlayerNumber].gameObjTr.position.y, -PositioningFreeSpace);
                    PositioningFaultCheckFunction(BackLinePlayerNumber, "z");
                    PositionFixedPlayer[BackLinePlayerNumber] = true;
                }

                if (FrontLineOHNumber < FrontLinePlayerNumber) TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position = new Vector3(TempPlayerCS[FrontLineOHNumber].gameObjTr.position.x - PositioningFreeSpace, TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position.y, -0.7f);
                else TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position = new Vector3((2 - PositioningFreeSpace), TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position.y, -0.4f);
                PositionFixedPlayer[FrontLinePlayerNumber] = true;
            }
            if (TempPlayerCS[BackLinePlayerNumber].CurrentPosition == "OP")
            {
                TempPlayerCS[BackLinePlayerNumber].gameObjTr.position = new Vector3(0, TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position.y, -3.7f);
                PositioningFaultCheckFunction(BackLinePlayerNumber, "z");
                PositionFixedPlayer[BackLinePlayerNumber] = true;

                if (FrontLineOHNumber < FrontLinePlayerNumber) TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position = new Vector3(TempPlayerCS[FrontLineOHNumber].gameObjTr.position.x - PositioningFreeSpace, TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position.y, -0.7f);
                else TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position = new Vector3(0.5f, TempPlayerCS[FrontLinePlayerNumber].gameObjTr.position.y, -0.4f);
                PositionFixedPlayer[FrontLinePlayerNumber] = true;
            }

            for (int i = 1; i <= 6; i++) if (!PositionFixedPlayer[i]) PositioningFaultCheckFunction(i);
        }

        for (int i = 1; i <= 6; i++)
        {
            Vector3 OriginPos = CurrentPlayingPlayerCS[i].gameObjTr.position;
            Vector3 TargetPos = (OriginPos - Vector3.up * OriginPos.y) * OpManipulator + Vector3.up * OriginPos.y;
            CurrentPlayingPlayerCS[i].gameObjTr.position = TargetPos;
            CurrentPlayingPlayerCS[i].gameObjTr.rotation = Quaternion.Euler(0, 90 - 90 * OpManipulator, 0);
        }
    }
    private void PositioningFaultCheckFunction(int PositionNumber, string FixedAxis = "none")
    {
        Vector3 PlayerPos = TempPlayerCS[PositionNumber].gameObjTr.position;
        float X = PlayerPos.x;
        float Z = PlayerPos.z;

        if (PositionNumber == 1)
        {
            if (FixedAxis != "x") X = Mathf.Lerp(TempPlayerCS[6].gameObjTr.position.x, 2 * OpManipulator, 0.5f);
            if (FixedAxis != "z") Z = Mathf.Lerp(TempPlayerCS[2].gameObjTr.position.z, -4 * OpManipulator, 0.5f);
        }
        if (PositionNumber == 2)
        {
            if (FixedAxis != "x") X = Mathf.Lerp(TempPlayerCS[3].gameObjTr.position.x, 2 * OpManipulator, 0.5f);
            if (FixedAxis != "z") Z = Mathf.Lerp(TempPlayerCS[1].gameObjTr.position.z, 0, 0.5f);
        }
        if (PositionNumber == 3)
        {
            if (FixedAxis != "x") X = Mathf.Lerp(TempPlayerCS[2].gameObjTr.position.x, TempPlayerCS[4].gameObjTr.position.x, 0.5f);
            if (FixedAxis != "z") Z = Mathf.Lerp(TempPlayerCS[6].gameObjTr.position.z, 0 * OpManipulator, 0.5f);
        }
        if (PositionNumber == 4)
        {
            if (FixedAxis != "x") X = Mathf.Lerp(TempPlayerCS[3].gameObjTr.position.x, -2 * OpManipulator, 0.5f);
            if (FixedAxis != "z") Z = Mathf.Lerp(TempPlayerCS[5].gameObjTr.position.z, 0, 0.5f);
        }
        if (PositionNumber == 5)
        {
            if (FixedAxis != "x") X = Mathf.Lerp(TempPlayerCS[6].gameObjTr.position.x, -2 * OpManipulator, 0.5f);
            if (FixedAxis != "z") Z = Mathf.Lerp(TempPlayerCS[4].gameObjTr.position.z, -4, 0.5f);
        }
        if (PositionNumber == 6)
        {
            if (FixedAxis != "x") X = Mathf.Lerp(TempPlayerCS[1].gameObjTr.position.x, TempPlayerCS[5].gameObjTr.position.x, 0.5f);
            if (FixedAxis != "z") Z = Mathf.Lerp(TempPlayerCS[3].gameObjTr.position.z, -4 * OpManipulator, 0.5f);
        }

        TempPlayerCS[PositionNumber].gameObjTr.position = new Vector3(X, PlayerPos.y, Z);
    }
    private void MyBallSetFunction(int ExcludingNumber = 0)
    {
        PlayerCS player = null;

        if (referee.TouchCount * OpManipulator < 0)
        {
            Vector3 BallArrivalPos = ballCS.BallArrivalPos(0.6f, -1);
            BallArrivalPos += new Vector3(ballCS.BallRigid.velocity.x * BallArrivalPosVelocityManipulator, 0, ballCS.BallRigid.velocity.z * BallArrivalPosVelocityManipulator);

            float ShortestDis = 1000;
            int ReceivingPlayerNumber = 0;

            for (int i = 1; i <= 6; i++)
            {
                if (i != ExcludingNumber)
                {
                    float dis = Vector3.Distance(BallArrivalPos, CurrentPlayingPlayerCS[i].gameObjTr.position);
                    if (CurrentPlayingPlayerCS[i].CurrentPosition == "S" || CurrentPlayingPlayerCS[i].CurrentPosition == "OP") dis += MyBallPlusDis;
                    if (dis < ShortestDis)
                    {
                        ShortestDis = dis;
                        ReceivingPlayerNumber = i;
                    }
                }
            }

            player = CurrentPlayingPlayerCS[ReceivingPlayerNumber].GetComponent<PlayerCS>();

            if (Receive_PlayerCtrl)
            {
                camCtrl.SetCamVar("ThirdPerson", player.gameObjTr);
                Change_PlayerCtrl_PlayerNumbur(player.StandingPositionNumber);
            }
        }
        if (referee.TouchCount * OpManipulator == 1)
        {
            Vector3 BallArrivalPos = ballCS.BallArrivalPos(0.6f, -1);

            float ShortestDis = 1000;
            int SecondTouchPlayerNumber = 0;

            for (int i = 1; i <= 6; i++)
            {
                if (i != ExcludingNumber)
                {
                    float dis = Vector3.Distance(BallArrivalPos, CurrentPlayingPlayerCS[i].gameObjTr.position);
                    if (!(CurrentPlayingPlayerCS[i].CurrentPosition == "S")) dis += MyBallPlusDis;
                    if (dis < ShortestDis)
                    {
                        ShortestDis = dis;
                        SecondTouchPlayerNumber = i;
                    }
                }
            }

            player = CurrentPlayingPlayerCS[SecondTouchPlayerNumber].GetComponent<PlayerCS>();
            MyBallPlayerNumber = SecondTouchPlayerNumber;

            SetupHandHight = player.Hight;
            if (player.SetupLv >= 3 && player.SetupJumpToss) SetupHandHight += 0.2f; 

            if (Setup_PlayerCtrl)
            {
                camCtrl.SetCamVar("ThirdPerson", player.gameObjTr);
                Change_PlayerCtrl_PlayerNumbur(player.StandingPositionNumber);
            }
        }

        player.MyBall = true;
    }
    private void AvailableAttackerNumberFunction()
    {
        if (CurrentPlayingPlayerCS[MyBallPlayerNumber].CurrentPosition == "S")
        {
            if (Vector3.Distance(SetterDefaultStandingPos * OpManipulator + Vector3.up * referee.NetHight, ballCS.BallArrivalPos(referee.NetHight, -1)) < StableReceiveDIs)
            {
                for (int i = 1; i <= 6; i++)
                {
                    PlayerCS playerCS = CurrentPlayingPlayerCS[i].GetComponent<PlayerCS>();

                    switch (playerCS.CurrentPosition)
                    {
                        case "S":
                            if (playerCS.StandingPositionNumber >= 2 && playerCS.StandingPositionNumber <= 4 && playerCS.SetupLv >= 3)
                            {
                                playerCS.AttackType = "SetterDump";
                                playerCS.Spike_Process = 1;
                            }
                            else playerCS.AttackType = "None";
                            break;
                        case "L":
                            playerCS.AttackType = "None";
                            break;
                        case "MB":
                            if (!playerCS.anim.GetCurrentAnimatorStateInfo(0).IsName("flying"))
                            {
                                if (playerCS.StandingPositionNumber >= 2 && playerCS.StandingPositionNumber <= 4 && CurrentPlayingPlayerCS[MyBallPlayerNumber].SetupLv >= 2)
                                {
                                    playerCS.AttackType = "Quik";
                                    playerCS.Spike_Process = 1;
                                    playerCS.SpikeRunTargetPos = SetupPosQuikSpike * OpManipulator;
                                }
                                else playerCS.AttackType = "None";
                            }
                            else playerCS.AttackType = "None";
                            break;
                        default:
                            if (!playerCS.anim.GetCurrentAnimatorStateInfo(0).IsName("flying"))
                            {
                                if (playerCS.StandingPositionNumber >= 2 && playerCS.StandingPositionNumber <= 4)
                                {
                                    if (playerCS.CurrentPosition == "OH")
                                    {
                                        playerCS.AttackType = "Left";
                                        playerCS.Spike_Process = 1;
                                        playerCS.SpikeRunTargetPos = SetupPosLeftSpike * OpManipulator;
                                    }
                                    else
                                    {
                                        playerCS.AttackType = "Right";
                                        playerCS.Spike_Process = 1;
                                        playerCS.SpikeRunTargetPos = SetupPosRightSpike * OpManipulator;
                                    }
                                }
                                else if (CurrentPlayingPlayerCS[MyBallPlayerNumber].SetupLv >= 2 && playerCS.SpikeLv >= 3)
                                {
                                    if (playerCS.CurrentPosition == "OH")
                                    {
                                        playerCS.AttackType = "LeftBack";
                                        playerCS.Spike_Process = 1;
                                        playerCS.SpikeRunTargetPos = SetupPosLeftBackSpike * OpManipulator;
                                    }
                                    else
                                    {
                                        playerCS.AttackType = "RightBack";
                                        playerCS.Spike_Process = 1;
                                        playerCS.SpikeRunTargetPos = SetupPosRightBackSpike * OpManipulator;
                                    }
                                }
                                else playerCS.AttackType = "None";
                            }
                            else playerCS.AttackType = "None";
                            break;
                    }
                }
            }
            else
            {
                
            }
        }
    }

    //변수 변환 함수
    public void Change_PlayerCtrl_PlayerNumbur(int n)
    {
        PlayerCtrl_PlayerNumber = n;
    }

    public IEnumerator ChangeTouchCount(int ChangeTouchCountTo)
    {
        if (ChangeTouchCountTo * OpManipulator < 0)
        {
            yield return WaitNanoSec;

            AttackerNumber = 0;
            if (ballCS.BallArrivalPos(referee.NetHight, -1).z * OpManipulator < 0)
            {
                MyBallDeclared = false;
                for (int i = 1; i <= 6; i++) CurrentPlayingPlayerCS[i].MyBall = false;
            }

            if (Receive_PlayerCtrl)
            {
                referee.ChangeTimeScale(0.2f);
                camCtrl.SetCamVar("ThirdPerson", CurrentPlayingPlayerCS[5].gameObjTr);
                Change_PlayerCtrl_PlayerNumbur(5);
            }
        }
        if (ChangeTouchCountTo * OpManipulator == 1)
        {
            yield return WaitNanoSec;

            for (int i = 1; i <= 6; i++) CurrentPlayingPlayerCS[i].MyBall = false;
            AttackerNumber = 0;

            MyBallSetFunction();
            MyBallDeclared = true;

            AvailableAttackerNumberFunction();

            if (Receive_PlayerCtrl) referee.ChangeTimeScale(1);
            if (Spike_PlayerCtrl)
            {
                referee.ChangeTimeScale(0.2f);
            }
        }
        if (ChangeTouchCountTo * OpManipulator == 2)
        {

        }
        if (ChangeTouchCountTo * OpManipulator == 3)
        {
            
        }

        for (int i = 1; i <= 6; i++) CurrentPlayingPlayerCS[i].BeforeChangeTouchCount(ChangeTouchCountTo);
    }
    public void BeforeChangeGameStat(int ChangeGameStatTo)
    {
        if (ChangeGameStatTo == 0)
        {
            if (!IsOp)
            {
                OpManipulator = 1;
                Receive_PlayerCtrl = true;
                Block_PlayerCtrl = false;
                Setup_PlayerCtrl = false;
                Spike_PlayerCtrl = false;
            }
            else
            {
                OpManipulator = -1;
                Receive_PlayerCtrl = false;
                Block_PlayerCtrl = false;
                Setup_PlayerCtrl = false;
                Spike_PlayerCtrl = false;
            }
            MyBallDeclared = true;
        }
        if (ChangeGameStatTo == 1)
        {
            for (int i = 1; i <= 14; i++)
            {
                AllPlayerCS[i].gameObjTr.position = new Vector3(-3, AllPlayerCS[i].gameObjTr.position.y, 0);
                AllPlayerCS[i].IsCurrentlyPlaying = false;
            }

            FrontOrderOrBackOrder = "back";
            if (FrontOrderOrBackOrder == "back")
            {
                CurrentPlayingPlayerCS[1] = AllPlayerCS[3].GetComponent<PlayerCS>();
                CurrentPlayingPlayerCS[1].CurrentPosition = "S";

                CurrentPlayingPlayerCS[2] = AllPlayerCS[5].GetComponent<PlayerCS>();
                CurrentPlayingPlayerCS[2].CurrentPosition = "OH";

                CurrentPlayingPlayerCS[3] = AllPlayerCS[8].GetComponent<PlayerCS>();
                CurrentPlayingPlayerCS[3].CurrentPosition = "MB";

                CurrentPlayingPlayerCS[4] = AllPlayerCS[1].GetComponent<PlayerCS>();
                CurrentPlayingPlayerCS[4].CurrentPosition = "OP";

                CurrentPlayingPlayerCS[5] = AllPlayerCS[6].GetComponent<PlayerCS>();
                CurrentPlayingPlayerCS[5].CurrentPosition = "OH";

                CurrentPlayingPlayerCS[6] = AllPlayerCS[11].GetComponent<PlayerCS>();
                CurrentPlayingPlayerCS[6].CurrentPosition = "L";

                for (int i = 1; i <= 6; i++)
                {
                    CurrentPlayingPlayerCS[i].IsCurrentlyPlaying = true;
                    CurrentPlayingPlayerCS[i].StandingPositionNumber = i;
                }
            }
        }
        if (ChangeGameStatTo == 2)
        {
            PlayerPositioningFunction("servereceive");
        }
        for (int i = 1; i <= 14; i++) AllPlayerCS[i].BeforeChangeGameStat(ChangeGameStatTo);
    }
    public void AfterChangeGameStat(int ChangedGameStat)
    {
        if (ChangedGameStat == 1)
        {
            if (!IsOp) CurrentPlayingPlayerCS[1].Serve_IsPlayerCtrl = true;
        }
        if (ChangedGameStat == 2)
        {
            MyBallDeclared = false;
        }
        for (int i = 1; i <= 6; i++) AllPlayerCS[i].AfterChangeGameStat(ChangedGameStat);
    }
}
