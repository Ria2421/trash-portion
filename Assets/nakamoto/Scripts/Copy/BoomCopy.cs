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