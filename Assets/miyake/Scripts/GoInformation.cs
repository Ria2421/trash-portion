using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoInformation : MonoBehaviour
{
    //information�̕ϐ�
    [SerializeField] Text information;

    //�J�[�\��    
    public GameObject arrow;

    void Start()
    {
        //�t�H���g�T�C�Y��64�A�F�����ɁA�J�[�\�����\��
        information.color = Color.black;
        information.fontSize = 64;
        arrow.SetActive(false);
    }

    //�}�E�X�J�[�\�����������
    public void OnMouseOver()
    {
        //�t�H���g�T�C�Y��80�A�F��ԂɁA�J�[�\����\��
        information.color = Color.red;
        information.fontSize = 80;
        arrow.SetActive(true);
    }

    //�}�E�X�J�[�\�������ꂽ��
    public void OnMouseExit()
    {
        //�t�H���g�T�C�Y��64�A�F�����ɁA�J�[�\�����\��
        information.color = Color.black;
        information.fontSize = 64;
        arrow.SetActive(false);
    }

    //�V�ѕ������V�[���J��
    public void HowToPlay()
    {
        Initiate.DoneFading();
        Initiate.Fade("Information", Color.black, 1.5f);
    }
}
