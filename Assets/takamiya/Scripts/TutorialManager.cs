//==================================================
//
//�`���[�g���A���̎���
//Author�F���{�S��
//date:3/7
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    //�\������摜������
    [SerializeField] GameObject[] TutorialImages;

    int imageNum;//�������ڂ��\������Ă��邩�̕ϐ�




    // Start is called before the first frame update
    void Start()
    {
        imageNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N�������ꂽ��
        if (Input.GetMouseButtonDown(0))
        {
            //�Ō�̉摜���\������Ă�����
            if (imageNum == TutorialImages.Length - 1)
            {
                Initiate.Fade("Title", Color.black, 1.0f);
            }
            else
            {
                //���\������Ă���摜���\���ɂ���
                TutorialImages[imageNum].SetActive(false);

                imageNum++;

                //���̉摜��\������
                TutorialImages[imageNum].SetActive(true);
            }
        }
        //�E�N���b�N�������ꂽ��
        else if(Input.GetMouseButtonDown(1))
        {
            //��ԍŏ��̉摜��������
            if (imageNum == 0)
            {
                Initiate.Fade("Title", Color.black, 1.0f);
            }
            else
            {
                //���\������Ă���摜���\���ɂ���
                TutorialImages[imageNum].SetActive(false);

                imageNum--;

                //�O�̉摜��\������
                TutorialImages[imageNum].SetActive(true);
            }
        }
    }
        
}
