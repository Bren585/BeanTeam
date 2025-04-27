using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialData
{
    public int wave_count;
    public EnemyData[,] enemies;
}
public class Trial : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy_prefab;
    private List<Enemy> enemies;
    private List<Enemy> dead_enemies;

    [SerializeField] private Vector2 range;

    private enum state
    {   
        Idle,
        WaveStart,
        WaveIdle,
        WaveClear,
        Finished
    };
    private state status;

    private int wave_count = 1;
    private int wave;

    private int enemy_count;

    void Start()
    {
        //enemy_prefab = Resources.Load("Prefabs/Enemy_PH") as GameObject;
        status = state.Idle;
        enemies = new List<Enemy>();
        dead_enemies = new List<Enemy>();
        wave = 0;
    }

    void Update()
    {
        //Debug.Log(status);
        switch (status)
        {
            case state.Idle:
                return;
            case state.WaveStart:
                enemy_count = UnityEngine.Random.Range(2, 4);
                //Debug.Log("Wave started with " + enemy_count + " enemies");
                for (int i = 0; i < enemy_count; i++)
                {
                    Vector3 pos = transform.position;
                    pos.x += UnityEngine.Random.Range(-range.x, range.x);
                    pos.y = 1.0f;
                    pos.z += UnityEngine.Random.Range(-range.y, range.y);
                    GameObject newEnemy = Instantiate(enemy_prefab, pos, Quaternion.identity);
                    enemies.Add(newEnemy.GetComponent<Enemy>());
                }
                status = state.WaveIdle;
                break;
                //goto case state.WaveIdle;
            case state.WaveIdle:
                foreach (Enemy enemy in enemies)
                {
                    if (!enemy.IsAlive())
                    {
                        dead_enemies.Add(enemy);
                    }
                }

                foreach (Enemy enemy in dead_enemies)
                {
                    enemies.Remove(enemy);
                }
                dead_enemies.Clear();

                if (enemies.Count == 0)
                {
                    status = state.WaveClear; 
                    goto case state.WaveClear;
                }
                break;
            case state.WaveClear:
                if (++wave >= wave_count)
                {
                    status = state.Finished;
                    Debug.Log("Victory");
                    goto case state.Finished;
                }
                status = state.WaveStart;
                break;
            case state.Finished:
                break;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (status == state.Idle)
        {
            status = state.WaveStart;
        }
    }

    public void Enable() { status = state.Idle; }
    public void Disable() { status = state.Finished; }

    public void ForceClear()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Die();
        }
        enemies.Clear();
    }
}
