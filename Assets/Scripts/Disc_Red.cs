<<<<<<< Updated upstream
ï»¿using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> Stashed changes
using UnityEngine;

public class Disc_Red : Disc
{
<<<<<<< Updated upstream
    private float boostedSpeed = 500.0f;
    private bool isBoosted = false;
    private float originalSpeed;
=======
    private float boostedSpeed = 500.0f; // ƒu[ƒXƒgŒã‚Ì‘¬“x
    private bool isBoosted = false;  // ƒu[ƒXƒgó‘Ô‚©‚Ç‚¤‚©
    private float originalSpeed;  // Œ³‚Ì‘¬“x
>>>>>>> Stashed changes
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
<<<<<<< Updated upstream
        originalSpeed = player.Acceleration;
=======
        originalSpeed = player.Acceleration;  // Œ³‚Ì‘¬“x‚ð•Û‘¶
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
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
=======
        Debug.Log("Red Skill : " + boostedSpeed);
     
    }
    public override void PassiveEnter()
    {
        if (player == null) return;

        player.Acceleration = boostedSpeed;  // ƒvƒŒƒCƒ„[‚Ì‰Á‘¬“x‚ð•ÏX
        isBoosted = true;  // ó‘Ô‚ðuƒu[ƒXƒg’†v‚ÉÝ’è
        Debug.Log("Boost Activated");
    }

    public override void PassiveExit()
    {
        if (player == null) return;

        player.Acceleration = originalSpeed;  // ƒIƒŠƒWƒiƒ‹‚Ì‘¬“x‚É–ß‚·
        isBoosted = false;  // ó‘Ô‚ðu’Êív‚ÉÝ’è
        Debug.Log("Boost Deactivated");
    }
}

>>>>>>> Stashed changes
