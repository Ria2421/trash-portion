//
// �v���C���[�R���g���[���X�N���v�g
// Name:���Y�W�� Date:2/8
//
using UnityEngine;

public class UnitControlle : MonoBehaviour
{
    //�v���[���[�̃^�C�v
    public const int TYPE_BLUE = 1;
    public const int TYPE_RED = 2;

    const float SELECT_POS_Y = 2;

    //�ǂ���̃v���C���[��
    public int PlayerNo;
    public int Type;

    /// <summary>
    /// �I�����̓���
    /// </summary>
    /// <param name="select">�I�� or ��I��</param>
    /// <returns>�A�j���[�V�����b��</returns>
    public float Select(bool select = true)
    {
        float ret = 0;
        Vector3 pos = new Vector3(transform.position.x, SELECT_POS_Y, transform.position.z);

        if (!select)
        {
            pos = new Vector3(transform.position.x, 0.6f, transform.position.z);
        }

        transform.position = pos;

        return ret;
    }
}
