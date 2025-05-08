using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc_Red : Disc
{
    private float boostedSpeed = 400.0f; // �u�[�X�g��̑��x
    private float originalSpeed;  // ���̑��x

    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        originalSpeed = player.Acceleration;  // ���̑��x��ۑ�
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

        Debug.Log("Red Skill : " + boostedSpeed);
     
    }
    public override void PassiveEnter()
    {
        if (player == null) return;

        player.Acceleration = boostedSpeed;  // �v���C���[�̉����x��ύX
        //isBoosted = true;  // ��Ԃ��u�u�[�X�g���v�ɐݒ�
        Debug.Log("�ԃu�[�X�g");
    }

    public override void PassiveExit()
    {
        if (player == null) return;

        player.Acceleration = originalSpeed;  // �I���W�i���̑��x�ɖ߂�
        //isBoosted = false;  // ��Ԃ��u�ʏ�v�ɐݒ�
        Debug.Log("�ԃu�[�X�ƏI��");
    }
}
