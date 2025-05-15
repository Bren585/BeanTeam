using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum Direction { North, East, South, West };
public enum SpawnPattern 
{ 
    Rows, 
    Arc, 
    Corners,
    Center,

    Plus,
    Cross,
    Border,
    Random,

    PatternCount,
    WaveOneCount = 4
}

public enum StageType
{
    Red,
    Green,
    Blue,
    Yellow,
    Mixed,
    Empty,

    TypeCount
}

public class StageData {

    public bool cleared;
    public StageType type;
    public List<List<EnemyData>> TrialData;
    
    private void MakeEnemies(int level)
    {
        TrialData = new List<List<EnemyData>>();

        int wave_count = 1 + (level / 5);
        //Debug.Log("Creating " + wave_count + " waves...");

        for (int wave = 0; wave < wave_count; ++wave)
        {
            TrialData.Add(new List<EnemyData>());
            int y = (int)Math.Log(level + 1);
            //Debug.Log("Difficulty Value : " + y);
            int enemy_count = 4 + y;
            //Debug.Log("Enemies : " + enemy_count);
            SpawnPattern pattern;
            if (wave == 0)  { pattern = (SpawnPattern)UnityEngine.Random.Range(0, (int)SpawnPattern.WaveOneCount); }
            else            { pattern = (SpawnPattern)UnityEngine.Random.Range(0, (int)SpawnPattern.PatternCount); }
            //pattern = SpawnPattern.Plus;
            //Debug.Log("Creating " + pattern + " pattern...");
            switch (pattern)
            {
                case SpawnPattern.Rows:
                    enemy_count += enemy_count % 2;
                    int column_count = enemy_count / 2;
                    for (int column = 0; column < column_count; column++)
                    {
                        float x = (float)column / (column_count - 1);
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+1.0f, 0, 1.0f - 2.0f * x)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-1.0f, 0, 1.0f - 2.0f * x)));
                    }
                    break;
                case SpawnPattern.Arc:
                    for (int enemy = 0; enemy < enemy_count; enemy++)
                    {
                        float x = (float)enemy / (enemy_count - 1);
                        TrialData[wave].Add(new EnemyData(level, new Vector3((float)Math.Cos(Math.PI * x), 0, (float)Math.Sin(Math.PI * x))));
                    }
                    break;
                case SpawnPattern.Corners:
                    enemy_count += 4 - enemy_count % 4;
                    int group_size = enemy_count / 4;
                    for (int enemy = 0; enemy < group_size; enemy++)
                    {
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+1, 0, +1)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+1, 0, -1)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-1, 0, +1)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-1, 0, -1)));
                    }
                    break;
                case SpawnPattern.Center:
                    for (int enemy = 0; enemy < enemy_count; enemy++)
                    {
                        TrialData[wave].Add(new EnemyData(level, new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0, UnityEngine.Random.Range(-0.3f, 0.3f))));
                    }
                    break;
                case SpawnPattern.Plus:
                    enemy_count += 4 - enemy_count % 4;
                    //Debug.Log("making plus with " + enemy_count + " enemies");
                    int plus_count = enemy_count / 4;
                    for (int enemy = 0; enemy < plus_count; enemy++)
                    {
                        //Debug.Log("division by " + (plus_count - 1));
                        float x = (float)(enemy + 1) / plus_count;
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+x, 0,  0)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-x, 0,  0)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3( 0, 0, +x)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3( 0, 0, -x)));
                    }
                    break;
                case SpawnPattern.Cross:
                    enemy_count += 4 - enemy_count % 4;
                    //Debug.Log("making cross with " + enemy_count + " enemies");
                    int cross_count = enemy_count / 4;
                    for (int enemy = 0; enemy < cross_count; enemy++)
                    {
                        //Debug.Log("division by " + (cross_count - 1));
                        float x = (float)(enemy + 1) / cross_count;
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+x, 0, +x)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-x, 0, +x)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+x, 0, -x)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-x, 0, -x)));
                    }
                    break;
                case SpawnPattern.Border:
                    enemy_count += 4 - enemy_count % 4;
                    int border_count = enemy_count / 4;
                    for (int enemy = 0; enemy < border_count; enemy++)
                    {
                        float x = (float)enemy / border_count;
                        TrialData[wave].Add(new EnemyData(level, new Vector3(+1, 0, 1 - 2 * x)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(-1, 0, 2 * x - 1)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(1 - 2 * x, 0, -1)));
                        TrialData[wave].Add(new EnemyData(level, new Vector3(2 * x - 1, 0, +1)));
                    }
                    break;
                case SpawnPattern.Random:
                    for (int enemy = 0; enemy < enemy_count; enemy++)
                    {
                        TrialData[wave].Add(new EnemyData(level, new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f))));
                    }
                    break;
            }
            //Debug.Log(enemy_count + " enemies created.");
        }

    }

    public StageData Init(int level)
    {
        cleared = false;
        float rng = UnityEngine.Random.Range(0.0f, 1.0f);

        if      (rng < 0.2f) { type = StageType.Red;    }
        else if (rng < 0.4f) { type = StageType.Green;  }
        else if (rng < 0.6f) { type = StageType.Blue;   }
        else if (rng < 0.8f) { type = StageType.Yellow; }
        else if (rng < 0.9f) { type = StageType.Mixed;  }
        else                 { type = StageType.Empty;  }

        switch (type)
        {
            case StageType.Empty:
                cleared = true;
                break;
            case StageType.Mixed:
            case StageType.Red:
            case StageType.Green:
            case StageType.Blue:
            case StageType.Yellow:
                MakeEnemies(level);
                foreach (List<EnemyData> wave in TrialData)
                {
                    foreach (EnemyData enemy in wave)
                    {
                        if (type == StageType.Mixed)
                        {
                            enemy.type = (EnemyType)UnityEngine.Random.Range(0, (int)EnemyType.TypeCount);
                        }
                        else
                        {
                            enemy.type = (EnemyType)type;
                        }
                    }
                }
                break;
        }
        return this;
    }

    public StageData InitStarter()
    {
        cleared = true;
        type = StageType.Empty;
        return this;
    }
}
public class Stage : MonoBehaviour
{
    private Player player;

