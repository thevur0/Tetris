using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTeam_O : BlockTeam
{
    static BlockTeam_O()
    {
        int[,] vPos = new int[4, 4]{
                {0,0,0,0},
                {0,1,1,0},
                {0,1,1,0},
                {0,0,0,0}};

        ms_RotPosData.Add(vPos);
    }
}
