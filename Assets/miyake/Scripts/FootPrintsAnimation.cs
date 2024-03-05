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
    bool isMove;                            //���������ǂ������ʂ���ϐ�

    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
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
        if (isMove)
        {
            leftFootPrint.transform.position = new Vector3(-0.2f, 1.2f, -1);
            rightFootPrint.transform.position = new Vector3(0.2f, 0.7f, -1);

            isMove = false;
        }
        else
        {
            leftFootPrint.transform.position = new Vector3(-0.3f, 0.9f, -1);
            rightFootPrint.transform.position = new Vector3(0.3f, 1.12f, -1);

            isMove = true;
        }
    }
}
