using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum Player�@
    {
        Player1,
        Player2,
        Player3,
        Player4
    }

    //Text�I�u�W�F�N�g���i�[���邽�߂̃t�B�[���h
    [SerializeField] Text PlayerText;
    [SerializeField] Text cycleCountText;
    private Player currentPlayer = Player.Player1;// ���݂̃v���C���[��\��enum�̕ϐ�
    private int cycleCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�G���^�[�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //���̃v���C���[�ɕύX����
            currentPlayer = (Player)(((int)currentPlayer % 4) + 1);
            trunChenge();// �v���C���[�e�L�X�g���X�V���郁�\�b�h���Ăяo��
        }
        else if (currentPlayer == Player.Player1)
        {
            cycleCount++;

            if (cycleCount ==2)
            {
                cycleCountText.text = "�^�[����";
            }
        }
    }

    public void trunChenge()
    {
        // ���݂̃v���C���[�ԍ����e�L�X�g�ɕ\������
        PlayerText.text = $"{(int)currentPlayer}P";

        //cycleCountText.text = $"{ (int)currentPlayer}�^�[����";
    }
}
