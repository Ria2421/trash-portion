using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NextImage : MonoBehaviour
{
    //[SerializeField] GameObject[] slides;//inspector����\���������摜��\����Image�I�u�W�F�N�g���h���b�O���h���b�v�Őݒ�


    //int head = 0;//�������ڂ�\�����Ă���̂��Ƃ����ϐ�


    // Start is called before the first frame update
    void Start()
    {
        //if (slides.Length != head)
        //{
        //    slides[head].SetActive(true);//�ꖇ�ڂ�\��
        //}
        //for (int i = head + 1; i < slides.Length; i++)
        //{
        //    slides[i].SetActive(false);//���̃X���C�h���\��
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    slides[head].SetActive(false);//���\�����Ă�����̂��\��
        //    if (slides.Length != ++head)//�����X���C�h���܂�����Ȃ�
        //        slides[head].SetActive(true);//���̃X���C�h��\��
        //}
    }
}
