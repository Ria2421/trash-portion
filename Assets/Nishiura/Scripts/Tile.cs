//
// �^�C���X�N���v�g
// Name:���Y�W�� Date:2/14
// Update:02/26
//
using UnityEngine;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// ����^�C���ϐF�֐�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        
        if (other.gameObject.tag == "Bomb")
        {
            GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
        
        if (other.gameObject.tag == "Bomb")
        {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}