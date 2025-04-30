using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Emitter emitter;

    [SerializeField]
    private Disc[] discs;
    private int equippedDisc = 0;

    override protected void Start()
    {
        emitter = GetComponent<Emitter>();
        base.Start();
    }

    protected override void Move()
    {
        Vector3 input;
        input.x = Input.GetAxis("Horizontal");
        input.y = 0;
        input.z = Input.GetAxis("Vertical");

        if (input.sqrMagnitude > 1f)
        {
            input = input.normalized;
        }

        body.velocity += acceleration * Time.deltaTime * input;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 pos = transform.position + transform.forward;
            Vector3 dir = transform.forward;
            if (discs.Length > equippedDisc && discs[equippedDisc] != null)
            {
                discs[equippedDisc].Shoot(pos, dir);
            }
            Debug.Log("Shoot attempted");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (discs.Length > equippedDisc && discs[equippedDisc] != null)
            {
                discs[equippedDisc].Skill();
            }
        }
    }
}
