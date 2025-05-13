using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Disc_Red : Disc
{
    private float boostedSpeed = 120.0f; // �u�[�X�g��̑��x
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

        Vector3 position = player.transform.position;

        const float step = Mathf.PI / 8;
        for (float angle = 0; angle < Mathf.PI; angle += step)
        {
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            Shoot(position, new Vector3(x, 0, y));
            Shoot(position, new Vector3(-x, 0, -y));
        }

        Debug.Log("Red Skill");
     
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
