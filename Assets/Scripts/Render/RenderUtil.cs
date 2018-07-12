using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderUtil
{
    static Dictionary<BlockWall.BlockColor, Color> m_dicColor = new Dictionary<BlockWall.BlockColor, Color>();
    static RenderUtil()
    {
        m_dicColor.Add(BlockWall.BlockColor.Red,Color.red);
        m_dicColor.Add(BlockWall.BlockColor.Yellow, Color.yellow);
    }
    static public void UpdateBlockWall(BlockWall bw,SpriteRenderer[,] sp)
	{
		for (int i = 0; i < bw.GetWidth();i++)
		{
			for (int j = 0; j < bw.GetHeight();j++)
			{
				BlockWall.BlockColor btColor;
				if (bw.GetBlockColor(i, j, out btColor))
				{
					sp[i, j].enabled = btColor != BlockWall.BlockColor.None;
                    Color color;
                    if(m_dicColor.TryGetValue(btColor, out color))
                    {
                        sp[i, j].color = color;
                    }
                    
				}
				else
					sp[i, j].enabled = false;
			}
		}
	}
}
