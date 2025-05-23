using UnityEngine;
using UnityEngine.Audio;

public class OrbitingShield : Projectile
{
    private void OnCollisionEnter(Collision collision)
    {
        var e = collision.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            e.Damage(damageAmount);

            // ヒット音を鳴らす
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
                Destroy(gameObject, hitSound.length); // 音が終わるまで待つ
                return;
            }
        }

        // 音が無い場合はすぐに破棄
        Destroy(gameObject);
    }
}
