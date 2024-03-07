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
    public GameObject good;
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    private bool maxValue;
    private bool isClicked;
    bool endCountDown;
    public Text limitTime;              //�~�j�Q�[���̐�������
    int limit;                          //�������Ԃ̕ϐ�
    bool isLimit;                     //�������Ԃ𒴂������ǂ���

    void Start()
    {
        slider.value = 0;
        maxValue = false;
        isClicked = false;
        endCountDown = false;
        limit = 5;
        isLimit = false;
        limitTime.enabled = false;
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 3.0f, 1.0f);
    }

    void Update()
    {
        if (!isLimit)
        {
            if (timerText.text == "GO!!")
            {
                endCountDown = true;
                limitTime.enabled = true;
            }

            if (endCountDown)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CancelInvoke();
                    isClicked = true;

                    if (slider.value >= 85)
                    {
                        veryGood.SetActive(true);
                    }
                    else if (slider.value >= 50)
                    {
                        good.SetActive(true);
                    }

                    else if (slider.value < 50)
                    {
                        Bad.SetActive(true);
                    }
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
                        slider.value -= 0.5f;
                    }
                    else
                    {
                        slider.value += 0.5f;
                    }
                }
            }
        }
    }

    void CountDownTimer()
    {
        limit--;
        limitTime.text = limit.ToString();
        if(limitTime.text == "-1")
        {
            Bad.SetActive (true);
            isLimit = true;
            CancelInvoke();
            Destroy(limitTime);
        }
    }
}
