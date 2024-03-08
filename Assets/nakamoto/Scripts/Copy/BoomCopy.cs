//
// �|�[�V���������X�N���v�g
// Name:���Y�W�� Date:02/26
// Update:02/29
//
using UnityEngine;
using static TMPro.Examples.ObjectSpin;

public class BoomCopy : MonoBehaviour
{
    /// <summary>
    /// �����G�t�F�N�g�̃v���n�u
    /// </summary>
    public GameObject explosionPrefab; 

    /// <summary>
    /// �Q�[���f�B���N�^�[
    /// </summary>
    GameDirector gameDirector;

    /// <summary>
    /// ���S����v���C���[�^�C�v
    /// </summary>
    int[] type = {2};

    /// <summary>
    /// �|�[�V�����̎��
    /// </summary>
    PotionType potionType;

    void Start()
    {
        potionType = new PotionType();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { //�w��ԍ��̃v���C���[���E�Q(�ԍ��̓T�[�o����擾)
            BoomPotion(type);
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="unitType"></param>
    void BoomPotion(int[] unitType)
    { 
        GameObject explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        explosion.transform.position += new Vector3(0f, 0.5f, 0f);

        this.gameObject.transform.position += new Vector3(0f, -50f, 0f);

        Invoke("PotionKill",0.2f);

        for(int i = 0; i < unitType.Length; i++)
        {
            switch(potionType.PotionTypes)
            { //�|�[�V�����ʏ���
                case TYPE.BOMB:     //�{���̏ꍇ
                    gameDirector.DestroyUnit(unitType[i]);
                    break;

                case TYPE.CRUSTER:  //�N���X�^�[�̏ꍇ
                    gameDirector.DestroyUnit(unitType[i]);
                    break;

                case TYPE.REFRESH:   //���t���b�V���̏ꍇ
                    gameDirector.BuffUnit(unitType[i],TYPE.REFRESH);
                    break;

                case TYPE.INVISIBLE: //���G�̏ꍇ
                    gameDirector.BuffUnit(unitType[i],TYPE.INVISIBLE);
                    break;

                case TYPE.MUSCLE:   //�ؗ͂̏ꍇ
                    gameDirector.BuffUnit(unitType[i],TYPE.MUSCLE);
                    break;

                case TYPE.ICE:      //�A�C�X�̏ꍇ
                    gameDirector.DebuffUnit(unitType[i],TYPE.ICE);
                    break;

                case TYPE.CURSE:    //�􂢂̏ꍇ
                    gameDirector.DebuffUnit(unitType[i],TYPE.CURSE);
                    break;

                case TYPE.SOUR:     //�X�b�p�C�ꍇ
                    gameDirector.DebuffUnit(unitType[i],TYPE.SOUR);
                    break;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// �|�[�V�����I�u�W�F�N�g�j�󏈗�
    /// </summary>
    void PotionKill()
    {
        //�|�[�V������j��
        Destroy(this.gameObject);
    }
}