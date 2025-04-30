using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float moveSpeed = 20f; 

    private Vector3 direction; 

    public int DamageAmount => damageAmount;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var e = collision.gameObject.GetComponent<Enemy>();
        if (e != null) e.Damage(damageAmount);

        Destroy(gameObject);
    }

    public void Initialize(Vector3 dir, float speed, int damage)
    {
        direction = dir.normalized;
        moveSpeed = speed;
        damageAmount = damage;
    }
}
