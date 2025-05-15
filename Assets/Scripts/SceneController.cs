using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static public string gameSceneName = "SampleScene";
    static public string titleSceneName = "TitleScene";
    static public string tutorialSceneName = "TutorialScene";

    void Update()
    {
        if (SceneManager.GetActiveScene().name == titleSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TitleBGMManager bgm = FindFirstObjectByType<TitleBGMManager>();
                if (bgm != null)
                {
                    bgm.FadeOutAndLoadScene(gameSceneName);
                }
                else
                {
                    SceneManager.LoadScene(gameSceneName);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == gameSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // �^�C�g���ɖ߂�O��BGM�I�u�W�F�N�g��j��
                TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>(); 
                if (existingBGM != null)
                {
                    Destroy(existingBGM.gameObject);
                }

                SceneManager.LoadScene(titleSceneName);
            }
        }
        else if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
           
                // �^�C�g���ɖ߂�O��BGM�I�u�W�F�N�g��j��
                TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>();
                if (existingBGM != null)
                {
                    Destroy(existingBGM.gameObject);
                }

                SceneManager.LoadScene(titleSceneName);
            
        }
    }
}
