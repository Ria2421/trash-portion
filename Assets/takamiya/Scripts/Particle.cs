//=========================================
//�����ᐶ������
//Author�F���{�S��
//Date2/28
//
//=========================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Particle : MonoBehaviour
{
    [SerializeField] GameObject particle;//Prefabs�̎w��

    [SerializeField] GameObject CrackerRight;//�E���̃p�[�e�B�N��

    [SerializeField] GameObject CrackerLeft;//�����̃p�[�e�B�N��

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateParticle",1.0f,2.0f);
        InvokeRepeating("GenerateParticle2", 1.0f, 2.0f);
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    //�E���̎�����𐶐�
    void GenerateParticle()
    {
        GameObject parent = particle;
        Instantiate(parent,CrackerRight.transform);

    }
    //�����̎�����𐶐�
    void GenerateParticle2()
    {
        GameObject parent = particle;
        Instantiate(parent, CrackerLeft.transform);
    }
}
