//======================================================
//�|�[�V�������ォ��~�炷
//Author�F���{�S��
//Date/2/20
//
//=======================================================
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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

    /// <summary>
    /// �{�^������������
    /// </summary>
    public async void ButtonPushSE()
    {
        // ���M����
        string json = "1";
        byte[] buffer = Encoding.UTF8.GetBytes(json);                      // JSON��byte�ɕϊ�
        buffer = buffer.Prepend((byte)EventID.InGameFlag).ToArray();       // ���M�f�[�^�̐擪�ɃC�x���gID��t�^
        NetworkStream stream = NetworkManager.MyTcpClient.GetStream();
        await stream.WriteAsync(buffer, 0, buffer.Length);                 // JSON���M����
#if DEBUG
        Debug.Log("�C���Q�[�����M");
#endif
    }
}
