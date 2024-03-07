//==============================================
//Autor:�O����l
//Day:3/4
//�~�j�Q�[���J�ڏ���
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMiniGame : MonoBehaviour
{
    public Text timerText;
    int timer = 0;              //�J�E���g�_�E���^�C�}�[�̕ϐ�
    int randScene = 0;          //�����_���Ń~�j�Q�[���̒��I���邽�߂̕ϐ�
    bool isLottery;             //���I������
    public GameObject slideGame;        //�~�j�Q�[��1
    public GameObject rouletteGame;     //�~�j�Q�[��2
    public GameObject ochaGame;         //�~�j�Q�[��3
    public GameObject ochaGameImg;      //�~�j�Q�[��3�̉摜

    void Start()
    {
        isLottery = false;
        timer = 3;
        timerText.enabled = false;

        //+++++++++++++++++++++++++++++++++++++++++++++++
        // �{�^���s�g�p
        timerText.enabled = true;
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 1.0f, 0.7f);
        //+++++++++++++++++++++++++++++++++++++++++++++++
    }

    //�V�[�����I����
    public void StartCountDown()
    {
        timerText.enabled = true;
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 1.0f, 0.7f);
    }

    //�J�E���g�_�E������
    void CountDownTimer()
    {
        if(!isLottery)
        {
            LotteryGame();
        }

        //�J�E���g�_�E�����ă^�C�}�[�̃e�L�X�g�ɕb����ݒ�
        timer--;
        timerText.text = timer.ToString();
        if(timerText.text == "2")
        {
            timerText.color = Color.yellow;
        }
        else if(timerText.text == "1")
        {
            timerText.color = Color.red;
        }

        //timer��0�ɂȂ�����I��
        if (timer == 0)
        {
            //Invoke����߂�
            CancelInvoke();
            timerText.text = "GO!!";
            Invoke("TextDestroy", 0.7f);
        }
    }

    void LotteryGame()
    {
        //1�`3�̐����������_���ő���B�����Œ��I
        randScene = Random.Range(1, 3);

        if (randScene == 1)
        {
            slideGame.SetActive(true);
        }
        else if (randScene == 2)
        {
            ochaGame.SetActive(true);
            ochaGameImg.SetActive(true);
        }
        else if (randScene == 3)
        {
            rouletteGame.SetActive(true);
        }

        isLottery = true;
    }
    void TextDestroy()
    {
        Destroy(timerText);
    }
}
