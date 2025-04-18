using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    protected override void Move() {
        Vector3 input;
        input.x = Input.GetAxis("Horizontal");
        input.y = 0;
        input.z = Input.GetAxis("Vertical");
        velocity += acceleration * Time.deltaTime * input;
    }
}
