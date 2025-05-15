using UnityEngine;

public class Disc_Green : Disc
{
    private float slowBulletSpeed = 10.0f;   // �e����x��
    private int poweredBulletDamage = 30;    // �_���[�W��傫��

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        float originalSpeed = bulletSpeed;
        int originalDamage = bulletDamage;

        bulletSpeed = slowBulletSpeed;
        bulletDamage = poweredBulletDamage;

        base.Shoot(position, direction);

        bulletSpeed = originalSpeed;
        bulletDamage = originalDamage;
    }

    public override void PassiveEnter()
    {
        //Debug.Log("�΃p�b�V�u");
        // �����Ƀv���C���[�ւ̌��ʂ�����Βǉ�
    }

    public override void PassiveExit()
    {
        //Debug.Log("�΃p�b�V�u�I���");
        // Passive�̏I������������Βǉ�
    }

    public override void Skill()
    {
        Player player = FindFirstObjectByType<Player>();
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            enemy.AddImpulse(player.transform.forward * player.Acceleration * Time.deltaTime * 100);
        }
        player.Unequip(player.IsEquipped<Disc_Green>());
        player.UpdateDiscVisuals();
        //Debug.Log("�΃f�B�X�N�X�L��");
        // �X�L����������Œǉ�����ꍇ
    }
}
