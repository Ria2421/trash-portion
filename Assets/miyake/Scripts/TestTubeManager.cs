//==============================================
//Autor:�O����l
//Day:3/1
//���~�߃Q�[������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTubeManager : MonoBehaviour
{
    public GameObject good;
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    private bool maxValue;
    bool endCountDown;
    public Text limitTime;              //�~�j�Q�[���̐�������
    int limit;                          //�������Ԃ̕ϐ�
    bool isLimit;                     //�������Ԃ𒴂������ǂ���

    void Start()
    {
        slider.value = 0;
        endCountDown = false;
        limit = 5;
        limitTime.enabled = false;
        isLimit = false;
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
                if (Input.GetMouseButtonUp(0))
                {
                    CancelInvoke();

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
                }

                //�N���b�N����Ă��Ȃ���Ύ��s
                if (Input.GetMouseButton(0))
                {
                    slider.value += 0.2f;

                    if (slider.value >= 94)
                    {
                        Bad.SetActive(true);
                    }
                }
            }
        }
    }

    void CountDownTimer()
    {
        limit--;
        limitTime.text = limit.ToString();
        if (limitTime.text == "-1")
        {
            Bad.SetActive(true);
            isLimit = true;
            CancelInvoke();
            Destroy(limitTime);
        }
    }
}
