using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private TitleBGMManager bgmManager;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Start").clicked += OnStartClicked;
        root.Q<Button>("Tutorial").clicked += OnTutorialClicked;
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
}