    private LoadingZone[] loadingZones;
    private Trial trial;

    private Vector2Int coords;
    private Dictionary<Vector2Int, StageData> stages;
    private int stages_cleared;

    [SerializeField] Material StageRed;
    [SerializeField] Material StageGreen;
    [SerializeField] Material StageBlue;
    [SerializeField] Material StageYellow;
    [SerializeField] Material StageMixed;

    public int getClearCount() { return stages_cleared; }

    void Start()
    {
        loadingZones = GetComponentsInChildren<LoadingZone>();
        //foreach(LoadingZone LZ in loadingZones) { LZ.SetParent(this); }
        trial = GetComponentInChildren<Trial>();

        player = FindFirstObjectByType<Player>();

        stages = new Dictionary<Vector2Int, StageData>();
        coords = new Vector2Int(0, 0);
        stages.Add(coords, new StageData().InitStarter());
        SetStage(stages[coords], Direction.North);

        stages_cleared = 0;

        SetOpenDoors();
    }

    // Update is called once per frame
    void Update() {}

    private void SetMaterial(Material mat)
    {
        //int count = 0;
        //Debug.Log(mat.name);
        GameObject[] objs =  GameObject.FindGameObjectsWithTag("MaterialHolder");
        if (objs == null) return;
        foreach (GameObject obj in objs)
        {
            Renderer r = obj.GetComponent<Renderer>();
            if (r != null)
            {
                r.material = mat;
                //count++;
            }
        }
        //Debug.Log("Updated " + count + " mats");
    }

    public void SetStage(StageData stage, Direction entrance)
    {
        switch (stage.type) 
        {
            case StageType.Red:
                SetMaterial(StageRed);
                //Debug.Log("Red Stage");
                break;
            case StageType.Green:
                SetMaterial(StageGreen);
                //Debug.Log("Green Stage");
                break;
            case StageType.Blue:
                SetMaterial(StageBlue);
                //Debug.Log("Blue Stage");
                break;
            case StageType.Yellow:
                SetMaterial(StageYellow);
                //Debug.Log("Yellow Stage");
                break;
            default:
                SetMaterial(StageMixed);
                //Debug.Log("Mixed Stage");
                break;
        }

        if (stage.cleared) { trial.Disable(); }
        else 
        { 
            trial.Enable(); 
            trial.LoadEnemies(entrance, stage.TrialData); 
            if (stage.type != StageType.Mixed) trial.LoadPrize((DiscType)stage.type);
            else
            {
                trial.LoadPrize((DiscType)UnityEngine.Random.Range(1, (int)DiscType.Count));
            }
        }
    }

    //public Vector3 GetLoadingZone(int index) { return loadingZones[index].transform.position; }
    //public void SetLoadingZoneState(int index, bool active) { loadingZones[index].SetActive(active); }
    public void SetOpenDoors() { foreach (Door door in GetComponentsInChildren<Door>(true)) { door.SetOpen(); } }
    public void OpenDoors() { foreach(Door door in GetComponentsInChildren<Door>(true)) { door.Open(); } }
    
    public void CloseDoors() { foreach (Door door in GetComponentsInChildren<Door>(true)) { door.Close(); } }
 
    public void MoveStage(Direction direction)
    {
        trial.ForceClear();
        stages[coords].cleared = true;
        if (stages[coords].type != StageType.Empty) { stages_cleared++; }
        switch (direction)
        {
            case Direction.North:
                coords.y += 1;
                loadingZones[(int)Direction.South].SetActive(false);
                player.Teleport(loadingZones[(int)Direction.South].transform.position);
                break;
            case Direction.East:
                coords.x += 1;
                loadingZones[(int)Direction.West].SetActive(false);
                player.Teleport(loadingZones[(int)Direction.West].transform.position);
                break;
            case Direction.South:
                coords.y -= 1;
                loadingZones[(int)Direction.North].SetActive(false);
                player.Teleport(loadingZones[(int)Direction.North].transform.position);
                break;
            case Direction.West:
                coords.x -= 1;
                loadingZones[(int)Direction.East].SetActive(false);
                player.Teleport(loadingZones[(int)Direction.East].transform.position);
                break;
        }
        if (!stages.ContainsKey(coords)) { stages.Add(coords, new StageData().Init(stages_cleared)); }
        SetStage(stages[coords], direction);
    }
}
