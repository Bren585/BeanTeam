using UnityEngine;

public class Disc_Yellow : Disc
{
    [SerializeField]
    private int numberOfShots = 5;  
    [SerializeField]
    private float spreadAngle = 15f;  

    [SerializeField]
    private float damageMultiplier = 2f;  

    public override void Skill()
    {
        Player player = FindFirstObjectByType<Player>();
        player.AddImpulse(player.transform.forward * player.Acceleration * Time.deltaTime * 200);
        player.SetInvincible(1.0f);
        //Debug.Log("���F�X�L��");
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        // �e�̈З͂�����
        int originalBulletDamage = bulletDamage;
        bulletDamage = (int)(bulletDamage * damageMultiplier);

        // ���˂���e�𕡐���A�قȂ�����ɔ���
        for (int i = 0; i < numberOfShots; i++)
        {
            // �e�e�̕������v�Z�i�����_���ɃX�v���b�h������j
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 shotDirection = rotation * direction;  // ��]������������

            // �e�𔭎�
            base.Shoot(position, shotDirection);
        }

        // ���̃_���[�W�ɖ߂�
        bulletDamage = originalBulletDamage;
    }
}
