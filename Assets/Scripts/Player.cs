
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum DiscType
{
    Red,
    Green,
    Blue,
    Yellow,
    Purple,

    Count
}

public class Player : Entity
{
    //private List<Disc> discs = new List<Disc>();
    private Disc[] discs;
    private int equippedDisc = 0;

    protected override void Start()
    {  
        base.Start();
        discs = new Disc[3];
        discs[0] = GetComponentInChildren<Disc_Purple>();
        equippedDisc = 0;
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
        if (Input.GetKeyDown(KeyCode.R)) TryAddDisc<Disc_Red>();
        if (Input.GetKeyDown(KeyCode.B)) TryAddDisc<Disc_Blue>();
        if (Input.GetKeyDown(KeyCode.G)) TryAddDisc<Disc_Green>();
        if (Input.GetKeyDown(KeyCode.Y)) TryAddDisc<Disc_Yellow>();
        if (Input.GetKeyDown(KeyCode.P)) TryAddDisc<Disc_Purple>();

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (discs[1] != null && discs[2] != null)
            {
                // 通常の 1⇄2 切り替え
                if (equippedDisc != 1 && equippedDisc != 2)
                {
                    equippedDisc = 1;
                    discs[equippedDisc].PassiveEnter();
                }
                else
                {
                    discs[equippedDisc].PassiveExit();
                    equippedDisc = (equippedDisc == 1) ? 2 : 1;
                    discs[equippedDisc].PassiveEnter();
                }

                Debug.Log("Cキー：1⇄2 切り替え -> Slot " + equippedDisc);
            }
            else if (discs[1] != null ^ discs[2] != null) // どちらか1つだけある
            {
                int other = (discs[1] != null) ? 1 : 2;

                if (equippedDisc == 0)
                {
                    equippedDisc = other;
                    discs[equippedDisc].PassiveEnter();
                }
                else if (equippedDisc == other)
                {
                    discs[equippedDisc].PassiveExit();
                    equippedDisc = 0;
                }

                Debug.Log("Cキー：0⇄" + other + " 切り替え -> Slot " + equippedDisc);
            }
            else
            {
                Debug.Log("Cキー：切り替えできるディスクがない");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            discs[equippedDisc].Skill();
        } 


    }
    public void AddDisc(Disc newDisc)
    {
        int slot = (discs[1] == null) ? 1 :
                   (discs[2] == null) ? 2 : -1;

        if (slot == -1)
        {
            // discs[1]を削除し、discs[2]を左にスライド
            discs[1].PassiveExit();
            Destroy(discs[1].gameObject);
            discs[1] = discs[2];
            discs[2] = null;
            slot = 2;
        }

        discs[slot] = newDisc;
        equippedDisc = slot;
        newDisc.PassiveEnter();
    }

    private void TryAddDisc<T>() where T : Disc
    {
        T foundDisc = Object.FindFirstObjectByType<T>();
        if (foundDisc != null)
            AddDisc(foundDisc);
    }

    public void AddDiscType(DiscType type)
    {
        switch (type)
        {
            case DiscType.Red:
                TryAddDisc<Disc_Red>();
                break;
            case DiscType.Green:
                TryAddDisc<Disc_Green>();
                break;
            case DiscType.Blue:
                TryAddDisc<Disc_Blue>();
                break;
            case DiscType.Yellow:
                TryAddDisc<Disc_Yellow>();
                break;
            default:
                break;
        }
    }

}
