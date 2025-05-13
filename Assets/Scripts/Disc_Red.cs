using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Disc_Red : Disc
{
    private float boostedSpeed = 120.0f; // ブースト後の速度
    private float originalSpeed;  // 元の速度

    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        originalSpeed = player.Acceleration;  // 元の速度を保存
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

        player.Acceleration = boostedSpeed;  // プレイヤーの加速度を変更
        //isBoosted = true;  // 状態を「ブースト中」に設定
        Debug.Log("赤ブースト");
    }

    public override void PassiveExit()
    {
        if (player == null) return;

        player.Acceleration = originalSpeed;  // オリジナルの速度に戻す
        //isBoosted = false;  // 状態を「通常」に設定
        Debug.Log("赤ブースと終了");
    }
}
