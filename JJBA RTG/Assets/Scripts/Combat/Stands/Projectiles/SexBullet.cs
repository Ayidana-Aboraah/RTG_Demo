using System.Collections;
using UnityEngine;

public class SexBullet : Projectile
{
    public int pistolType;
    public Transform target;
    Animator ani;

    internal override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
    }

    public override void Atk()
    {
        if (!box.AtkProjectile(damageMultiplier)) return;

        switch (pistolType)
        {
            case 2: //Barrage pistol
                StartCoroutine(Barrage());
                break;

            case 1: //Shield break pistol
                Collider[] plrs = Physics.OverlapSphere(box.point.position, box.range, box.opponent);
                foreach (Collider other in plrs)
                    if (other.transform != box.parent)
                        other.GetComponent<Stats>().ShieldBreak(); //Just get a object that would be responsible for shield break
                break;

            default:
                Destroy(gameObject);
                break;
        }
    }

    public override void Move()
    {
        if (target == null)
            base.Move();
        else
            rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, speed));
    }

    IEnumerator Barrage()
    {
        ani.enabled = true;
        ani.SetBool("Barrage", true);
        yield return new WaitUntil(() => !ani.GetBool("Barrage"));
        ani.enabled = false;
    }

    public void StopBarrage() => ani.SetBool("Barrage", false);
}
