using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
// <summary>
/// �N��������
/// </summary>
private void Awake()
    {
        // UI�V�[���̒ǉ�
        SceneManager.LoadScene("UIManager", LoadSceneMode.Additive);
    }
}
