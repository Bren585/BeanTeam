using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Start").clicked += OnStartClicked;

    }

    void Update()
    {
        
    }

    void OnStartClicked() { SceneManager.LoadScene(SceneController.gameSceneName); }
}
