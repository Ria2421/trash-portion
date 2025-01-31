//
// タイルスクリプト
// Name:西浦晃太 Date:2/14
// Update:02/26
//
using UnityEngine;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// プレイヤーフラグ
    /// </summary>
    bool playerFlag;

    /// <summary>
    /// ボムフラグ
    /// </summary>
    bool bombFlag;

    /// <summary>
    /// ボムの投擲範囲フラグ
    /// </summary>
    bool bombRangeFlag;

    /// <summary>
    /// 可動域タイル変色関数
    /// </summary>
    /// <param name="collision"></param>
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MoveRange")
        {
            playerFlag = true;
        }

        if (other.gameObject.tag == "Bomb")
        {
            bombFlag = true;
        }

        if(other.gameObject.tag == "BombRange")
        {
            bombRangeFlag = true;
        }

        if (playerFlag || bombRangeFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else if (bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 1f);
        }
        else if (playerFlag && bombFlag || bombRangeFlag && bombFlag)
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
        if (other.gameObject.tag == "MoveRange")
        {
            playerFlag = false;
        }
        
        if (other.gameObject.tag == "Bomb")
        {
            bombFlag = false;
        }

        if (other.gameObject.tag == "BombRange")
        {
            bombRangeFlag = false;
        }

        if (playerFlag || bombRangeFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else if (bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 1f);
        }
        else if (playerFlag && bombFlag || bombRangeFlag && bombFlag)
        {
            GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    /// <summary>
    /// 現在のプレイヤーフラグの状況
    /// </summary>
    public bool ReturnPlayerFlag()
    {
        return bombRangeFlag;
    }
}