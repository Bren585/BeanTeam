using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private LoadingZone[] loadingZones;

    private enum state
    {
        Open,
        Closed
    };

    void Start()
    {
        loadingZones = GetComponentsInChildren<LoadingZone>();
        Debug.Log(loadingZones);
        foreach(LoadingZone LZ in loadingZones) { LZ.SetParent(this); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetLoadingZone(int index) { return loadingZones[index].transform.position; }
    public void SetLoadingZoneState(int index, bool active) { loadingZones[index].SetActive(active); }
}
