//
//�����_���ɃG���[�g��\������X�N���v�g
//Author:MiuraYuki
//Date:2024/03/12
//Update:2024/03/12
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteManager: MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();//Animator���擾
        Random.InitState(6);//�����_���̐��l��������
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int nom = Random.Range(0, 6);//�����_���ɏo��l�͈̔͂̎w��(1�`5)
            animator.SetInteger("TransitionNom", nom);//TransitionNom��nom��������
        }
    }
}
