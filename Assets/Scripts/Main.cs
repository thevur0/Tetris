using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitTimer();

    }
    // Update is called once per frame
    void Update () {
        CheckMouse();
    }

    BlockWall m_BlockWall = new BlockWall();
    public void OnGameStart()
    {
        m_BlockWall.Reset();
        StartTimer();
    }
    public void OnPauseGame()
    {
        if (m_Timer.Enabled)
            StopTimer();
        else
            StartTimer();
    }
    void InitTimer()
    {
        m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimer);
        m_Timer.Interval = 1.0f;
    }

    System.Timers.Timer m_Timer = new System.Timers.Timer();
    void StartTimer()
    {
        m_Timer.Enabled = true;
        m_Timer.Start();   
    }

    void OnTimer(object sender,System.Timers.ElapsedEventArgs e)
    {
        if (sender == m_Timer)
        {
            m_BlockWall.OnTimer();
        }
    }

    void StopTimer()
    {
        m_Timer.Stop();
        m_Timer.Enabled = false;
    }


    void OnClick()
    {
        Debug.Log("OnClick");
    }
    void OnLeftMove()
    {
        Debug.Log("OnLeftMove");
    }
    void OnRightMove()
    {
        Debug.Log("OnRightMove");
    }
    void OnDropDown()
    {
        Debug.Log("OnDropDown");
    }
    bool m_bGetBeginPos = false;
    Vector3 m_BeginPosition = Vector3.zero;
    float m_fCheckTime = 0.5f;
    float m_fSaveTime = 0.0f;
    float m_fMoveDis = 1.0f;
    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_fSaveTime = 0;
            if (m_bGetBeginPos)
            {
                m_BeginPosition = Input.mousePosition;
                m_bGetBeginPos = false;
            }
        }
        bool bMove = false;
        if (Input.GetMouseButton(0))
        {
            m_fSaveTime += Time.deltaTime;
            if (m_fSaveTime > m_fCheckTime)
            {
                Vector3 vDis = Input.mousePosition - m_BeginPosition;
                if (Mathf.Abs(vDis.x) > m_fMoveDis)
                {
                    if (vDis.x < 0)
                        OnLeftMove();
                    else
                        OnRightMove();
                    m_BeginPosition = Input.mousePosition;
                    bMove = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_bGetBeginPos = true;
            if (!bMove)
            {
                Vector3 vDis = Input.mousePosition - m_BeginPosition;
                if (Mathf.Abs(vDis.y) > Mathf.Abs(vDis.x) * 3.0f / 2.0f)
                {
                    OnDropDown();
                }
                else if (m_fSaveTime < m_fCheckTime) 
                {
                    OnClick();
                }
            }
        }
    }
}
