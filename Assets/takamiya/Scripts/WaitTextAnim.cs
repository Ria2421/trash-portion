//---------------------------------------------------------------
//
//  �ҋ@�e�L�X�g�A�j���[�V���� [ WaitTextAnim.cs ]
//  Author:Kenta Nakamoto
//  Data 2024/02/29
//  Update 2024/02/29
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitTextAnim : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // �t�B�[���h ----------------------------------------

    /// <summary>
    /// �J��Ԃ��Ԋu
    /// </summary>
    private float _repeatSpan;

    /// <summary>
    /// �o�ߎ���
    /// </summary>
    private float _timeElapsed;

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 0.5f;
        _timeElapsed = 0;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����
        if (_timeElapsed >= _repeatSpan)
        {   // ���Ԍo�߂Ńe�L�X�g�\��
            GetComponent<Text>().text = "���[�h�I��";
        }
        if (_timeElapsed >= _repeatSpan + 0.5f)
        {   // ���Ԍo�߂Ńe�L�X�g�\��(��E)
            GetComponent<Text>().text = "���[�h�I��.";
        }
        if (_timeElapsed >= _repeatSpan + 1.0f)
        {   // ���Ԍo�߂Ńe�L�X�g�\��(��E)
            GetComponent<Text>().text = "���[�h�I��..";
        }
        if (_timeElapsed >= _repeatSpan + 1.5f)
        {   // ���Ԍo�߂Ńe�L�X�g�\��(��E)
            GetComponent<Text>().text = "���[�h�I��...";
            _timeElapsed = 0;   //�o�ߎ��Ԃ����Z�b�g����
        }
    }
}
