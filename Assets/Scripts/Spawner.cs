using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy_prefab;
    [SerializeField] private float spawn_height = 0.2f;

    private float   timer;
    private bool    active;
    private Enemy   enemy;

    void Start() { }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (!active)
            { 
                enemy.gameObject.SetActive(true);
                active = true;
            }
            if (timer < -0.5) Destroy(gameObject);
        }
    }

    public void Init(EnemyData data, float timer = 1.0f)
    {
        transform.position += new Vector3(Random.Range(1.0f, -1.0f), spawn_height, Random.Range(1.0f, -1.0f));  

        enemy = Instantiate(
            enemy_prefab, 
            transform.position, 
            transform.rotation
        ).GetComponent<Enemy>();

        enemy.gameObject.SetActive(false);
        active = false;

        this.timer = timer;
    }

}
