
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
            
            if (discs[2] != null)
            {
                Debug.Log("Cキー：1⇄2 切り替え -> Slot " + equippedDisc);
                if (cussetSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(cussetSound);
                }
                // 通常の 1⇄2 切り替え
                discs[equippedDisc].PassiveExit();
                equippedDisc = (equippedDisc == 2) ? 1 : 2;
                discs[equippedDisc].PassiveEnter();

            }
            else if (discs[1] != null)
            {
                Debug.Log("Cキー：0⇄1 切り替え -> Slot " + equippedDisc);
                if (cussetSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(cussetSound);
                }

                discs[equippedDisc].PassiveExit();
                equippedDisc = (equippedDisc == 1) ? 0 : 1;
                discs[equippedDisc].PassiveEnter();

            }
            else
            {
                Debug.Log("Cキー：切り替えできるディスクがない");
                equippedDisc = 0;
            }
            Debug.Log("Now Using Slot " + equippedDisc);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            discs[equippedDisc].Skill();
        }
    }

    private void OnDestroy()
    {
        FindFirstObjectByType<GameBGMManager>().PlayDeathBGM();
        var gameOverUI = FindFirstObjectByType<GameOverUIController>();
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
        }
    }
    protected override void OnDeath()
    {
        GetComponent<ParticleSystem>().Play();
    }
    public void AddDisc(Disc newDisc)
    {
        int slot = (discs[1] == null) ? 1 :
                   (discs[2] == null) ? 2 : -1;

        if (slot == -1)
        {
            // 使ってないディスクを新しいディスクに入れ替え
            int unquippedSlot = (equippedDisc) == 1 ? 2 : 1;

            Unequip(unquippedSlot);
            slot = 2;
        }

        discs[slot] = newDisc;
        discs[equippedDisc].PassiveExit();
        equippedDisc = slot;
        newDisc.PassiveEnter();
        Debug.Log("Added new disc to slot " + slot);
    }
    public void Unequip(int slot)
    {
        if (slot < 1 || slot > 2) return;
        Debug.Log("Unequipping Slot " + slot);

        if (equippedDisc == slot) {
            discs[slot].PassiveExit();
            Debug.Log("Changing active slot (" + equippedDisc + ")");
            if (slot == 1)
            {
                if (discs[2] != null)
                {
                    Debug.Log("Moving disc in slot 2");
                    discs[1] = discs[2];
                    discs[2] = null;
                    equippedDisc = 1;
                    Debug.Log("Disc moved " + ((discs[1] == null) ? "unsucessfully" : "sucessfully"));
                }
                else
                {
                    Debug.Log("No disc in slot 2");
                    discs[1] = null;
                    equippedDisc = 0;
                }
            }
            else
            {
                Debug.Log("Deleting Slot 2");
                discs[2] = null;
                equippedDisc = 1;
            }
        } 
        else 
        {
            Debug.Log("Deleting inactive slot");
            discs[slot] = null;

            if (slot == 1 && equippedDisc == 2)
            {
                Debug.Log("Moving disc in slot 2");
                discs[1] = discs[2];
                discs[2] = null;
                equippedDisc = 1;
                Debug.Log("Disc moved " + ((discs[1] == null) ? "unsucessfully" : "sucessfully"));
            }
        }

        Debug.Log("Unequipped Slot " + slot);
    }

    public int IsEquipped<T>() where T : Disc
    {
        if (discs[1] == null) return 0;
        if (discs[1].gameObject.GetComponent<T>() != null) { return 1; }
        if (discs[2] == null) return 0;
        if (discs[2].gameObject.GetComponent<T>() != null) { return 2; }
        return 0;
    }

    private void TryAddDisc<T>() where T : Disc
    {
        if (IsEquipped<T>() != 0) return;
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
