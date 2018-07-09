using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BlockWall {


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

    int[,] m_WallData = new int[10, 20];

    public void Reset()
    {
        for (int i = 0; i < m_WallData.GetLength(0); i++)
        {
            for (int j = 0; j < m_WallData.GetLength(1); j++)
            {
                m_WallData[i, j] = 0;
            }
        }
        m_CurBlockTeam = null;
    }

    System.Random rand = new System.Random();

    enum BT_Move_Type
    {
        BTM_None = 0,
        BTM_Left,
        BTM_Right,
        BTM_Down
    }

    bool IsCollide(BlockTeam bt, BT_Move_Type eBTMove = BT_Move_Type.BTM_None)
    {
        Vector2Int vPos = bt.GetPos();
        switch (eBTMove)
        {
            case BT_Move_Type.BTM_Left:
                vPos.x -= 1;
                break;
            case BT_Move_Type.BTM_Right:
                vPos.x += 1;
                break;
            case BT_Move_Type.BTM_Down:
                vPos.y += 1;
                break;
            default:
                break;
        }
        for (int i = 0; i < bt.GetWidth(); i++)
        {
            for (int j = 0; j < bt.GetHeight(); j++)
            {
                int iBTValue, iWallValue;
                if (bt.GetValue(i, j, out iBTValue) && GetValue(i+ vPos.x, j+ vPos.y, out iWallValue))
                {
                    if (iBTValue + iWallValue > 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void NewBlockTeam()
    {
        int iIndex = rand.Next(0, m_BlockTeamType.Count);
        Type type = m_BlockTeamType[iIndex];
        BlockTeam bt = Activator.CreateInstance(type) as BlockTeam;
        InitBlockTeamPos(bt);
        m_CurBlockTeam = bt;
    }

    void InitBlockTeamPos(BlockTeam bt)
    {
        int iX = (this.GetWidth() - bt.GetWidth()) / 2;
        int iY = 0 - bt.GetTopOffset();
        bt.SetPos(iX, iY);
    }

    int GetWidth()
    {
        return m_WallData.GetLength(0);
    }

    int GetHeight()
    {
        return m_WallData.GetLength(1);
    }

    bool GetValue(int iX, int iY, out int iValue)
    {
        iValue = 0;
        if (iX >= GetWidth() || iY >= GetHeight())
            return false;

        iValue = m_WallData[iX, iY];
        return true;
    }

    bool SetValue(int iX, int iY, int iValue)
    {
        if (iX >= GetWidth() || iY >= GetHeight())
            return false;

        m_WallData[iX, iY] = iValue;
        return true;
    }

    BlockTeam m_CurBlockTeam = null;
    public void OnLeft()
    {
        BlockTeam bt = m_CurBlockTeam;
        if (!IsCollide(bt, BT_Move_Type.BTM_Left))
            bt.OnLeft();
    }

    public void OnRight()
    {
        BlockTeam bt = m_CurBlockTeam;
        if (!IsCollide(bt, BT_Move_Type.BTM_Right))
            bt.OnRight();
    }

    public void OnTimer()
    {
        if (m_CurBlockTeam == null)
            NewBlockTeam();
        else
            OnDown();
    }
    public void OnDown()
    {
        BlockTeam bt = m_CurBlockTeam;
        if (!IsCollide(bt, BT_Move_Type.BTM_Down))
            bt.OnDown();
        else
        {
            Merge(bt);
            m_CurBlockTeam = null;
        }
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
}
