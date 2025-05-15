using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBGMManager : MonoBehaviour
{
    public AudioClip bgm1; // タイトルシーンの最初のBGM
    public AudioClip bgm2; // クロスフェードするBGM
    public float crossFadeDuration = 1.0f; // クロスフェードの時間
    public float fadeOutDuration = 1.0f; // フェードアウトの時間

    private AudioSource source1;
    private AudioSource source2;
    private bool isCrossFading = false;
    private bool isFadingOut = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // シーン遷移を跨いでオブジェクトを残す

        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();

        source1.loop = false; // BGM1はループしない
        source2.loop = true; // BGM2はループする

        source1.clip = bgm1;
        source1.volume = 1f;
        source1.Play();

        StartCoroutine(WaitAndCrossFade(bgm1.length)); // BGM1の再生が終わるのを待ってクロスフェード
    }

    IEnumerator WaitAndCrossFade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime - crossFadeDuration);
        StartCoroutine(CrossFade(source1, source2, crossFadeDuration)); // クロスフェード開始
    }

    IEnumerator CrossFade(AudioSource from, AudioSource to, float duration)
    {
        if (isCrossFading) yield break; // すでにクロスフェード中なら何もしない

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

    // フェードアウトしてからシーン遷移するメソッド
    public void FadeOutAndLoadScene(string sceneName)
    {
        if (!isFadingOut) // フェードアウトが実行されていない場合のみ実行
        {
            StartCoroutine(FadeOutAndSwitchScene(sceneName));
        }
    }

    // フェードアウト後にシーン遷移するメソッド
    IEnumerator FadeOutAndSwitchScene(string sceneName)
    {
        isFadingOut = true;
        AudioSource active = source2.isPlaying ? source2 : source1; // 再生中の音源を選択

        float startVol = active.volume;
        float t = 0f;

        // フェードアウトを開始
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            active.volume = Mathf.Lerp(startVol, 0f, t / fadeOutDuration);
            yield return null;
        }

        active.Stop(); // フェードアウト後に停止
        active.volume = 1f;

        Destroy(gameObject);  // シーン遷移前にBGMオブジェクトを破棄

        // シーン遷移
        SceneManager.LoadScene(sceneName);
    }
}
