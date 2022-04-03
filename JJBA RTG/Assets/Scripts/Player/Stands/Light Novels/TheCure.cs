using UnityEngine;

public class TheCure : Melee
{
    public override void initialize()
    {
        base.initialize();
        //Set the block type to the absorbant one
        stats.blockType = 0;
    }
}
