//
// �|�[�V���������X�N���v�g
// Name:���Y�W�� Date:02/26
// Update:02/27
//
using UnityEngine;

public class PotionBoom : MonoBehaviour
{
    public int potionType = 0;  //�|�[�V�����̎��
    float waitTimer = 1.5f;     //�ҋ@����
    GameDirector gameDirector;
    int[] type = { 2 };

    private void Start()
    {
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
        this.gameObject.transform.position += new Vector3(0f, -50f, 0f);

        Invoke("PotionKill",1.5f);

        for(int i = 0; i < unitType.Length; i++)
        {
            gameDirector.DestroyUnit(unitType[i]);           
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