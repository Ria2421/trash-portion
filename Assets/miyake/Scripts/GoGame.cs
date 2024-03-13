using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoGame : MonoBehaviour
{
    //�J�[�\��    
    public GameObject arrow;

    //start�̕ϐ�
    [SerializeField] Text start;
    
    void Start()
    {
        //�t�H���g�T�C�Y��64�A�F�����ɁA�J�[�\�����\��
        arrow.SetActive(false);
        start.color = Color.black;
        start.fontSize = 64;
    }

    //�}�E�X�J�[�\�����������
    public void OnMouseOver()
    {
        //�t�H���g�T�C�Y��80�A�F��ԂɁA�J�[�\����\��
        arrow.SetActive(true);
        start.color = Color.red;
        start.fontSize = 80;
    }

    //�}�E�X�J�[�\�������ꂽ��
    public void OnMouseExit()
    {
        //�t�H���g�T�C�Y��64�A�F�����ɁA�J�[�\�����\��
        arrow.SetActive(false);
        start.color= Color.black;
        start.fontSize = 64;
    }

    public void GoGameScene()
    {
        Initiate.DoneFading();
        Initiate.Fade("Connect", Color.black, 1.5f);
    }
}
