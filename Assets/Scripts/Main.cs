using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Main : MonoBehaviour {

	// Use this for initialization
	GameObject m_BlockItem;
	SpriteRenderer[,] m_Sprites;

	void InitSprites()
	{
		m_BlockItem = GameObject.Find("2D").transform.Find("blockitem").gameObject;
		Transform tranParent = GameObject.Find("2D").transform.Find("blockparent");
		m_Sprites = new SpriteRenderer[m_BlockWall.GetWidth(),m_BlockWall.GetHeight()];
		for (int i = 0; i < m_Sprites.GetLength(0);i++)
		{
			for (int j = 0; j < m_Sprites.GetLength(1);j++)
			{
				GameObject go = GameObject.Instantiate(m_BlockItem,tranParent);
				m_Sprites[i,j] = go.GetComponent<SpriteRenderer>();
				m_Sprites[i, j].enabled = false;
				Vector3 pos = go.transform.position;
				pos.x = pos.x + i * 0.40f;
				pos.y = pos.y - j * 0.40f;
				go.transform.position = pos;
				go.SetActive(true);

			}
		}
	}
	void Start () {

		InitSprites();
        InitTimer();

    }
	// Update is called once per frame
	bool bOnTimer = false;
    void Update () {
        CheckMouse();
		if (bOnTimer)
		{
			m_BlockWall.OnTimer();
			RenderUtil.UpdateBlockWall(m_BlockWall, m_Sprites);
			bOnTimer = false;
		}
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
        m_Timer.Interval = 1000.0f;
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
			bOnTimer = true;
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
		m_BlockWall.OnRot();
		RenderUtil.UpdateBlockWall(m_BlockWall, m_Sprites);
    }
    void OnLeftMove()
    {
        Debug.Log("OnLeftMove");
		m_BlockWall.OnLeft();
		RenderUtil.UpdateBlockWall(m_BlockWall, m_Sprites);
    }
    void OnRightMove()
    {
        Debug.Log("OnRightMove");
		m_BlockWall.OnRight();
		RenderUtil.UpdateBlockWall(m_BlockWall, m_Sprites);
    }
    void OnDropDown()
    {
        Debug.Log("OnDropDown");
		m_BlockWall.OnDrop();
		RenderUtil.UpdateBlockWall(m_BlockWall, m_Sprites);
    }
    bool m_bGetBeginPos = false;
    Vector3 m_BeginPosition = Vector3.zero;
    float m_fCheckTime = 0.5f;
    float m_fSaveTime = 0.0f;
    float m_fMoveDis = 10.0f;
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
