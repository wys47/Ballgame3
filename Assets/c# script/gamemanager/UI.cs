using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject startscrean;

    public GameObject 게임용UI;
    public GameObject ContinueUI;
    public GameObject Serve_MouseOriginPos;
    private RectTransform Serve_MouseOriginPosTr;

    public GameObject 메뉴화면UI;
    public GameObject 업그레이드뷰;
    public GameObject 플레이뷰;

    public Ref referee;

    public GameObject ObjForMatchHolder;

    private void Start()
    {
        startscrean.SetActive(true);
        메뉴화면UI.SetActive(false);
        게임용UI.SetActive(false);
        Serve_MouseOriginPosTr = Serve_MouseOriginPos.GetComponent<RectTransform>();
    }

    //버튼작동법
    public void 시작창클릭()
    {
        startscrean.SetActive(false);
        메뉴화면UI.SetActive(true);
        게임용UI.SetActive(false);
    }

    public void 개인연습버튼클릭()
    {
        업그레이드뷰.SetActive(true);
        플레이뷰.SetActive(false);
    }

    public void 연습경기버튼클릭()
    {
        업그레이드뷰.SetActive(false);
        플레이뷰.SetActive(true);
    }

    public void 게임버튼클릭()
    {
        게임용UI.SetActive(true);
        ContinueUI.SetActive(false);
        메뉴화면UI.SetActive(false);
        ObjForMatchHolder.SetActive(true);
    }

    public void 나가기버튼클릭()
    {
        게임용UI.SetActive(false);
        메뉴화면UI.SetActive(true);
        ObjForMatchHolder.SetActive(false);
    }

    public void ContunueUISetActive()
    {
        ContinueUI.SetActive(true);
    }

    public void 다음경기시작()
    {
        ContinueUI.SetActive(false);
        StartCoroutine(referee.ChangeGameStat(2));
    }

    public void Serve_ShowMouseOriginPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Serve_MouseOriginPosTr.position = Input.mousePosition;
            Serve_MouseOriginPos.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Serve_MouseOriginPos.SetActive(false);
        }
    }
}
