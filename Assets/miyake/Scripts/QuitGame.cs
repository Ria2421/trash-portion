using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    //quit�̕ϐ�
    [SerializeField] Text quit;

    //�J�[�\��    
    public GameObject arrow;

    void Start()
    {
        //�t�H���g�T�C�Y��64�A�F�����ɁA�J�[�\�����\��
        quit.color = Color.black;
        quit.fontSize = 64;
        arrow.SetActive(false);
    }

    //�}�E�X�J�[�\�����������
    public void OnMouseOver()
    {
        //�t�H���g�T�C�Y��80�A�F��ԂɁA�J�[�\����\��
        quit.color = Color.red;
        quit.fontSize = 80;
        arrow.SetActive(true);
    }

    //�}�E�X�J�[�\�������ꂽ��
    public void OnMouseExit()
    {
        //�t�H���g�T�C�Y��64�A�F�����ɁA�J�[�\�����\��
        quit.color = Color.black;
        quit.fontSize = 64;
        arrow.SetActive(false);
    }

    //�Q�[���I������
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
