using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool isPlayer;
    public int PlayerNo;
    //�N���A������
    public bool IsClear;

    //����̐Ԃ�����Ƃ�ƃ_���[�W
    public int Hp = 1;

    //����̐�����Ƃ�Ɖ��_
    public int Socre;

    public Player(bool isPlayer, int playerno)
    {
        this.isPlayer = isPlayer;
        this.PlayerNo = playerno;
    }

    public string GetPlayerName()
    {
        string ret = "";
        string playerName = PlayerNo + "P";

        if(!isPlayer)
        {
            playerName = "�������̔��m";
        }
        else
        {
            ret = playerName;
        }

        return ret;
    }
}
