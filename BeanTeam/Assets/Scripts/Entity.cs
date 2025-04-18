using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // ************ MEMBERS ********************************

    // ************ PHYSICS 
    private Rigidbody body;
    protected Vector3 velocity;

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
    protected virtual void Init() { }

    void Start()
    {
        HP = max_HP;
        alive = true;
        body = GetComponent<Rigidbody>();
        Init();
    }

    // ************ UPDATE 
    protected virtual void Move() { }

    void Update()
    {
        if (alive)
        {
            Move();
        }
        velocity *= drag;

        if (velocity.magnitude > terminal_velocity)
        {
            velocity *= terminal_velocity / velocity.magnitude;
        }

    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position += velocity * Time.deltaTime;
        body.MovePosition(position);
    }

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
    private void Die()
    {
        alive = false;
    }

    public bool IsAlive() { return alive; }
}
