using UnityEngine;

public class Disc_Yellow : Disc
{
    [SerializeField]
    private int numberOfShots = 5;  
    [SerializeField]
    private float spreadAngle = 15f;  

    [SerializeField]
    private float damageMultiplier = 2f;  

    public override void Skill()
    {
        Player player = FindFirstObjectByType<Player>();
        player.AddImpulse(player.transform.forward * player.Acceleration * Time.deltaTime * 200);
        player.SetInvincible(1.0f);
        //Debug.Log("黄色スキル");
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        // 弾の威力を強化
        int originalBulletDamage = bulletDamage;
        bulletDamage = (int)(bulletDamage * damageMultiplier);

        // 発射する弾を複数回、異なる方向に発射
        for (int i = 0; i < numberOfShots; i++)
        {
            // 各弾の方向を計算（ランダムにスプレッドさせる）
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 shotDirection = rotation * direction;  // 回転を加えた方向

            // 弾を発射
            base.Shoot(position, shotDirection);
        }

        // 元のダメージに戻す
        bulletDamage = originalBulletDamage;
    }
}
