//
// �|�[�V���������X�N���v�g
// Name:���Y�W�� Date:02/26
// Update:02/26
//
using UnityEngine;

public class PotionBoom : MonoBehaviour
{
    public int potionType = 0;
    float waitTimer = 1.5f;

    GameObject potion;

    private void Start()
    {
        potion = GameObject.FindWithTag("Potion");
    }

    // �E�F�C�g�̏���
    bool isWait()
    {
        bool ret = false;


        // �^�C�}�[
        if (0 < waitTimer)
        {
            waitTimer -= Time.deltaTime;
            ret = true;
        }

        return ret;
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void Boom()
    {
        potion.transform.position += new Vector3(0f,100f,0f);

        if (isWait()) return;

        Destroy(potion);
    }
}
