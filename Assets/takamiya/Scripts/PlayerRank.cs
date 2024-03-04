//===================================================
//
//�v���C���[�̐���
//Author�F���{�S��
//Date:3/1
//Update/3/4
//
//====================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRank : MonoBehaviour
{
    [SerializeField] GameObject[] characterPrefabs; // �\������L�����N�^�[�̃v���n�u
    [SerializeField] Text rankText; //���ʂ�\������UI

    // Start is called before the first frame update
    void Start()
    {
        // �����_���ȃv���C���[�̐�������
        var rand = new System.Random(); //�����_���錾
        int playerCount = rand.Next(1, 5);//1����4�̃����_��

        // �v���C���[�̐������J��Ԃ��A�L�����N�^�[�𐶐�
        for (int i = 0; i < playerCount; ++i)
        {
            // �v���n�u���w��̈ʒu�ɐ������A180�x��]������
            Instantiate(characterPrefabs[i], new Vector3(i * 1.5f, 0f,-5f), Quaternion.Euler(0f, 180f, 0f));
        }
        // �����N���e�L�X�gUI�ɕ\��
        rankText.text ="1��";

        //�����N�e�L�X�g��芴�o�Ŋg��A�k������
        rankText.transform
             .DOScale(new Vector3(1.5f, 1.5f, 0.5f), 0.5f)
             .SetLoops(-1, LoopType.Yoyo)//�J��Ԃ��ݒ肷��
             .SetEase(Ease.Linear);//���̊��o�œ������Ă���

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
