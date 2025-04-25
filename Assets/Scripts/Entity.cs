using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Die()
    {
        alive = false;
        OnDeath();
    }

    public bool IsAlive() { return alive; }
}
