using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Close() { this.gameObject.SetActive(true); }
    public void Open() { this.gameObject.SetActive(false); }
}
