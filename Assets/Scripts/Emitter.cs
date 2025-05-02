using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] protected float bulletSpeed = 20f;

    [SerializeField] protected int bulletDamage = 10; 

    public virtual void Shoot(Vector3 position, Vector3 direction)
    {
        GameObject bullet = Instantiate(prefab, position, Quaternion.LookRotation(direction));

        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Initialize(direction, bulletSpeed, bulletDamage);
        }
    }
}
