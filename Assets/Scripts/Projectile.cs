using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damageAmount = 10;
    public int DamageAmount => damageAmount;  

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var e = collision.gameObject.GetComponent<Enemy>();
        if (e != null) e.Damage(damageAmount);

        Destroy(gameObject);
    }
}
