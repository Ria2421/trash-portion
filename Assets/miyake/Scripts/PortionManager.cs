using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionManager : MonoBehaviour
{
    [SerializeField] GameObject[] portionPrefabs;       //�|�[�V�����̃v���n�u
    [SerializeField] bool slowFlag;                     //�|�[�V�����̃t���O����
    int rand;                                           //�|�[�V���������������_���ɂ��邽�߂̕ϐ�
    int randAngle;                                      //�|�[�V�����̊p�x�̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //1.5�b�Ԋu�Ŋ֐������s
        InvokeRepeating("SlowPortion", 5.0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�|�[�V�����ˏo
    public void SlowPortion()
    {
        rand = Random.Range(0, 5);
        randAngle = Random.Range(-180, 180);

        //�|�[�V�����𐶐����ăR���|�[�l���g���擾
        GameObject portion = Instantiate(portionPrefabs[rand],transform.position,Quaternion.Euler(-90 + randAngle,0,0));
        if(slowFlag)
        {
            portion.GetComponent<portion>().SlowLeft();
        }
        else
        {
            portion.GetComponent<portion>().SlowRight();
        }
    }
}
