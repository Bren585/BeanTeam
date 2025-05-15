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
        bgmManager = FindFirstObjectByType<TitleBGMManager>(); // BGMマネージャーを取得

    }

    void OnStartClicked()
    {
        if (bgmManager != null)
        {
            bgmManager.FadeOutAndLoadScene(SceneController.gameSceneName); // フェードアウトしてからゲームシーンへ
        }

        else
        {
            Debug.LogWarning("TitleBGMManager が見つかりません。通常のシーン遷移を行います。");
            SceneManager.LoadScene(SceneController.gameSceneName); // フォールバック
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
