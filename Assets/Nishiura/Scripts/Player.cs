//
// �v���C���[���X�N���v�g
// Name:���Y�W�� Date:2/8
//

public class Player
{
    public int PlayerNo;
    //�N���A������
    public bool IsClear;

    //����̐Ԃ�����Ƃ�ƃ_���[�W
    public int Hp = 4;

    //����̐�����Ƃ�Ɖ��_
    public int Score;

    public Player(int playerno)
    {
        this.PlayerNo = playerno;
    }

    //public string GetPlayerName()
    //{
    //    string ret = "";
    //    string playerName = PlayerNo + "P";
    //    ret = playerName;

    //    return ret;
    //}
}
