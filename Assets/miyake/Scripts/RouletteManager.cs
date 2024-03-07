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
    public Text limitTime;              //�~�j�Q�[���̐�������
    int limit;                          //�������Ԃ̕ϐ�
    bool isLimit;                     //�������Ԃ𒴂������ǂ���

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�����[�g��60�ɌŒ�
        Application.targetFrameRate = 60;
        endCountDown = false;
        limit = 5;
        limitTime.enabled = false;
        isLimit = false;
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 3.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLimit)
        {
            if (timerText.text == "GO!!")
            {
                endCountDown = true;
                limitTime.enabled = true;
            }
            if (endCountDown)
            {
                //���[���b�g����]
                transform.Rotate(0, 0,rouletteSpeed);

                if (Input.GetMouseButtonDown(0))
                {//���N���b�N���ꂽ��
                    rouletteSpeed = 0;
                    Judge();
                    CancelInvoke();
                }
            }
        }
    }

    //���菈��
    void Judge()
    {

        angle = roulette.transform.eulerAngles.y;
        float angleA = (174 + angle) % 360;     //�听���̒[
        float angleB = (201 + angle) % 360;     //�听���̒[
        float angleC = (133 + angle) % 360;     //�����̒[
        float angleD = (244 + angle) % 360;     //�����̒[


        //�听��
        if (angleA > angleB)
        {//360�x�𒴂��Ă�����
            if ((angleA <= transform.eulerAngles.y && transform.eulerAngles.y <= 360) || (0 <= transform.eulerAngles.y && transform.eulerAngles.y <= angleB))
            {
                verygood.SetActive(true);

                return;
            }
        }
        else
        {
            if (transform.eulerAngles.y >= angleA && transform.eulerAngles.y <= angleB)
            {
                verygood.SetActive(true);
                return;
            }
        }


        //����
        if (angleC > angleD)
        {
            if ((angleC <= transform.eulerAngles.y && transform.eulerAngles.y <= 360) || (0 <= transform.eulerAngles.y && transform.eulerAngles.y <= angleD))
            {
                good.SetActive(true);
                return;
            }
        }
        else
        {
            if (transform.eulerAngles.y >= angleC && transform.eulerAngles.y <= angleD)
            {
                good.SetActive(true);
                return;
            }
        }
        
        bad.SetActive(true);
    }

    void CountDownTimer()
    {
        limit--;
        limitTime.text = limit.ToString();
        if (limitTime.text == "-1")
        {
            bad.SetActive(true);
            isLimit = true;
            CancelInvoke();
            Destroy(limitTime);
        }
    }
}
