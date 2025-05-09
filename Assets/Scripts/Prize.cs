using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private float timer;

    [SerializeField] private DiscType disc;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += new Vector3(0, Mathf.Cos(timer * 3) * 0.01f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = GetComponent<Player>();
        if (player != null) {
            Debug.Log("Add Disc " + disc);
        }
        gameObject.SetActive(false);
    }
}
