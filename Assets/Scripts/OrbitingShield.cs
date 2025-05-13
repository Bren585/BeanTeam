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

            // ƒqƒbƒg‰¹‚ğ–Â‚ç‚·
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
                Destroy(gameObject, hitSound.length); // ‰¹‚ªI‚í‚é‚Ü‚Å‘Ò‚Â
                return;
            }
        }

        // ‰¹‚ª–³‚¢ê‡‚Í‚·‚®‚É”jŠü
        Destroy(gameObject);
    }
}
