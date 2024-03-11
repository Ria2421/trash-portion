//
// �Q�[���f�B���N�^�[�R�s�[�X�N���v�g
// Name:���Y�W�� Date:02/07
// Update:03/07
//
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameDirectorCopy : MonoBehaviour
{
    /// <summary>
    /// �^�C���z�u�ݒ�
    /// 0:Wall 1:NormalTile 2:SpawnPoint 3:Object1 4: -
    /// </summary>
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

    /// <summary>
    /// �v���C���[�����z�u
    /// </summary>
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

    /// <summary>
    /// �Q�[�����[�h
    /// </summary>
    public enum MODE
    {
        NONE = -1,
        WAIT_TURN_START,
        MOVE_SELECT,
        POTION_THROW,
        FIELD_UPDATE,
        WAIT_TURN_END,
        TURN_CHANGE,
    }

    ///========================================
    ///
    /// �t�B�[���h
    /// 
    ///========================================

    /// <summary>
    /// �v���C���[ �z��
    /// </summary>
    Player[] player = new Player[playerNum];

    /// <summary>
    /// �|�[�V�����̎��
    /// </summary>
    PotionType potionType = new PotionType();

    /// <summary>
    /// �v���C���[�l��
    /// </summary>
    const int playerNum = 4;

    /// <summary>
    /// �t�B�[���h��̃v���C���[���X�g
    /// </summary>
    List<GameObject>[,] unitData;

    /// <summary>
    /// �I�����j�b�g
    /// </summary>
    GameObject selectUnit;
    int oldX, oldY;

    /// <summary>
    /// �v���C���[�̈ړ�����ϐ�
    /// </summary>
    bool isMoved = false;

    /// <summary>
    /// �v���C���[�^�C�v
    /// </summary>
    int playerType = UnitController.TYPE_RED;

    /// <summary>
    /// ���݂̃^�[���̃v���C���[�^�C�v
    /// </summary>
    int nowPlayerType = 0;

    /// <summary>
    /// �^�C���f�[�^�\���̂̐錾
    /// </summary>
    TileData[,] tileData;

    /// <summary>
    /// ���݂̃^�[��
    /// </summary>
    int nowTurn;

    /// <summary>
    /// �J�����ړ��X�N���v�g
    /// </summary>
    MoveCameraManager cameraManager;

    /// <summary>
    /// ���݂̃��[�h
    /// </summary>
    MODE nowMode;

    /// <summary>
    /// ���̃��[�h
    /// </summary>
    public MODE nextMode;

    /// <summary>
    /// �e�|�[�V�����̃A�C�R��
    /// </summary>
    [SerializeField] GameObject[] BoomPotion1;
    [SerializeField] GameObject[] BoomPotion2;

    /// <summary>
    /// (��)�|�[�V���������_�������ϐ�
    /// </summary>
    System.Random r = new System.Random();

    public bool IsMoved
    { //�ړ�����̃v���p�e�B
        get { return isMoved; }
    }

    //+++++++++++++++++++++++++++++++++++++++++
    // �����{�^���I�u�W�F�N�g
    //+++++++++++++++++++++++++++++++++++++++++
    [SerializeField] GameObject[] brewingButton; 

    //+++++++++++++++++++++++++++++++++++++++++
    // �^�[���\���e�L�X�g
    //+++++++++++++++++++++++++++++++++++++++++
    [SerializeField] GameObject turnText;

    //+++++++++++++++++++++++++++++++++++++++++
    // �݌v�^�[���\���e�L�X�g
    //+++++++++++++++++++++++++++++++++++++++++
    [SerializeField] Text allTurnText;

    //+++++++++++++++++++++++++++++++++++++++++
    // PLNo���ߑł��p
    //+++++++++++++++++++++++++++++++++++++++++
    [SerializeField] int plNo;

    //+++++++++++++++++++++++++++++++++++++++++
    // �~�j�Q�[���v���n�u
    //+++++++++++++++++++++++++++++++++++++++++
    [SerializeField] GameObject minigamePrefab;

    //+++++++++++++++++++++++++++++++++++++++++
    // �ePL�̃|�[�V�����X�e�[�^�X (4���)
    // [0.����  1.���s  2.����  3.�ҋ@]
    //+++++++++++++++++++++++++++++++++++++++++
    [SerializeField] GameObject[] potionStatus1;
    [SerializeField] GameObject[] potionStatus2;
    [SerializeField] GameObject[] potionStatus3;
    [SerializeField] GameObject[] potionStatus4;

    //+++++++++++++++++++++++++++++++++++++++++
    // �SPL�̃|�[�V�����X�e�[�^�X�i�[�p
    //+++++++++++++++++++++++++++++++++++++++++
    List<GameObject[]> allPotionStatus;

    //+++++++++++++++++++++++++++++++++++++++++
    // �݌v�^�[���i�[�p
    //+++++++++++++++++++++++++++++++++++++++++
    public static int AllTurnNum
    { get; set; }

    //+++++++++++++++++++++++++++++++++++++++++
    // NetworkManager�i�[�p�ϐ�
    //+++++++++++++++++++++++++++++++++++++++++
    NetworkManager networkManager;

    //+++++++++++++++++++++++++++++++++++++++++
    // �w��^�C����pos�i�[�p
    //+++++++++++++++++++++++++++++++++++++++++
    Vector3 tilePos;

    //+++++++++++++++++++++++++++++++++++++++++
    // �|�[�V���������t���O
    //+++++++++++++++++++++++++++++++++++++++++
    bool generateFlag;

    //+++++++++++++++++++++++++++++++++++++++++
    // �L�����ړ��A�C�R���i�[�p
    //+++++++++++++++++++++++++++++++++++++++++
    GameObject[] moveImgs = new GameObject[playerNum];

    //+++++++++++++++++++++++++++++++++++++++++
    // �L���������A�C�R���i�[�p
    //+++++++++++++++++++++++++++++++++++++++++
    GameObject[] generateImgs = new GameObject[playerNum];

    ///========================================
    ///
    /// ���\�b�h
    /// 
    ///========================================

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        //++++++++++++++++++++++++++++++++++++++
        // �݌v�^�[���̏�����
        //++++++++++++++++++++++++++++++++++++++
        AllTurnNum = 1;

        //++++++++++++++++++++++++++++++++++++++
        // ��PLNo�̑��
        //++++++++++++++++++++++++++++++++++++++
#if DEBUG
        //NetworkManager.MyNo = plNo;
#endif
        //++++++++++++++++++++++++++++++++++++++
        // �����̐����{�^����\��
        //++++++++++++++++++++++++++++++++++++++
        brewingButton[NetworkManager.MyNo - 1].SetActive(true);

        //++++++++++++++++++++++++++++++++++++++
        // �|�[�V�����X�e�[�^�X���X�g�̍쐬
        //++++++++++++++++++++++++++++++++++++++
        allPotionStatus = new List<GameObject[]>()
        {
            potionStatus1,    // �v���C���[1
            potionStatus2,    // �v���C���[2
            potionStatus3,    // �v���C���[3
            potionStatus4,    // �v���C���[4
        };

        //++++++++++++++++++++++++++++++++++++++
        // �����ȊO�̃|�[�V�����X�e�[�^�X��\��
        //++++++++++++++++++++++++++++++++++++++
        for (int i = 0; i < playerNum; i++)
        {
            if(i != NetworkManager.MyNo - 1)
            {   // �����ȊO��PL�̃|�[�V�����X�e�[�^�X��\��
                allPotionStatus[i][3].SetActive(true);
            }
        }

        //++++++++++++++++++++++++++++++++++++++
        // NetworkManager���擾
        //++++++++++++++++++++++++++++++++++++++
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        //++++++++++++++++++++++++++++++++++++++
        // �|�[�V���������t���O
        //++++++++++++++++++++++++++++++++++++++
        generateFlag = false;

        for (int i = 0; i < player.Length; i++)
        { //�z�񕪂̃v���C���[�̍\���̂𐶐�
            player[i] = new Player();
        }

        for (int i = 0; i < player.Length; i++)
        { //�v���C���[�����l�ݒ�
            player[i].PlayerState = PLAYERSTATE.NORMAL_STATE;
            player[i].PlayerNo = i + 1;
        }

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
        unitData = new List<GameObject>[tileData.GetLength(0), tileData.GetLength(1)];

        //�^�C��������
        for (int i = 0; i < tileData.GetLength(0); i++)
        {
            for (int j = 0; j < tileData.GetLength(1); j++)
            {
                float x = j - (tileData.GetLength(1) / 2 - 0.5f);
                float z = i - (tileData.GetLength(0) / 2 - 0.5f);

                //�^�C���z�u
                string resname = "";

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
                }
                else if (2 == tileData[i, j].pNo)
                { //2P���j�b�g�z�u
                    resname = "Unit2";
                    playerType = UnitController.TYPE_BLUE;
                    angle.y = 180;        // �I�u�W�F�N�g�̌���
                }
                else if (3 == tileData[i, j].pNo)
                { //3P���j�b�g�z�u
                    resname = "Unit3";
                    playerType = UnitController.TYPE_YELLOW;
                }
                else if (4 == tileData[i, j].pNo)
                { //4P���j�b�g�z�u
                    resname = "Unit4";
                    playerType = UnitController.TYPE_GREEN;
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
                    unitData[i, j].Add(unit);
                }
            }
        }

        for (int i = 0; i < playerNum; i++)
        {   // �L�����A�C�R�����擾
            moveImgs[i] = GameObject.Find((i + 1).ToString() + "MoveImg");
            generateImgs[i] = GameObject.Find((i + 1).ToString() + "GenerateImg");
        }

        for (int i = 0; i < playerNum; i++)
        {   // �L�����A�C�R�����\����
            moveImgs[i].SetActive(false);
            generateImgs[i].SetActive(false);
        }

        // 1P�̈ړ��A�C�R����\��
        SetMoveIcon(1, true);

        nowTurn = 0;
        nextMode = MODE.MOVE_SELECT;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (nowPlayerType >= 4)
        {
            nowPlayerType = 0;
        }

        Mode();

        if (MODE.NONE != nextMode) InitMode(nextMode);
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
        else if(MODE.POTION_THROW == nowMode)
        {
            ThrowPotionMode();
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
        }
        else if (MODE.WAIT_TURN_END == next)
        {
        }
        else if (MODE.FIELD_UPDATE == next)
        {
        }
        nowMode = next;
        nextMode = MODE.NONE;
    }

    /// <summary>
    /// ���j�b�g�ړ����[�h
    /// </summary>
    void SelectMode()
    {
        if (player[nowPlayerType].IsDead == true)
        {   // ���݃^�[���̃v���C���[������ł�����X�L�b�v

            // ���̃^�[����
            nextMode = MODE.FIELD_UPDATE;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ���݂�PL�^�[���\��
        turnText.GetComponent<Text>().text = (nowPlayerType + 1).ToString() + "P�̃^�[��";
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // �����̃^�[���̂ݍs���\��
        if (NetworkManager.MyNo == nowPlayerType + 1)
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        {
            if (player[nowPlayerType].PlayerState == PLAYERSTATE.FROZEN_STATE || player[nowPlayerType].PlayerState == PLAYERSTATE.CURSED_STATE || player[nowPlayerType].IsDead == true)
            { //�����Ă����ꍇ
                Debug.Log((nowPlayerType + 1) + "P�͂������Ȃ��I");
                nextMode = MODE.FIELD_UPDATE;
            }
            else
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
                            // �}�E�X�ŃN���b�N�����^�C����pos���擾
                            tilePos = hit.collider.gameObject.transform.position;

                            // �N���b�N�����^�C���̔z��ԍ����v�Z
                            int x = (int)(tilePos.x + (tileData.GetLength(1) / 2 - 0.5f));
                            int z = (int)(tilePos.z + (tileData.GetLength(0) / 2 - 0.5f));

                            if (0 < unitData[z, x].Count && player[nowTurn].PlayerNo == unitData[z, x][0].GetComponent<UnitController>().PlayerNo)
                            {   // ���j�b�g�I��(�I�������}�X�̃��j�b�g����0�ȏ�E���݂�PL�^�[���ƃN���b�N�����^�C���̃��j�b�g��PLNo����v���Ă���)

                                if (null != selectUnit)
                                {   // ���Ƀ��j�b�g��I�����Ă����ꍇ
                                    selectUnit.GetComponent<UnitController>().Select(false);
                                }

                                //++++++++++++++++++++++++++++++++++++++++++++++++++++//
                                // ��PL�^�[���́u�I�������v�Ƃ��������T�[�o�[�ɑ��� //
                                //++++++++++++++++++++++++++++++++++++++++++++++++++++//
                                networkManager.SendSelectUnit(z, x);
#if DEBUG
                                Debug.Log("�I����񑗐M����");
#endif
                            }
                            else if (null != selectUnit)
                            {   //�ړ���^�C���I��(���j�b�g���I������Ă����ꍇ)

                                if (movableTile(oldX, oldY, x, z))
                                {   // �ړ����肪�ʂ����ꍇ

                                    // ��PL�^�[���́u�ړ������v�Ƃ������(�ړ���̃^�C��)���T�[�o�[�ɑ��� //
                                    // �S�N���C�A���g�Ɉړ�����n������ɉ�ʂɔ��f������ //
                                    networkManager.SendMoveUnit(x, z, tilePos.x, tilePos.z);

                                    // �ړ���񑗐M��ɐ����t���O��false�ɖ߂�
                                    generateFlag = false;
#if DEBUG
                                    Debug.Log("�ړ���񑗐M����");
#endif
                                }

                                Debug.Log("���݂̃v���C���[:" + (nowPlayerType + 1));
                            }
                        }
                    }
                }
            }
        }
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �������W�̃^�C���ɋ��郆�j�b�g��I����Ԃɂ��鏈��
    /// </summary>
    /// <param name="z"> �^�C����z���W </param>
    /// <param name="x"> �^�C����x���W </param>
    public void SelectUnit(int z, int x)
    {
        // �I���������j�b�g��GameObject�����擾
        selectUnit = unitData[z, x][0];

        // �I�����̍��W��ۑ�
        oldX = x;
        oldY = z;

        // �I�����j�b�g��I�����(�����オ�������)�ɕύX
        selectUnit.GetComponent<UnitController>().Select();
    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �������W�̃^�C���ɑI�����ꂽ���j�b�g���ړ����鏈��
    /// </summary>
    /// <param name="z"></param>
    /// <param name="x"></param>
    public void MoveUnit(int z,int x, Vector3 pos)
    {
        isMoved = true;

        // �O�񂢂��^�C���̈ʒu�̃��j�b�g�f�[�^���폜
        unitData[oldY, oldX].Clear();

        // ���j�b�g��I�������^�C����pos�Ɉړ�
        pos.y += 0.1f;
        selectUnit.transform.position = pos;

        // ���݈ʒu��unitData�Ɉړ����������j�b�g��ǉ�
        unitData[z, x].Add(selectUnit);

        // �ړ�����R���C�_�[���I�t��
        unitData[z, x][0].GetComponent<UnitController>().OffColliderEnable();

        // ���̃^�[����
        nextMode = MODE.FIELD_UPDATE;
    }
    // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        //+++++++++++++++++
        // ��������
        //+++++++++++++++++

        //object�Ƃ����^�O���̃Q�[���I�u�W�F�N�g�𕡐��擾��������
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Bomb");

        // �z��̗v�f���ɑ΂��ď������s��
        foreach (GameObject obj in objects)
        {
            // Bomb�̃J�E���g�_�E����������
            obj.GetComponent<PotionBoom>().bombCntDown();
        }
        //+++++++++++++++++

        //+++++++++++++++++
        // �I������
        //+++++++++++++++++

        nextMode = MODE.MOVE_SELECT;

        // �݌v�^�[���\��
        AllTurnNum++;
        allTurnText.text = AllTurnNum.ToString() + "�^�[����"; 

        nowPlayerType++;        
    }

    int getNextTurn()
    { //���^�[�����擾
        int ret = nowTurn;

        ret++;
        if (3 < ret) ret = 0;

        return ret;
    }

    /// <summary>
    /// ���\�[�X���I�u�W�F�N�g�z�u�֐�
    /// </summary>
    /// <param name="name">Object's Name</param>
    /// <param name="pos">Object's Position</param>
    /// <param name="angle">Object's Angle</param>
    /// <returns></returns>
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
    public void TurnEnda()
    {
        nextMode = MODE.MOVE_SELECT;
    }

    /// <summary>
    /// �|�[�V����������񑗐M�{�^��
    /// </summary>
    public void Brewing()
    {
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // �����̃^�[���ȊO�����\��
        if (NetworkManager.MyNo != nowPlayerType + 1 && !generateFlag)
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        {
            // int rndPotion = r.Next(4);
            if (player[nowPlayerType].PlayerState == PLAYERSTATE.PARALYSIS_STATE || player[nowPlayerType].PlayerState == PLAYERSTATE.FROZEN_STATE || player[nowPlayerType].IsDead == true)
            { //���т�Ă����ꍇ�܂��͓����Ă����ꍇ
                Debug.Log((NetworkManager.MyNo) + "P�͂��т�Ă���B�|�[�V���������Ȃ��I");
            }
            else
            {
                if (player[nowPlayerType].OwnedPotionList?.Count >= 2)
                { //�g�����܂��Ă����ꍇ
                    Debug.Log((NetworkManager.MyNo) + "P�̃|�[�V�����g�͖��t���I�I");
                }
                else
                {
                    // �|�[�V���������t���O��true��
                    generateFlag = true;

                    // �~�j�Q�[���̍Đ�(��)
                    Instantiate(minigamePrefab, minigamePrefab.GetComponent<Transform>().position, Quaternion.identity);

                    // �|�[�V�������������T�[�o�[�ɑ��M
                    networkManager.SendPotionStatus((int)EventID.PotionGenerate);
                }
            }
        }
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �|�[�V������������
    /// </summary>
    /// <param name="plNo">��������PLNo</param>
    public void GeneratePotion(int plNo)
    {
        if (player[plNo - 1].OwnedPotionList.Count == 0)
        {   // �|�[�V�����̎��������O�̎�

            // ���j�|�[�V����1�̐���
            BoomPotion1[plNo - 1].SetActive(true);
            player[plNo - 1].OwnedPotionList.Add(TYPE.BOMB);
        }
        else if (player[plNo - 1].OwnedPotionList.Count == 1)
        {
            // ���j�|�[�V����2�̐���
            BoomPotion2[plNo - 1].SetActive(true);
            player[plNo - 1].OwnedPotionList.Add(TYPE.BOMB);
        }
    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �w��PLNo�̃|�[�V�����X�e�[�^�X��ҋ@�ɕύX����
    /// </summary>
    /// <param name="plNo">�v���C���[No</param>
    /// <param name="staNum">�X�e�[�^�XNo</param>
    IEnumerator ChangeStatusWait(int plNo, float delay) // [0.����  1.���s  2.����  3.�ҋ@]
    {
        // �w��b���ҋ@��
        yield return new WaitForSeconds(delay);

        // �X�e�[�^�X�\����S��\����
        for (int i = 0; i < allPotionStatus[plNo - 1].GetLength(0); i++)
        {
            allPotionStatus[plNo - 1][i].SetActive(false);
        }

        if (NetworkManager.MyNo != plNo)
        {
            // �w��PLNo���w��X�e�ɕύX
            allPotionStatus[plNo - 1][3].SetActive(true);
        }
    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �w��PLNo�̃|�[�V�����X�e�[�^�X�ύX����
    /// </summary>
    /// <param name="plNo">�v���C���[No</param>
    /// <param name="staNum">�X�e�[�^�XNo</param>
    public void ChangePotionStatus(int plNo,int staNum) // [0.����  1.���s  2.����  3.�ҋ@]
    {
        // �X�e�[�^�X�\����S��\����
        for (int i = 0; i < allPotionStatus[plNo - 1].GetLength(0); i++)
        {
            allPotionStatus[plNo - 1][i].SetActive(false);
        }

        if(NetworkManager.MyNo != plNo)
        {   // ������PLNo�ȊO�̎�

            // �w��PLNo���w��X�e�ɕύX
            allPotionStatus[plNo - 1][staNum].SetActive(true);
        }

        if(staNum == 0 ||  staNum == 1) 
        {   // �����E���s�\���̏ꍇ

            // �w��b����A�w��X�e����ҋ@�ɖ߂�
            StartCoroutine(ChangeStatusWait(plNo, 2.0f));
        }
    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    /// <summary>
    /// �|�[�V������������
    /// </summary>
    void ThrowPotionMode()
    {
        if (Input.GetMouseButtonDown(0))
        { //�N���b�N���A�����ʒu��ݒ�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (null != hit.collider.gameObject)
                {
                    Vector3 selectPos = hit.collider.gameObject.transform.position;

                    // �ݒu�����T�[�o�[�ɑ��M
                    networkManager.SendThrowPos(selectPos.x, selectPos.z);
                }
            }
        }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �|�[�V������������
    /// </summary>
    public void PlayerThrow()
    {
        // �������[�h�ɕύX
        nowMode = MODE.POTION_THROW;

        //���̃|�[�V�����ɂ������A�j���[�V����������
        GameObject.Find("Unit" + (nowPlayerType + 1) + "(Clone)").GetComponent<UnitController>().animator.SetBool("isThrow", true);

        Vector3 pos = SerchUnit((nowPlayerType + 1));

        int x = (int)(pos.x + (tileData.GetLength(1) / 2 - 0.5f));
        int z = (int)(pos.z + (tileData.GetLength(0) / 2 - 0.5f));

        unitData[z, x][0].GetComponent<UnitController>().ThrowSelect();
        unitData[z, x][0].GetComponent<UnitController>().OnThrowColliderEnable();
    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// �|�[�V�����ݒu����
    /// </summary>
    public void SetPotion(Vector3 pos)
    {
        BoomPotion1[nowPlayerType].SetActive(false);                //�g�p�����|�[�V�����̃A�C�R��������
        player[nowPlayerType].OwnedPotionList.Remove(TYPE.BOMB);    //�g�p�����|�[�V���������X�g����폜����

        Vector3 PlPos = SerchUnit((nowPlayerType + 1));

        int x = (int)(PlPos.x + (tileData.GetLength(1) / 2 - 0.5f));
        int z = (int)(PlPos.z + (tileData.GetLength(0) / 2 - 0.5f));

        unitData[z, x][0].GetComponent<UnitController>().OffThrowColliderEnable();

        string resname = "BombPotion";
        resourcesInstantiate(resname, pos, Quaternion.Euler(0, 0, 0));
        nextMode = MODE.FIELD_UPDATE;
    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    /// <summary>
    /// �|�[�V�����g�p�{�^��
    /// </summary>
    public void UsePotion(int buttonNum)
    {
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // �����̃^�[���̂ݍs���\��
        if (NetworkManager.MyNo == nowPlayerType + 1)
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        {
            if (player[nowPlayerType].PlayerState == PLAYERSTATE.FROZEN_STATE || player[nowPlayerType].IsDead == true)
            { //�����Ă����ꍇ
                Debug.Log((nowPlayerType + 1) + "P�͓����Ă���B�|�[�V�����͎g���Ȃ��I");
            }
            else
            {
                //�g�ʔ���
                if (buttonNum == 1)
                { //1�Ԗڂ̏ꍇ
                    if (player[nowPlayerType].OwnedPotionList.Contains(TYPE.BOMB))
                    {
                        // �T�[�o�[�Ƀ{���g�p�t���O�𑗐M
                        networkManager.SendPotionStatus((int)EventID.PotionThrow);
                    }
                    else
                    {
                        Debug.Log((nowPlayerType + 1) + "P�͂܂�1�g�ڂ̃|�[�V����������ĂȂ�!!");
                    }
                }
                else if (buttonNum == 2)
                { //�Q�Ԗڂ̏ꍇ
                    if (player[nowPlayerType].OwnedPotionList.Contains(TYPE.BOMB))
                    {
                        // �T�[�o�[�Ƀ{���g�p�t���O�𑗐M
                        networkManager.SendPotionStatus((int)EventID.PotionThrow);
                    }
                    else
                    {
                        Debug.Log((nowPlayerType + 1) + "P�͂܂�2�g�ڂ̃|�[�V����������ĂȂ�!!");
                    }
                }
            }
        }
    }

    /// <summary>
    /// �w�胆�j�b�g�j�󏈗�
    /// </summary>
    /// <param name="unitType"></param>
    public void DestroyUnit(List<int> deadList)
    {
        GameObject Unit;
        for (int i = 0; i < unitData.GetLength(0); i++)
        {
            for (int j = 0; j < unitData.GetLength(1); j++)
            {
                if (unitData[i, j].Count > 0)
                {
                    Unit = unitData[i, j][0];

                    for (int k = 0;k <deadList.Count;k++)
                    {
                        if (Unit.GetComponent<UnitController>().Type == deadList[k])
                        { //���Y���j�b�g���E��
                            Destroy(Unit);
                            player[deadList[k]-1].IsDead = true;
                        }
                    }
                }
            }
        }

        Debug.Log("���S");
    }

    /// <summary>
    /// �o�t����
    /// </summary>
    /// <param name="unitType"></param>
    public void BuffUnit(int unitType, TYPE buffType)
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
                        switch (buffType)
                        { //�o�t�|�[�V�����ʏ���
                            case TYPE.REFRESH: //���t���b�V���|�[�V�����̏���
                                player[unitType].PlayerState = PLAYERSTATE.NORMAL_STATE;
                                break;
                            case TYPE.INVISIBLE: //���G�|�[�V�����̏���
                                player[unitType].PlayerState = PLAYERSTATE.INVICIBLE_STATE;
                                break;
                            case TYPE.MUSCLE: //�ؗ̓|�[�V�����̏���
                                player[unitType].PlayerState = PLAYERSTATE.MUSCLE_STATE;
                                break;
                        }
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// �f�o�t����
    /// </summary>
    /// <param name="unitType"></param>
    public void DebuffUnit(int unitType, TYPE debuffType)
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
                        switch (debuffType)
                        { //�o�t�|�[�V�����ʏ���
                            case TYPE.SOUR: //���X�b�p�C�|�[�V�����̏���
                                GameObject.Find("Unit" + (nowPlayerType + 1) + "(Clone)").GetComponent<UnitController>().animator.SetBool("isParalysis", true);
                                player[unitType].PlayerState = PLAYERSTATE.PARALYSIS_STATE;
                                break;

                            case TYPE.CURSE: //�r�l�߂̎􂢂̏���
                                GameObject.Find("Unit" + (nowPlayerType + 1) + "(Clone)").GetComponent<UnitController>().animator.SetBool("isCurse", true);
                                player[unitType].PlayerState = PLAYERSTATE.CURSED_STATE;
                                break;

                            case TYPE.ICE: //�A�C�X�|�[�V�����̏���
                                GameObject.Find("Unit" + (nowPlayerType + 1) + "(Clone)").GetComponent<UnitController>().animator.SetBool("isFrost", true);
                                player[unitType].PlayerState = PLAYERSTATE.FROZEN_STATE;
                                break;
                        }
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ���j�b�g�w�菈��
    /// </summary>
    /// <param name="unitType"></param>
    public Vector3 SerchUnit(int unitType)
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
                        Vector3 pos = Unit.transform.position;
                        return pos;
                    }
                }
            }
        }
        return new Vector3(0, 0, 0);
    }

    /// <summary>
    /// �ړ��A�C�R���\��
    /// </summary>
    /// <param name="plNo">�v���C���[No</param>
    /// <param name="flag">�\���E��\���t���O</param>
    public void SetMoveIcon(int plNo, bool flag)
    {
        if (moveImgs[plNo-1] != null) 
        {
            moveImgs[plNo - 1].SetActive(flag);
        }
    }

    /// <summary>
    /// �����A�C�R���\��
    /// </summary>
    /// <param name="plNo">�v���C���[No</param>
    /// <param name="flag">�\���E��\���t���O</param>
    public void SetGenerateIcon(int plNo, bool flag)
    {
        if(generateImgs[plNo - 1] != null)
        {
            generateImgs[plNo - 1].SetActive(flag);
        }
    }
}