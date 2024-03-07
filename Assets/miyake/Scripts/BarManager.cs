//==============================================
//Autor:�O����l
//Day:3/5
//�X���C�_�[�i�o�[�j�Q�[������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject good;
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    private bool maxValue;
    private bool isClicked;
    bool endCountDown;
    private NetworkManager networkManager;

    void Start()
    {
        slider.value = 0;
        maxValue = false;
        isClicked = false;
        endCountDown = false;

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
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;

                if (slider.value >= 85)
                {   // �听��
                    veryGood.SetActive(true);
                    // �������̑��M
                    networkManager.SendPotionStatus((int)EventID.PotionComplete);
                }
                else if (slider.value >= 50)
                {   // ����
                    good.SetActive(true);
                    // �������̑��M
                    networkManager.SendPotionStatus((int)EventID.PotionComplete);
                }
                else if (slider.value < 50)
                {   // ���s
                    Bad.SetActive(true);
                    // ���s���̑��M
                    networkManager.SendPotionStatus((int)EventID.PotionFailure);
                }

                // �~�j�Q�[���̏I��
                Invoke("MiniGameDestroy", 1.5f);
            }

            //�N���b�N����Ă��Ȃ���Ύ��s
            if (!isClicked)
            {
                //�ő�l�ɒB�����ꍇ�ƁA�ŏ��l�ɖ߂����ꍇ�̃t���O�ؑւ�
                if (slider.value == slider.maxValue)
                {
                    maxValue = true;
                }

                if (slider.value == slider.minValue)
                {
                    maxValue = false;
                }

                //�t���O�ɂ��X���C�_�[�l�̑���
                if (maxValue)
                {
                    slider.value -= speed;
                }
                else
                {
                    slider.value += speed;
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
