//==============================================
//Autor:�O����l
//Day:2/28
//���[���b�g����
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteManager : MonoBehaviour
{
    public float rouletteSpeed = 0;        //���[���b�g�̉�]���x

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�����[�g��60�ɌŒ�
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //
        transform.Rotate(0, 0, rouletteSpeed);
    }
}
