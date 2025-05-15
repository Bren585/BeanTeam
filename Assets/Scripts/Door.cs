using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;

    private bool open;
    private Vector3 pos;
    [SerializeField] private float open_y = -5.1f;
    [SerializeField] private float close_y = 1.5f;

    private void Start()
    {
        pos = transform.position;
    }
    private void Update()
    {
        if (open)
        {
            if (pos.y > open_y) { pos.y -= speed; }
        }
        else
        {
            if (pos.y < close_y) { pos.y += speed; }
        }
        transform.position = pos;
    }
    public void Close() {
        open = false; 
    }
    public void SetClose()
    {
        open = false;
        pos.y = 0.15f;
    }
    public void Open() {
        open = true;
    }
    public void SetOpen()
    {
        open = true;
        pos.y = -0.51f;
    }
}
