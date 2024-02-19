//---------------------------------------------------------------
//
//  �Ƃ������ہ[�����I�N���C�A���g [ Trash_Portion_Server ]
// Author:Kenta Nakamoto
// Data 2024/02/08
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System;
using UnityEngine.UI;
using System.Linq;

public class Client : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // �t�B�[���h ----------------------------------------

    /// <summary>
    /// �N���C�A���g�쐬
    /// </summary>
    private TcpClient tcpClient;

    /// <summary>
    /// �ڑ���IP�A�h���X
    /// </summary>
    private const string ipAddress = "127.0.0.1";

    /// <summary>
    /// ����M�T�C�Y
    /// </summary>
    private const int dataSize = 1024;

    /// <summary>
    /// �ڑ����i�[�p
    /// </summary>
    private NetworkStream stream;

    /// <summary>
    /// �����̃v���C���[�ԍ�
    /// </summary>
    private int myNo;

    /// <summary>
    /// �ڑ����̕\���e�L�X�g
    /// </summary>
    [SerializeField] Text connectText;

    /// <summary>
    /// �v���C���[�ԍ��\���e�L�X�g
    /// </summary>
    [SerializeField] Text playerText;

    /// <summary>
    /// ���O���͗pUI
    /// </summary>
    [SerializeField] InputField nameInput;

    //------------------------------------------------------------------------------
    // ���\�b�h ------------------------------------------

    // Start is called before the first frame update
    async void Start()
    {
        // �N���C�A���g����
        await StartClient(ipAddress, 20001);
    }

    /// <summary>
    /// �N���C�A���g�ڑ�����
    /// </summary>
    /// <param name="ipaddress"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    private async Task StartClient(string ipaddress, int port)
    {
        // �T�[�o�[�֐ڑ�
        try
        {
            //�N���C�A���g�쐬
            tcpClient = new TcpClient();

            // ����M�^�C���A�E�g�ݒ� (msec)
            tcpClient.SendTimeout = 1000;
            tcpClient.ReceiveTimeout = 1000;

            // �T�[�o�[�֐ڑ��v��
            await tcpClient.ConnectAsync(ipaddress, port);
            connectText.text = "�ڑ�����";

            // �T�[�o�[����PL�ԍ�����M�ҋ@
            byte[] recvBuffer = new byte[dataSize];                                    // ����M�f�[�^�i�[�p
            stream = tcpClient.GetStream();                                            // �N���C�A���g�̃f�[�^����M�Ɏg��NetworkStream���擾
            int length = await stream.ReadAsync(recvBuffer, 0, recvBuffer.Length);     // ��M�f�[�^�̃o�C�g�����擾

            // ��M�f�[�^����C�x���gID�����o��
            int eventID = recvBuffer[0];

            // ��M�f�[�^���當��������o��
            byte[] bufferJson = recvBuffer.Skip(1).ToArray();                          // 1�o�C�g�ڂ��X�L�b�v
            string recevieString = Encoding.UTF8.GetString(recvBuffer, 0, length);     // ��M�f�[�^�𕶎���ɕϊ�

            // ��P���\��
            playerText.text = "���Ȃ���" + recevieString + "P�ł�";

            //// �ڑ������̎�M�҂�
            //length = await stream.ReadAsync(buffer, 0, buffer.Length);      // ��M�f�[�^�̃o�C�g�����擾
            //recevieString = Encoding.UTF8.GetString(buffer, 0, length);     // ��M�f�[�^�𕶎���ɕϊ�

            //if(recevieString == "����")
            //{
            //    /* �t�F�[�h���� (��)  
            //        ( "�V�[����",�t�F�[�h�̐F, ����);  */
            //    Initiate.DoneFading();
            //    Initiate.Fade("NextScene", Color.black, 1.5f);
            //}
        }
        catch (Exception ex)
        {
            // �G���[������
            Debug.Log(ex);
            connectText.text = "�ڑ����s";
        }
    }

    public async void sendUserData()
    {
        // ���M�p�f�[�^�̍쐬
        UserData userData = new UserData();
        userData.UserName = nameInput.text;   // ���͂��ꂽ���O���i�[
        userData.PlayerNo = myNo;             // �����̃v���C���[�ԍ����i�[

        // ���M�f�[�^��JSON�V���A���C�Y
        string json = JsonConvert.SerializeObject(userData);

        // ���M����
        byte[] buffer = Encoding.UTF8.GetBytes(json);         // JSON��byte�ɕϊ�
        await stream.WriteAsync(buffer, 0, buffer.Length);    // JSON���M����

        // ���M�ҋ@�����\��
        connectText.text = "���M��...";


    }
}
