using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc_Red : Disc
{
    private float boostedSpeed = 500.0f;
    private bool isBoosted = false;
    private float originalSpeed;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        originalSpeed = player.Acceleration;
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        float originalBulletSpeed = bulletSpeed;
        int originalBulletDamage = bulletDamage;

        bulletSpeed *= 1.5f;
        bulletDamage = (int)(bulletDamage * 0.8f);

        base.Shoot(position, direction);

        bulletSpeed = originalBulletSpeed;
        bulletDamage = originalBulletDamage;
    }

    public override void Skill()
    {
        if (player == null) return;

        if (!isBoosted)
        {
            PassiveEnter(); 
        }
        else
        {
            PassiveExit();   
        }
    }

    public void PassiveEnter()
    {
        if (player == null) return;

        player.Acceleration = boostedSpeed; 
        isBoosted = true;  
        Debug.Log("Boost Activated");
    }

    public void PassiveExit()
    {
        if (player == null) return;

        player.Acceleration = originalSpeed;  
        isBoosted = false;  
        Debug.Log("Boost Deactivated");
    }
}