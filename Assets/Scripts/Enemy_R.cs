using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_R : Enemy
{
    protected override void Move()
    {
        base.Move();
        animation_state = Animation.Walk;
    }
}
