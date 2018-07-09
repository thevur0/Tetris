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

}
