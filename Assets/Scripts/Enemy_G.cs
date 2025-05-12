using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_G : Enemy
{
    private enum State
    {
        Idle_Enter,
        Idle,
        Chase_Enter,
        Chase,
        Attack_Enter,
        Attack,
        Attack_End
    }

    [SerializeField] private Emitter emitter;
    [SerializeField] private float cooldown = 1.0f;
    private float timer;

    private State state;
    [SerializeField] private float attack_trigger_range = 3.0f;

    protected override void Start()
    {
        state = State.Idle_Enter;
        emitter = GetComponent<Emitter>();
        base.Start();
    }

    protected override void Move()
    {
        if (timer > 0.0f) { timer -= Time.deltaTime; }

        switch (state)
        {
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
                if (
                    (player.gameObject.transform.position - transform.position).magnitude < attack_trigger_range
                    && Mathf.Abs(Mathf.Acos(Vector3.Dot(player.transform.position, transform.forward))) < 0.1f
                    && timer <= 0.0f 
                    ) { state = State.Attack_Enter; }
                break;
            case State.Attack_Enter:
                animation_state = Animation.Attack;
                timer = cooldown;
                state = State.Attack;
                break;
            case State.Attack:
                if (timer > 0.0f) break;
                emitter.Shoot(transform.position, transform.forward);
                state = State.Attack_End;
                break;
            case State.Attack_End:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("aruki")) { state = State.Chase_Enter; }
                break;
        }

    }
}
