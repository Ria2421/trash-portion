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
    public GameObject leftFootPrint;        //����
    public GameObject rightFootPrint;       //�E��

    // Start is called before the first frame update
    void Start()
    {
        //0.7�b�Ԋu�ŌĂ΂ꑱ����
        InvokeRepeating("Animation",1.0f,0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�A�j���[�V��������
    void Animation()
    {
        if(leftFootPrint.activeSelf)
        {//�������\������Ă���Ƃ�
            leftFootPrint.SetActive(false);
            rightFootPrint.SetActive(true);
        }
        else
        {
            leftFootPrint.SetActive(true);
            rightFootPrint.SetActive(false);
        }
    }
}
