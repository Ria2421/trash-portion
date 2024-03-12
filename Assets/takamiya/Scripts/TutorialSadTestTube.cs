//==============================================
//Autor:�O����l
//Day:3/4
//���~�߃Q�[���̓����|�[�V�����̕��̌�������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSadTestTube : MonoBehaviour
{
    [SerializeField] float speed;
    public Slider slider;
    private bool isClicked;
    private bool _endCountDown;

    public bool endCountDown
    {
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
        Init();
    }
    public void Init()
    {
        slider.value = 94;
        isClicked = false;
        endCountDown = false;
    }

    void Update()
    {
        if (endCountDown)
        {
            if (isClicked == false)
            {
                //�N���b�N�𗣂�����
                if (Input.GetMouseButtonUp(0))
                {
                    isClicked = true;
                }


                //����������Ă���Ԏ��s
                if (Input.GetMouseButton(0))
                {
                    slider.value -= speed;
                }
            }
        }
    }
}
