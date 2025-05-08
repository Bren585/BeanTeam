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
            // �f�B�X�N��؂�ւ���
            discs[equippedDisc].PassiveExit();
            equippedDisc = (equippedDisc + 1) % discs.Length;
            discs[equippedDisc].PassiveEnter();
            // �V�����f�B�X�N�ŃX�L���𔭓�
            if (discs.Length > equippedDisc && discs[equippedDisc] != null)
            {
                discs[equippedDisc].Skill();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1�L�[");
            // ���Ƀf�B�X�N��1�ȏ�Ȃ�ԃf�B�X�N��ǉ�
            if (discs.Length == 0)
            {
                Disc_Red redDisc = Object.FindFirstObjectByType<Disc_Red>();
                if (redDisc != null)
                {
                    AddDisc(redDisc); // �ԃf�B�X�N��ǉ�
                    redDisc.PassiveEnter(); // �ԃf�B�X�N�̌��ʂ�K�p
                    equippedDisc = discs.Length - 1; // �ԃf�B�X�N�𑕔�
                }
                else
                {
                    Debug.LogError("�ԃf�B�X�N��������܂���B");
                }
            }
        }


    }
    public void AddDisc(Disc newDisc)
    {
        // �V�����f�B�X�N���v���C���[�̃f�B�X�N���X�g�ɒǉ�
        List<Disc> discList = new List<Disc>(discs);  // �����̃f�B�X�N���ꎞ�I�Ƀ��X�g�ɕϊ�
        discList.Add(newDisc);  // �V�����f�B�X�N��ǉ�

        // �X�V�������X�g���ēx�z��ɕϊ����Đݒ�
        discs = discList.ToArray();

        // �ǉ����ꂽ�f�B�X�N���ŏ��̃f�B�X�N�Ȃ�A����𑕔�
        if (discs.Length == 1)
        {
            equippedDisc = 0;
            discs[equippedDisc].PassiveEnter();  // �V�����f�B�X�N�̌��ʂ�K�p
        }
    }
}
