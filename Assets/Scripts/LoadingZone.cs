using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    [SerializeField]
    private bool active = true;

    [SerializeField]
    private Direction direction;
    //private int destination_index;

    private Stage parent;

    // Start is called before the first frame update
    void Start() 
    {
        parent = GetComponentInParent<Stage>();
    }

    // Update is called once per frame
    void Update() {}

    void OnTriggerEnter(Collider collision)
    {
        if (!active) return;
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            parent.MoveStage(direction);
            //parent.SetLoadingZoneState(destination_index, false);
            //player.Teleport(parent.GetLoadingZone(destination_index));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        active = true;
    }

    public void SetActive(bool state = true) { active = state; }

    public void SetParent(Stage s) { parent = s; }
}
