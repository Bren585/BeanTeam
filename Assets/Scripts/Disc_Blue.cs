using System.Collections.Generic;
using UnityEngine;

public class Disc_Blue : Disc
{
    public GameObject orbitBulletPrefab;
    [SerializeField] private int bulletCount = 8;         // ’e”‚ğ8‚É•ÏX
    [SerializeField] private float orbitRadius = 3.5f;    // ”¼Œa‚ğL‚­
    [SerializeField] private float rotationSpeed = 100f;

    private List<GameObject> orbitBullets = new List<GameObject>();
    private float currentAngle = 0f;
    private Transform playerTransform;

    private void Update()
    {
        if (orbitBullets.Count == 0) return;
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
        if (orbitBullets.Count > 0) return;

        if (playerTransform == null)
            playerTransform = GetComponentInParent<Player>()?.transform;

        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½");
            return;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(orbitBulletPrefab);
            orbitBullets.Add(bullet);
        }
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
