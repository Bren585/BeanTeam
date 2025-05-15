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

    private Emitter emitter;
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

                Vector3 from    = transform.position;
                Vector3 to      = player.gameObject.transform.position;
                from.y          = 0f;
                to.y            = 0f;

                Vector3 dist = to - from;
                float angle = Vector3.SignedAngle(transform.forward, dist, Vector3.up);
                //Debug.Log(angle);

                if (
                    dist.magnitude < attack_trigger_range
                    && angle < 0.1f
                    && timer <= 0.0f 
                    ) { state = State.Attack_Enter; }
                break;

            case State.Attack_Enter:
                animation_state = Animation.Attack;
                state = State.Attack;
                timer = cooldown;
                //Debug.Log("Attack");
                break;

            case State.Attack:
                if (timer > 0.0f) break;
                Vector3 shoot_pos = transform.position;
                shoot_pos.y = 1.0f;
                emitter.Shoot(shoot_pos, transform.forward);
                state = State.Attack_End;
                break;

            case State.Attack_End:
                //Debug.Log("Attack_End");
                timer = cooldown;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("aruki")) { state = State.Chase_Enter; }
                break;
        }

    }
}
