using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static public string gameSceneName = "SampleScene";
    static public string titleSceneName = "TitleScene";

    void Update()
    {
        if (SceneManager.GetActiveScene().name == titleSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(gameSceneName);
            }
        }
        else if (SceneManager.GetActiveScene().name == gameSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(titleSceneName);
            }
        }
    }
}
