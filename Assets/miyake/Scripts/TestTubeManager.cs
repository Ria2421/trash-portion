//==============================================
//Autor:�O����l
//Day:3/1
//���~�߃Q�[������
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
    [SerializeField] AudioClip goodSE;          //����SE
    [SerializeField] AudioClip badSE;           //���sSE
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        slider.value = 0;
        endCountDown = false;
        gameFlag = false;

        // �l�b�g���[�N�}�l�[�W���[�̎擾
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
                    {   // ���s
                        Bad.SetActive(true);
                        //���sSE
                        audioSource.PlayOneShot(badSE);
                        // ���s���̑��M
                        networkManager.SendPotionStatus((int)EventID.PotionFailure);
                        gameFlag = true;
                    }
                    else if (slider.value >= 67 && slider.value < 94)
                    {   // �听��
                        good.SetActive(true);
                        //�听��SE
                        audioSource.PlayOneShot(goodSE);
                        // �������̑��M
                        networkManager.SendPotionStatus((int)EventID.PotionComplete);
                        gameFlag = true;
                    }
                    else if (slider.value < 66)
                    {   // ���s
                        Bad.SetActive(true);
                        //���sSE
                        audioSource.PlayOneShot(badSE);
                        // ���s���̑��M
                        networkManager.SendPotionStatus((int)EventID.PotionFailure);
                        gameFlag = true;
                    }
                }
                
                // �~�j�Q�[���̏I��
                Invoke("MiniGameDestroy", 1.5f);
            }

            //�N���b�N����Ă��Ȃ���Ύ��s
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
    /// �~�j�Q�[���̔j��
    /// </summary>
    private void MiniGameDestroy()
    {
        // �~�j�Q�[�����I��
        Destroy(GameObject.Find("MiniGames(Clone)"));
    }
}
