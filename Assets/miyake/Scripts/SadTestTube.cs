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
    [SerializeField] float speed;
    public Slider slider;
    public Text timerText;
    private bool isClicked;
    bool endCountDown;

    void Start()
    {
        slider.value = 94;
        isClicked = false;
        endCountDown = false;
    }

    void Update()
    {
        if (timerText.text == "GO!!")
        {
            endCountDown = true;
        }

        if (endCountDown)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isClicked = true;
            }

            //�N���b�N����Ă��Ȃ���Ύ��s
            if (Input.GetMouseButton(0))
            {
                slider.value -= speed;
            }
        }
    }
}
