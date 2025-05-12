using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] protected int damageAmount = 10;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] protected AudioClip hitSound;
    protected AudioSource audioSource;

    private Vector3 direction; 

    public int DamageAmount => damageAmount;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var e = collision.gameObject.GetComponent<Entity>();
        if (e != null)
        {
            e.Damage(damageAmount);

            // �q�b�g����炷
            if (hitSound != null && audioSource != null)
            {
                audioSource.volume = 0.4f;
                audioSource.PlayOneShot(hitSound);
                Destroy(gameObject); // �����I���܂ő҂�
                return;
            }
        }

        // ���������ꍇ�͂����ɔj��
        Destroy(gameObject);
    }



    public void Initialize(Vector3 dir, float speed, int damage)
    {
        direction = dir.normalized;
        moveSpeed = speed;
        damageAmount = damage;
    }
}
