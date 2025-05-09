using System.Collections;
using UnityEngine;

public class GameBGMManager : MonoBehaviour
{
    public AudioClip bgm1; // �Q�[���V�[���̃C���g��BGM
    public AudioClip bgm2; // ���[�vBGM
    public float crossFadeDuration = 1.0f; // �N���X�t�F�[�h�̎���

    private AudioSource source1;
    private AudioSource source2;
    private bool isCrossFading = false;

    void Awake()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();

        source1.loop = false;
        source2.loop = true;

        source1.clip = bgm1;
        source1.volume = 1f;
        source1.Play();

        StartCoroutine(WaitAndCrossFade(bgm1.length));
    }

    IEnumerator WaitAndCrossFade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime - crossFadeDuration);
        StartCoroutine(CrossFade(source1, source2, crossFadeDuration));
    }

    IEnumerator CrossFade(AudioSource from, AudioSource to, float duration)
    {
        if (isCrossFading) yield break;

        isCrossFading = true;
        float time = 0f;
        to.clip = bgm2;
        to.volume = 0f;
        to.Play();

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            from.volume = Mathf.Lerp(1f, 0f, t);
            to.volume = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        from.Stop();
        from.volume = 1f;
        isCrossFading = false;
    }
}
