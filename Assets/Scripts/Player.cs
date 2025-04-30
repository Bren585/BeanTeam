using UnityEngine;

public class Player : Entity
{
    private Disc[] discs;
    private int equippedDisc = 0;

    protected override void Start()
    {
        base.Start();
        discs = GetComponentsInChildren<Disc>();
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
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
           

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // ディスクを切り替える
            discs[equippedDisc].PassiveExit();
            equippedDisc = (equippedDisc + 1) % discs.Length;
            discs[equippedDisc].PassiveEnter();
            // 新しいディスクでスキルを発動
            if (discs.Length > equippedDisc && discs[equippedDisc] != null)
            {
                discs[equippedDisc].Skill();
            }
        }

    }
}
