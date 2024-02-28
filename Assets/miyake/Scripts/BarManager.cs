using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public GameObject good;
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    private bool maxValue;
    private bool isClicked;

    void Start()
    {
        slider.value = 0;
        maxValue = false;
        isClicked = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;

            if (slider.value >= 85)
            {
                veryGood.SetActive(true);
            }
            else if (slider.value >= 50)
            {
                good.SetActive(true);
            }

            else if (slider.value < 50)
            {
                Bad.SetActive(true);
            }
        }

        //�N���b�N����Ă��Ȃ���Ύ��s
        if (!isClicked)
        {
            //�ő�l�ɒB�����ꍇ�ƁA�ŏ��l�ɖ߂����ꍇ�̃t���O�ؑւ�
            if (slider.value == slider.maxValue)
            {
                maxValue = true;
            }

            if (slider.value == slider.minValue)
            {
                maxValue = false;
            }

            //�t���O�ɂ��X���C�_�[�l�̑���
            if (maxValue)
            {
                slider.value -= 0.5f;
            }
            else
            {
                slider.value += 0.5f;
            }
        }
    }
}
