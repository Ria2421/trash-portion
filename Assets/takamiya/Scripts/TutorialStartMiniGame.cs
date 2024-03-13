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
    bool isLottery;             //���I������
    public GameObject slideGame;        //�~�j�Q�[��1
    public GameObject rouletteGame;     //�~�j�Q�[��2
    public GameObject ochaGame;         //�~�j�Q�[��3
    public GameObject ochaGameImg;      //�~�j�Q�[��3�̉摜

    public GameObject AgainButton; //������x�{�^��
    public GameObject NextButton;  //���̃{�^��
    public GameObject BackTitleButton;//�^�C�g���ɖ߂�{�^��

    TutorialBarManager tutorialBarManager;

    TutorialTestTubeManager tutorialTestTube;

    TutorialSadTestTube tutorialSadTestTube;

    TutorialRouletteManager tutorialRouletteManager;

    TutorialStartRoulette tutorialStartRoulette;

    StartMiniGame tutorialMiniGame;

    GameObject countDownObject;

    public enum GAMEMODE
    {
        NONE = 0,
        SLIDE_MODE,//�X���C�h�~�j�Q�[�����[�h
        OCHA_MODE,//������~�j�Q�[�����[�h
        ROULETTE_MODE,//���[���b�g�~�j�Q�[�����[�h
    }

    public GAMEMODE gameNum;

    void Start()
    {
        gameNum = GAMEMODE.SLIDE_MODE;

        //MiniGameCanvas�I�u�W�F�N�g���擾
        GameObject parentObject = GameObject.Find("MiniGameCanvas");
        //CountDown�I�u�W�F�N�g���擾
        countDownObject = parentObject.transform.Find("CountDown").gameObject;

        //OchaGame�I�u�W�F�N�g���擾
        GameObject ochaGameObject = parentObject.transform.Find("OchaGame").gameObject;
        //Slider�I�u�W�F�N�g���擾
        GameObject SliderObject = ochaGameObject.transform.Find("Slider").gameObject;

        //Slider�I�u�W�F�N�g�ɓ����Ă���R���|�[�l���g���擾
        tutorialTestTube = SliderObject.GetComponent<TutorialTestTubeManager>();

        //TestTube2Slider�I�u�W�F�N�g���擾
        GameObject testTube2SliderObject = ochaGameObject.transform.Find("TestTube2Slider").gameObject;

        //TestTube2Slider�I�u�W�F�N�g�ɓ����Ă���R���|�[�l���g���擾
        tutorialSadTestTube = testTube2SliderObject.GetComponent<TutorialSadTestTube>();

        //MiniGames�I�u�W�F�N�g���擾
        GameObject miniGamesObject = GameObject.Find("MiniGames");
        //RouletteGame�I�u�W�F�N�g���擾
        GameObject rouletteGameObject = miniGamesObject.transform.Find("RouletteGame").gameObject;
        //Roulette�I�u�W�F�N�g���擾
        GameObject rouletteObject = rouletteGameObject.transform.Find("Roulette").gameObject;
        //RouletteManager�I�u�W�F�N�g���擾
        GameObject rouletteManagerObject = rouletteGameObject.transform.Find("RouletteManager").gameObject;

        //RouletteManager�I�u�W�F�N�g�ɓ����Ă���R���|�[�l���g���擾
        tutorialRouletteManager = rouletteManagerObject.GetComponent<TutorialRouletteManager>();
        //Roulette�I�u�W�F�N�g�ɓ����Ă���R���|�[�l���g���擾
        tutorialStartRoulette = rouletteObject.GetComponent<TutorialStartRoulette>();
        //MiniGameManager�I�u�W�F�N�g���擾
        tutorialMiniGame = GameObject.Find("MiniGameManager").GetComponent<StartMiniGame>();

        Init();
    }
    public void Init()
    {
        isLottery = false;
        timer = 3;
        
        AgainButton.SetActive(false);//������x�{�^�����\���ɂ���
        NextButton.SetActive(false);//���փ{�^�����\���ɂ���
        countDownObject.SetActive(true);
        timerText.text = timer.ToString();
        timerText.color = new Color(0, 255, 253, 255);
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 1.0f, 0.7f);
    }
    /// <summary>
    /// ������x�~�j�Q�[�����J��Ԃ�
    /// </summary>
    public void Retry()
    {
        switch (gameNum)
        {
            case GAMEMODE.SLIDE_MODE:
                tutorialBarManager.Init();
                Init();
                break;
            case GAMEMODE.OCHA_MODE:
                tutorialTestTube.Init();
                tutorialSadTestTube.Init();
                Init();
                break;
            case GAMEMODE.ROULETTE_MODE:
                tutorialRouletteManager.Init();
                Init();
                break;

        }
        tutorialMiniGame.NextButton.SetActive(false);//���փ{�^�����\���ɂ���
        tutorialMiniGame.BackTitleButton.SetActive(false);//�^�C�g���ɖ߂�{�^�����\���ɂ���
        tutorialMiniGame.AgainButton.SetActive(false);//������x�{�^�����\���ɂ���
    }

    //�J�E���g�_�E������
    void CountDownTimer()
    {
        if (!isLottery)
        {
            LotteryGame();
        }

        //�J�E���g�_�E�����ă^�C�}�[�̃e�L�X�g�ɕb����ݒ�
        timer--;
        timerText.text = timer.ToString();
        if (timerText.text == "2")
        {
            timerText.color = Color.yellow;//�e�L�X�g�J���[�����F�ɕς���
        }
        else if (timerText.text == "1")
        {
            timerText.color = Color.red;//�e�L�X�g�J���[��Ԃɕς���
        }

        //timer��0�ɂȂ�����I��
        if (timer == 0)
        {
            //Invoke����߂�
            CancelInvoke();
            timerText.text = "GO!!";
            if (gameNum == GAMEMODE.SLIDE_MODE)
            {
                tutorialBarManager.endCountDown = true;
            }
            else if (gameNum == GAMEMODE.OCHA_MODE)
            {
                tutorialTestTube.endCountDown = true;
                tutorialSadTestTube.endCountDown = true;
            }
            else if (gameNum == GAMEMODE.ROULETTE_MODE)
            {
                tutorialRouletteManager.endCountDown = true;
                tutorialStartRoulette.LotteryAngle();
            }
            Invoke("TextDestroy", 0.7f);
        }
    }

    void LotteryGame()
    {
        switch (gameNum)
        {
            case GAMEMODE.SLIDE_MODE://�X���C�h�Q�[���̏ꍇ
                slideGame.SetActive(true);
                tutorialBarManager = GameObject.Find("Slider").GetComponent<TutorialBarManager>();
                break;

            case GAMEMODE.OCHA_MODE://������Q�[���̏ꍇ
                ochaGame.SetActive(true);
                ochaGameImg.SetActive(true);
                break;

            case GAMEMODE.ROULETTE_MODE://���[���b�g�Q�[���̏ꍇ
                rouletteGame.SetActive(true);
                break;

            default:
                break;
        }

        isLottery = true;
    }
    /// <summary>
    /// �J�E���g�_�E���I�u�W�F�N�g���\���ɂ���
    /// </summary>
    void TextDestroy()
    {
        countDownObject.SetActive(false);
    }

    /// <summary>
    /// ���̃~�j�Q�[���ɍs�����̏���
    /// </summary>
    public void NextGameButton()
    {
        switch (gameNum)
        {
            case GAMEMODE.SLIDE_MODE://�X���C�h�Q�[���̏ꍇ
                tutorialBarManager.DestroyMiniGame();
                gameNum++;
                Init();
                break;

            case GAMEMODE.OCHA_MODE://������Q�[���̏ꍇ
                tutorialTestTube.DestroyMiniGame();
                gameNum++;
                Init();
                break;

            case GAMEMODE.ROULETTE_MODE://���[���b�g�Q�[���̏ꍇ
                Initiate.Fade("Title", Color.black, 1.0f);
                break;

            default:
                break;
        }
        tutorialMiniGame.NextButton.SetActive(false);
        tutorialMiniGame.AgainButton.SetActive(false);
    }
}
