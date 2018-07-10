using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderUtil
{

	static public void UpdateBlockWall(BlockWall bw,SpriteRenderer[,] sp)
	{
		for (int i = 0; i < bw.GetWidth();i++)
		{
			for (int j = 0; j < bw.GetHeight();j++)
			{
				BlockWall.BlockColor color;
				if (bw.GetBlockColor(i, j, out color))
				{
					sp[i, j].enabled = color != BlockWall.BlockColor.None;
				}
				else
					sp[i, j].enabled = false;
			}
		}
	}
}
