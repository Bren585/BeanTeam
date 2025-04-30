using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Entity : MonoBehaviour
{
    // ************ MEMBERS ********************************

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

    private bool alive;
    private int HP;

    [SerializeField]
    private int max_HP = 1;


    // ************ FUNCTIONS ********************************
    virtual protected void Start()
    {
        HP = max_HP;
        alive = true;
        body = GetComponent<Rigidbody>();
    }

    // ************ UPDATE 
    protected virtual void Move() { }

    void Update()
    {
        if (alive)
        {
            Move();
        }

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

    // ************ DAMAGE 
    public void Damage(int damage)
    {
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
    public float Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }
}
