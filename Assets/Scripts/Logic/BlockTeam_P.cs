using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTeam_P : BlockTeam
{
    static BlockTeam_P()
    {
        int[,] vPos = new int[4, 4]{
                {0,1,0,0},
                {0,1,1,0},
                {0,0,1,0},
                {0,0,0,0}};

        ms_RotPosData.Add(vPos);

        vPos = new int[4, 4]{
                {0,0,0,0},
                {0,0,1,1},
                {0,1,1,0},
                {0,0,0,0}};

        ms_RotPosData.Add(vPos);
    }
}
