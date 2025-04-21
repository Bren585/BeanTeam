using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected Player player;
    protected override void Init()
    {
        player = FindFirstObjectByType<Player>();
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

    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null) { p.Damage(1); }
    }
}
