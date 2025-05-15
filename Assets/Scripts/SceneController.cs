using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const string titleSceneName = "TitleScene";
    public const string gameSceneName = "SampleScene";

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}
