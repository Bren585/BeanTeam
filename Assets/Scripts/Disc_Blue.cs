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
        Debug.Log("�p�b�V�u");
    }
    public override void PassiveExit()
    {
        Debug.Log("�p�b�V�u�I���");
        // �p�b�V�u�I������
    }

    public override void Skill()
    {
        Debug.Log("�f�B�X�N�X�L��");
        // �X�L��������Ό�Œǉ�
    }
}
