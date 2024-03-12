//==============================================
//Autor:�O����l
//Day:2/28
//���[���b�g����
//==============================================
using UnityEngine;
using UnityEngine.UI;

public class TutorialRouletteManager : MonoBehaviour
{
    public float rouletteSpeed = 0;        //��]���x
    public GameObject verygood;
    public GameObject good;
    public GameObject bad;
    public GameObject roulette;           //���[���b�g�{��
    public Text timerText;
    public float angle = 0;              //��]�̊p�x�̕ϐ�
    public bool endCountDown;
    public Text limitTime;              //�~�j�Q�[���̐�������
    int limit;                         //�������Ԃ̕ϐ�
    bool isLimit;                     //�������Ԃ𒴂������ǂ���
    private bool isClicked;

    StartMiniGame tutorialMiniGame;

    // Start is called before the first frame update
    void Start()
    {
        tutorialMiniGame = GameObject.Find("MiniGameManager").GetComponent<StartMiniGame>();
        Init();
    }
    public void Init()
    {
        //�t���[�����[�g��60�ɌŒ�
        Application.targetFrameRate = 60;
        endCountDown = false;
        limit = 5;
        limitTime.enabled = true;
        isLimit = false;
        isClicked = false;
        verygood.SetActive(false);
        good.SetActive(false);
        bad.SetActive(false);
        //1�b���ƂɊ֐������s
        //InvokeRepeating("CountDownTimer", 3.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isClicked ==true)
        {
            return;
        }
        if (!isLimit)
        {
            if (endCountDown)
            {
                //���[���b�g����]
                transform.Rotate(0, 0, rouletteSpeed);

                if (Input.GetMouseButtonDown(0))
                {//���N���b�N���ꂽ��
                    isClicked = true;
                    //rouletteSpeed = 0;
                    Judge();
                    CancelInvoke();
                    tutorialMiniGame.BackTitleButton.SetActive(true);
                    tutorialMiniGame.AgainButton.SetActive(true);
                }
            }
        }
    }

    //���菈��
    void Judge()
    {
        angle = roulette.transform.eulerAngles.z;
        float angleA = (155 + angle) % 360;     //�听���̒[
        float angleB = (183 + angle) % 360;     //�听���̒[
        float angleC = (110 + angle) % 360;     //�����̒[
        float angleD = (229 + angle) % 360;     //�����̒[

        //�听��
        if (angleA > angleB)
        {//360�x�𒴂��Ă�����
            if ((angleA <= transform.eulerAngles.z && transform.eulerAngles.z <= 360) || (0 <= transform.eulerAngles.z && transform.eulerAngles.z <= angleB))
            {
                verygood.SetActive(true);

                return;
            }
        }
        else
        {
            if (transform.eulerAngles.z >= angleA && transform.eulerAngles.z <= angleB)
            {
                verygood.SetActive(true);
                return;
            }
        }

        //����
        if (angleC > angleD)
        {
            if ((angleC <= transform.eulerAngles.z && transform.eulerAngles.z <= 360) || (0 <= transform.eulerAngles.z && transform.eulerAngles.z <= angleD))
            {
                good.SetActive(true);
                return;
            }
        }
        else
        {
            if (transform.eulerAngles.z >= angleC && transform.eulerAngles.z <= angleD)
            {
                good.SetActive(true);
                return;
            }
        }

        // �͈͊O�͂��ׂĎ��s
        bad.SetActive(true);
    }

    void CountDownTimer()
    {
        limit--;
        limitTime.text = limit.ToString();
        if (limitTime.text == "-1")
        {
            bad.SetActive(true);
            isLimit = true;
            CancelInvoke();
        }
    }
}
