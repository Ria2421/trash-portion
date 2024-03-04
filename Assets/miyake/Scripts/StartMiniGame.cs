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
    bool endCountDown;          //�J�E���g�_�E�����I��������ǂ���
    public GameObject slideGame;        //�~�j�Q�[��1
    public GameObject rouletteGame;     //�~�j�Q�[��2
    public GameObject ochaGame;         //�~�j�Q�[��3

    void Start()
    {
        timer = 3;
        endCountDown = false;
    }

    //�V�[�����I����
    public void StartCountDown()
    {
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 1.0f, 1.0f);
    }

    //�J�E���g�_�E������
    void CountDownTimer ()
    {
        //�J�E���g�_�E�����ă^�C�}�[�̃e�L�X�g�ɕb����ݒ�
        timer--;
        timerText.text = timer.ToString();

        //timer��0�ɂȂ�����I��
        if (timer < 0)
        {
            //Invoke����߂�
            CancelInvoke();
            endCountDown=true;
            Destroy(timerText);
            LotteryGame();
        }
    }

    void LotteryGame()
    {
        if (endCountDown)
        {
            //1�`3�̐����������_���ő���B�ȉ��Œ��I
            randScene = Random.Range(1, 3);

            if (randScene == 1)
            {
                slideGame.SetActive(true);
            }
            else if (randScene == 2)
            {
                rouletteGame.SetActive(true);
            }
            else if (randScene == 3)
            {
                ochaGame.SetActive(true);
            }
        }
    }
}
