//==============================================
//Autor:�O����l
//Day:3/7
//�^�C�g����ʏ���
//==============================================
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{    
    GameObject networkManager;   
    
    // Start is called before the first frame update
    void Start()
    {
        if(networkManager = GameObject.Find("NetworkManager"))
        {   // NetworkManager�I�u�W�F�N�g�����݂��鎞�͔j��
            Destroy(networkManager);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
