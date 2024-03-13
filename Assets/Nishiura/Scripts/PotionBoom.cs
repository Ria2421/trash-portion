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
    /// �I�[�f�B�I�\�[�X
    /// </summary>
    [SerializeField] AudioSource audioSource;

    /// <summary>
    /// ������SE
    /// </summary>
    [SerializeField] AudioClip boomSE;      

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
        bombCnt = 6;
        potionType = new PotionType();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirectorCopy>();
    }

    void Update()
    {
        if (bombCnt == 0)
        { //�w��ԍ��̃v���C���[���E�Q(�ԍ��̓T�[�o����擾)
            audioSource.PlayOneShot(boomSE);
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
            if (other.gameObject != null)
            {   //null�`�F�b�N
                if (!deadList.Contains(other.gameObject.GetComponent<UnitController>().Type))
                {   // �Ԃ������v���C���[��No���擾
                    deadList.Add(other.gameObject.GetComponent<UnitController>().Type);
                }
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
            if (other.gameObject != null)
            {   //null�`�F�b�N
                if (deadList.Contains(other.gameObject.GetComponent<UnitController>().Type))
                {   // �Ԃ������v���C���[��No������
                    deadList.Remove(other.gameObject.GetComponent<UnitController>().Type);
                }
            }
        }
    }
}