using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class BlockTeam {

    public delegate void ActionBTMove();
    public delegate void ActionBTRot();

    public event ActionBTMove OnBlockMove;
    public event ActionBTRot OnBlockRot;

    enum BlockIndex
    {
        BI_First = 0,
        BI_Second,
        BI_Third,
        BI_Torth,
        BI_Count
    }
    protected List<BaseBlock> m_BlockList = new List<BaseBlock>((int)BlockIndex.BI_Count);
    
    int m_iIndexPos = 0;
    protected Vector2Int m_Pos = Vector2Int.zero;

    public BlockTeam()
    {

    }
    public void Rot()
    {
        m_iIndexPos++;
		if (m_iIndexPos >= GetRotData().Count)
            m_iIndexPos = 0;
        if (OnBlockRot != null)
            OnBlockRot.Invoke();
    }

    int[,] CurrentPos()
    {
		return GetRotData()[m_iIndexPos];
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
    public int GetBlockWidth()
    {
        int iWidth = 0;
        int[,] vPos = CurrentPos();
        for (int i = 0; i < GetHeight(); i++)
        {
            int iSum = 0;
            for (int j = 0; j < GetWidth(); j++)
            {
                iSum += vPos[j, i];
            }
            if (iSum != 0)
            {
                iWidth++;
            }
        }
        return iWidth;
    }
    public int GetTopOffset()
    {
        int[,] vPos = CurrentPos();
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
    public void CopyTo(BlockTeam bt)
    {
        bt.SetPos(GetPos().x, GetPos().y);
        bt.SetRotIndex(m_iIndexPos);
    }

    //public int GetBottomOffset()
    //{
    //    int[,] vPos = CurrentPos();
    //    int iIndex = 0;
    //    for (int i = GetWidth()-1; i >=0; i--)
    //    {
    //        int iSum = 0;
    //        for (int j = GetHeight()-1; j >=0 ; j--)
    //        {
    //            iSum += vPos[i, j];
    //        }
    //        if (iSum != 0)
    //        {
    //            return iIndex;
    //        }
    //        else
    //        {
    //            iIndex++;
    //        }
    //    }
    //    return iIndex;
    //}

    //public int GetLeftOffset()
    //{
    //    int[,] vPos = CurrentPos();
    //    int iIndex = 0;
    //    for (int i = 0; i < GetHeight(); i++)
    //    {
    //        int iSum = 0;
    //        for (int j = 0; j < GetWidth(); j++)
    //        {
    //            iSum += vPos[j, i];
    //        }
    //        if (iSum != 0)
    //        {
    //            return iIndex;
    //        }
    //        else
    //        {
    //            iIndex++;
    //        }
    //    }
    //    return iIndex;
    //}

    //public int GetRightOffset()
    //{
    //    int[,] vPos = CurrentPos();
    //    int iIndex = 0;
    //    for (int i = GetHeight()-1; i >= 0; i++)
    //    {
    //        int iSum = 0;
    //        for (int j = GetWidth()-1; j >= 0; j++)
    //        {
    //            iSum += vPos[j, i];
    //        }
    //        if (iSum != 0)
    //        {
    //            return iIndex;
    //        }
    //        else
    //        {
    //            iIndex++;
    //        }
    //    }
    //    return iIndex;
    //}

    public bool GetValue(int iX, int iY,out int iValue)
    {
        iValue = 0;
		if (iX >= GetWidth() || iY >= GetHeight() || iX < 0 || iY<0)
            return false;
		try
		{
			iValue = CurrentPos()[iX, iY];
		}
		catch(System.Exception ex)
		{
			Debug.LogException(ex);
		}
        return true;
    }

    public void MoveLeft()
    {
        m_Pos.x -= 1;
        if(OnBlockMove!=null)
            OnBlockMove.Invoke();
    }

    public void MoveRight()
    {
        m_Pos.x += 1;
        if (OnBlockMove != null)
            OnBlockMove.Invoke();
    }

    public void MoveDown()
    {
        m_Pos.y += 1;
    }

    public virtual BlockTeam Clone()
    {
        BlockTeam bt = Activator.CreateInstance(this.GetType()) as BlockTeam;
		bt.SetPos(this.GetPos().x, this.GetPos().y);
		bt.SetRotIndex(m_iIndexPos);

        return bt;
    }
	public void SetRotIndex(int iIndex)
	{
		m_iIndexPos = iIndex;
        if (OnBlockRot != null)
            OnBlockRot.Invoke();

    }

	protected abstract List<int[,]> GetRotData();

}
