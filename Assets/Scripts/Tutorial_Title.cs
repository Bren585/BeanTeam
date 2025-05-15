using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Title : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // BGM�}�l�[�W���[������Δj���i�K�v�ɉ����āj
            TitleBGMManager existingBGM = FindFirstObjectByType<TitleBGMManager>();
            if (existingBGM != null)
            {
                Destroy(existingBGM.gameObject);
            }

            // �^�C�g���V�[���ɑJ��
            SceneManager.LoadScene("TitleScene");
        }
    }
}
