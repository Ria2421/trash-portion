//==============================================
//Autor:�O����l
//Day:3/5
//�X���C�_�[�i�o�[�j�Q�[������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBarManager : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject good;
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    private bool maxValue;
    private bool isClicked;
    private bool _endCountDown;
    StartMiniGame tutorialMiniGame;
    [SerializeField] AudioClip veryGoodSE;      //�听��SE
    [SerializeField] AudioClip goodSE;          //����SE
    [SerializeField] AudioClip badSE;           //���sSE
    [SerializeField] AudioSource audioSource;
    public bool endCountDown {
        get
        {
            return _endCountDown;
        }
        set
        {
            _endCountDown = value;
        }
    }

    void Start()
    {
        tutorialMiniGame = GameObject.Find("MiniGameManager").GetComponent<StartMiniGame>();
        Init();
    }
    public void Init()
    {
        slider.value = 0;
        maxValue = false;
        isClicked = false;
        endCountDown = false;
        veryGood.SetActive(false);
        good.SetActive(false);
        Bad.SetActive(false);

        tutorialMiniGame.gameNum = StartMiniGame.GAMEMODE.SLIDE_MODE;
    }

    void Update()
    {
        if (endCountDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;

                if (slider.value >= 85)
                {
                    //�听��SE
                    audioSource.PlayOneShot(veryGoodSE);

                    veryGood.SetActive(true);
                }
                else if (slider.value >= 50)
                {
                    //����SE
                    audioSource.PlayOneShot(goodSE);

                    good.SetActive(true);
                }
                else if (slider.value < 50)
                {
                    //���sSE
                    audioSource.PlayOneShot(badSE);

                    Bad.SetActive(true);
                }
                tutorialMiniGame.NextButton.SetActive(true);
                tutorialMiniGame.AgainButton.SetActive(true);
                // �~�j�Q�[���̏I��
                Invoke("MiniGameDestroy", 1f);
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
    public void DestroyMiniGame()
    {
        // �~�j�Q�[�����I��
        veryGood.SetActive(false);
        good.SetActive(false);
        Bad.SetActive(false);
        Destroy(GameObject.Find("Slidegame"));
    }
}
