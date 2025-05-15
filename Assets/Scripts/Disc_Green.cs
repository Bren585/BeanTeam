using UnityEngine;

public class Disc_Green : Disc
{
    private float slowBulletSpeed = 10.0f;   // 弾速を遅く
    private int poweredBulletDamage = 30;    // ダメージを大きく

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        float originalSpeed = bulletSpeed;
        int originalDamage = bulletDamage;

        bulletSpeed = slowBulletSpeed;
        bulletDamage = poweredBulletDamage;

        base.Shoot(position, direction);

        bulletSpeed = originalSpeed;
        bulletDamage = originalDamage;
    }

    public override void PassiveEnter()
    {
        //Debug.Log("緑パッシブ");
        // ここにプレイヤーへの効果があれば追加
    }

    public override void PassiveExit()
    {
        //Debug.Log("緑パッシブ終わり");
        // Passiveの終了処理があれば追加
    }

    public override void Skill()
    {
        Player player = FindFirstObjectByType<Player>();
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            enemy.AddImpulse(player.transform.forward * player.Acceleration * Time.deltaTime * 100);
        }
        player.Unequip(player.IsEquipped<Disc_Green>());
        player.UpdateDiscVisuals();
        //Debug.Log("緑ディスクスキル");
        // スキル処理を後で追加する場合
    }
}
