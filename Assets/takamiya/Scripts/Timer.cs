//=============================================
//�^�C�}�[�X�N���v�g
//Author�F���{�S��
//Date:2/26
//Update:2/27
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Timer : MonoBehaviour
{
    //Mesh�^�̃e�L�X�g���󂯎���Ă���
    [SerializeField] private TextMeshProUGUI TimerText;
    // 5�b�ȉ��ɂȂ�������SE���󂯎���Ă���
    [SerializeField] private AudioClip timerse;

    private AudioSource audioSource;

    private float time = 15;

    bool ishurryup = false;

    bool iscolorchange = true;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource �R���|�[�l���g�̎擾
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�^�C����5�b�ȉ��ɂȂ�����e�L�X�g���g�債����A�k���������肷��
        if (ishurryup == false)
        {
            if (time <= 5)
            {
                ishurryup = true;
                iscolorchange = true;
                TimerText.transform
              .DOScale(new Vector3(1.5f, 1.5f, 0.5f), 0.5f)
              .SetLoops(-1, LoopType.Yoyo)//�J��Ԃ��ݒ肷��
              .SetEase(Ease.Linear);//���̊��o�œ������Ă���

                TextColorChange();

                // �����̃N���b�v���Đ�
                audioSource.PlayOneShot(timerse);
            }
        }
        TimerCountdown();
    }  
    void TimerCountdown()
    {
        //���Ԃ��J�E���g�_�E������
        time -= Time.deltaTime;
        //�^�C����0�b�ɂ���
        if (time < 0)
        {
            time = 0;
            ishurryup = false;
            iscolorchange = false;
            audioSource.Stop();
        }
        //�^�C�����e�L�X�g�ɕ\��
        TimerText.text = time.ToString("0");
    }
    /// <summary>
    /// �e�L�X�g�̐F�ς���
    /// </summary>
    private void TextColorChange()
    {
        if (iscolorchange == true)
        {
            TimerText
          .DOColor(ChangeColor(), 0.5f)
          .OnComplete(TextColorChange);
        }
    }
    /// <summary>
    /// �e�L�X�g�̃J���[�𔒂���Ԃɕς���
    /// </summary>
    /// <returns></returns>
    private Color ChangeColor()
    {
        Color nowColor = TimerText.color;
        if (nowColor == Color.white)
        {
            return Color.red;
        }
        else
        {
            return Color.white;
        }
    }
}
