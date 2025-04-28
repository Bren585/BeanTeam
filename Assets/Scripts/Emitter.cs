using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField] private GameObject prefab;    
    [SerializeField] private float bulletSpeed = 20f;

    public void Shoot(Vector3 position, Vector3 direction)
    {
        //Debug.Log("Shoot(): " + position + " Å® " + direction);

        GameObject bullet = Instantiate(prefab, position, Quaternion.LookRotation(direction));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }
}
