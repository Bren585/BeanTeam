using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const string creditsSceneName = "Credits";
    public const string tutorialSceneName = "TutorialScene";
    public const string titleSceneName = "TitleScene";
    public const string gameSceneName = "SampleScene";


    void Update()
    {
      
        if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // タイトルに戻る前にBGMオブジェクトを破棄
                TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>();
                TutorialBGMManager existingBGM2=FindFirstObjectByType<TutorialBGMManager>();
                if (existingBGM != null)
                {
                    Destroy(existingBGM.gameObject);
                    Destroy(existingBGM2.gameObject);
                }

                SceneManager.LoadScene(titleSceneName);
            }
         
            
        }
         if (SceneManager.GetActiveScene().name == creditsSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // タイトルに戻る前にBGMオブジェクトを破棄
                TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>();
                TutorialBGMManager existingBGM2=FindFirstObjectByType<TutorialBGMManager>();
                if (existingBGM != null)
                {
                    Destroy(existingBGM.gameObject);
                    Destroy(existingBGM2.gameObject);
                }

                SceneManager.LoadScene(titleSceneName);
            }
         
            
        }
    }
}
