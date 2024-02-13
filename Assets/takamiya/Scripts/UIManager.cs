using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
// <summary>
/// 起動時処理
/// </summary>
private void Awake()
    {
        // UIシーンの追加
        SceneManager.LoadScene("UIManager", LoadSceneMode.Additive);
    }
}
