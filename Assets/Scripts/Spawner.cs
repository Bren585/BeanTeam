using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy_prefab;

    private float timer;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            enemy.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void Init(EnemyData data, float timer = 1.0f)
    {
        transform.position += new Vector3(UnityEngine.Random.Range(1.0f, -1.0f), 0, UnityEngine.Random.Range(1.0f, -1.0f));  

        enemy = Instantiate(
            enemy_prefab, 
            transform.position, 
            transform.rotation
        ).GetComponent<Enemy>();

        enemy.gameObject.SetActive(false);

        this.timer = timer;
    }

}
