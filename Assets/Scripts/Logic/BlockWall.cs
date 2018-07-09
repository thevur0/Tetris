using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall {
    int[,] m_WallData = new int[10,20];

    public void Reset()
    {
        for (int i = 0; i < m_WallData.GetLength(0); i++)
        {
            for (int j = 0; j < m_WallData.GetLength(1); j++)
            {
                m_WallData[i,j] = 0;
            }
        }
    }

    public bool IsCollide(BlockTeam bt)
    {
        return true;
    }
}
