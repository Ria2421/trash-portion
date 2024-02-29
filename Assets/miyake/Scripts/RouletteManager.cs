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
    public float rouletteSpeed = 0;        //��]���x
    public GameObject verygood;
    public GameObject good;
    public GameObject bad;
    public GameObject roulette;            //���[���b�g�{��
    float angle = 0;                       //��]�̊p�x�̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�����[�g��60�ɌŒ�
        Application.targetFrameRate = 60;

        angle = roulette.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        //���[���b�g����]
        transform.Rotate(0, 0, rouletteSpeed);

        if (Input.GetMouseButtonDown(0))
        {//���N���b�N���ꂽ��
            rouletteSpeed = 0;
            Judge();
        }
    }

    //���菈��
    void Judge()
    {
        //�听��
        if(transform.eulerAngles.z >= 154 + angle && transform.eulerAngles.z  <= 186 + angle)
        {
            verygood.SetActive(true);
        }

        //����
        else if(transform.eulerAngles.z  >= 185 + angle && transform.eulerAngles.z  <= 230 + angle ||  transform.eulerAngles.z  >= 110 + angle && transform.eulerAngles.z  <= 155 + angle)
        {
            good.SetActive(true);
        }

        else
        {
            bad.SetActive(true);
        }
    }
}
