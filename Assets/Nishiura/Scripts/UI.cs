//
// UI�X�N���v�g
// Name:���Y�W�� Date:2/13
//
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{ 
    /// <summary>
    /// �N��������
    /// </summary>
    private void Awake()
    {
        // UI�V�[���̒ǉ�
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }
}
