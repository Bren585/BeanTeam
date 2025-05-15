using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private float timer;

    private DiscType disc;

    [SerializeField] private Renderer material_holder;

    [SerializeField] private Material[] materials;

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
        Player player = FindFirstObjectByType<Player>();
        if (player != null) {
            player.AddDiscType(disc);
        }
        gameObject.SetActive(false);
    }

    public void SetPrize(DiscType type)
    {
        material_holder.material = materials[(int)type];
        disc = type;
    }
}
