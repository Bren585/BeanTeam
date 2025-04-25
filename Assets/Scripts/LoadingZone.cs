using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    [SerializeField]
    private bool active = false;

    [SerializeField]
    private int destination_index;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    void OnTriggerEnter(Collider collision)
    {
        if (!active) return;
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.Teleport(0, 1, 0);
        }
    }

    public void setActive(bool state = true) { active = state; }
}
