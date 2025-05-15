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

            // �q�b�g����炷
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
                Destroy(gameObject, hitSound.length); // �����I���܂ő҂�
                return;
            }
        }

        // ���������ꍇ�͂����ɔj��
        Destroy(gameObject);
    }
}
