using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum Direction { North, East, South, West };

public class StageData {
    
    public enum StageType
    {
        None,
        Red,
        Green,
        Blue,
        Yellow,
        Empty,

        TypeCount
    }

    public bool cleared;
    public StageType type;

    public StageData Init(int level)
    {
        cleared = false;
        type = (StageType)UnityEngine.Random.Range(0, ((int)StageType.TypeCount));
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
    private Material material;
    private Trial trial;

    private Vector2Int coords;
    private Dictionary<Vector2Int, StageData> stages;
    private int stages_cleared;

    void Start()
    {
        loadingZones = GetComponentsInChildren<LoadingZone>();
        //foreach(LoadingZone LZ in loadingZones) { LZ.SetParent(this); }
        trial = GetComponentInChildren<Trial>();

        material = GetComponent<Renderer>().material;
        player = FindFirstObjectByType<Player>();

        stages = new Dictionary<Vector2Int, StageData>();
        coords = new Vector2Int(0, 0);
        stages.Add(coords, new StageData().InitStarter());
        SetStage(stages[coords]);

        stages_cleared = -1;
    }

    // Update is called once per frame
    void Update() {}

    public void SetStage(StageData stage)
    {
        switch (stage.type) 
        {
            case StageData.StageType.Red:
                material.color = new Color(1, 0, 0, 1);
                break;
            case StageData.StageType.Green:
                material.color = new Color(0, 1, 0, 1);
                break;
            case StageData.StageType.Blue:
                material.color = new Color(0, 0, 1, 1);
                break;
            case StageData.StageType.Yellow:
                material.color = new Color(1, 1, 0, 1);
                break;
            default:
                material.color = new Color(1, 1, 1, 1);
                break;
        }

        if (stage.cleared) { trial.Disable(); }
        else { trial.Enable(); }
    }

    //public Vector3 GetLoadingZone(int index) { return loadingZones[index].transform.position; }
    //public void SetLoadingZoneState(int index, bool active) { loadingZones[index].SetActive(active); }

    public void MoveStage(Direction direction)
    {
        trial.ForceClear();
        stages[coords].cleared = true;
        stages_cleared++;
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
        SetStage(stages[coords]);
    }
}
