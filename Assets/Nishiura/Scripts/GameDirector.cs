using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    //Player
    public bool[] isPlayer; 
    Player[] player;
    int nowTurn;

    //GameMode
    enum MODE
    {
        NONE=-1,
        WAIT_TURN_START,
        MOVE_SELECT,
        FIELD_UPDATE,
        WAIT_TURN_END,
        TURN_CHANGE,
    }

    //MODE 
    MODE nowMode;
    MODE nextMode;

    //�ҋ@���Ԓ�`
    float waitTime;

    //�t�B�[���h
    int[,] tileData = new int[,]
    {//��O
        {0,0,8,0,0,0,0,0,8,0,0},
        {0,2,1,1,1,1,1,1,1,2,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,2,1,1,1,1,1,1,1,2,0},
        {0,0,4,0,0,0,0,0,4,0,0},
    };

    //�v���C���[�����z�u
    int[,]initUnitData = new int[,] 
    {//��O
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,1,0,0,0,0,0,1,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
        {0,0,2,0,0,0,0,0,2,0,0},
        {0,0,0,0,0,0,0,0,0,0,0},
    };

    //�v���C���[�ő吔
    const int PLAYER_MAX = 12;

    //�t�B�[���h��̃v���C���[
    List<GameObject>[,] unitData;

    //�v���C���[�I�����[�h�Ŏg��
    GameObject selectUnit;
    int oldX, oldY;


    //�{�^�����̃I�u�W�F�N�g
    GameObject txtInfo;
    GameObject buttonTurnEnd;
    GameObject objCamera;

    // Start is called before the first frame update
    void Start()
    {
        List<int> p1rnd = getRandomList(PLAYER_MAX, PLAYER_MAX / 2);
        List<int> p2rnd = getRandomList(PLAYER_MAX, PLAYER_MAX / 2);
        int p1 = 0;
        int p2 = 0;

        unitData = new List<GameObject>[tileData.GetLength(0), tileData.GetLength(1)];

        //�v���C���[�ݒ�
        player = new Player[2];     //�l��
        player[0] = new Player(isPlayer[0], 1);
        player[1] = new Player(isPlayer[1], 2);

        //�^�C��������
        for(int i = 0; i<tileData.GetLength(0); i++)
        {
            for (int j = 0; j < tileData.GetLength(1); j++)
            {
                float x =j - (tileData.GetLength(1) /2 - 0.5f);
                float z = i - (tileData.GetLength(0) / 2 - 0.5f);

                //�^�C���z�u
                string resname = "";

                //1:NormalTile 2:GoalTile 3:1P'sGoal 4:2P's Goal
                int no = tileData[i,j];
                if (4 == no || 8 == no) no = 5;

                resname = "Cube (" + no + ")";

                resourcesInstantiate(resname, new Vector3(x, 0, z), Quaternion.identity);

                //�v���C���[�z�u
                unitData[i,j] = new List<GameObject>();

                Vector3 angle = new Vector3(0,0,0);
                int playerType = UnitController.TYPE_BLUE;

                List<int> unitrnd = new List<int>();

                int unitNum = -1;

                //1P�z�u
                resname = "Unit1";

                if(1 == initUnitData[i,j])
                {
                    unitrnd = p1rnd;
                    unitNum = p1++;
                }
                else if (2 == initUnitData[i, j])
                {
                    unitrnd = p2rnd;
                    unitNum = p2++;
                    angle.y = 180;
                }
                else
                {
                    resname = "";
                }

                //�ԃ��j�b�g�z�u����
                if (-1 < unitrnd.IndexOf(unitNum))
                {
                    resname = "Unit2";
                    playerType = UnitController.TYPE_RED;
                }

                GameObject unit = resourcesInstantiate(resname, new Vector3(x, 0.6f, z), Quaternion.Euler(angle));

                if(null != unit)
                {
                    unit.GetComponent<UnitController>().PlayerNo = initUnitData[i,j];
                    unit.GetComponent<UnitController>().Type = playerType;

                    unitData[i,j].Add(unit);
                }
            }

            nowTurn = 0;
            nextMode = MODE.MOVE_SELECT;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWait()) return;

        Mode();

        if (MODE.NONE != nextMode) InitMode(nextMode);
    }

    bool isWait()
    { //CPU�̑ҋ@����(�g���) �܂��͉�������ҋ@����
        bool ret = false;

        if(0 <waitTime)
        {
            if (MODE.NONE != nextMode) InitMode(nextMode);
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// ���C�����[�h
    /// </summary>
    /// <param name="next"></param>
    void Mode()
    {
        if(MODE.MOVE_SELECT ==nowMode)
        {
            SelectMode();
        }
        else if(MODE.FIELD_UPDATE ==nowMode)
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
        if(MODE.MOVE_SELECT ==next)
        {
            selectUnit = null;
        }

        nowMode = next;
        nextMode = MODE.NONE;
    }

    void SelectMode()
    {
        GameObject hitobj = null;

        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,100))
            {
                hitobj = hit.collider.gameObject;
            }
        }

        if(null == hitobj) return;

        Vector3 pos = hitobj.transform.position;

        int x = (int)(pos.x + (tileData.GetLength(1) / 2 - 0.5f));
        int z = (int)(pos.z + (tileData.GetLength(0) / 2 - 0.5f));

        //���j�b�g�I��
        if(0 < unitData[z,x].Count
           && player[nowTurn].PlayerNo == unitData[z, x][0].GetComponent<UnitController>().PlayerNo)
        {
            Debug.Log("aaaaaaaaaa");

            if(null != selectUnit)
            {
                selectUnit.GetComponent<UnitController>().Select(false);
            }
            selectUnit = unitData[z, x][0];
            oldX = x;
            oldY = z;

            selectUnit.GetComponent<UnitController>().Select();
        }
        else if(null != selectUnit)
        {//�ړ���^�C���I��
            if(movableTile(oldX,oldY,x,z))
            {
                unitData[oldY, oldX].Clear();
                pos.y += 0.5f;
                selectUnit.transform.position = pos ;

                unitData[z, x].Add(selectUnit);

                nextMode = MODE.FIELD_UPDATE;
            }
        }
    }

    void FieldUpdateMode()
    {
        for(int i = 0; i <unitData.GetLength(0); i++)
        {
            for (int j = 0; j < unitData.GetLength(0); j++)
            {
                //�S�[�����폜
                if(1 == unitData[i,j].Count && player[nowTurn].PlayerNo*4 == tileData[i,j])
                {
                    if(UnitController.TYPE_BLUE == unitData[i, j][0].GetComponent<UnitController>().Type) 
                    {//����win
                        player[nowTurn].IsClear = true;
                    }
                    Destroy(unitData[i, j][0]);
                    unitData[i, j].RemoveAt(0);
                }
                //�d�����A���j�b�g���폜
                if (1 < unitData[i, j].Count)
                {
                    if (UnitController.TYPE_RED == unitData[i, j][0].GetComponent<UnitController>().Type)
                    {//�ԃ��j�b�g������
                        player[nowTurn].Hp++;
                        waitTime = 1.5f;
                    }
                    else
                    {//���j�b�g������
                        player[nowTurn].Socre++;
                        waitTime = 1.5f;
                    }

                    Destroy(unitData[i, j][0]);
                    unitData[i, j].RemoveAt(0);
                }
            }
        }

        nextMode = MODE.TURN_CHANGE;
    }

    void TurnChangeMode()
    {
        //���̃^�[����
        nowTurn = getNextTurn();
        nextMode = MODE.MOVE_SELECT;
    }

    int getNextTurn()
    {
        int ret = nowTurn;

        ret++;
        if (1 < ret) ret = 0;

        return ret;
    }

    //�����_���z�u�֐�(�g���)
    List<int>getRandomList(int range,int count)
    {
        List<int> ret = new List<int>();

        if(range < count)
        {
            Debug.LogError("���X�g�����G���[");
            return ret;
        }

        while(true)
        {
            int no = Random.Range(0,range);

            if(-1 == ret.IndexOf(no))
            {
                ret.Add(no);
            }
            if(count <= ret.Count)
            {
                break;
            }
        }

        return ret;
    }

    GameObject resourcesInstantiate(string name,Vector3 pos, Quaternion angle)
    {
        GameObject prefab =(GameObject)Resources.Load(name);

        if(null == prefab )
        {
            return null;
        }

        GameObject ret = Instantiate(prefab, pos, angle);

        return ret;
    }

    bool movableTile(int oldx,int oldz,int x, int z)
    {
        bool ret = false;

        //�������擾
        int dx = Mathf.Abs(oldx - x);
        int dz = Mathf.Abs(oldz - z);

        //�΂ߐi�s�s��
        //if(1 <dx + dz)
        //{
        //    ret = false;
        //}

        //�ǈȊO
        if(1 == tileData[z,x] || 2 == tileData[z,x] || player[nowTurn].PlayerNo*4 == tileData[z, x])
        {
           
            if( 0== unitData[z,x].Count) 
            { //�N�����Ȃ��}�X
                ret = true;
            }
            else
            {//�N������}�X

                if (unitData[z, x][0].GetComponent<UnitController>().PlayerNo != player[nowTurn].PlayerNo)
                {//�G�������ꍇ
                    ret = true;
                }
            }
        }

        return ret;
    }
}
