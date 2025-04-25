using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    [SerializeField]
    private bool active = false;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    void OnTrigagerEnter(Collider collision)
    {
        Debug.Log("Collision");
        if (!active) return;
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.Teleport(0, 2, 0);
        }
    }

    public void setActive(bool state = true) { active = state; }
}
