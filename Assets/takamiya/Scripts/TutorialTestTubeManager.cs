//==============================================
//Autor:�O����l
//Day:3/1
//���~�߃Q�[������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTestTubeManager : MonoBehaviour
{
    public Text timerText;                 //�^�C�}�[�e�L�X�g�̎w��
    int timer = 0;                        //�J�E���g�_�E���^�C�}�[�̕ϐ�
    [SerializeField] float speed;         //���x����
    public GameObject good;              //good�e�L�X�g�w��
    public GameObject veryGood;         //veryGood�e�L�X�g�̎w��
    public GameObject Bad;             //Bad�e�L�X�g�w��
    [SerializeField] Slider slider;   //�X���C�_�[�̎w��
    [SerializeField] AudioClip veryGoodSE;      //�听��SE
    [SerializeField] AudioClip goodSE;          //����SE
    [SerializeField] AudioClip badSE;           //���sSE
    [SerializeField] AudioSource audioSource;

    StartMiniGame tutorialMiniGame;

    private bool _endCountDown;

    private bool isClicked;

    public bool endCountDown
    {
        get
        {
            return _endCountDown;
        }
        set
        {
            _endCountDown = value;
        }
    }
    //private bool maxValue;
   
    void Start()
    {
        tutorialMiniGame = GameObject.Find("MiniGameManager").GetComponent<StartMiniGame>();
        Init();
    }
    public void Init()
    {
        slider.value = 0;//������

        endCountDown = false;
        isClicked = false;

        tutorialMiniGame.gameNum = StartMiniGame.GAMEMODE.OCHA_MODE;

        veryGood.SetActive(false);//�听���e�L�X�g���\���ɂ���
        good.SetActive(false);//�����e�L�X�g���\���ɂ���
        Bad.SetActive(false);//���s�e�L�X�g���\���ɂ���
    }

    void Update()
    {
        if(endCountDown)
        {
            if(isClicked == false)
            {
                //�N���b�N�𗣂����u�Ԃ̔���
                if (Input.GetMouseButtonUp(0))
                {
                    isClicked = true;

                    if (slider.value >= 94)
                    {
                        //���sSE
                        audioSource.PlayOneShot(badSE);

                        Bad.SetActive(true);//���s�e�L�X�g��\���ɂ���
                    }
                    else if (slider.value >= 68 && slider.value < 84)
                    {
                        //����SE
                        audioSource.PlayOneShot(goodSE);

                        good.SetActive(true);//�����e�L�X�g��\���ɂ���
                    }
                    else if (slider.value >= 84 && slider.value < 94)
                    {
                        //�听��SE
                        audioSource.PlayOneShot(veryGoodSE);

                        veryGood.SetActive(true);//�听���e�L�X�g��\���ɂ���
                    }
                    else if (slider.value < 68)
                    {
                        //���sSE
                        audioSource.PlayOneShot(badSE);

                        Bad.SetActive(true);//���s�e�L�X�g��\���ɂ���
                    }
                    tutorialMiniGame.NextButton.SetActive(true);
                    tutorialMiniGame.AgainButton.SetActive(true);
                }
                //�N���b�N����Ă���Ԏ��s
                if (Input.GetMouseButton(0))
                {
                    slider.value += speed;

                    if (slider.value >= 94)
                    {
                        //���sSE
                        audioSource.PlayOneShot(badSE);

                        Bad.SetActive(true);//���s�e�L�X�g��\���ɂ���
                    }
                }
            }
        }
       
    }
    /// <summary>
    /// �~�j�Q�[���̔j��
    /// </summary>
    public void DestroyMiniGame()
    {
        // �~�j�Q�[�����I��
        veryGood.SetActive(false);//�听���e�L�X�g���\���ɂ���
        good.SetActive(false);//�����e�L�X�g���\���ɂ���
        Bad.SetActive(false);//���s�e�L�X�g���\���ɂ���
        Destroy(GameObject.Find("OchaGame"));//������Q�[�����폜
        Destroy(GameObject.Find("OchaGameimgs"));//������Q�[���摜���폜

    }
}
