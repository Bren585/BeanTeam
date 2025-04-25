using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Emitter emitter;

    protected override void Move()
    {
        Vector3 input;
        input.x = Input.GetAxis("Horizontal");
        input.y = 0;
        input.z = Input.GetAxis("Vertical");

        body.velocity += acceleration * Time.deltaTime * input;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 pos = transform.position + transform.forward;
            Vector3 dir = transform.forward;
            emitter.Shoot(pos, dir);
        }
    }




}
