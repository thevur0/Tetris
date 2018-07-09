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
    static protected List<int[,]> ms_RotPos = new List<int[,]>();
    protected List<int[,]>.Enumerator m_PosEnumer;

    public BlockTeam()
    {
        m_PosEnumer = ms_RotPos.GetEnumerator();
        m_PosEnumer.MoveNext();
    }
    public void Rot()
    {
        if (!m_PosEnumer.MoveNext())
        {
            m_PosEnumer = ms_RotPos.GetEnumerator();
            m_PosEnumer.MoveNext();
        }
    }

}
