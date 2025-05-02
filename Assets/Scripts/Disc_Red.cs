using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc_Red : Disc
{
    private float boostedSpeed = 400.0f; // ブースト後の速度
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

        Debug.Log("Red Skill : " + boostedSpeed);
     
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
