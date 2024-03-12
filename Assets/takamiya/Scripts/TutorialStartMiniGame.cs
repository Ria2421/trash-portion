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
    //int gameNum = 3;          //�����_���Ń~�j�Q�[���̒��I���邽�߂̕ϐ�
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
        GameObject parentObject = GameObject.Find("MiniGameCanvas");
        countDownObject = parentObject.transform.Find("CountDown").gameObject;

        //tutorialTestTube = GameObject.Find("OchaGame").GetComponent<TutorialTestTubeManager>();
       GameObject ochaGameObject = parentObject.transform.Find("OchaGame").gameObject;
       GameObject  SliderObject = ochaGameObject.transform.Find("Slider").gameObject;
        tutorialTestTube = SliderObject.GetComponent<TutorialTestTubeManager>();

        GameObject testTube2SliderObject = ochaGameObject.transform.Find("TestTube2Slider").gameObject;
        tutorialSadTestTube = testTube2SliderObject.GetComponent<TutorialSadTestTube>();

        GameObject miniGamesObject = GameObject.Find("MiniGames");
        GameObject rouletteGameObject = miniGamesObject.transform.Find("RouletteGame").gameObject;
        GameObject rouletteManagerObject = rouletteGameObject.transform.Find("RouletteManager").gameObject;
        tutorialRouletteManager = rouletteManagerObject.GetComponent<TutorialRouletteManager>();

        tutorialMiniGame = GameObject.Find("MiniGameManager").GetComponent<StartMiniGame>();

        Init();
    }
    public void Init()
    {
        isLottery = false;
        timer = 3;

        AgainButton.SetActive(false);
        NextButton.SetActive(false);
        countDownObject.SetActive(true);
        timerText.text = timer.ToString();
        timerText.color = new Color(0,255,253,255);
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
        tutorialMiniGame.BackTitleButton.SetActive(false);
        tutorialMiniGame.AgainButton.SetActive(false);
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
            if(gameNum == GAMEMODE.SLIDE_MODE)
            {
                tutorialBarManager.endCountDown = true;
            }
            else if(gameNum == GAMEMODE.OCHA_MODE)
            {
                tutorialTestTube.endCountDown = true;
                tutorialSadTestTube.endCountDown = true;
            }
            else if(gameNum == GAMEMODE.ROULETTE_MODE)
            {
                tutorialRouletteManager.endCountDown = true;
            }
            Invoke("TextDestroy", 0.7f);
        }
    }

    void LotteryGame()
    { 
        switch(gameNum)
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
    void TextDestroy()
    {
        //Destroy(timerText);
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
                //slideGame.SetActive(false);
                //ochaGame.SetActive(true);
                //ochaGameImg.SetActive(true);
                //tutorialBarManager.good.SetActive(false);
                //tutorialBarManager.veryGood.SetActive(false);
                //tutorialBarManager.Bad.SetActive(false);
                //NextButton.SetActive(false);
                //AgainButton.SetActive(false);
               tutorialBarManager.DestroyMiniGame();
                //tutorialTestTube.Init();
                gameNum++;
                Init();
                break;

            case GAMEMODE.OCHA_MODE://������Q�[���̏ꍇ
                //ochaGame.SetActive(false);
                //ochaGameImg.SetActive(false);
                //rouletteGame.SetActive(true);
                //tutorialTestTube.good.SetActive(false);
                //tutorialTestTube.veryGood.SetActive(false);
                //tutorialTestTube.Bad.SetActive(false);
                //NextButton.SetActive(false);
                //AgainButton.SetActive(false);
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
