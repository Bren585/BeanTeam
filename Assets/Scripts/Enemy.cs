using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Red,
    Blue,
    Green,
    Yellow,

    TypeCount,
    Unset
}
public class EnemyData
{ 
    public EnemyType    type;
    public int          level;
    public Vector3      spawn_postition;

    public EnemyData(int level, Vector3 spawn_postition)
    {
        this.type               = EnemyType.Unset;
        this.level              = level;
        this.spawn_postition    = spawn_postition;
    }
}

public class Enemy : Entity
{
    protected   Player      player;

    protected enum Animation { 
        Idle,
        Walk,
        Attack,
        Death
    }

    protected Animation animation_state;

    protected override void Start()
    {
        player = FindFirstObjectByType<Player>();
        animation_state = Animation.Idle;
        base.Start();
    }

    override protected void Move()
    {
        if (player.IsAlive())
        {
            Vector3 distance = player.transform.position - transform.position;
            Vector3 direction = distance / distance.magnitude;
            body.velocity += direction * acceleration * Time.deltaTime;
        }
    }

    protected override void Animate()
    {
        animator.SetInteger("State", (int)animation_state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null) { 
            p.Damage(1); 
            //if (p.IsInvincible()) Damage(1);
        }
    }

    override protected void OnDeath() {
        animation_state = Animation.Death;
        animator.SetTrigger("Die");
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<ParticleSystem>().Play();
    }
}

