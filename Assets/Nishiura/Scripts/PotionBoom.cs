//
// �|�[�V���������X�N���v�g
// Name:���Y�W�� Date:02/26
// Update:02/29
//
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.Examples.ObjectSpin;

public class PotionBoom : MonoBehaviour
{
    /// <summary>
    /// �����G�t�F�N�g�̃v���n�u
    /// </summary>
    public GameObject explosionPrefab;

    /// <summary>
    /// �Q�[���f�B���N�^�[
    /// </summary>
    GameDirectorCopy gameDirector;

    /// <summary>
    /// ���S����v���C���[�^�C�v
    /// </summary>
    List<int> deadList = new List<int>();

    /// <summary>
    /// �|�[�V�����̎��
    /// </summary>
    PotionType potionType;

    /// <summary>
    /// ���j�J�E���g
    /// </summary>
    int bombCnt;

    /// <summary>
    /// �J�E���g�e�L�X�g
    /// </summary>
    [SerializeField] Text countText;

    void Start()
    {
        bombCnt = 2;
        potionType = new PotionType();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirectorCopy>();
    }

    void Update()
    {
        if (bombCnt == 0)
        { //�w��ԍ��̃v���C���[���E�Q(�ԍ��̓T�[�o����擾)
            BoomPotion(deadList);
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="unitType"></param>
    void BoomPotion(List<int> deadList)
    { 
        GameObject explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        explosion.transform.position += new Vector3(0f, 0.5f, 0f);

        this.gameObject.transform.position += new Vector3(0f, -50f, 0f);

        gameDirector.DestroyUnit(deadList);

        Invoke("PotionKill",0.2f);

        //for(int i = 0; i < unitType.Length; i++)
        //{
        //    switch(potionType.PotionTypes)
        //    { //�|�[�V�����ʏ���
        //        case TYPE.BOMB:     //�{���̏ꍇ
        //            gameDirector.DestroyUnit(unitType[i]);
        //            break;

        //        case TYPE.CRUSTER:  //�N���X�^�[�̏ꍇ
        //            gameDirector.DestroyUnit(unitType[i]);
        //            break;

        //        case TYPE.REFRESH:   //���t���b�V���̏ꍇ
        //            gameDirector.BuffUnit(unitType[i],TYPE.REFRESH);
        //            break;

        //        case TYPE.INVISIBLE: //���G�̏ꍇ
        //            gameDirector.BuffUnit(unitType[i],TYPE.INVISIBLE);
        //            break;

        //        case TYPE.MUSCLE:   //�ؗ͂̏ꍇ
        //            gameDirector.BuffUnit(unitType[i],TYPE.MUSCLE);
        //            break;

        //        case TYPE.ICE:      //�A�C�X�̏ꍇ
        //            gameDirector.DebuffUnit(unitType[i],TYPE.ICE);
        //            break;

        //        case TYPE.CURSE:    //�􂢂̏ꍇ
        //            gameDirector.DebuffUnit(unitType[i],TYPE.CURSE);
        //            break;

        //        case TYPE.SOUR:     //�X�b�p�C�ꍇ
        //            gameDirector.DebuffUnit(unitType[i],TYPE.SOUR);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }

    /// <summary>
    /// �|�[�V�����I�u�W�F�N�g�j�󏈗�
    /// </summary>
    void PotionKill()
    {
        //�|�[�V������j��
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �{���̃J�E���g�_�E��
    /// </summary>
    public void bombCntDown()
    {
        bombCnt--;
        countText.text = bombCnt.ToString();
    }

    /// <summary>
    /// �R���C�_�[�ɃI�u�W�F�N�g����������
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   // "player"�^�O�ɂԂ�������

            if (!deadList.Contains(other.gameObject.GetComponent<UnitController>().Type))
            {   // �Ԃ������v���C���[��No���擾
                deadList.Add(other.gameObject.GetComponent<UnitController>().Type);
            }
        }

    }

    /// <summary>
    /// �R���C�_�[����o���I�u�W�F�N�g����������
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   // "player"�^�O���o����

            if (deadList.Contains(other.gameObject.GetComponent<UnitController>().Type))
            {   // �Ԃ������v���C���[��No������
                deadList.Remove(other.gameObject.GetComponent<UnitController>().Type);
            }
        }
    }
}