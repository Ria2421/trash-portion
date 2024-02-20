using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;      //�C�x���g�V�X�e��
    bool select = false;            //�{�^�����I�����ꂽ��true
    public GameObject arrow;        //�J�[�\��
    GameObject selectObject;
    [SerializeField] Text start;          //start�̕ϐ�
    [SerializeField] Text quit;           //qiot�̕ϐ�
    // Start is called before the first frame update
    void Start()
    {
        start.color = Color.black;
        quit.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if(select == false)
        {
            
            if(eventSystem.currentSelectedGameObject != null)
            {
                selectObject = eventSystem.currentSelectedGameObject.gameObject;
            }
            //�����{�^�����I������Ă�����
            if(this.gameObject == selectObject)
            {
                //����\��
                arrow.SetActive(true);
                select = true;

                //�I�΂ꂽ���̃e�L�X�g�̐F�Ƒ傫����ς���
                start.color= Color.red;
                quit.color= Color.black;
                start.fontSize = 80;
                quit.fontSize = 64;
            }
        }
        else
        {
            if (eventSystem.currentSelectedGameObject != null)
            {
                selectObject = eventSystem.currentSelectedGameObject.gameObject;
            }
            //�����{�^�����I������O�ꂽ��
            if (this.gameObject != selectObject)
            {
                //�����\��
                arrow.SetActive(false);
                select = false;

                //�I�΂�ĂȂ����̃e�L�X�g�̐F�Ƒ傫����ς���
                start.color = Color.black;
                quit.color = Color.red;
                start.fontSize = 64;
                quit.fontSize = 80;
            }
        }
    }
}
