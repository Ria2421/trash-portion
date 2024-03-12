//
// �^�C���X�N���v�g
// Name:���Y�W�� Date:2/14
// Update:02/26
//
using UnityEngine;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�t���O
    /// </summary>
    bool playerFlag;

    /// <summary>
    /// �{���t���O
    /// </summary>
    bool bombFlag;

    /// <summary>
    /// ����^�C���ϐF�֐�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerFlag = true;
        }

        if (other.gameObject.tag == "Bomb")
        {
            bombFlag = true;
        }

        if (playerFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else if (bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 1f);
        }
        else if (playerFlag && bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerFlag = false;
        }
        
        if (other.gameObject.tag == "Bomb")
        {
            bombFlag = false;
        }

        if (playerFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else if (bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 1f);
        }
        else if (playerFlag && bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    /// <summary>
    /// ���݂̃v���C���[�t���O�̏�
    /// </summary>
    public bool ReturnPlayerFlag()
    {
        return playerFlag;
    }
}