//==============================================
//Autor:�O����l
//Day:3/4
//���~�߃Q�[���̓����|�[�V�����̕��̌�������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SadTestTube : MonoBehaviour
{
    public Slider slider;
    public Text timerText;
    private bool isClicked;
    bool endCountDown;
    public Text limitTime;              //�~�j�Q�[���̐�������
    int limit;                          //�������Ԃ̕ϐ�
    bool isLimit;                     //�������Ԃ𒴂������ǂ���

    void Start()
    {
        slider.value = 94;
        isClicked = false;
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
                    isClicked = true;
                    CancelInvoke();
                }

                //�N���b�N����Ă��Ȃ���Ύ��s
                if (Input.GetMouseButton(0))
                {
                    slider.value -= 0.2f;
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
            isLimit = true;
            CancelInvoke();
            Destroy(limitTime);
        }
    }
}
