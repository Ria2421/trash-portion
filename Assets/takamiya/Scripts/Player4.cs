//
// �v���C���[���X�N���v�g
// Name:���Y�W�� Date:2/8
//

public class Player4
{
    public bool isPlayer;
    public int PlayerNo;
    //�N���A������
    public bool IsClear;

    //����̐Ԃ�����Ƃ�ƃ_���[�W
    public int Hp = 1;

    //����̐�����Ƃ�Ɖ��_
    public int Score;

    public Player4(bool isPlayer, int playerno)
    {
        this.isPlayer = isPlayer;
        this.PlayerNo = playerno;
    }

    public string GetPlayerName()
    {
        string ret = "";
        string playerName = PlayerNo + "P";

        if (!isPlayer)
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
