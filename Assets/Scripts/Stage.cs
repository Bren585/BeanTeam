using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private LoadingZone[] loadingZones;

    private enum state
    {

    };

    void Start()
    {
        loadingZones = GetComponentsInChildren<LoadingZone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
