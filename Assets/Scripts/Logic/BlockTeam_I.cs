using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTeam_I : BlockTeam
{
    static BlockTeam_I()
    {
        int[,] vPos = new int[4,4]{
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,0}};

        ms_RotPos.Add(vPos);

        vPos = new int[4, 4]{
                {0,0,0,0},
                {0,0,0,0},
                {1,1,1,1},
                {0,0,0,0}};

        ms_RotPos.Add(vPos);
    }
}
