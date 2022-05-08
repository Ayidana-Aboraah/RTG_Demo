using UnityEngine;

public sealed class TheCure : Melee
{
    public override void initialize()
    {
        base.initialize();
        //Set the block type to the absorbant one
        stats.blockType = 1;
    }

    public override void despawn()
    {
        stats.blockType = 0;
        base.despawn();
    }
}
