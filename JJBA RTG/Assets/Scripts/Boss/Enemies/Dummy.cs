using UnityEngine;

public sealed class Dummy : EnemyAI
{
    public float minDistance;
    [HideInInspector]public int mode = -1;

    public override void InputCycles()
    {
        base.InputCycles();

        switch (mode)
        {
            case 5: // Dodge
            case 4: // Run
            if (distance < minDistance) agent.SetDestination(transform.TransformDirection(transform.position) - Vector3.back);
            break;

            case 3: // Block
            Block(true);
            break;
            
            case 2: // Attack When in distance
            if (distance < minDistance) Atk();
            break;
            
            case 1: Movement(); //Follow
            break;
            
            case 0: //Attack and Chase
            if (distance < minDistance) Atk();
            else Movement();
            break;
        }
    }  

}
