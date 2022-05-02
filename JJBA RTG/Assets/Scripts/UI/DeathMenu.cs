using UnityEngine;

public class DeathMenu : Menu
{
    public Transform spawnPoint;
    public void Restart(Transform caller)
    {
        Evaluate();
        
        caller.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        caller.GetComponent<Player>().Respawn();
    }
}
