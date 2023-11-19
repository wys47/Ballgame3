using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ref : MonoBehaviour
{
    public float NetHight;

    public Transform ball;

    [HideInInspector] public int GameStat;// 1.시작 2.서브 3.경기 4.정지
    [HideInInspector] public int TouchCount;
    [HideInInspector] public int WhoServe;
    [HideInInspector] private int ServePlayerNumber;

    [HideInInspector] public float TimeScaleDefault;

    public Text HomeScoreBoard;
    public int HomeScore;
    public Text OpScoreBoard;
    public int OpScore;

    private bool ballin;
    private bool IsBallTouchedBlock;
    private bool IsBallTouchedOpBlock;

    private bool ActivedContinueUI;

    private WaitForSeconds Sec_2 = new WaitForSeconds(2);
    private WaitForSecondsRealtime Sec_1_Real = new WaitForSecondsRealtime(1);

    public UI UICS;
    public TeamCS teamCS;
    public TeamCS opTeamCS;
    public BallCS ballCS;
    public CamCtrl camCtrl;

    // Start is called before the first frame update
    void Start()
    {
        TimeScaleDefault = 1;
        Application.targetFrameRate = 60;
    }

    void Awake()//경기가 시작될때 이 코드도 활성화시킨다.
    {
        StartCoroutine(ChangeGameStat(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStat == 2)
        {
            if (WhoServe == 1)
            {
                PlayerCS Player = teamCS.CurrentPlayingPlayerCS[ServePlayerNumber];
                if (Player.Serve_IsPlayerCtrl && Player.gameObjTr.position.z > -4 && Player.gameObjRigid.velocity.y == 0)
                {
                    PlusPoint(2);
                }
                if (ball.position.y <= 0.218f)
                {
                    PlusPoint(2);
                }
            }
        }
        if (GameStat == 3)
        {
            if (ball.position.y <= 0.218f)
            {
                if ((TouchCount > 0 && IsBallTouchedOpBlock == false) || IsBallTouchedBlock)
                {
                    if (ballin)
                    {
                        if (ball.position.z > 0)
                        {
                            PlusPoint(1);
                        }
                        else
                        {
                            PlusPoint(2);
                        }
                    }
                    else
                    {
                        PlusPoint(2);
                    }
                }
                else
                {
                    if (ballin)
                    {
                        if (ball.position.z < 0)
                        {
                            PlusPoint(2);
                        }
                        else
                        {
                            PlusPoint(1);
                        }
                    }
                    else
                    {
                        PlusPoint(1);
                    }
                }
            }
            else
            {
                if (IsBallTouchedBlock)
                {
                    if (TouchCount == 1 || TouchCount == -1)
                    {
                        IsBallTouchedBlock = false;
                    }
                }
                if (IsBallTouchedOpBlock)
                {
                    if (TouchCount == -1 || TouchCount == 1)
                    {
                        IsBallTouchedOpBlock = false;
                    }
                }
            }
            //점수계산,서브, GameStat = 3
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            TimeScaleDefault = 0.2f;
            Time.timeScale *= TimeScaleDefault;
            Time.fixedDeltaTime *= TimeScaleDefault;
        }
    }

    private void PlusPoint(int WhatTeam)
    {
        if (WhatTeam == 1)
        {
            HomeScore++;
            HomeScoreBoard.text = HomeScore.ToString();
            WhoServe = 1;
            for (int i = 1; i <= 6; i++)
            {
                if (teamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber == 1) ServePlayerNumber = i;
                if (opTeamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber != 1) opTeamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber--;
                else opTeamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber = 6;
            }
        }
        else
        {
            OpScore++;
            OpScoreBoard.text = OpScore.ToString();
            WhoServe = -1;
            for (int i = 1; i <= 6; i++)
            {
                if (opTeamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber == 1) ServePlayerNumber = i;
                if (teamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber != 1) teamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber--;
                else teamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber = 6;
            }
        }
        StartCoroutine(ChangeGameStat(4));
    }

    public void PlusTouchCount(int HomeOrAi)
    {
        switch (GameStat)
        {
            case 2:
                TouchCount = HomeOrAi * 3;
                GameStat = 3;
                break;
            case 3:
                if (TouchCount * HomeOrAi > 0) TouchCount += HomeOrAi;
                else TouchCount = HomeOrAi;
                break;
        }

        StartCoroutine(teamCS.ChangeTouchCount(TouchCount));
        StartCoroutine(opTeamCS.ChangeTouchCount(TouchCount));
    }

    public void ChangeTimeScale(float time)
    {
        Time.timeScale = TimeScaleDefault * time;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball")
        {
            ballin = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ball")
        {
            ballin = false;
        }
    }

    public IEnumerator ChangeGameStat(int ChangeGameStatTo)
    {
        if (ChangeGameStatTo == 1)
        {
            HomeScoreBoard.text = "0";
            HomeScore = 0;
            OpScoreBoard.text = "0";
            OpScore = 0;

            WhoServe = -1;//Random.Range(1, 3);
        }
        if (ChangeGameStatTo == 2)
        {
            ballin = false;
            IsBallTouchedBlock = false;
            IsBallTouchedOpBlock = false;
            TouchCount = 0;

            yield return Sec_2;

            ball.position = new Vector3(0, 100, 0);
        }
        teamCS.BeforeChangeGameStat(ChangeGameStatTo);
        opTeamCS.BeforeChangeGameStat(ChangeGameStatTo);
        ballCS.BeforeChangeGameStat(ChangeGameStatTo);
        camCtrl.BeforeChangeGameStat(ChangeGameStatTo);
        GameStat = ChangeGameStatTo;
        if (ChangeGameStatTo == 0) StartCoroutine(ChangeGameStat(1));
        if (ChangeGameStatTo == 1)
        {
            for (int i = 1; i <= 6; i++)
            {
                if (teamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber == 1) ServePlayerNumber = i;
                if (opTeamCS.CurrentPlayingPlayerCS[i].StandingPositionNumber == 1) ServePlayerNumber = i;
            }

            yield return Sec_2;
            UICS.ContunueUISetActive();
        }
        if (ChangeGameStatTo == 4)
        {
            Time.timeScale = 0;
            camCtrl.SetCamVar("none", ball, 1, 0, 0, 0);
            yield return Sec_1_Real;
            Time.timeScale = TimeScaleDefault;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            yield return Sec_2;
            UICS.ContunueUISetActive();
        }
        ballCS.AfterChangeGameStat(ChangeGameStatTo);
        teamCS.AfterChangeGameStat(ChangeGameStatTo);
        opTeamCS.AfterChangeGameStat(ChangeGameStatTo);
    }
}
