//==============================================
//Autor:三宅歩人
//Day:3/1
//寸止めゲーム処理
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTubeManager : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject good;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    bool maxValue;
    bool endCountDown;
    NetworkManager networkManager;
    bool gameFlag;
    [SerializeField] AudioClip goodSE;          //成功SE
    [SerializeField] AudioClip badSE;           //失敗SE
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        slider.value = 0;
        endCountDown = false;
        gameFlag = false;

        // ネットワークマネージャーの取得
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (timerText.text == "GO!!")
        {
            endCountDown = true;
        }

        if (endCountDown)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(!gameFlag)
                {
                    if (slider.value >= 94)
                    {   // 失敗
                        Bad.SetActive(true);
                        //失敗SE
                        audioSource.PlayOneShot(badSE);
                        // 失敗情報の送信
                        networkManager.SendPotionStatus((int)EventID.PotionFailure);
                        gameFlag = true;
                    }
                    else if (slider.value >= 67 && slider.value < 94)
                    {   // 大成功
                        good.SetActive(true);
                        //大成功SE
                        audioSource.PlayOneShot(goodSE);
                        // 生成情報の送信
                        networkManager.SendPotionStatus((int)EventID.PotionComplete);
                        gameFlag = true;
                    }
                    else if (slider.value < 66)
                    {   // 失敗
                        Bad.SetActive(true);
                        //失敗SE
                        audioSource.PlayOneShot(badSE);
                        // 失敗情報の送信
                        networkManager.SendPotionStatus((int)EventID.PotionFailure);
                        gameFlag = true;
                    }
                }
                
                // ミニゲームの終了
                Invoke("MiniGameDestroy", 1.5f);
            }

            //クリックされていなければ実行
            if (Input.GetMouseButton(0))
            {
                slider.value += speed * Time.deltaTime;

                if (slider.value >= 94)
                {
                    Bad.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// ミニゲームの破棄
    /// </summary>
    private void MiniGameDestroy()
    {
        // ミニゲームを終了
        Destroy(GameObject.Find("MiniGames(Clone)"));
    }
}
