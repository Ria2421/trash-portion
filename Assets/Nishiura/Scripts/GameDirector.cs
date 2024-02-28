//
// �Q�[���f�B���N�^�[�X�N���v�g
// Name:���Y�W�� Date:02/07
// Update:02/28
//
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameDirector : MonoBehaviour
{
    //Player
    public bool[] isDead;                                                   //���S����ϐ�
    Player[] player;                                                        //�v���C���[
    int nowTurn;                                                            //���݂̃^�[��
    //CinemachineVirtualCamera[] camera = new CinemachineVirtualCamera[5];    //�X�v���C���[���_�p�J����
    MoveCameraManager cameraManager;

    //�|�[�V�����̃A�C�R��
    [SerializeField] GameObject[] BoomPotion;
    [SerializeField] GameObject[] BuffPotion;
    [SerializeField] GameObject[] DebuffPotion;
    [SerializeField] GameObject[] Potion;

    //�v���C���[�^�C�v
    int playerType = UnitController.TYPE_RED;
    int nowPlayerType = 0;

    //�����_���ϐ�
    System.Random r = new System.Random();

    //�|�[�V�����������f�p�ϐ�
    bool[,] isGetted = new bool[4,4];

    /// <summary>
    /// �Q�[�����[�h
    /// </summary>
    public enum MODE
    {
        NONE = -1,
        WAIT_TURN_START,
        MOVE_SELECT,
        FIELD_UPDATE,
        WAIT_TURN_END,
        TURN_CHANGE,
    }

    /// <summary>
    /// �v���C���[���
    /// </summary>
    public enum PLAYERSTATE
    {
        NONE = 0,
        NORMAL_STATE,       //�ʏ���
        PARALYSIS_STATE,    //�}�q���
        FRIZE_STATE,        //�X�����
        CURSED_STATE,       //�􂢏��
        FLAME_STATE,        //������
        MUSCLE_STATE,       //�ؗ͏㏸���
        INVICIBLE_STATE,    //���G���
    }

    //MODE�J��
    MODE nowMode;
    public MODE nextMode;

    //0:Wall 1:NormalTile 2:SpawnPoint 3:Object1 4: -
    //�t�B�[���h

    TileData[,] tileData;

    // �^�C���z�u�ݒ�
    int[,] initTileData = new int[,]
    {
        {0,0,0,0,0,0,0,0,0,0,0},//��O
        {0,2,1,1,1,1,1,1,1,2,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,3,3,1,3,3,1,1,0},
        {0,1,1,3,1,1,1,3,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,3,1,1,1,3,1,1,0},
        {0,1,1,3,3,1,3,3,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,2,1,1,1,1,1,1,1,2,0},
        {0,0,0,0,0,0,0,0,0,0,0},
    };

    //�v���C���[�����z�u
    int[,] initUnitData = new int[,]
    {
        {0,0,0,0,0,0,0,0,0,0,0},//��O
        {0,3,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,4,0,0,0,0,0,0,0,2,0},
        {0,0,0,0,0,0,0,0,0,0,0},
    };

    //�t�B�[���h��̃v���C���[
    List<GameObject>[,] unitData;

    //�v���C���[�I�����[�h�Ŏg��
    GameObject selectUnit;
    int oldX, oldY;

    //�{�^�����̃I�u�W�F�N�g
    //GameObject buttonTurnEnd;

    //�v���C���[�̈ړ�����ϐ�
    bool isMoved = false;

    public bool IsMoved
    { //�ړ�����̃v���p�e�B
        get { return isMoved; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // �^�C���z�u��񕪂̔z��𐶐�
        tileData = new TileData[initTileData.GetLength(0), initTileData.GetLength(1)];

        // �^�C���f�[�^�ɔz�u�ʒu����
        for (int i = 0; i < initTileData.GetLength(0); i++)
        {
            for (int j = 0; j < initTileData.GetLength(1); j++)
            {
                tileData[i, j] = new TileData(initTileData[i, j]);
            }
        }

        // �^�C���f�[�^�Ƀv���C���[������
        for (int i = 0; i < initTileData.GetLength(0); i++)
        {
            for (int j = 0; j < initTileData.GetLength(1); j++)
            {
                tileData[i, j].pNo = initUnitData[i, j];
            }
        }

        //��ʏ�̃I�u�W�F�N�g�擾
        //buttonTurnEnd = GameObject.Find("EndButton");
        //cameraManager = GameObject.Find("CameraManager").GetComponent<MoveCameraManager>();

        //for (int i = 1; i < 6; i++)
        //{
        //    camera[i - 1] = GameObject.Find("VCam" + i).GetComponent<CinemachineVirtualCamera>();
        //}

        //txtInfo.GetComponent<Text>().text = "";
        unitData = new List<GameObject>[tileData.GetLength(0), tileData.GetLength(1)];

        //�v���C���[�ݒ�
        player = new Player[4];     //�l��
        player[0] = new Player(1);
        player[1] = new Player(2);
        player[2] = new Player(3);
        player[3] = new Player(4);

        //�^�C��������
        for (int i = 0; i < tileData.GetLength(0); i++)
        {
            for (int j = 0; j < tileData.GetLength(1); j++)
            {
                float x = j - (tileData.GetLength(1) / 2 - 0.5f);
                float z = i - (tileData.GetLength(0) / 2 - 0.5f);

                //�^�C���z�u
                string resname = "";
                int p1 = 0;
                int p2 = 0;
                int p3 = 0;
                int p4 = 0;

                int no = tileData[i, j].tNo;
                if (4 == no || 8 == no) no = 5;

                resname = "Cube (" + no + ")";

                resourcesInstantiate(resname, new Vector3(x, 0, z), Quaternion.identity);

                //�v���C���[�z�u
                unitData[i, j] = new List<GameObject>();

                //�v���C���[���ݒ�
                Vector3 angle = new Vector3(0, 0, 0);


                if (1 == tileData[i, j].pNo)
                { //1P���j�b�g�z�u
                    resname = "Unit1";
                    playerType = UnitController.TYPE_RED;
                    p1++;
                }
                else if (2 == tileData[i, j].pNo)
                { //2P���j�b�g�z�u
                    resname = "Unit2";
                    playerType = UnitController.TYPE_BLUE;
                    p2++;
                    // �I�u�W�F�N�g�̌���
                    angle.y = 180;
                }
                else if (3 == tileData[i, j].pNo)
                { //3P���j�b�g�z�u
                    resname = "Unit3";
                    playerType = UnitController.TYPE_YELLOW;
                    p3++;
                }
                else if (4 == tileData[i, j].pNo)
                { //4P���j�b�g�z�u
                    resname = "Unit4";
                    playerType = UnitController.TYPE_GREEN;
                    p4++;
                    angle.y = 180;
                }
                else
                {
                    resname = "";
                }

                GameObject unit = resourcesInstantiate(resname, new Vector3(x, 0.1f, z), Quaternion.Euler(angle));

                if (null != unit)
                {
                    unit.GetComponent<UnitController>().PlayerNo = initUnitData[i, j];
                    unit.GetComponent<UnitController>().Type = playerType;
                    //camera[playerType - 1].Follow = unit.transform;

                    //if (playerType == 1)
                    //{ //Case Player1
                    //    CinemachineTransposer cinemachineTransposer = camera[0].GetCinemachineComponent<CinemachineTransposer>();
                    //    cinemachineTransposer.m_FollowOffset.x = 0.0f;
                    //    cinemachineTransposer.m_FollowOffset.y = 0.6f;
                    //    cinemachineTransposer.m_FollowOffset.z = -2.0f;
                    //}
                    //else if (playerType == 2)
                    //{ //Case Player2
                    //    CinemachineTransposer cinemachineTransposer = camera[1].GetCinemachineComponent<CinemachineTransposer>();
                    //    cinemachineTransposer.m_FollowOffset.x = 0.0f;
                    //    cinemachineTransposer.m_FollowOffset.y = 0.6f;
                    //    cinemachineTransposer.m_FollowOffset.z = 2.5f;
                    //}
                    //else if (playerType == 3)
                    //{ //Case Player3
                    //    CinemachineTransposer cinemachineTransposer = camera[2].GetCinemachineComponent<CinemachineTransposer>();
                    //    cinemachineTransposer.m_FollowOffset.x = 0.0f;
                    //    cinemachineTransposer.m_FollowOffset.y = 0.6f;
                    //    cinemachineTransposer.m_FollowOffset.z = -2.0f;
                    //}
                    //else if (playerType == 4)
                    //{ //Case Player4
                    //    CinemachineTransposer cinemachineTransposer = camera[3].GetCinemachineComponent<CinemachineTransposer>();
                    //    cinemachineTransposer.m_FollowOffset.x = 0.0f;
                    //    cinemachineTransposer.m_FollowOffset.y = 0.6f;
                    //    cinemachineTransposer.m_FollowOffset.z = 2.5f;
                    //}
                    unitData[i, j].Add(unit);
                }
            }
        }

        nowTurn = 0;
        nextMode = MODE.MOVE_SELECT;
    }

    // Update is called once per frame
    void Update()
    {  
        if (nowPlayerType >= 4)
        {
            nowPlayerType = 0;
        }

        Mode();

        if (MODE.NONE != nextMode) InitMode(nextMode);

        //if(Input.GetKeyDown(KeyCode.Return))
        //{
        //    Initiate.DoneFading();
        //    Initiate.Fade("Result", Color.black, 1.5f);
        //}
    }

    /// <summary>
    /// ���C�����[�h
    /// </summary>
    /// <param name="next"></param>
    void Mode()
    {
        if (MODE.MOVE_SELECT == nowMode)
        {
            SelectMode();
        }
        else if (MODE.FIELD_UPDATE == nowMode)
        {
            FieldUpdateMode();
        }
        else if (MODE.TURN_CHANGE == nowMode)
        {
            TurnChangeMode();
        }
    }

    /// <summary>
    /// ���̃��[�h����
    /// </summary>
    /// <param name="next"></param>
    void InitMode(MODE next)
    {
        if (MODE.WAIT_TURN_START == next)
        {
        }
        else if (MODE.MOVE_SELECT == next)
        {
            selectUnit = null;
            //buttonTurnEnd.SetActive(false);
        }
        else if (MODE.WAIT_TURN_END == next)
        {
        }
        else if (MODE.FIELD_UPDATE == next)
        {
            //buttonTurnEnd.SetActive(false);
        }

        nowMode = next;
        nextMode = MODE.NONE;
    }

    /// <summary>
    /// ���j�b�g�ړ����[�h
    /// </summary>
    void SelectMode()
    {
        if (Input.GetMouseButtonDown(0))
        { //�N���b�N���A���j�b�g�I��
            isMoved = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (null != hit.collider.gameObject)
                {
                    Vector3 pos = hit.collider.gameObject.transform.position;

                    int x = (int)(pos.x + (tileData.GetLength(1) / 2 - 0.5f));
                    int z = (int)(pos.z + (tileData.GetLength(0) / 2 - 0.5f));

                    if (0 < unitData[z, x].Count && player[nowTurn].PlayerNo == unitData[z, x][0].GetComponent<UnitController>().PlayerNo)
                    { //���j�b�g�I��

                        ////�ړ��̍ۃ|�[�V�����g�p�𖳌���
                        //cameraManager.Flame1.SetActive(false);
                        //cameraManager.Flame2.SetActive(false);
                        //cameraManager.Flame3.SetActive(false);
                        //cameraManager.Flame4.SetActive(false);

                        if (null != selectUnit)
                        {
                            selectUnit.GetComponent<UnitController>().Select(false);
                        }
                        selectUnit = unitData[z, x][0];
                        oldX = x;
                        oldY = z;

                        selectUnit.GetComponent<UnitController>().Select();
                    }
                    else if (null != selectUnit)
                    { //�ړ���^�C���I��
                        if (movableTile(oldX, oldY, x, z))
                        {
                            isMoved = true;
                            unitData[oldY, oldX].Clear();
                            pos.y += 0.1f;
                            selectUnit.transform.position = pos;
                            unitData[z, x].Add(selectUnit);
                            unitData[z, x][0].GetComponent<UnitController>().OffColliderEnable();
                            nextMode = MODE.FIELD_UPDATE;
                        }
                        Debug.Log("���݂̃v���C���[:" + nowPlayerType);
                    }
                }
            }  
        }
    }

    void FieldUpdateMode()
    {
        nextMode = MODE.TURN_CHANGE;
    }

    /// <summary>
    /// �^�[���`�F���W
    /// </summary>
    void TurnChangeMode()
    {
        nextMode = MODE.NONE;
        nextMode = MODE.MOVE_SELECT;

        int oldTurn = nowTurn;
        nowTurn = getNextTurn();

        nextMode = MODE.MOVE_SELECT;

        nowPlayerType++;
    }

    int getNextTurn()
    { //���^�[�����擾
        int ret = nowTurn;

        ret++;
        if (3 < ret) ret = 0;

        return ret;
    }

    GameObject resourcesInstantiate(string name, Vector3 pos, Quaternion angle)
    {
        GameObject prefab = (GameObject)Resources.Load(name);

        if (null == prefab)
        {
            return null;
        }

        GameObject ret = Instantiate(prefab, pos, angle);
        return ret;
    }

    public bool movableTile(int oldx, int oldz, int x, int z)
    {
        bool ret = false;

        // �������擾
        int dx = Mathf.Abs(oldx - x);
        int dz = Mathf.Abs(oldz - z);

        Debug.Log("x:" + x);
        Debug.Log("z:" + z);

        // �΂ߐi�s�s��
        if (dx + dz > 2 || dx > 1 || dz > 1)
        {
            Debug.Log("�i�s�s��");
            Debug.Log("Z:" + z + " " + "X:" + x);

            return ret = false;
        }

        // �ǈȊO
        if (1 == tileData[z, x].tNo
           || 2 == tileData[z, x].tNo
           || player[nowTurn].PlayerNo * 4 == tileData[z, x].tNo)
        {
            if (0 == unitData[z, x].Count)
            { //�N�����Ȃ��}�X
                ret = true;
            }
            else
            { //�N������}�X
                if (unitData[z, x][0].GetComponent<UnitController>().PlayerNo != player[nowTurn].PlayerNo)
                { //�G�������ꍇ
                    ret = true;
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// �^�[���I���{�^��
    /// </summary>
    public void TurnEnd()
    {
        nextMode = MODE.MOVE_SELECT;
    }

    /// <summary>
    /// �|�[�V���������{�^��
    /// </summary>
    public void Brewing()
    {
        int rndPotion = r.Next(4);

        //if (isGetted[nowPlayerType, 0] == true && isGetted[nowPlayerType, 1] == true && isGetted[nowPlayerType, 2] == true && isGetted[nowPlayerType, 3] == true)
        //{ //���ׂẴ|�[�V�����g�����܂��Ă����ꍇ�̏���
        //    Debug.Log("�g�����܂��Ă��܂�");
        //    return;
        //}

        if (rndPotion == 0)
        { //�����|�[�V����
            if (isGetted[nowPlayerType, 0] == true) return;

            BoomPotion[nowPlayerType].SetActive(true);
            isGetted[nowPlayerType,0] = true;
        }
        else if (rndPotion == 1)
        { //�o�t�|�[�V����
            if (isGetted[nowPlayerType, 1] == true) return;

            BuffPotion[nowPlayerType].SetActive(true);
            isGetted[nowPlayerType,1] = true;
        }
        else if (rndPotion == 2)
        { //�f�o�t�|�[�V����
            if (isGetted[nowPlayerType, 2] == true) return;

            DebuffPotion[nowPlayerType].SetActive(true);
            isGetted[nowPlayerType,2] = true;
        }
        else if (rndPotion == 3)
        { //�t���[�g
            if (isGetted[nowPlayerType, 3] == true) return;

            Potion[nowPlayerType].SetActive(true);
            isGetted[nowPlayerType,3] = true;
        }

        //cameraManager.CameraShift();
        nextMode = MODE.FIELD_UPDATE;
    }

    /// <summary>
    /// �|�[�V�����g�p�{�^��
    /// </summary>
    public void UsePotion()
    {
        GameObject.Find("Unit" + (nowPlayerType+1)+ "(Clone)").GetComponent<UnitController>().animator.SetBool("isThrow", true);
        //cameraManager.MoveButton();
        nextMode = MODE.FIELD_UPDATE;
    }

    /// <summary>
    /// �w�胆�j�b�g�j�󏈗�
    /// </summary>
    /// <param name="unitType"></param>
    public void DestroyUnit(int unitType)
    {
        GameObject Unit;
        for (int i = 0; i < unitData.GetLength(0); i++)
        {
            for (int j = 0; j < unitData.GetLength(1); j++)
            {
                if (unitData[i, j].Count > 0)
                {
                    Unit = unitData[i, j][0];

                    if (Unit.GetComponent<UnitController>().Type == unitType)
                    {
                        Destroy(Unit);
                        //isDead[unitType] = true;
                        return;
                    }
                }
            }
        }
    }
}
