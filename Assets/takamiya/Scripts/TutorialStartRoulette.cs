//==============================================
//Autor:�O����l
//Day:2/29
//���[���b�g��]����
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStartRoulette : MonoBehaviour
{
    float randAngle = 0;        //�����_���ŉ�]����p�x�̕ϐ�
   
    public void LotteryAngle()
    {
        randAngle = Random.Range(-180, 180);
        //randAngle = 180;

        transform.eulerAngles = new Vector3(0, 0, randAngle);
    }
}
