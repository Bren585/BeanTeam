using UnityEngine;

public class Disc : Emitter
{
    public AudioClip shootSound;
    private AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public override void Shoot(Vector3 position, Vector3 direction)
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        base.Shoot(position, direction);
    }

    public virtual void Skill() { }
    public virtual void PassiveEnter() { }
    public virtual void PassiveExit() { }
}
