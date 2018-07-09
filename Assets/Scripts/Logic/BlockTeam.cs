using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTeam  {

    enum BlockIndex
    {
        BI_First = 0,
        BI_Second,
        BI_Third,
        BI_Torth,
        BI_Count
    }
    protected List<BaseBlock> m_BlockList = new List<BaseBlock>((int) BlockIndex.BI_Count);
    static protected List<int[,]> ms_RotPosData = new List<int[,]>();
    protected List<int[,]>.Enumerator m_PosEnumer;
    protected Vector2Int m_Pos = Vector2Int.zero;

    public BlockTeam()
    {
        m_PosEnumer = ms_RotPosData.GetEnumerator();
        m_PosEnumer.MoveNext();
    }
    public void Rot()
    {
        if (!m_PosEnumer.MoveNext())
        {
            m_PosEnumer = ms_RotPosData.GetEnumerator();
            m_PosEnumer.MoveNext();
        }
    }
    public void SetPos(int iX,int iY)
    {
        m_Pos.x = iX;
        m_Pos.y = iY;
    }
    public Vector2Int GetPos()
    {
        return m_Pos;
    }
    public virtual int GetWidth()
    {
        return 4;
    }
    public virtual int GetHeight()
    {
        return 4;
    }

    public int GetTopOffset()
    {
        int[,] vPos = m_PosEnumer.Current;
        int iIndex = 0;
        for (int i = 0;i<GetWidth();i++)
        {
            int iSum = 0;
            for (int j = 0;j <GetHeight();j++)
            {
                iSum+= vPos[i,j];
            }
            if (iSum != 0)
            {
                return iIndex;
            }
            else
            {
                iIndex++;
            }
        }
        return iIndex;
    }

    public bool GetValue(int iX, int iY,out int iValue)
    {
        iValue = 0;
        if (iX >= GetWidth() || iY >= GetHeight())
            return false;

        iValue = m_PosEnumer.Current[iX, iY];
        return true;
    }

    public void OnLeft()
    {
        m_Pos.x -= 1;
    }

    public void OnRight()
    {
        m_Pos.x += 1;
    }

    public void OnDown()
    {
        m_Pos.y += 1;
    }
}
