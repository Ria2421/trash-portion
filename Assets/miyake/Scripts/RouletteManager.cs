//==============================================
//Autor:�O����l
//Day:2/28
//���[���b�g����
//==============================================
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    public float rouletteSpeed = 0;        //��]���x
    public GameObject verygood;
    public GameObject good;
    public GameObject bad;
    public GameObject roulette;            //���[���b�g�{��
    public Text timerText;
    float angle = 0;                       //��]�̊p�x�̕ϐ�
    bool endCountDown;

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�����[�g��60�ɌŒ�
        Application.targetFrameRate = 60;
        endCountDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerText.text == "GO!!")
        {
            endCountDown = true;
        }
        if (endCountDown) 
        { 
            //���[���b�g����]
            transform.Rotate(0, 0, rouletteSpeed);

            if (Input.GetMouseButtonDown(0))
            {//���N���b�N���ꂽ��
                rouletteSpeed = 0;
                Judge();
            }
        }
    }

    //���菈��
    void Judge()
    {

        angle = roulette.transform.eulerAngles.z;

        //�听��

        float angleA = (154 + angle) % 360;
        float angleB = (186 + angle) % 360;
        float angleC = (110 + angle) % 360;
        float angleD = (230 + angle) % 360;

        if (angleA > angleB)
        {//360�x�𒴂��Ă�����
            if ((angleA <= transform.eulerAngles.z && transform.eulerAngles.z <= 360) || (0 <= transform.eulerAngles.z && transform.eulerAngles.z <= angleB))
            {
                verygood.SetActive(true);

                return;
            }
        }
        else
        {
            if (transform.eulerAngles.z >= angleA && transform.eulerAngles.z <= angleB)
            {
                verygood.SetActive(true);
                return;
            }
        }


        //����
        if (angleC > angleD)
        {
            if ((angleC <= transform.eulerAngles.z && transform.eulerAngles.z <= 360) || (0 <= transform.eulerAngles.z && transform.eulerAngles.z <= angleD))
            {
                good.SetActive(true);
                return;
            }
        }
        else
        {
            if (transform.eulerAngles.z >= angleC && transform.eulerAngles.z <= angleD)
            {
                good.SetActive(true);
                return;
            }
        }
        
        bad.SetActive(true);
    }
}
