using System.Collections.Generic;
using UnityEngine;

public class Disc_Blue : Disc
{
    public GameObject orbitBulletPrefab;
    [SerializeField] private int bulletCount = 8;
    [SerializeField] private float orbitRadius = 3.5f;
    [SerializeField] private float rotationSpeed = 100f;

    private List<GameObject> orbitBullets = new List<GameObject>();
    private float currentAngle = 0f;
    private Transform playerTransform;
    private Player player;

    private void Awake()
    {
        // �v���C���[��Transform�擾
        player = GetComponent<Player>();
        playerTransform = GetComponentInParent<Player>()?.transform;
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform ��������܂���ł���");
        }
    }

    private void Update()
    {
        if (orbitBullets.Count == 0 || playerTransform == null) return;

        currentAngle += rotationSpeed * Time.deltaTime;

        for (int i = 0; i < orbitBullets.Count; i++)
        {
            float angle = currentAngle + (360f / bulletCount) * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * orbitRadius;
            orbitBullets[i].transform.position = playerTransform.position + offset;
        }
    }

    public override void Skill()
    {
        if (player == null)
        {
            return;
        }

        player.Heal(1);
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        if (playerTransform == null)
        {
            return;
        }

        if (orbitBullets.Count > 0) return; // ���łɐ����ς݂Ȃ�X�L�b�v

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(orbitBulletPrefab, playerTransform.position, Quaternion.identity);
            orbitBullets.Add(bullet);
        }

        currentAngle = 0f; // �����p�x�Ƀ��Z�b�g
    }

    public override void PassiveExit()
    {
        foreach (var bullet in orbitBullets)
        {
            Destroy(bullet);
        }
        orbitBullets.Clear();
    }
}
