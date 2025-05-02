using System.Collections;
using UnityEngine;

public class Disc_Purple : Disc
{
    public float burstInterval = 0.3f;  // 少し長めの間隔
    public int burstCount = 5;          // 5発バースト

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        StartCoroutine(BurstShoot(position, direction));
    }

    private IEnumerator BurstShoot(Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < burstCount; i++)
        {
            base.Shoot(position, direction);
            yield return new WaitForSeconds(burstInterval);
        }
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
