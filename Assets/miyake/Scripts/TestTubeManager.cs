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
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    private bool maxValue;
    bool endCountDown;
    private NetworkManager networkManager;

    void Start()
    {
        slider.value = 0;
        endCountDown = false;

        // �l�b�g���[�N�}�l�[�W���[�̎擾
        //networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
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
                if (slider.value >= 94)
                {   // ���s
                    Bad.SetActive(true);
                    // ���s���̑��M
                    //networkManager.SendPotionStatus((int)EventID.PotionFailure);
                }
                else if (slider.value >= 68 && slider.value < 84)
                {   // ����
                    good.SetActive(true);
                    // �������̑��M
                    //networkManager.SendPotionStatus((int)EventID.PotionComplete);
                }
                else if (slider.value >= 84 && slider.value < 94)
                {   // �听��
                    veryGood.SetActive(true);
                    // �������̑��M
                    //networkManager.SendPotionStatus((int)EventID.PotionComplete);
                }
                else if(slider.value < 68)
                {   // ���s
                    Bad.SetActive(true);
                    // ���s���̑��M
                    //networkManager.SendPotionStatus((int)EventID.PotionFailure);
                }

                // �~�j�Q�[���̏I��
                Invoke("MiniGameDestroy", 1.5f);
            }

            //�N���b�N����Ă��Ȃ���Ύ��s
            if (Input.GetMouseButton(0))
            {
                slider.value += speed;

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
