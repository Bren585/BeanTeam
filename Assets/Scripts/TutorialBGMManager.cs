using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBGMManager : MonoBehaviour
{
    public AudioClip bgm1; // �^�C�g���V�[���̍ŏ���BGM
    public AudioClip bgm2; // �N���X�t�F�[�h����BGM
    public float crossFadeDuration = 1.0f; // �N���X�t�F�[�h�̎���
    public float fadeOutDuration = 1.0f; // �t�F�[�h�A�E�g�̎���

    private AudioSource source1;
    private AudioSource source2;
    private bool isCrossFading = false;
    private bool isFadingOut = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��ׂ��ŃI�u�W�F�N�g���c��

        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();

        source1.loop = false; // BGM1�̓��[�v���Ȃ�
        source2.loop = true; // BGM2�̓��[�v����

        source1.clip = bgm1;
        source1.volume = 1f;
        source1.Play();

        StartCoroutine(WaitAndCrossFade(bgm1.length)); // BGM1�̍Đ����I���̂�҂��ăN���X�t�F�[�h
    }

    IEnumerator WaitAndCrossFade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime - crossFadeDuration);
        StartCoroutine(CrossFade(source1, source2, crossFadeDuration)); // �N���X�t�F�[�h�J�n
    }

    IEnumerator CrossFade(AudioSource from, AudioSource to, float duration)
    {
        if (isCrossFading) yield break; // ���łɃN���X�t�F�[�h���Ȃ牽�����Ȃ�

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

    // �t�F�[�h�A�E�g���Ă���V�[���J�ڂ��郁�\�b�h
    public void FadeOutAndLoadScene(string sceneName)
    {
        if (!isFadingOut) // �t�F�[�h�A�E�g�����s����Ă��Ȃ��ꍇ�̂ݎ��s
        {
            StartCoroutine(FadeOutAndSwitchScene(sceneName));
        }
    }

    // �t�F�[�h�A�E�g��ɃV�[���J�ڂ��郁�\�b�h
    IEnumerator FadeOutAndSwitchScene(string sceneName)
    {
        isFadingOut = true;
        AudioSource active = source2.isPlaying ? source2 : source1; // �Đ����̉�����I��

        float startVol = active.volume;
        float t = 0f;

        // �t�F�[�h�A�E�g���J�n
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            active.volume = Mathf.Lerp(startVol, 0f, t / fadeOutDuration);
            yield return null;
        }

        active.Stop(); // �t�F�[�h�A�E�g��ɒ�~
        active.volume = 1f;

        Destroy(gameObject);  // �V�[���J�ڑO��BGM�I�u�W�F�N�g��j��

        // �V�[���J��
        SceneManager.LoadScene(sceneName);
    }
}
