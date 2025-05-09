using UnityEngine;

public class Disc_Purple : Disc
{
    public override void Shoot(Vector3 position, Vector3 direction)
    {
        base.Shoot(position, direction); // ← 通常の1発発射
    }

    public override void PassiveEnter()
    {
        Debug.Log("紫パッシブ");
        // パッシブ効果があればここに
    }

    public override void PassiveExit()
    {
        Debug.Log("紫パッシブ終わり");
        // パッシブ終了処理
    }

    public override void Skill()
    {
        Debug.Log("紫ディスクスキル");

        // スキルがあれば後で追加
    }
}
