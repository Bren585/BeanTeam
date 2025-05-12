using System.Collections;
using UnityEngine;

public class GameBGMManager : MonoBehaviour
{
    public AudioClip bgm1; // ゲーム開始イントロ
    public AudioClip bgm2; // ゲームループ
    public AudioClip deathIntroBgm; // ゲームオーバーイントロ
    public AudioClip deathLoopBgm;  // ゲームオーバーループ
    public float crossFadeDuration = 1.0f;

    private AudioSource source1;
    private AudioSource source2;
    private bool isCrossFading = false;
    private bool hasDied = false;

    void Awake()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();

        source1.loop = false;
        source2.loop = true;

        // スタート時のBGM再生
        source1.clip = bgm1;
        source1.volume = 1f;
        source1.Play();

        StartCoroutine(WaitAndCrossFade(source1, source2, bgm2, bgm1.length));
    }

    IEnumerator WaitAndCrossFade(AudioSource from, AudioSource to, AudioClip toClip, float waitTime)
    {
        yield return new WaitForSeconds(waitTime - crossFadeDuration);
        if (!hasDied)
            StartCoroutine(CrossFade(from, to, toClip, crossFadeDuration));
    }

    IEnumerator CrossFade(AudioSource from, AudioSource to, AudioClip toClip, float duration)
    {
        if (isCrossFading) yield break;

        isCrossFading = true;
        float time = 0f;
        to.clip = toClip;
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

    // ▶ プレイヤー死亡時に呼び出す
    public void PlayDeathBGM()
    {
        if (hasDied) return;
        hasDied = true;

        StopAllCoroutines();
        source1.Stop();
        source2.Stop();

        // 死亡イントロを source1 で再生
        source1.clip = deathIntroBgm;
        source1.loop = false;
        source1.volume = 1f;
        source1.Play();

        // 死亡ループBGMへのクロスフェード準備
        StartCoroutine(WaitAndCrossFade(source1, source2, deathLoopBgm, deathIntroBgm.length));
    }
}
