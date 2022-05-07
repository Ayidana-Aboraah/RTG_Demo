using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public sealed class HermitPurple : Standx
{
    [Header("Hitboxes")]
    public Hitbox strongBox;
    public Hitbox heavyBox;
    public Hitbox atkSlBox;

    public Hitbox A2Box;
    public Hitbox A3Box;

    [Header("Special Specs")]
    public Timer visionTimer;
    public RenderObjects urp;

    public float hermitSwing;
    public float grappleRange;
    public float overdriveDuration;
    public float overdriveAmount;

    private bool grappling;
    private SpringJoint joint;
    
    private StandAttribute attributes;
    private Transform target;
    private Rigidbody rb;
    
    public override void Atk()
    {
        if (!grappling)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, grappleRange))
            {
                transform.parent.gameObject.AddComponent<SpringJoint>();
                joint = GetComponentInParent<SpringJoint>();

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = transform.position;

                float distanceFromPoint = Vector3.Distance(transform.parent.position, target.position);
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;

                //Adjust these values to fit your game.
                joint.spring = 4.5f;
                joint.damper = 7f;
                joint.massScale = 4.5f;
            }

            grappling = true;
        }
        else
        {
            Destroy(joint);
            grappling = false;
        }
    }

    public override void SpAtk()
    {
        urp.SetActive(true);
        visionTimer.Start();
    }

    public override void Strong()
    {
        strongBox.Effect(3, 3f);
        strongBox.Atk(stats.damageMultiplier);
    }

    public override void Heavy()
    {
        heavyBox.Atk(stats.damageMultiplier);
    }

    public override void A1()
    {
        rb.AddForce((rb.position - target.position) * -1 * hermitSwing * Time.deltaTime);
    }

    public void A1x2()
    {
        transform.parent.GetComponent<Animator>().SetBool("Traveling", true);
        rb.AddForce((rb.position - target.position) * hermitSwing * Time.deltaTime);
    }

    public override void A2()
    {
        A2Box.Atk(stats.damageMultiplier);
    }

    public override void A3()
    {
        A3Box.Atk(stats.damageMultiplier);
    }

    public void A4()
    {
        attributes.StartBuff(2,  overdriveDuration, overdriveAmount);
        attributes.StartBuff(3,  overdriveDuration, overdriveAmount);
    }

    public void AtkSL()
    {
        atkSlBox.Atk(stats.damageMultiplier);
    }

    public override void Ult()
    {
    }

    public override void DrawBoxes()
    {
        atkSlBox.DrawHitBox();
        strongBox.DrawHitBox();
        heavyBox.DrawHitBox();
        A2Box.DrawHitBox();
        A3Box.DrawHitBox();
    }

    public override void initialize()
    {
        base.initialize();
        attributes = GetComponentInParent<StandAttribute>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        visionTimer.UpdateTimer();
        if(visionTimer.complete)
            urp.SetActive(false);
    }
}