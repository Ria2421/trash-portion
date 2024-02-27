//---------------------------------------------------------------
//
//  �Ƃ������ہ[�����I�N���C�A���g [ Client.cs ]
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
using System.Threading;

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
    /// �|�[�g�ԍ�
    /// </summary>
    private const int portNum = 20001;

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
    /// �S�v���C���[�̃f�[�^���X�g
    /// </summary>
    private UserDataList userDataList;

    /// <summary>
    /// ���O���͗pUI
    /// </summary>
    [SerializeField] GameObject nameInput;

    /// <summary>
    /// �}�b�`���O���e�L�X�g
    /// </summary>
    [SerializeField] GameObject matchingText;

    /// <summary>
    /// �ҋ@��UI
    /// </summary>
    [SerializeField] GameObject readyUI;

    /// <summary>
    /// ���@���w�����̃I�u�W�F�N�g
    /// </summary>
    [SerializeField] GameObject[] arrowYouObjs;

    /// <summary>
    /// �S�v���C���[�̖��O�i�[ 
    /// </summary>
    [SerializeField] GameObject[] playerNames;

    /// <summary>
    /// �S�v���C���[�̑ҋ@��Ԋi�[�p
    /// </summary>
    [SerializeField] GameObject[] playerStatus;

    /// <summary>
    /// ���������{�^��
    /// </summary>
    [SerializeField] GameObject completeButton;

    /// <summary>
    /// ���C���X���b�h�ɏ������s���˗��������
    /// </summary>
    SynchronizationContext context;

    //------------------------------------------------------------------------------
    // ���\�b�h ------------------------------------------

    // Start is called before the first frame update
    async void Start()
    {

        // �ڑ������̎��s
        await StartClient(ipAddress, portNum);
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
            await tcpClient.ConnectAsync(ipAddress, port);

            // ��M�p�X���b�h�̋N��
            Thread thread = new Thread(new ParameterizedThreadStart(RecvProc));
            thread.Start(tcpClient);

            // �T�[�o�[����PL�ԍ�����M�ҋ@
            byte[] recvBuffer = new byte[dataSize];                                    // ����M�f�[�^�i�[�p
            stream = tcpClient.GetStream();                                            // �N���C�A���g�̃f�[�^����M�Ɏg��NetworkStream���擾
            int length = await stream.ReadAsync(recvBuffer, 0, recvBuffer.Length);     // ��M�f�[�^�̃o�C�g�����擾

            // ��M�f�[�^����C�x���gID�����o��
            int eventID = recvBuffer[0];

            // ��M�f�[�^���當��������o��
            byte[] bufferJson = recvBuffer.Skip(1).ToArray();                            // 1�o�C�g�ڂ��X�L�b�v
            string recevieString = Encoding.UTF8.GetString(bufferJson, 0, length - 1);   // ��M�f�[�^�𕶎���ɕϊ�

            // ������PL No��ۑ�
            myNo = int.Parse(recevieString[0].ToString());

            // �}�b�`���O�e�L�X�g���\��
            matchingText.SetActive(false);

            // �ҋ@��UI��\��
            readyUI.SetActive(true);

            // ������PL�ԍ��̏�ɖ��\��
            arrowYouObjs[myNo - 1].SetActive(true);

#if DEBUG
            // �����{�^�����̗L����
            Debug.Log("�ҋ@��ʕ\��");
#endif
        }
        catch (Exception ex)
        {
            // �G���[������
            Debug.Log(ex);
        }
    }

    /// <summary>
    /// ��M�p�X���b�h
    /// </summary>
    /// <param name="arg"></param>
    async void RecvProc(object arg)
    {
        TcpClient tcpClient = (TcpClient)arg;

        NetworkStream stream = tcpClient.GetStream();

        while (true)
        {
            // ��M�ҋ@����
            byte[] recvBuffer = new byte[dataSize];
            int length = await stream.ReadAsync(recvBuffer, 0, recvBuffer.Length);

            // �ڑ��ؒf�`�F�b�N
            if(length <= 0)
            {
                
            }

            // ��M�f�[�^����C�x���gID�����o��
            int eventID = recvBuffer[0];

            // ��M�f�[�^����JSON����������o��
            byte[] bufferJson = recvBuffer.Skip(1).ToArray();     // 1�o�C�g�ڂ��X�L�b�v
            string jsonString = System.Text.Encoding.UTF8.GetString(bufferJson, 0, length - 1);

            context.Post(_ =>
            {
                switch (eventID)
                {
                    case 2: // �ePL�̖��O�\������

                        // Json�f�V���A���C�Y
                        UserData userData = JsonConvert.DeserializeObject<UserData>(jsonString);

                        // ���M����Ă���PLNo�̖��O���N���C�A���g�̃e�L�X�g�ɔ��f������
                        playerNames[userData.PlayerNo - 1].GetComponent<Text>().text = userData.UserName;

                        break;

                    case 4: // �ePL�̏��������\������

                        // Json�f�V���A���C�Y
                        userData = JsonConvert.DeserializeObject<UserData>(jsonString);

                        // �����\��
                        playerStatus[userData.PlayerNo - 1].GetComponent<Text>().text = "��������";

                        break;

                    case 5: // �C���Q�[������
                        NextScene();
                        break;

                    default: 
                        break;
                }
            }, null);
        }

        // �ʐM��ؒf
        tcpClient.Close();
    }

    /// <summary>
    /// ���[�U�[�f�[�^���M����
    /// </summary>
    public async void sendUserData()
    {
        // ���M�p���[�U�[�f�[�^�̍쐬 ---------------------------------------------------------------------------

        UserData userData = new UserData();
        userData.UserName = nameInput.GetComponent<InputField>().text;   // ���͂��ꂽ���O���i�[
        userData.PlayerNo = myNo;                                        // �����̃v���C���[�ԍ����i�[

        // ���M�f�[�^��JSON�V���A���C�Y
        string json = JsonConvert.SerializeObject(userData);

        // ���M����
        byte[] buffer = Encoding.UTF8.GetBytes(json);                // JSON��byte�ɕϊ�
        buffer = buffer.Prepend((byte)EventID.UserData).ToArray();   // ���M�f�[�^�̐擪�ɃC�x���gID��t�^
        await stream.WriteAsync(buffer, 0, buffer.Length);           // JSON���M����

        // ���̓t�B�[���h�̖�����
        nameInput.SetActive(false);

        // ���������{�^���̗L����
        completeButton.SetActive(true);
    }

    /// <summary>
    /// ���������t���O���M����
    /// </summary>
    public async void SendComplete()
    {   // ���������t���O���M���� -------------------------------------------------------------------------------------------------------------

        // ���������{�^���̔�\��
        completeButton.SetActive(false);

        UserData userData = new UserData();
        userData.IsReady = true;                 // ���������t���O��true��
        userData.PlayerNo = myNo;                // �����̃v���C���[�ԍ����i�[

        // ���M�f�[�^��JSON�V���A���C�Y
        string json = JsonConvert.SerializeObject(userData);

        // ���M����
        byte[] buffer = Encoding.UTF8.GetBytes(json);                    // JSON��byte�ɕϊ�
        buffer = buffer.Prepend((byte)EventID.CompleteFlag).ToArray();   // ���M�f�[�^�̐擪�ɃC�x���gID��t�^
        await stream.WriteAsync(buffer, 0, buffer.Length);               // JSON���M����

#if DEBUG
        // ���M�ҋ@�����\��
        Debug.Log("�����������M");
#endif
    }

    /// <summary>
    /// �V�[���ړ�����
    /// </summary>
    private void NextScene()
    {
        /* �t�F�[�h���� (��)  
            ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("ModeSelection", Color.black, 1.5f);
    }
}
