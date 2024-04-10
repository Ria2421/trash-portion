//==============================================
//Autor:�O����l
//Day:2/28
//���[���b�g����
//==============================================
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    public float rouletteSpeed = 0;        //��]���x
    public GameObject good;
    public GameObject bad;
    public GameObject roulette;            //���[���b�g�{��
    public Text timerText;
    float angle = 0;                       //��]�̊p�x�̕ϐ�
    bool endCountDown;
    public Text limitTime;              //�~�j�Q�[���̐�������
    int limit;                          //�������Ԃ̕ϐ�
    bool isLimit;                     //�������Ԃ𒴂������ǂ���
    bool gameFlag;
    NetworkManager networkManager;
    [SerializeField] AudioClip goodSE;          //����SE
    [SerializeField] AudioClip badSE;           //���sSE
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�����[�g��60�ɌŒ�
        Application.targetFrameRate = 60;
        endCountDown = false;
        isLimit = false;
        gameFlag = false;
        //1�b���ƂɊ֐������s
        InvokeRepeating("CountDownTimer", 3.0f, 1.0f);

        // �l�b�g���[�N�}�l�[�W���[�̎擾
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLimit)
        {
            if (timerText.text == "GO!!")
            {
                endCountDown = true;
            }
            if (endCountDown)
            {
                //���[���b�g����]
                transform.Rotate(0, 0, rouletteSpeed);

                if (Input.GetMouseButtonDown(0))
                {//���N���b�N���ꂽ��
                    rouletteSpeed = 0;
                    CancelInvoke();
                    Judge();
                }
            }
        }
    }

    //���菈��
    void Judge()
    {

        angle = roulette.transform.eulerAngles.y;
        float angleA = (156 + angle) % 360;     //�����̒[
        float angleB = (200 + angle) % 360;     //�����̒[

        if (!gameFlag)
        {
            //����
            if (angleA > angleB)
            {//360�x�𒴂��Ă�����
                if ((angleA <= transform.eulerAngles.y && transform.eulerAngles.y <= 360) || (0 <= transform.eulerAngles.y && transform.eulerAngles.y <= angleB))
                {
                    //����SE
                    audioSource.PlayOneShot(goodSE);

                    good.SetActive(true);

                    // �������̑��M
                    networkManager.SendPotionStatus((int)EventID.PotionComplete);
                    gameFlag = true;

                    // �~�j�Q�[���̏I��
                    Invoke("MiniGameDestroy", 1.5f);

                    return;
                }
            }
            else
            {
                if (transform.eulerAngles.y >= angleA && transform.eulerAngles.y <= angleB)
                {
                    //����SE
                    audioSource.PlayOneShot(goodSE);

                    good.SetActive(true);

                    // �������̑��M
                    networkManager.SendPotionStatus((int)EventID.PotionComplete);
                    gameFlag = true;

                    // �~�j�Q�[���̏I��
                    Invoke("MiniGameDestroy", 1.5f);

                    return;
                }
            }

            // �͈͊O�͂��ׂĎ��s

            //���sSE
            audioSource.PlayOneShot(badSE);

            bad.SetActive(true);

            // ���s���̑��M
            networkManager.SendPotionStatus((int)EventID.PotionFailure);
            gameFlag = true;
        }

        // �~�j�Q�[���̏I��
        Invoke("MiniGameDestroy", 1.5f);
    }

    /// <summary>
    /// �~�j�Q�[���̔j��
    /// </summary>
    private void MiniGameDestroy()
    {
        // �~�j�Q�[�����I��
        Destroy(GameObject.Find("MiniGames(Clone)"));
    }

    //void CountDownTimer()
    //{
    //    limit--;
    //    limitTime.text = limit.ToString();
    //    if (limitTime.text == "-1")
    //    {
    //        bad.SetActive(true);
    //        isLimit = true;
    //        CancelInvoke();
    //        Destroy(limitTime);
    //    }
    //}
}
