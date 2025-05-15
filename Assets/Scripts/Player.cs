
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
    [SerializeField] private DiscSwitcher discSwitcher;
    [SerializeField] private Material redMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material yellowMat;
    [SerializeField] private Material purpleMat;
    [SerializeField] private Material none;
    private Disc[] discs;
    private int equippedDisc = 0;

    protected override void Start()
    {  
        base.Start();
        discs = new Disc[3];
        discs[0] = GetComponentInChildren<Disc_Purple>();
        equippedDisc = 0;
        discSwitcher.SetMaterials(purpleMat, none); 
        discSwitcher.SwitchToFront(true);  // 紫が前に来るように設定
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

                Debug.Log("Cキー：1⇄2 切り替え -> Slot " + equippedDisc);
                UpdateDiscVisuals();

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

                Debug.Log("Cキー：0⇄" + other + " 切り替え -> Slot " + equippedDisc);
                UpdateDiscVisuals();

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
        var bgm = FindFirstObjectByType<GameBGMManager>();
        if (bgm != null) bgm.PlayDeathBGM();
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
    //public void AddDisc(Disc newDisc)
    //{
    //    int slot = (discs[1] == null) ? 1 :
    //               (discs[2] == null) ? 2 : -1;

    //    if (slot == -1)
    //    {
    //        // discs[1] を削除、discs[2] を左にスライド
    //        if (discs[1] != null)
    //        {
    //            discs[1].PassiveExit();
    //            Destroy(discs[1].gameObject);
    //        }
    //        discs[1] = discs[2];
    //        discs[2] = null;
    //        slot = 2;
    //    }

    //    discs[slot] = newDisc;
    //    equippedDisc = slot;
    //    newDisc.PassiveEnter();

    //    UpdateDiscVisuals();

    //    // ディスクが2つ揃ったときにDiscSwitcherにセット
    //    if (discs[1] != null && discs[2] != null)
    //    {
    //        int front = equippedDisc;
    //        int back = (front == 1) ? 2 : 1;

    //        //if (discSwitcher != null)
    //       // {
    //            discSwitcher.SetMaterials(discs[front].discMaterial, discs[back].discMaterial);
    //            discSwitcher.SwitchToFront(true, true); 
    //       // }
    //    }
    //}
    //public void AddDisc(Disc newDisc)
    //{
    //    int slot = (discs[1] == null) ? 1 :
    //               (discs[2] == null) ? 2 : -1;

    //    if (slot == -1)
    //    {
    //        if (discs[1] != null)
    //        {
    //            discs[1].PassiveExit();
    //            Destroy(discs[1].gameObject);
    //        }
    //        discs[1] = discs[2];
    //        discs[2] = null;
    //        slot = 2;
    //    }

    //    discs[slot] = newDisc;
    //    equippedDisc = slot;
    //    newDisc.PassiveEnter();

    //    if (discs[0] != null && slot == 1)
    //    {
    //        discSwitcher.SetMaterials(discs[0].discMaterial, discs[1].discMaterial);
    //        discSwitcher.SwitchToFront(true, true); // 紫を front に
    //        equippedDisc = 0; // 紫に戻す（見た目と一致）
    //        return;
    //    }
    //    // 新たに2枚の組が揃ったら DiscSwitcher を更新
    //    int front = equippedDisc;
    //    int back = GetBackDiscIndex();

    //    if (front != -1 && back != -1 &&
    //        discs[front] != null && discs[back] != null)
    //    {
    //        discSwitcher.SetMaterials(discs[front].discMaterial, discs[back].discMaterial);
    //        discSwitcher.SwitchToFront(front == 1 || front == 2, true); // 1か2なら isADiscFront
    //    }


    //}

    public void AddDisc(Disc newDisc)
    {
        int slot = (discs[1] == null) ? 1 :
                   (discs[2] == null) ? 2 : -1;

        // 3つ目のディスクを入れる場合、古いものを押し出す
        if (slot == -1)
        {

            //if (discs[1] != null)
            //{
            //    discs[1].PassiveExit();
            //    Destroy(discs[1].gameObject);
            //}
            //discs[1] = discs[2];
            //discs[2] = null;

            // 使ってないディスクを新しいディスクに入れ替え
            int unquippedSlot = (equippedDisc) == 1 ? 2 : 1;

            Unequip(unquippedSlot);
            slot = 2;
        }

        // 新しいディスクをセットして装備中にする
        discs[slot] = newDisc;
        newDisc.PassiveEnter();

        // 🟡 装備中ディスクを新しく入手したディスクにする
        equippedDisc = slot;

        // front = 入手したディスク, back = もう1つ（または none）
        int front = equippedDisc;
        int back = GetBackDiscIndex();

        Material frontMat = (discs[front] != null) ? discs[front].discMaterial : none;
        Material backMat = (back != -1 && discs[back] != null) ? discs[back].discMaterial : none;

        // 💡 DiscSwitcher に反映
        discSwitcher.SetMaterials(frontMat, backMat);

        // 💡 DiscSwitcher にアニメーション付きで切り替え指示
        discSwitcher.SwitchToFront(false, true); // force = true で切り替え実行

        discs[equippedDisc].PassiveExit();
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

    private void UpdateDiscVisuals()
    {
        Material frontMat = discs[equippedDisc]?.discMaterial;

        int backIndex = GetBackDiscIndex();
        Material backMat = (backIndex != -1 && discs[backIndex] != null)
                           ? discs[backIndex].discMaterial
                           : none;

        Debug.Log("Switching " + equippedDisc + " and " + backIndex);

        if (discSwitcher != null)
        {
            discSwitcher.SetMaterials(frontMat, backMat);

            discSwitcher.SwitchToFront(false);
        }
    }



    private int GetBackDiscIndex()
    {
        if (equippedDisc == 1) 
        {
            if (discs[2] != null) { return 2; } else { return 0; } 
        }
        if (equippedDisc == 2)
        {
            if (discs[1] != null) { return 1; } else { return 0; }
        }
        if (discs[1] != null) { return 1; }
        if (discs[2] != null) { return 2; }
        return -1;

        //for (int i = 2; i >= 0; i--)
        //{
        //    if (i != equippedDisc && discs[i] != null)
        //        return i;
        //}
        //return -1;
    }

    private Material GetMaterial(System.Type type)
    {
        string typeName = type?.Name;

        switch (typeName)
        {
            case nameof(Disc_Red): return redMat;
            case nameof(Disc_Green): return greenMat;
            case nameof(Disc_Blue): return blueMat;
            case nameof(Disc_Yellow): return yellowMat;
            case nameof(Disc_Purple): return purpleMat;
            default:
                Debug.LogWarning("未対応のディスクタイプ: " + typeName);
                return null;
        }
    }


}
