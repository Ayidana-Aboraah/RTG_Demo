using UnityEngine;
public class Killplane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => other.GetComponent<Stats>().Die();
}
