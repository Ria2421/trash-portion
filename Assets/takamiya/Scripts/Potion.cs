//
//�|�[�V�������ォ�痎�Ƃ��X�N���v�g
//Author�F���{�S��
//Date:2/19
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    //�|�[�V�����̗�������t���O
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.one);
        transform.Translate(Vector3.down * speed, Space.World);
    }
}
