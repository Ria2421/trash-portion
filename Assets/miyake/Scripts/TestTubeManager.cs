//==============================================
//Autor:�O����l
//Day:3/1
//���~�߃Q�[������
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTubeManager : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject good;
    public GameObject veryGood;
    public GameObject Bad;
    public Slider slider;
    public Text timerText;
    private bool maxValue;
    bool endCountDown;

    void Start()
    {
        slider.value = 0;
        endCountDown = false;
    }

    void Update()
    {
        if (timerText.text == "GO!!")
        {
            endCountDown = true;
        }

        if (endCountDown)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (slider.value >= 94)
                {
                    Bad.SetActive(true);
                }
                else if (slider.value >= 68 && slider.value < 84)
                {
                    good.SetActive(true);
                }
                else if (slider.value >= 84 && slider.value < 94)
                {
                    veryGood.SetActive(true);
                }
                else if(slider.value < 68)
                {
                    Bad.SetActive(true);
                }

                // �~�j�Q�[���̏I��
                Invoke("MiniGameDestroy", 1f);
            }

            //�N���b�N����Ă��Ȃ���Ύ��s
            if (Input.GetMouseButton(0))
            {
                slider.value += speed;

                if (slider.value >= 94)
                {
                    Bad.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// �~�j�Q�[���̔j��
    /// </summary>
    private void MiniGameDestroy()
    {
        // �~�j�Q�[�����I��
        Destroy(GameObject.Find("MiniGames(Clone)"));
    }
}
