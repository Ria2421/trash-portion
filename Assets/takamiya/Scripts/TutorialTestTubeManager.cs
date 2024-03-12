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

    StartMiniGame tutorialMiniGame;
    TutorialBarManager tutorialBarManager;

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

        //timer = 3;

        endCountDown = false;
        isClicked = false;

        tutorialMiniGame.gameNum = StartMiniGame.GAMEMODE.OCHA_MODE;

        veryGood.SetActive(false);
        good.SetActive(false);
        Bad.SetActive(false);

        //1�b���ƂɊ֐������s
        //InvokeRepeating("CountDownTimer", 1.0f, 0.7f);

        //tutorialBarManager = GameObject.Find("Slider").GetComponent<TutorialBarManager>();
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
                        Bad.SetActive(true);
                    }
                    else if (slider.value >= 68 && slider.value < 84)
                    {
                        good.SetActive(true);
                    }
                    else if (slider.value >= 84 && slider.value < 94)
                    {
                        veryGood.SetActive(true);
                    }
                    else if (slider.value < 68)
                    {
                        Bad.SetActive(true);
                    }
                    tutorialMiniGame.NextButton.SetActive(true);
                    tutorialMiniGame.AgainButton.SetActive(true);

                    // �~�j�Q�[���̏I��
                    //Invoke("MiniGameDestroy", 1f);

                }
                //�N���b�N����Ă���Ԏ��s
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
       
    }
    /// <summary>
    /// �~�j�Q�[���̔j��
    /// </summary>
    public void DestroyMiniGame()
    {
        // �~�j�Q�[�����I��
        //Destroy(GameObject.Find("MiniGames"));
        veryGood.SetActive(false);
        good.SetActive(false);
        Bad.SetActive(false);
        Destroy(GameObject.Find("OchaGame"));
        Destroy(GameObject.Find("OchaGameimgs"));

    }
}
