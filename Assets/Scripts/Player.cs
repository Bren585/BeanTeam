using System.Collections.Generic;
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1キー");
            // 既にディスクが1個以上なら赤ディスクを追加
            if (discs.Length == 0)
            {
                Disc_Red redDisc = Object.FindFirstObjectByType<Disc_Red>();
                if (redDisc != null)
                {
                    AddDisc(redDisc); // 赤ディスクを追加
                    redDisc.PassiveEnter(); // 赤ディスクの効果を適用
                    equippedDisc = discs.Length - 1; // 赤ディスクを装備
                }
                else
                {
                    Debug.LogError("赤ディスクが見つかりません。");
                }
            }
        }


    }
    public void AddDisc(Disc newDisc)
    {
        // 新しいディスクをプレイヤーのディスクリストに追加
        List<Disc> discList = new List<Disc>(discs);  // 既存のディスクを一時的にリストに変換
        discList.Add(newDisc);  // 新しいディスクを追加

        // 更新したリストを再度配列に変換して設定
        discs = discList.ToArray();

        // 追加されたディスクが最初のディスクなら、それを装備
        if (discs.Length == 1)
        {
            equippedDisc = 0;
            discs[equippedDisc].PassiveEnter();  // 新しいディスクの効果を適用
        }
    }
}
