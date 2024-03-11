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
    public Text timerText;
    int timer = 0;              //�J�E���g�_�E���^�C�}�[�̕ϐ�
    [SerializeField] float speed;
    public GameObject good;//good�e�L�X�g�w��
    public GameObject veryGood;//veryGood�e�L�X�g�̎w��
    public GameObject Bad;//Bad�e�L�X�g�w��
    [SerializeField] Slider slider;//�X���C�_�[�̎w��
    StartMiniGame tutorialMiniGame;
    TutorialBarManager tutorialBarManager;

    private bool _endCountDown;

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
            //�N���b�N�𗣂����u�Ԃ̔���
            if (Input.GetMouseButtonUp(0))
            {
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
                Invoke("MiniGameDestroy", 1f);
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
        else if(!endCountDown)
        {
            slider.value = 0;
            tutorialMiniGame.NextButton.SetActive(false);
            tutorialMiniGame.AgainButton.SetActive(false);
        }

    }
    /// <summary>
    /// �~�j�Q�[���̔j��
    /// </summary>
    private void MiniGameDestroy()
    {
        // �~�j�Q�[�����I��
        //Destroy(GameObject.Find("MiniGames"));

    }

    //void CountDownTimer()
    //{
   
    //    //�J�E���g�_�E�����ă^�C�}�[�̃e�L�X�g�ɕb����ݒ�
    //    timer--;
    //    timerText.text = timer.ToString();
    //    if (timerText.text == "2")
    //    {
    //        timerText.color = Color.yellow;
    //    }
    //    else if (timerText.text == "1")
    //    {
    //        timerText.color = Color.red;
    //    }

    //    //timer��0�ɂȂ�����I��
    //    if (timer == 0)
    //    {
    //        //Invoke����߂�
    //        CancelInvoke();
    //        timerText.text = "GO!!";
    //        if(tutorialMiniGame.gameNum == StartMiniGame.GAMEMODE.OCHA_MODE)
    //        {
    //            tutorialBarManager.endCountDown = true;
    //        }
            
    //        Invoke("TextDestroy", 0.7f);
    //    }
    //}
}
