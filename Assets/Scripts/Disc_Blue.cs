using System.Collections;
using UnityEngine;

public class Disc_Blue : Disc
{
    public float burstInterval = 0.1f; 
    public int burstCount = 3;         

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
        Debug.Log("青パッシブ");
    }
    public override void PassiveExit()
    {
        Debug.Log("青パッシブ終わり");
        // パッシブ終了処理
    }

    public override void Skill()
    {
        Debug.Log("青ディスクスキル");
        // スキルがあれば後で追加
    }
}
