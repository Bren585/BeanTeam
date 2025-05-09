using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Entity : MonoBehaviour
{
    // ************ MEMBERS ********************************

    protected Animator animator;

    private Vector3 default_scale;

    // ************ PHYSICS 
    protected Rigidbody body;

    [SerializeField]
    protected float acceleration = 10f;
    [SerializeField]
    private float drag = 0.95f;
    [SerializeField]
    private float terminal_velocity = 100f;
    [SerializeField]
    protected float rotation_speed = 1.0f;

    // ************ STATUS 

    [SerializeField]
    private float despawn_timer;
    private float despawn_timer_max;

    private float invincible = 0;
    private bool alive;
    private int HP;

    [SerializeField]
    private int max_HP = 1;

    public float Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }

    // ************ FUNCTIONS ********************************
    virtual protected void Start()
    {
        HP = max_HP;
        alive = true;
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        despawn_timer_max = despawn_timer;
        default_scale = transform.localScale;
    }

    // ************ UPDATE 
    protected virtual void Move() { }
    protected virtual void Animate() { }

    void Update()
    {
        if (alive)
        {
            Move();
            if (invincible > 0) invincible -= Time.deltaTime;
        } 
        else
        {
            despawn_timer -= Time.deltaTime;
            float despawn_percent = despawn_timer / despawn_timer_max * 4;
            if (despawn_percent > 1) { despawn_percent = 1; }
            transform.localScale = new Vector3 (despawn_percent * default_scale.x, despawn_percent * default_scale.y, despawn_percent * default_scale.z);

            float delta_percent = Time.deltaTime * 2 / despawn_timer_max;
            transform.rotation *= Quaternion.AngleAxis(delta_percent * 90, new Vector3(-1, 0, 0));

            if (despawn_timer < 0)
            {
                Destroy(gameObject);
            }
        }
        Animate();

        body.velocity *= drag;

        if (body.velocity.magnitude > terminal_velocity)
        {
            body.velocity *= terminal_velocity / body.velocity.magnitude;
        }

        Vector3 moveDir = body.velocity.normalized;

        if (body.velocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotation_speed * Time.fixedDeltaTime);
        }
        else 
        {
            body.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    // ************ MOVEMENT
    
    public void Teleport(Vector3 to)
    {
        body.position = to;
        body.velocity = new Vector3(0, 0, 0);
    }

    public void Teleport(float x, float y, float z) { Teleport(new Vector3(x, y, z)); }

    public void AddImpulse(Vector3 impulse) { body.velocity += impulse; }

    // ************ DAMAGE 

    public void SetInvincible(float timer) { invincible = timer; }

    public bool IsInvincible() { return invincible > 0; }
    public void Damage(int damage)
    {
        if (invincible > 0) return;

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    protected virtual void OnDeath() { }
    public void Die()
    {
        alive = false;
        OnDeath();
    }
    public bool IsAlive() { return alive; }

}
