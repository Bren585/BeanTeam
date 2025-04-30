<<<<<<< Updated upstream
﻿using System.Collections;
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
    private float boostedSpeed = 500.0f; // �u�[�X�g��̑��x
    private bool isBoosted = false;  // �u�[�X�g��Ԃ��ǂ���
    private float originalSpeed;  // ���̑��x
>>>>>>> Stashed changes
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
<<<<<<< Updated upstream
        originalSpeed = player.Acceleration;
=======
        originalSpeed = player.Acceleration;  // ���̑��x��ۑ�
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

        player.Acceleration = boostedSpeed;  // �v���C���[�̉����x��ύX
        isBoosted = true;  // ��Ԃ��u�u�[�X�g���v�ɐݒ�
        Debug.Log("Boost Activated");
    }

    public override void PassiveExit()
    {
        if (player == null) return;

        player.Acceleration = originalSpeed;  // �I���W�i���̑��x�ɖ߂�
        isBoosted = false;  // ��Ԃ��u�ʏ�v�ɐݒ�
        Debug.Log("Boost Deactivated");
    }
}

>>>>>>> Stashed changes
