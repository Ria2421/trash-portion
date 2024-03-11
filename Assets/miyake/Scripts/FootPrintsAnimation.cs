//==============================================
//Autor:�O����l
//Day:3/5
//UI�̃A�j���[�V��������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintsAnimation : MonoBehaviour
{
    [SerializeField] GameObject footPrint1;  //��1
    [SerializeField] GameObject footPrint2;  //��2
    bool isMove;                             //���������ǂ������ʂ���ϐ�

    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
        //0.7�b�Ԋu�ŌĂ΂ꑱ����
        InvokeRepeating("Animation",1.0f,0.7f);
    }

    //�A�j���[�V��������
    void Animation()
    {
        if (isMove)
        {
            footPrint1.SetActive(true);
            footPrint2.SetActive(false);

            isMove = false;
        }
        else
        {
            footPrint1.SetActive(false);
            footPrint2.SetActive(true);

            isMove = true;
        }
    }
}
