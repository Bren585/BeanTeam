using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public const string tutorialSceneName = "TutorialScene";
    public const string titleSceneName = "TitleScene";
    public const string gameSceneName = "SampleScene";


    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(titleSceneName);
        }
        else if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
           
                // タイトルに戻る前にBGMオブジェクトを破棄
                TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>();
                if (existingBGM != null)
                {
                    Destroy(existingBGM.gameObject);
                }

                SceneManager.LoadScene(titleSceneName);
            
        }
    }
}
