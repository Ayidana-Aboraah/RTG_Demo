using UnityEngine;

public class DeathMenu : Menu
{
    public Transform spawnPoint;
    public override void Start() {}

    public void Restart(Transform caller)
    {
        Evaluate();
        
        caller.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        caller.GetComponent<Player>().Respawn();
    }
}
