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


    // Start is called before the first frame update
    void Start()
    {

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
            }
        }
    }
}
