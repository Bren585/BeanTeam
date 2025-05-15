using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private TitleBGMManager bgmManager;

    void Start()
    {
        Time.timeScale = 1;
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Start").clicked += OnStartClicked;
        root.Q<Button>("Tutorial").clicked += OnTutorialClicked;
        root.Q<Button>("Credits").clicked += OnCreditsClicked;
        bgmManager = FindFirstObjectByType<TitleBGMManager>(); // BGM�}�l�[�W���[���擾

    }

    void OnStartClicked()
    {
        if (bgmManager != null)
        {
            bgmManager.FadeOutAndLoadScene(SceneController.gameSceneName); // �t�F�[�h�A�E�g���Ă���Q�[���V�[����
        }

        else
        {
            Debug.LogWarning("TitleBGMManager ��������܂���B�ʏ�̃V�[���J�ڂ��s���܂��B");
            SceneManager.LoadScene(SceneController.gameSceneName); // �t�H�[���o�b�N
        }
    }

    void OnTutorialClicked()
    {
        string TutorialSceneName = "TutorialScene"; 
        if (bgmManager != null)
        {
            bgmManager.FadeOutAndLoadScene(TutorialSceneName);
        }
        else
        {
            SceneManager.LoadScene(TutorialSceneName);
        }
    }
    void OnCreditsClicked()
    {
        string CreditsSceneName = "Credits";
        if (bgmManager != null)
        {
            bgmManager.FadeOutAndLoadScene(CreditsSceneName);
        }
        else
        {
            SceneManager.LoadScene(CreditsSceneName);
        }
    }
}
