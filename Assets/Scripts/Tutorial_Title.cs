using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Title : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // BGMマネージャーがあれば破棄（必要に応じて）
            TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>();
            if (existingBGM != null)
            {
                Destroy(existingBGM.gameObject);
            }

            // タイトルシーンに遷移
            SceneManager.LoadScene("TitleScene");
        }
    }
}
