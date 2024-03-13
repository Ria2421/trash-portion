//===================================================
//
//�v���C���[�̐���
//Author�F���{�S��
//Date:3/1
//Update/3/6
//
//====================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRank : MonoBehaviour
{
    [SerializeField] GameObject[] characterPrefabs; // �\������L�����N�^�[�̃v���n�u
    [SerializeField] GameObject rankObject;//�\�����郉���N�I�u�W�F�N�g
    [SerializeField] GameObject congratulationObj;//�\������congratulation�I�u�W�F�N�g
    [SerializeField] int[] PlayerID;//�v���C���[�𐶐�����ID�̎w��
    [SerializeField] Text winnerName; //���Җ����i�[
    NetworkManager networkManager;

    /// <summary>
    /// ���Ҕԍ�
    /// </summary>
    public static int[] WinnerID
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        //�v���C���[����l�̏ꍇ
        if (WinnerID.Length == 1)
        {
            // �v���n�u���w��̈ʒu�ɐ������A180�x��]������
            Instantiate(characterPrefabs[WinnerID[0]], new Vector3(0.60f, 0f, -5f), Quaternion.Euler(0f, 180f, 0f));

            // ����PLNo�̖��O����
            winnerName.text = networkManager.PlayerNames[WinnerID[0]];
        }
        //����ȊO�̏ꍇ
        else
        {
            // �v���n�u���w��̈ʒu�ɐ������A180�x��]������
            for (int i = 0; i < 4; ++i)
            {   // �v���C���[�̐������J��Ԃ��A�L�����N�^�[�𐶐�
                Instantiate(characterPrefabs[WinnerID[i]], new Vector3(0.60f, 0f, -5f), Quaternion.Euler(0f, 180f, 0f));
            }

            // ��������
            winnerName.text = "��������";
        }

        //�����N�I�u�W�F�N�g����芴�o�Ŋg��A�k������
        rankObject.transform
             .DOScale(new Vector3(1.5f, 1.5f, 0.5f), 0.5f)
             .SetLoops(-1, LoopType.Yoyo)//�J��Ԃ��ݒ肷��
             .SetEase(Ease.Linear);//���̊��o�œ������Ă���

        //conguratulation�I�u�W�F�N�g����芴�o�Ŋg��A�k������
        congratulationObj.transform
             .DOScale(new Vector3(1.5f, 1.5f, 0.5f), 0.5f)
             .SetLoops(-1, LoopType.Yoyo)//�J��Ԃ��ݒ肷��
             .SetEase(Ease.Linear);//���̊��o�œ������Ă���
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
