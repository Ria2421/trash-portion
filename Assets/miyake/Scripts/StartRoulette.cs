//==============================================
//Autor:�O����l
//Day:2/29
//���[���b�g��]����
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRoulette : MonoBehaviour
{
    public Text timerText;
    float randAngle = 0;        //�����_���ŉ�]����p�x�̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
            randAngle = Random.Range(-180, 180);

            transform.eulerAngles = new Vector3(90, 0, randAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
