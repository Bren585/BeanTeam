using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Y : Enemy
{
    private enum State
    {
        Idle_Enter,
        Idle,
        Chase_Enter,
        Chase,
        Attack_Enter,
        Attack,
    }

    private State state;
    [SerializeField] private float attack_trigger_range = 3.0f;

    protected override void Start()
    {
        state = State.Idle_Enter;
        base.Start();
    }

    protected override void Move()
    {
        switch (state) {
            case State.Idle_Enter:
                animation_state = Animation.Idle;
                state = State.Idle;
                goto case State.Idle;
            case State.Idle:
                state = State.Chase_Enter;
                break;
            case State.Chase_Enter:
                animation_state = Animation.Walk;
                state = State.Chase;
                goto case State.Chase;
            case State.Chase:
                base.Move();
                if ((player.gameObject.transform.position - transform.position).magnitude < attack_trigger_range) { state = State.Attack_Enter; }
                break;
            case State.Attack_Enter:
                animation_state = Animation.Attack;
                state = State.Attack;
                goto case State.Attack;
            case State.Attack:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("aruki")) { state = State.Chase_Enter; }
                break;
        }

    }
}
