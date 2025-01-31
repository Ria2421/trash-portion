//
// ゲームディレクターコピースクリプト
// Name:中本健太 Date:02/07
// Update:04/15
//
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameDirectorCopy : MonoBehaviour
{
    /// <summary>
    /// タイル配置設定
    /// 0:Wall 1:NormalTile 2:SpawnPoint 3:Object1 4:1P 5:2P 6:3P 7:4P
    /// </summary>
    int[,] initTileData = new int[,]
    {
        {0,0,0,0,0,0,0,0,0,0,0},//手前
        {0,6,1,1,1,1,1,1,1,4,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,3,1,3,1,1,1,0},
        {0,1,1,3,1,1,1,3,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,1,1,3,1,1,1,3,1,1,0},
        {0,1,1,1,3,1,3,1,1,1,0},
        {0,1,1,1,1,1,1,1,1,1,0},
        {0,7,1,1,1,1,1,1,1,5,0},
        {0,0,0,0,0,0,0,0,0,0,0},
    };

    /// <summary>
    /// プレイヤー初期配置
    /// </summary>
    int[,] initUnitData = new int[,]
    {
        {0,0,0,0,0,0,0,0,0,0,0},//手前
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
    /// ゲームモード
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
        END_GAME,
    }

    ///========================================
    ///
    /// フィールド
    /// 
    ///========================================

    /// <summary>
    /// 各サウンドエフェクト
    /// </summary>
    [SerializeField] AudioClip deathSE;     //死亡時SE
    [SerializeField] AudioClip boomSE;      //爆発時SE
    [SerializeField] AudioClip selectSE;    //選択時SE
    [SerializeField] AudioClip throwSE;     //投擲時SE
    [SerializeField] AudioSource audioSource;

    /// <summary>
    /// プレイヤー 配列
    /// </summary>
    Player[] player = new Player[playerNum];

    /// <summary>
    /// ポーションの種類
    /// </summary>
    PotionType potionType = new PotionType();

    /// <summary>
    /// プレイヤー人数
    /// </summary>
    const int playerNum = 4;

    /// <summary>
    /// ブロックレイヤー特定用マスク
    /// </summary>
    const int blockLayerMask = 1 << 7;

    /// <summary>
    /// フィールド上のプレイヤーリスト
    /// </summary>
    List<GameObject>[,] unitData;

    /// <summary>
    /// 選択ユニット
    /// </summary>
    GameObject selectUnit;
    int oldX, oldY;

    /// <summary>
    /// プレイヤーの移動判定変数
    /// </summary>
    bool isMoved;

    /// <summary>
    /// プレイヤータイプ
    /// </summary>
    int playerType = UnitController.TYPE_RED;

    /// <summary>
    /// 現在のターンのプレイヤータイプ
    /// </summary>
    int nowPlayerType = 0;

    /// <summary>
    /// タイルデータ構造体の宣言
    /// </summary>
    TileData[,] tileData;

    /// <summary>
    /// 現在のターン
    /// </summary>
    int nowTurn;

    /// <summary>
    /// 現在のモード
    /// </summary>
    MODE nowMode;

    /// <summary>
    /// 次のモード
    /// </summary>
    public MODE nextMode;

    /// <summary>
    /// (仮)ポーションランダム生成変数
    /// </summary>
    System.Random r = new System.Random();

    public bool IsMoved
    { //移動判定のプロパティ
        get { return isMoved; }
    }

    /// <summary>
    /// ターン表示テキスト
    /// </summary>
    [SerializeField] GameObject turnText;

    /// <summary>
    /// 累計ターン表示テキスト
    /// </summary>
    [SerializeField] Text allTurnText;

    /// <summary>
    /// PLNo決め打ち用
    /// </summary>
    [SerializeField] int plNo;

    /// <summary>
    /// ミニゲームプレハブ
    /// </summary>
    [SerializeField] GameObject minigamePrefab;

    /// <summary>
    /// 全PLのポーションステータス格納用
    /// </summary>
    List<GameObject[]> allPotionStatus;

    /// <summary>
    /// 累計ターン格納用
    /// </summary>
    public static int AllTurnNum
    { get; set; }

    /// <summary>
    /// NetworkManager格納用変数
    /// </summary>
    NetworkManager networkManager;

    /// <summary>
    /// 指定タイルのpos格納用
    /// </summary>
    Vector3 tilePos;

    /// <summary>
    /// ポーション生成フラグ
    /// </summary>
    bool generateFlag;

    /// <summary>
    /// 勝利テキスト格納用
    /// </summary>
    [SerializeField] GameObject winText;

    /// <summary>
    /// 各ポーションのアイコン
    /// </summary>
    [SerializeField] GameObject[] BoomPotion1;
    [SerializeField] GameObject[] BoomPotion2;

    /// <summary>
    /// キャラ移動アイコン格納用
    /// </summary>
    GameObject[] moveImgs = new GameObject[playerNum];

    /// <summary>
    /// キャラ生成アイコン格納用
    /// </summary>
    GameObject[] generateImgs = new GameObject[playerNum];

    /// <summary>
    /// 死亡人数カウント用変数
    /// </summary>
    public int DeadPlayerCnt
    { get; private set; }

    /// <summary>
    /// 勝利PLNo
    /// </summary>
    int winnerNum;

    /// <summary>
    /// GameEndフラグ
    /// </summary>
    bool gameEnd = false;

    /// <summary>
    /// 生成ボタンオブジェクト
    /// </summary>
    [SerializeField] GameObject[] brewingButton;

    /// <summary>
    /// 生成禁止ボタンオブジェクト
    /// </summary>
    [SerializeField] GameObject[] cantBrewingButton;

    /// <summary>
    /// 各PLのポーションステータス (4種類) [0.完了  1.失敗  2.生成  3.待機]
    /// </summary>
    [SerializeField] GameObject[] potionStatus1;
    [SerializeField] GameObject[] potionStatus2;
    [SerializeField] GameObject[] potionStatus3;
    [SerializeField] GameObject[] potionStatus4;

    /// <summary>
    /// キャラノーマルアイコン
    /// </summary>
    [SerializeField] GameObject[] normalIcons;

    /// <summary>
    /// キャラデスアイコン
    /// </summary>
    [SerializeField] GameObject[] deathIcons;

    /// <summary>
    /// プレイヤー名格納用テキスト
    /// </summary>
    [SerializeField] Text[] playerNameText;

    ///========================================
    ///
    /// メソッド
    /// 
    ///========================================

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // 累計ターンの初期化
        AllTurnNum = 1;

        // 仮PLNoの代入
#if DEBUG
        //NetworkManager.MyNo = plNo;
#endif
        // 自分の生成ボタンを表示
        brewingButton[NetworkManager.MyNo - 1].SetActive(true);

        // ポーションステータスリストの作成
        allPotionStatus = new List<GameObject[]>()
        {
            potionStatus1,    // プレイヤー1
            potionStatus2,    // プレイヤー2
            potionStatus3,    // プレイヤー3
            potionStatus4,    // プレイヤー4
        };

        // 自分以外のポーションステータスを表示
        for (int i = 0; i < playerNum; i++)
        {
            if(i != NetworkManager.MyNo - 1)
            {   // 自分以外のPLのポーションステータスを表示
                allPotionStatus[i][3].SetActive(true);
            }
        }

        // NetworkManagerを取得
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        // プレイヤー名を取得
        for (int i = 0; i < playerNum; i++)
        {
            playerNameText[i].text = networkManager.PlayerNames[i];
        }

        // ポーション生成フラグ
        generateFlag = false;

        // 移動フラグ
        isMoved = true;

        for (int i = 0; i < player.Length; i++)
        { //配列分のプレイヤーの構造体を生成
            player[i] = new Player();
        }

        for (int i = 0; i < player.Length; i++)
        { //プレイヤー初期値設定
            player[i].PlayerState = PLAYERSTATE.NORMAL_STATE;
            player[i].PlayerNo = i + 1;
        }

        // タイル配置情報分の配列を生成
        tileData = new TileData[initTileData.GetLength(0), initTileData.GetLength(1)];

        // タイルデータに配置位置を代入
        for (int i = 0; i < initTileData.GetLength(0); i++)
        {
            for (int j = 0; j < initTileData.GetLength(1); j++)
            {
                tileData[i, j] = new TileData(initTileData[i, j]);
            }
        }

        // タイルデータにプレイヤー情報を代入
        for (int i = 0; i < initTileData.GetLength(0); i++)
        {
            for (int j = 0; j < initTileData.GetLength(1); j++)
            {
                tileData[i, j].pNo = initUnitData[i, j];
            }
        }
        unitData = new List<GameObject>[tileData.GetLength(0), tileData.GetLength(1)];

        //タイル初期化
        for (int i = 0; i < tileData.GetLength(0); i++)
        {
            for (int j = 0; j < tileData.GetLength(1); j++)
            {
                float x = j - (tileData.GetLength(1) / 2 - 0.5f);
                float z = i - (tileData.GetLength(0) / 2 - 0.5f);

                //タイル配置
                string resname = "";

                int no = tileData[i, j].tNo;
                //if (4 == no || 8 == no) no = 5;
                if (8 == no) no = 5;

                resname = "Cube (" + no + ")";

                resourcesInstantiate(resname, new Vector3(x, 0, z), Quaternion.identity);

                //プレイヤー配置
                unitData[i, j] = new List<GameObject>();

                //プレイヤー毎設定
                Vector3 angle = new Vector3(0, 0, 0);

                if (1 == tileData[i, j].pNo)
                { //1Pユニット配置
                    resname = "Unit1";
                    playerType = UnitController.TYPE_RED;
                }
                else if (2 == tileData[i, j].pNo)
                { //2Pユニット配置
                    resname = "Unit2";
                    playerType = UnitController.TYPE_BLUE;
                    angle.y = 180;        // オブジェクトの向き
                }
                else if (3 == tileData[i, j].pNo)
                { //3Pユニット配置
                    resname = "Unit3";
                    playerType = UnitController.TYPE_YELLOW;
                }
                else if (4 == tileData[i, j].pNo)
                { //4Pユニット配置
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
        {   // キャラアイコン情報取得
            moveImgs[i] = GameObject.Find((i + 1).ToString() + "MoveImg");
            generateImgs[i] = GameObject.Find((i + 1).ToString() + "GenerateImg");
        }

        for (int i = 0; i < playerNum; i++)
        {   // キャラアイコンを非表示に
            moveImgs[i].SetActive(false);
            generateImgs[i].SetActive(false);
        }

        // 1Pの移動アイコンを表示
        SetMoveIcon(1, true);

        if(NetworkManager.MyNo == 1)
        {   // 1Pの生成ボタンを非表示に
            brewingButton[NetworkManager.MyNo - 1].SetActive(false);
            cantBrewingButton[NetworkManager.MyNo - 1].SetActive(true);
        }

        nowTurn = 0;
        nextMode = MODE.MOVE_SELECT;
    }

    /// <summary>
    /// 更新処理
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
    /// メインモード
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
        else if(MODE.END_GAME == nowMode)
        {
            if(!gameEnd)
            {
                GameEnd();
            }
        }
    }

    /// <summary>
    /// 次のモード準備
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
        else if(MODE.END_GAME == next)
        {
        }
        nowMode = next;
        nextMode = MODE.NONE;
    }

    /// <summary>
    /// ユニット移動モード
    /// </summary>
    void SelectMode()
    {
        if (DeadPlayerCnt >= 3)
        {   // 終了判定
            nextMode = MODE.END_GAME;
        }

        if (player[nowPlayerType].IsDead == true)
        {   // 現在ターンのプレイヤーが死んでいたらスキップ

            // 次のターンへ
            nextMode = MODE.FIELD_UPDATE;
        }
        else
        {
            // 現在ターンのプレイヤーの移動アイコンを表示
            SetMoveIcon(nowPlayerType+1, true);
        }

        // 現在のPLターン表示
        turnText.GetComponent<Text>().text = playerNameText[nowPlayerType].text;

        // 自分のターンのみ行動可能に
        if (NetworkManager.MyNo == nowPlayerType + 1)
        {
            if (player[nowPlayerType].PlayerState == PLAYERSTATE.FROZEN_STATE || player[nowPlayerType].PlayerState == PLAYERSTATE.CURSED_STATE || player[nowPlayerType].IsDead == true)
            { //凍っていた場合
                Debug.Log((nowPlayerType + 1) + "Pはうごけない！");
                nextMode = MODE.FIELD_UPDATE;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                { //クリック時、ユニット選択
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100, blockLayerMask))
                    {
                        if (null != hit.collider.gameObject)
                        {
                            // マウスでクリックしたタイルのposを取得
                            tilePos = hit.collider.gameObject.transform.position;

                            // クリックしたタイルの配列番号を計算
                            int x = (int)(tilePos.x + (tileData.GetLength(1) / 2 - 0.5f));
                            int z = (int)(tilePos.z + (tileData.GetLength(0) / 2 - 0.5f));

                            if (0 < unitData[z, x].Count && player[nowTurn].PlayerNo == unitData[z, x][0].GetComponent<UnitController>().PlayerNo)
                            {   // ユニット選択(選択したマスのユニット数が0以上・現在のPLターンとクリックしたタイルのユニットのPLNoが一致してたら)

                                // 移動可能フラグをfalseに
                                isMoved = false;

                                // 現PLターンの「選択した」という情報をサーバーに送る //
                                networkManager.SendSelectUnit(z, x);

                                //選択SE
                                audioSource.PlayOneShot(selectSE);
#if DEBUG
                                Debug.Log("選択情報送信完了");
#endif
                            }
                            else if (null != selectUnit)
                            {   //移動先タイル選択(ユニットが選択されていた場合)

                                if (movableTile(oldX, oldY, x, z))
                                {   // 移動判定が通った場合

                                    // 移動情報送信後に生成フラグをfalseに戻す
                                    generateFlag = false;
                                    brewingButton[NetworkManager.MyNo-1].SetActive(true);
                                    cantBrewingButton[NetworkManager.MyNo - 1].SetActive(false);

                                    // 現PLターンの「移動した」という情報(移動先のタイル)をサーバーに送る //
                                    networkManager.SendMoveUnit(x, z, tilePos.x, tilePos.z);

                                    //選択SE
                                    audioSource.PlayOneShot(selectSE);
#if DEBUG
                                    Debug.Log("移動情報送信完了");
#endif
                                }

                                Debug.Log("現在のプレイヤー:" + (nowPlayerType + 1));
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 引数座標のタイルに居るユニットを選択状態にする処理
    /// </summary>
    /// <param name="z"> タイルのz座標 </param>
    /// <param name="x"> タイルのx座標 </param>
    public void SelectUnit(int z, int x)
    {
        // 選択したユニットのGameObject情報を取得
        selectUnit = unitData[z, x][0];

        // 選択時の座標を保存
        oldX = x;
        oldY = z;

        // 選択ユニットを選択状態(持ち上がった状態)に変更
        selectUnit.GetComponent<UnitController>().Select();
    }

    /// <summary>
    /// 引数座標のタイルに選択されたユニットを移動する処理
    /// </summary>
    /// <param name="z"></param>
    /// <param name="x"></param>
    public void MoveUnit(int z,int x, Vector3 pos)
    {
        isMoved = true;

        // 前回いたタイルの位置のユニットデータを削除
        unitData[oldY, oldX].Clear();

        // ユニットを選択したタイルのposに移動
        pos.y += 0.1f;
        selectUnit.transform.position = pos;

        // 現在位置のunitDataに移動させたユニットを追加
        unitData[z, x].Add(selectUnit);

        // 移動判定コライダーをオフに
        unitData[z, x][0].GetComponent<UnitController>().OffColliderEnable();

        // 次のターンへ
        nextMode = MODE.FIELD_UPDATE;
    }

    void FieldUpdateMode()
    {
        nextMode = MODE.TURN_CHANGE;
    }

    /// <summary>
    /// ターンチェンジ
    /// </summary>
    void TurnChangeMode()
    {
        nextMode = MODE.NONE;
        nextMode = MODE.MOVE_SELECT;

        int oldTurn = nowTurn;
        nowTurn = getNextTurn();

        /* 爆発判定 */

        //objectというタグ名のゲームオブジェクトを複数取得したい時
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Bomb");

        if (!player[nowPlayerType].IsDead)
        {   // 現在ターンのPLが死亡していない時のみ

            // 配列の要素一つ一つに対して処理を行う
            foreach (GameObject obj in objects)
            {
                // Bombのカウントダウンを下げる
                obj.GetComponent<PotionBoom>().bombCntDown();
            }
        }

        /* 終了判定 */
        if (DeadPlayerCnt >= 3)
        {   
            nextMode = MODE.END_GAME;
        }
        else
        {
            nextMode = MODE.MOVE_SELECT;

            // 累計ターン表示
            AllTurnNum++;
            allTurnText.text = AllTurnNum.ToString() + " ターン目";

            nowPlayerType++;
        }     
    }

    int getNextTurn()
    { //次ターンを取得
        int ret = nowTurn;

        ret++;
        if (3 < ret) ret = 0;

        return ret;
    }

    /// <summary>
    /// リソース内オブジェクト配置関数
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

        // 差分を取得
        int dx = Mathf.Abs(oldx - x);
        int dz = Mathf.Abs(oldz - z);

        Debug.Log("x:" + x);
        Debug.Log("z:" + z);

        // 斜め進行不可
        if (dx + dz > 2 || dx > 1 || dz > 1)
        {
            Debug.Log("進行不可");
            Debug.Log("Z:" + z + " " + "X:" + x);

            return ret = false;
        }

        // 壁以外
        if (1 == tileData[z, x].tNo
           || 2 == tileData[z, x].tNo
           || player[nowTurn].PlayerNo * 4 == tileData[z, x].tNo)
        {
            if (0 == unitData[z, x].Count)
            { //誰もいないマス
                ret = true;
            }
            else
            { //誰かいるマス
                if (unitData[z, x][0].GetComponent<UnitController>().PlayerNo != player[nowTurn].PlayerNo)
                { //敵だった場合
                    ret = false;
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// ターン終了ボタン
    /// </summary>
    public void TurnEnda()
    {
        nextMode = MODE.MOVE_SELECT;
    }

    /// <summary>
    /// ポーション生成情報送信ボタン
    /// </summary>
    public void Brewing()
    {
        // 自分のターン以外生成可能に
        if (NetworkManager.MyNo != nowPlayerType + 1 && !generateFlag)
        {
            if (player[NetworkManager.MyNo - 1].IsDead == true)
            { //死亡しているかどうか
                Debug.Log(NetworkManager.MyNo + "Pはしんでいる。ポーションが作れない！");
            }
            else
            {
                if (player[NetworkManager.MyNo - 1].OwnedPotionList?.Count >= 2)
                { //枠が埋まっていた場合
                    Debug.Log(NetworkManager.MyNo + "Pのポーション枠は満杯だ！！");
                }
                else
                {
                    // ポーション生成フラグをtrueに
                    generateFlag = true;
                    brewingButton[NetworkManager.MyNo - 1].SetActive(false);
                    cantBrewingButton[NetworkManager.MyNo - 1].SetActive(true);

                    // ミニゲームの再生(仮)
                    Instantiate(minigamePrefab, minigamePrefab.GetComponent<Transform>().position, Quaternion.identity);

                    // ポーション生成情報をサーバーに送信
                    networkManager.SendPotionStatus((int)EventID.PotionGenerate);
                }
            }
        }
    }

    /// <summary>
    /// ポーション生成処理
    /// </summary>
    /// <param name="plNo">生成したPLNo</param>
    public void GeneratePotion(int plNo)
    {
        if (player[plNo - 1].OwnedPotionList.Count == 0)
        {   // ポーションの持ち数が０個の時

            // 爆破ポーション1の生成
            BoomPotion1[plNo - 1].SetActive(true);
            player[plNo - 1].OwnedPotionList.Add(TYPE.BOMB1);
        }
        else if (player[plNo - 1].OwnedPotionList.Count == 1)
        {
            // 爆破ポーション2の生成
            BoomPotion2[plNo - 1].SetActive(true);
            player[plNo - 1].OwnedPotionList.Add(TYPE.BOMB2);
        }
    }

    /// <summary>
    /// 指定PLNoのポーションステータスを待機に変更処理
    /// </summary>
    /// <param name="plNo">プレイヤーNo</param>
    /// <param name="staNum">ステータスNo</param>
    IEnumerator ChangeStatusWait(int plNo, float delay) // [0.完了  1.失敗  2.生成  3.待機]
    {
        // 指定秒数待機後
        yield return new WaitForSeconds(delay);

        // ステータス表示を全非表示に
        for (int i = 0; i < allPotionStatus[plNo - 1].GetLength(0); i++)
        {
            allPotionStatus[plNo - 1][i].SetActive(false);
        }

        if (NetworkManager.MyNo != plNo)
        {
            // 指定PLNoを指定ステに変更
            allPotionStatus[plNo - 1][3].SetActive(true);
        }
    }

    /// <summary>
    /// 指定PLNoのポーションステータス変更処理
    /// </summary>
    /// <param name="plNo">プレイヤーNo</param>
    /// <param name="staNum">ステータスNo</param>
    public void ChangePotionStatus(int plNo,int staNum) // [0.完了  1.失敗  2.生成  3.待機]
    {
        // ステータス表示を全非表示に
        for (int i = 0; i < allPotionStatus[plNo - 1].GetLength(0); i++)
        {
            allPotionStatus[plNo - 1][i].SetActive(false);
        }

        if(NetworkManager.MyNo != plNo)
        {   // 自分のPLNo以外の時

            // 指定PLNoを指定ステに変更
            allPotionStatus[plNo - 1][staNum].SetActive(true);
        }

        if(staNum == 0 ||  staNum == 1) 
        {   // 成功・失敗表示の場合

            // 指定秒数後、指定ステから待機に戻す
            StartCoroutine(ChangeStatusWait(plNo, 2.0f));
        }
    }

    /// <summary>
    /// ポーション投擲処理
    /// </summary>
    void ThrowPotionMode()
    {
        if (NetworkManager.MyNo == nowPlayerType + 1)
        {   // 自分のターンのみ行動可能に
            if (Input.GetMouseButtonDown(0))
            {   //クリック時、投擲位置を設定
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, blockLayerMask))
                {
                    if (null != hit.collider.gameObject)
                    {   // RayにTileオブジェが当たった時

                        if (hit.collider.gameObject.GetComponent<Tile>().ReturnPlayerFlag())
                        {   // タイルのプレイヤーフラグがtrueだったら

                            Vector3 selectPos = hit.collider.gameObject.transform.position;

                            // 設置情報をサーバーに送信
                            networkManager.SendThrowPos(selectPos.x, selectPos.z);

                            // ポーション生成可能に
                            generateFlag = false;
                            brewingButton[NetworkManager.MyNo - 1].SetActive(true);
                            cantBrewingButton[NetworkManager.MyNo - 1].SetActive(false);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ポーション投擲処理
    /// </summary>
    public void PlayerThrow()
    {
        //選択SE
        audioSource.PlayOneShot(selectSE);

        // 投擲モードに変更
        nowMode = MODE.POTION_THROW;

        //そのポーションにあったアニメーションをする
        GameObject.Find("Unit" + (nowPlayerType + 1) + "(Clone)").GetComponent<UnitController>().animator.SetBool("isThrow", true);

        Vector3 pos = SerchUnit((nowPlayerType + 1));

        int x = (int)(pos.x + (tileData.GetLength(1) / 2 - 0.5f));
        int z = (int)(pos.z + (tileData.GetLength(0) / 2 - 0.5f));

        unitData[z, x][0].GetComponent<UnitController>().ThrowSelect();
        unitData[z, x][0].GetComponent<UnitController>().OnThrowColliderEnable();
    }

    /// <summary>
    /// ポーション設置処理
    /// </summary>
    public void SetPotion(Vector3 pos)
    {
        //投擲SE
        audioSource.PlayOneShot(throwSE);

        if (player[nowPlayerType].OwnedPotionList.Count == 1)
        {   // ポーションの持ち数が０個の時

            // 爆破ポーション1の生成
            BoomPotion1[nowPlayerType].SetActive(false);                //使用したポーションのアイコンを消す
            player[nowPlayerType].OwnedPotionList.Remove(TYPE.BOMB1);    //使用したポーションをリストから削除する
        }
        else if (player[nowPlayerType].OwnedPotionList.Count == 2)
        {
            // 爆破ポーション2の生成
            BoomPotion2[nowPlayerType].SetActive(false);                //使用したポーションのアイコンを消す
            player[nowPlayerType].OwnedPotionList.Remove(TYPE.BOMB2);    //使用したポーションをリストから削除する
        }

        Vector3 PlPos = SerchUnit((nowPlayerType + 1));

        int x = (int)(PlPos.x + (tileData.GetLength(1) / 2 - 0.5f));
        int z = (int)(PlPos.z + (tileData.GetLength(0) / 2 - 0.5f));

        unitData[z, x][0].GetComponent<UnitController>().OffThrowColliderEnable();

        string resname = "BombPotion";
        resourcesInstantiate(resname, pos, Quaternion.Euler(0, 0, 0));

        // 移動モードに戻る
        nowMode = MODE.MOVE_SELECT;
    }

    /// <summary>
    /// ポーション使用ボタン
    /// </summary>
    public void UsePotion(int buttonNum)
    {
        if (!isMoved)
        {   // 移動中の時は使用できない
            return;
        }

        // 自分のターンのみ行動可能に
        if (NetworkManager.MyNo == nowPlayerType + 1)
        {
            if (player[nowPlayerType].PlayerState == PLAYERSTATE.FROZEN_STATE || player[nowPlayerType].IsDead == true)
            { //凍っていた場合
                Debug.Log((nowPlayerType + 1) + "Pは凍っている。ポーションは使えない！");
            }
            else
            {
                //選択SE
                audioSource.PlayOneShot(selectSE);

                //枠別判定
                if (buttonNum == 1)
                { //1番目の場合
                    if (player[nowPlayerType].OwnedPotionList.Contains(TYPE.BOMB1))
                    {
                        // 移動情報送信後に生成フラグをfalseに戻す
                        generateFlag = false;
                        brewingButton[NetworkManager.MyNo - 1].SetActive(true);
                        cantBrewingButton[NetworkManager.MyNo - 1].SetActive(false);

                        // サーバーにボム使用フラグを送信
                        networkManager.SendPotionStatus((int)EventID.PotionThrow);
                    }
                    else
                    {
                        Debug.Log((nowPlayerType + 1) + "Pはまだ1枠目のポーションを作ってない!!");
                    }
                }
                else if (buttonNum == 2)
                { //２番目の場合
                    if (player[nowPlayerType].OwnedPotionList.Contains(TYPE.BOMB2))
                    {
                        // 移動情報送信後に生成フラグをfalseに戻す
                        generateFlag = false;
                        brewingButton[NetworkManager.MyNo - 1].SetActive(true);
                        cantBrewingButton[NetworkManager.MyNo - 1].SetActive(false);

                        // サーバーにボム使用フラグを送信
                        networkManager.SendPotionStatus((int)EventID.PotionThrow);
                    }
                    else
                    {
                        Debug.Log((nowPlayerType + 1) + "Pはまだ2枠目のポーションを作ってない!!");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 指定ユニット破壊処理
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
                        { //当該ユニットを殺す
                            //死亡SE
                            audioSource.PlayOneShot(deathSE);

                            // 該当ユニットを破棄
                            Destroy(Unit);

                            // 死亡フラグをtrueに
                            player[deadList[k]-1].IsDead = true;

                            // 死亡アイコンの表示
                            deathIcons[deadList[k]-1].SetActive(true);

                            unitData[i, j] = new List<GameObject>();
                            DeadPlayerCnt++;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ユニット指定処理
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
    /// 移動アイコン表示
    /// </summary>
    /// <param name="plNo">プレイヤーNo</param>
    /// <param name="flag">表示・非表示フラグ</param>
    public void SetMoveIcon(int plNo, bool flag)
    {
        for (int i = 0; i < playerNum; i++)
        {   // 全員非表示に
            if (player[i].IsDead == false)
            {
                moveImgs[i].SetActive(false);
            }
        }

        if (moveImgs[plNo - 1] != null)
        {
            moveImgs[plNo - 1].SetActive(flag);
        }
    }

    /// <summary>
    /// 生成アイコン表示
    /// </summary>
    /// <param name="plNo">プレイヤーNo</param>
    /// <param name="flag">表示・非表示フラグ</param>
    public void SetGenerateIcon(int plNo, bool flag)
    {
        if(generateImgs[plNo - 1] != null)
        {
            generateImgs[plNo - 1].SetActive(flag);
        }
    }

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    void GameEnd()
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].IsDead == false)
            {   // 勝利したPLNoを取得
                winnerNum = player[i].PlayerNo;
            }
        }

        // 勝利テキストを有効化
        winText.SetActive(true);

        if(winnerNum == 0) 
        {   // 勝者が0人の時
            winText.GetComponent<Text>().text = "引き分け";

            PlayerRank.WinnerID = new int[] { 0, 1, 2, 3 };
        }
        else
        {   // 誰か勝者がいるとき
            winText.GetComponent<Text>().text = winnerNum.ToString() + "の勝ち!";

            PlayerRank.WinnerID = new int[] { winnerNum - 1 };
        }

        Invoke("InResultScene",1.0f);

        // サーバーにゲーム終了フラグを送信
        if(NetworkManager.MyNo == 1)
        {   // 1Pのみ

            // ゲーム終了フラグを送信
            networkManager.SendEndFlag();
        }

        gameEnd = true;
    }

    /// <summary>
    /// リザルトシーンに移動
    /// </summary>
    void InResultScene()
    {
        /* フェード処理 (黒)  
             ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("Result", Color.black, 1.5f);
    }
}