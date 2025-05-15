using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trial : MonoBehaviour
{
    //[SerializeField] private GameObject enemy_prefab;

    //[SerializeField] private GameObject spawner_prefab;

    [SerializeField] private GameObject[] spawners;

    [SerializeField] private Vector2 range;

    private Prize prize;

    private enum state
    {   
        Idle,
        WaveStart,
        WaveIdle,
        WaveClear,
        Finished
    };
    private state status;

    private int wave;

    private Direction               player_entrance;
    private List<List<EnemyData>>   TrialData;

    void Start()
    {
        //enemy_prefab = Resources.Load("Prefabs/Enemy_PH") as GameObject;
        status = state.Idle;
        wave = 0;
    }

    void Update()
    {
        //Debug.Log(status);
        switch (status)
        {
            case state.Idle:
                wave = 0;
                return;
            case state.WaveStart:
                //Debug.Log("WaveBegins");
                GetComponentInParent<Stage>().CloseDoors();

                //Debug.Log("Wave " + wave + " starting");
                for (int enemy = 0; enemy < TrialData[wave].Count(); enemy++)
                {
                    //Debug.Log("Creating Enemy with data : " + TrialData[wave][enemy]);
                    Vector3 pos = TrialData[wave][enemy].spawn_postition;
                    pos.x *= range.x;
                    pos.z *= range.y;

                    Instantiate(
                        spawners[(int)TrialData[wave][enemy].type], 
                        pos, 
                        Quaternion.LookRotation(FindFirstObjectByType<Player>().transform.position)
                    ).GetComponent<Spawner>().Init(TrialData[wave][enemy], 1.0f);
                }
                status = state.WaveIdle;
                break;

            case state.WaveIdle:
                //Debug.Log("Idling with " + FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None).Count() + "Enemies remaining.");
                if (FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None).Count() <= 0)
                {
                    status = state.WaveClear; 
                    goto case state.WaveClear;
                }
                break;

            case state.WaveClear:
                //Debug.Log("Cleared");
                if (++wave >= TrialData.Count())
                {
                    status = state.Finished;
                    GetComponentInParent<Stage>().OpenDoors();
                    if (prize) prize.gameObject.SetActive(true);
                    goto case state.Finished;
                }
                status = state.WaveStart;
                break;

            case state.Finished:
                //Debug.Log("Finished");
                break;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (status == state.Idle)
        {
            if (TrialData == null) status = state.Finished;
            else status = state.WaveStart;
        }
    }

    public void LoadEnemies(Direction d, List<List<EnemyData>> data) { player_entrance = d; TrialData = data; }
    public void LoadPrize(DiscType disc) { 
        if (prize) prize.gameObject.SetActive(false);
        switch (disc)
        {
            default:
                prize = FindFirstObjectByType<Prize>(FindObjectsInactive.Include);
                break;
        }
        if (prize) prize.gameObject.SetActive(false);
    } 
    public void Enable() { status = state.Idle; }
    public void Disable() { status = state.Finished; }

    public void ForceClear()
    {
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            //enemy.Die();
            Destroy(enemy.gameObject);
        }
        foreach (Spawner spawner in FindObjectsByType<Spawner>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            Destroy(spawner.gameObject);
        }
        if (prize) prize.gameObject.SetActive(false);
    }
}
