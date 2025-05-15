using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUIController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement overlay;
    private VisualElement window;
    private Label clearCountLabel;

    void Start()
    {
        Time.timeScale = 1;
    }
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        overlay = root.Q<VisualElement>("Overlay");
        var gameOverRoot = root.Q<VisualElement>("GameOverRoot");

        clearCountLabel = root.Q<Label>("ClearCountLabel");  // ここを追加

        if (overlay != null)
            overlay.visible = false;
        if (gameOverRoot != null)
            gameOverRoot.visible = false;

        // ボタン登録
        root.Q<Button>("Retry")?.RegisterCallback<ClickEvent>(_ =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        root.Q<Button>("TitleBack")?.RegisterCallback<ClickEvent>(_ =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TitleScene");
        });
    }


    public void SetClearCount(int count)
    {
        if (clearCountLabel != null)
        {
            clearCountLabel.text = $"Cleared Stages: {count}";
        }
    }
    public void ShowGameOver()
    {
        Debug.Log("ShowGameOver Called");
        var gameOverRoot = root.Q<VisualElement>("GameOverRoot");
        var overlay = root.Q<VisualElement>("Overlay");

        Time.timeScale = 0;

        if (gameOverRoot != null)
            gameOverRoot.visible = true;
        if (overlay != null)
            overlay.visible = true;
    }

    internal void SetClearCount(object stages_cleared)
    {
        throw new NotImplementedException();
    }
}
