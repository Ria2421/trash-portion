//===================================================
//
//�v���C���[�̐���
//Author�F���{�S��
//Date:3/1
//Update/3/5
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
    [SerializeField] GameObject rankObject;//�\�����郉���N�I�u�W�F�N�g
    [SerializeField] GameObject congratulationObj;//�\������congratulation�I�u�W�F�N�g


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

        //�����N�I�u�W�F�N�g����芴�o�Ŋg��A�k������
        rankObject.transform
             .DOScale(new Vector3(1.5f, 1.5f, 0.5f), 0.5f)
             .SetLoops(-1, LoopType.Yoyo)//�J��Ԃ��ݒ肷��
             .SetEase(Ease.Linear);//���̊��o�œ������Ă���

       //conguratulation�I�u�W�F�N�g����芴�o�Ŋg��A�k������
        congratulationObj.transform
             .DOScale(new Vector3(1.5f, 1.5f, 0.5f), 0.5f)
             .SetLoops(-1, LoopType.Yoyo)//�J��Ԃ��ݒ肷��
             .SetEase(Ease.Linear);//���̊��o�œ������Ă���

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
