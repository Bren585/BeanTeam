using UnityEngine;

public class Disc_Purple : Disc
{
    public override void Shoot(Vector3 position, Vector3 direction)
    {
        base.Shoot(position, direction); // �� �ʏ��1������
    }

    public override void PassiveEnter()
    {
        Debug.Log("���p�b�V�u");
        // �p�b�V�u���ʂ�����΂�����
    }

    public override void PassiveExit()
    {
        Debug.Log("���p�b�V�u�I���");
        // �p�b�V�u�I������
    }

    public override void Skill()
    {
        Debug.Log("���f�B�X�N�X�L��");

        // �X�L��������Ό�Œǉ�
    }
}
