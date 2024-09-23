using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct curCatchPuzzle
{
    public GameObject puzzle;
    public int oldx;
    public int oldy;
}

public class _GameManager : MonoBehaviour
{
    private static _GameManager instance;
    public static _GameManager Instance
    {
        get {
            if (instance == null) {
                instance = new _GameManager();
            }
            return instance;
        }
    }

    //ʵ����UI�ܹ��Ĳ��Ա���
    public Action OnPuzzleDrug;
    public Action OnPuzzleSetDown;

    [SerializeField] private int CanMoveCount = 3;
    [SerializeField] private int CurrentLevel = 1;
    [SerializeField] private float time = 0f;

    private BaseModel baseModel;

    /// ����Ϸ�߼��йصı���
    private Grid grid;
    [SerializeField] private LevelGenerator levelGenerator;   //�ؿ�������
    [SerializeField] private int width = 7;
    [SerializeField] private int height = 3;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector3 originPos;
    [SerializeField] private int x,y;   //��굱ǰ�����ĸ�������
    [SerializeField] private GameObject baseBrick;
    [SerializeField] private GameObject puzzleBrick;

    private curCatchPuzzle curCatchBrick;   //��굱ǰץס�ķ���

    public event Action  OnMouseDown;
    public event Action  OnMouseMove;
    public event Action  OnMouseUp;

    public Grid Grid
    {
        get { return grid; }
    }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        baseModel = BaseModel.Instance;
        baseModel.InitData(CurrentLevel, time,CanMoveCount);

        OnPuzzleSetDown += UpdateGameData;
        OnMouseDown += CatchPuzzle;
        OnMouseMove += PuzzleMove;
        OnMouseMove += GetXYFromMousePos;
        OnMouseUp += SetPuzzleDown;
    }

    private void Start()
    {
        GameStart(CanMoveCount, CurrentLevel);
    }

    private void Update()
    {
        UpdateTimeDate();

        //������ƴͼ�ƶ�
        if(Input.GetMouseButtonDown(0)){
            OnMouseDown?.Invoke();
        }
        if(Input.GetMouseButton(0) && curCatchBrick.puzzle!= null){
            OnMouseMove?.Invoke();
        }
        if(Input.GetMouseButtonUp(0)){
            OnMouseUp?.Invoke();
        }

    }

    private void OnDestroy()
    {
        OnPuzzleSetDown -= UpdateGameData;
        OnMouseDown -= CatchPuzzle;
        OnMouseMove -= PuzzleMove;
        OnMouseUp -= SetPuzzleDown;
    }

    private void UpdateGameData()
    {
        baseModel.UpdateData(CurrentLevel, CanMoveCount);
    }

    private void UpdateTimeDate()
    {
        time += Time.deltaTime;
        baseModel.UpdateTime(time);
    }


//----������Ϸ���̵ĺ���----
    /// <summary>
    /// ��Ϸ��ʼʱ���õĺ�������������������������Ϣ�ĳ�ʼ��
    /// </summary>
    /// <param name="Count">���ƶ�����</param>
    /// <param name="Level">��ǰ�ؿ�</param>
    public void GameStart(int Count, int Level)
    {
        CanMoveCount = Count;
        CurrentLevel = Level;

        grid = new Grid(width, height, cellSize, originPos);

        //��ʱ��ʼ��һЩ�����õ�ש��
        //TODO����ʼ������ש��
        // for(x = 0; x < width; x++){
        //     for(y = 0; y < height; y++){
        //         grid.SetBase(x, y, Instantiate(baseBrick));
        //     }
        // }

        // for(x = 0; x < width; x++){
        //     for(y = 0; y < 1; y++){
        //         grid.SetPuzzle(x, y, Instantiate(puzzleBrick));
        //     }
        // }

         levelGenerator.LevelInit(Level,Count,grid);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(0);
    }

//----������Ϸ���ݺ��߼��ĺ���----
    private void GetXYFromMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        grid.GetXY(worldPos, out x, out y);
    }

    private void CatchPuzzle()
    {
        if (curCatchBrick.puzzle != null)
        {return;}

        GetXYFromMousePos();
        curCatchBrick.puzzle = grid.CatchPuzzle(x, y);
        if (curCatchBrick.puzzle != null)
        {
            curCatchBrick.oldx = x;
            curCatchBrick.oldy = y;
            //Debug.Log($"ץס��ƴͼ{curCatchBrick.puzzle.name},������λ����{curCatchBrick.oldx},{curCatchBrick.oldy}");
        }
        else {
            //Debug.Log("�ô�û�п�ץȡ��ƴͼ");
        }
    }

    private void PuzzleMove()
    {
        if (curCatchBrick.puzzle == null)
        {return;}

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curCatchBrick.puzzle.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
    }

    private void SetPuzzleDown()
    {
        if (curCatchBrick.puzzle == null)
        {return;}
        
        if(grid.SetPuzzleDown(curCatchBrick.oldx, curCatchBrick.oldy, x, y, curCatchBrick.puzzle)){
            //Debug.Log("ƴͼ������");
        }
        else {
            //Debug.Log("����ʧ�ܣ���λ����������ƴͼ");
        }
        curCatchBrick.puzzle = null;
    }



    public void UseOnePuzzle()
    {
        if(CanMoveCount > 0) {
            CanMoveCount--;
            Debug.Log("ʣ����ƶ�������"  + CanMoveCount);
        }
    }

    public bool CanMovePuzzle()
    {
        if (CanMoveCount > 0) {
            return true;
        }
        else {
            Debug.Log("ʣ����ƶ��������㣬�޷��ƶ�");
            return false;
        }
    }
}
