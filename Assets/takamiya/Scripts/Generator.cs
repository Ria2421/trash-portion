//======================================================
//�|�[�V�������ォ��~�炷
//Author�F���{�S��
//Date/2/20
//
//=======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // �t�B�[���h ----------------------------------------

    /// <summary>
    /// ����������|�[�V�����̃v���n�u
    /// </summary>
    public GameObject[] falls;

    /// <summary>
    /// ���[�h�I��UI
    /// </summary>
    [SerializeField] GameObject selectUI;

    /// <summary>
    /// �I��ҋ@UI
    /// </summary>
    [SerializeField] GameObject waitUI;

    //------------------------------------------------------------------------------
    // ���\�b�h ------------------------------------------

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        if(NetworkManager.MyNo == 1)
        {   // 1P�̎��͑I����ʂ�\��
            selectUI.SetActive(true);
            waitUI.SetActive(false);
        }
        else
        {   // 1P�ȊO�͑ҋ@��ʂ�\��
            selectUI.SetActive(false);
            waitUI.SetActive(true);
        }
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (Time.frameCount % 30 == 0)
        {   // �w�肵���Ԋu�Ń|�[�V�����̐���
            GameObject potion = Instantiate(
                falls[Random.Range(0, falls.Length)],
                new Vector3(Random.Range(-14f, 14f), transform.position.y, transform.position.z),
                Quaternion.identity
                );

            // ��莞�Ԍ�Ƀ|�[�V�����̔j��
            Destroy(potion, 8f);
        }
    }
}
