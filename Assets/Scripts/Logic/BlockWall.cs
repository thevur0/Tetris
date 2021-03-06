﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BlockWall {

	public enum BlockColor
	{
		None = 0,
        Gray,
        Red,
        Yellow,
        Green,
        Blue,
	}

    static List<Type> m_BlockTeamType = new List<Type>();
    static BlockWall()
    {
        m_BlockTeamType.Add(typeof(BlockTeam_I));
        m_BlockTeamType.Add(typeof(BlockTeam_J));
        m_BlockTeamType.Add(typeof(BlockTeam_L));
        m_BlockTeamType.Add(typeof(BlockTeam_O));
        m_BlockTeamType.Add(typeof(BlockTeam_P));
        m_BlockTeamType.Add(typeof(BlockTeam_Q));
        m_BlockTeamType.Add(typeof(BlockTeam_T));
    }

    int[,] m_WallData = new int[12, 20];

    BlockTeam m_CurBlockTeam = null;
    BlockTeam m_DropDownBT = null;
    Queue<BlockTeam> m_BlockTeamQueue = new Queue<BlockTeam>();

    void Reset()
    {
        for (int i = 0; i < m_WallData.GetLength(0); i++)
        {
            for (int j = 0; j < m_WallData.GetLength(1); j++)
            {
                m_WallData[i, j] = 0;
            }
        }
        m_CurBlockTeam = null;
        m_DropDownBT = null;
        m_BlockTeamQueue.Clear();
    }

    public void Start()
    {
        Reset();
        EnqueueBlockTeam();
        EnqueueBlockTeam();
    }
    System.Random m_Rand = new System.Random();

    enum BT_Move_Type
    {
        BTM_None = 0,
        BTM_Left,
        BTM_Right,
        BTM_Down
    }

    bool IsCollide(BlockTeam bt, BT_Move_Type eBTMove = BT_Move_Type.BTM_None)
    {
		if (bt == null)
			return true;
        Vector2Int vPos = bt.GetPos();
		int iX = vPos.x;
		int iY = vPos.y;
        switch (eBTMove)
        {
            case BT_Move_Type.BTM_Left:
				iX = vPos.x - 1;
                break;
            case BT_Move_Type.BTM_Right:
				iX = vPos.x + 1;
                break;
            case BT_Move_Type.BTM_Down:
				iY = vPos.y + 1;
                break;
            default:
                break;
        }
        for (int i = 0; i < bt.GetWidth(); i++)
        {
            for (int j = 0; j < bt.GetHeight(); j++)
            {
                int iBTValue, iWallValue;
                if (bt.GetValue(i, j, out iBTValue))
                {
					if(iBTValue != 0)
                    {
						if(GetValue(i + iX, j + iY, out iWallValue))
						{
							if (iWallValue != 0)
                                return true;
						}
						else
						{
							return true;
						}
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    void EnqueueBlockTeam()
    {
        int iIndex = m_Rand.Next(0, m_BlockTeamType.Count);
        Type type = m_BlockTeamType[iIndex];
        BlockTeam bt = Activator.CreateInstance(type) as BlockTeam;
        m_BlockTeamQueue.Enqueue(bt);
    }
    void NewBlockTeam()
    {
        EnqueueBlockTeam();
        BlockTeam bt = m_BlockTeamQueue.Dequeue();
        InitBlockTeamPos(bt);
        m_CurBlockTeam = bt;
        m_DropDownBT = m_CurBlockTeam.Clone();
        m_CurBlockTeam.OnBlockMove += OnCurBlockTeamUpdate;
        m_CurBlockTeam.OnBlockRot += OnCurBlockTeamUpdate;
        OnCurBlockTeamUpdate();
    }

    void OnCurBlockTeamUpdate()
    {
        m_CurBlockTeam.CopyTo(m_DropDownBT);
        while (!IsCollide(m_DropDownBT, BT_Move_Type.BTM_Down))
        {
            m_DropDownBT.MoveDown();
        }
    }

    void InitBlockTeamPos(BlockTeam bt)
    {
        int iX = (this.GetWidth() - bt.GetWidth()) / 2;
        int iY = 0 - bt.GetTopOffset();
        bt.SetPos(iX, iY);
    }

    public int GetWidth()
    {
        return m_WallData.GetLength(0);
    }

    public int GetHeight()
    {
        return m_WallData.GetLength(1);
    }

    bool GetValue(int iX, int iY, out int iValue)
    {
        iValue = 0;
		if (iX >= GetWidth() || iY >= GetHeight() || iX<0 || iY<0)
            return false;

        iValue = m_WallData[iX, iY];
        return true;
    }

	public bool GetBlockColor(int iX,int iY,out BlockColor blockColor)
	{
		blockColor = BlockColor.None;
		int iValue = 0;
		if(GetValue(iX,iY,out iValue))
		{
            if (iValue != 0)
                blockColor = BlockColor.Gray;
            else
            {
				if (m_DropDownBT != null)
                {
                    int iBTX = iX - m_DropDownBT.GetPos().x;
                    int iBTY = iY - m_DropDownBT.GetPos().y;
                    if (m_DropDownBT.GetValue(iBTX, iBTY, out iValue))
                    {
                        if (iValue != 0)
                            blockColor = BlockColor.Yellow;
                    }
                }

                if (m_CurBlockTeam != null)
                {
                    int iBTX = iX - m_CurBlockTeam.GetPos().x;
                    int iBTY = iY - m_CurBlockTeam.GetPos().y;
					if (m_CurBlockTeam.GetValue(iBTX, iBTY, out iValue))
                    {
                        if (iValue != 0)
                            blockColor = BlockColor.Red;
                    }
                } 
            }
				
			return true;
		}
		else
			return false;
	}

    bool SetValue(int iX, int iY, int iValue)
    {
        if (iX >= GetWidth() || iY >= GetHeight())
            return false;

        m_WallData[iX, iY] = iValue;
        return true;
    }

    

    public void OnLeft()
    {
        BlockTeam bt = m_CurBlockTeam;
        if (!IsCollide(bt, BT_Move_Type.BTM_Left))
            bt.MoveLeft();
    }

    public void OnRight()
    {
        BlockTeam bt = m_CurBlockTeam;
        if (!IsCollide(bt, BT_Move_Type.BTM_Right))
            bt.MoveRight();
    }

    public void OnDown()
    {
        BlockTeam bt = m_CurBlockTeam;
        if (!IsCollide(bt, BT_Move_Type.BTM_Down))
            bt.MoveDown();
        else
        {
            Merge(bt);
            m_CurBlockTeam = null;
            m_DropDownBT = null;
        }
    }
	public void OnDrop()
	{
        if (m_DropDownBT == null || m_CurBlockTeam == null)
            return;
        m_DropDownBT.CopyTo(m_CurBlockTeam);
	}

    public void OnRot()
    {
		if (m_CurBlockTeam == null)
			return;
        BlockTeam bt = m_CurBlockTeam.Clone();
        bt.Rot();
        if (!IsCollide(bt, BT_Move_Type.BTM_None))
            m_CurBlockTeam.Rot();
        else
        {
            BlockTeam btLeft = bt;
            BlockTeam btRight = bt.Clone();
            for (int i = 0;i< bt.GetBlockWidth(); i++)
            {
                btLeft.MoveLeft();
                if (!IsCollide(btLeft, BT_Move_Type.BTM_None))
                {
                    m_CurBlockTeam.Rot();
                    for (int j = i;j>=0;j--)
                    {
                        m_CurBlockTeam.MoveLeft();
                    }
                    break;
                }
                btRight.MoveRight();
                if (!IsCollide(btRight, BT_Move_Type.BTM_None))
                {
                    m_CurBlockTeam.Rot();
                    for (int j = i; j >= 0; j--)
                    {
                        m_CurBlockTeam.MoveRight();
                    }
                    break;
                }
            }
        }
    }

    public void OnTimer()
    {
        if (m_CurBlockTeam == null)
        {
            if(!Dispel())
                NewBlockTeam();
        }
        else
            OnDown();
    }

    void Merge(BlockTeam bt)
    {
        Vector2Int vPos = bt.GetPos();
        for (int i = 0; i < bt.GetWidth(); i++)
        {
            for (int j = 0; j < bt.GetHeight(); j++)
            {
                int iBTValue;
                if (bt.GetValue(i, j, out iBTValue) )
                {
                    if (iBTValue == 1)
                        SetValue(i + vPos.x, j + vPos.y, iBTValue);
                }
            }
        }
    }

    bool Dispel()
    {
        bool bDispel = false;
		int iIndex = GetHeight() - 1;
        while (iIndex >= 0)
        {
			bool bContuine = false;
			for (int i = 0;i<GetWidth();i++)
            {
                int iValue;
				if (GetValue(i, iIndex, out iValue))
                {
					if(iValue == 0)
					{
						bContuine = true;
						break;
					}
                }
				else
				{
					bContuine = true;
					break;
				}
            }
			if (bContuine)
			{
				iIndex--;
				continue;
			}
			
            for (int j = iIndex;j>=0;j--)
            {
				for (int i = 0; i < GetWidth(); i++)
                {
                    if (j - 1 >= 0)
                        m_WallData[i, j] = m_WallData[i, j-1];
                    else
                        m_WallData[i, j] = 0;
                }
            }
            bDispel = true;

        }
        return bDispel;
    }
}
