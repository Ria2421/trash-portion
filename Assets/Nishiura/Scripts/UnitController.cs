//
// �v���C���[�R���g���[���X�N���v�g
// Name:���Y�W�� Date:2/8
//
using Unity.VisualScripting;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    //�v���[���[�̃^�C�v
    public const int TYPE_RED = 1;
    public const int TYPE_BLUE = 2;
    public const int TYPE_YELLOW = 3;
    public const int TYPE_GREEN = 4;

    const float SELECT_POS_Y = 2;

    //�ǂ���̃v���C���[��
    public int PlayerNo;
    public int Type;
    
    void OnColliderEnable()
    {
        GetComponent<BoxCollider>().center = new Vector3(0f, -2f, 0f);
        GetComponent<BoxCollider>().enabled = true;
    }

    public void OffColliderEnable()
    {
        GetComponent<BoxCollider>().center = new Vector3(0f, 100f,0f);
    }

    /// <summary>
    /// �I�����̓���
    /// </summary>
    /// <param name="select">�I�� or ��I��</param>
    /// <returns>�A�j���[�V�����b��</returns>
    public float Select(bool select =true)
    {
        float ret = 0;
        Vector3 pos = new Vector3(transform.position.x, SELECT_POS_Y, transform.position.z);
        OnColliderEnable();

        if (!select)
        {
            pos = new Vector3(transform.position.x, 0.6f, transform.position.z);
        }

        transform.position = pos;
        return ret;
    }
}
