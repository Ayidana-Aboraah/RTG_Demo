using UnityEngine;

[System.Serializable]
public class Hitbox
{
    public Vector3 angle;
    public float range;
    public float damage;
    public Transform point;
    public Transform parent;
    public LayerMask opponent;
    public Color boxColor;

    public void Atk()
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;

            if (other.GetComponent<Rigidbody>())
               other.GetComponent<Rigidbody>().AddRelativeForce(angle, ForceMode.Impulse);

            other.GetComponent<Stats>().TakeDamage(damage);
        }
    }

    public void Atk(float multiplier)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;

            if (other.GetComponent<Rigidbody>())
                other.GetComponent<Rigidbody>().AddRelativeForce(angle, ForceMode.Impulse);

            other.GetComponent<Stats>().TakeDamage(damage + multiplier);
        }
    }

    public bool AtkProjectile(float multiplier)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;

            if (other.GetComponent<Rigidbody>())
                other.GetComponent<Rigidbody>().AddRelativeForce(angle, ForceMode.Impulse);

            other.GetComponent<Stats>().TakeDamage(multiplier + damage);
            return true;
        }

        return false;
    }


    ///<Summary>
    /// Calls the attribtue system indirectly
    /// 0 is Time Stop
    /// 1 is Poison/bleed/freeze (Damage over time)
    /// 2 is Stun
    /// 3 is Tripping/Stun with animation
    /// 4 is visable/tracked
    ///</Summary>
    public void Effect(int AttributeType, float AttributeDuration)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;
            other.GetComponent<StandAttribute>().StartDebuff(AttributeType, AttributeDuration);
        }
    }

    ///<Summary>
    /// Calls the attribtue system indirectly
    /// 0 is Time Stop
    /// 1 is Poison/bleed/freeze (Damage over time)
    /// 2 is Stun
    /// 3 is Tripping/Stun with animation
    ///</Summary>
    public void Effect(int attributeType, float attributeDuration, float attributeDamage)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;
            other.GetComponent<StandAttribute>().StartDebuff(attributeType, attributeDuration, attributeDamage);
        }
    }

    public void DrawHitBox()
    {
        Gizmos.color = boxColor;
        Gizmos.DrawWireSphere(point.position, range);
    }
}