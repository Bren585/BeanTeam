using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] protected int damageAmount = 1;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float knockback = 10.0f;
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
            if (e.IsInvincible()) return;
            e.Damage(damageAmount);
            e.AddImpulse(direction * knockback);

            // ヒット音を鳴らす
            if (hitSound != null && audioSource != null)
            {
                audioSource.volume = 0.4f;
                audioSource.PlayOneShot(hitSound);
                Destroy(gameObject); // 音が終わるまで待つ
                return;
            }
        }

        // 音が無い場合はすぐに破棄
        Destroy(gameObject);
    }



    public void Initialize(Vector3 dir, float speed, int damage)
    {
        direction = dir.normalized;
        moveSpeed = speed;
        damageAmount = 1;
    }
}
