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
    private bool isClicked;

    void Start()
    {
        slider.value = 94;
        isClicked = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }

        //�N���b�N����Ă��Ȃ���Ύ��s
        if (!isClicked)
        {
            slider.value -= 0.2f;
        }
    }
}
