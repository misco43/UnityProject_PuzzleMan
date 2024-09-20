using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Action OnPuzzleDrug;
    public Action OnPuzzleSetDown;

    [SerializeField] private int CanMoveCount = 3;
    [SerializeField] private int CurrentLevel = 1;
    [SerializeField] private float time = 0f;

    private BaseModel baseModel;

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
    }

    private void Update()
    {
        UpdateTimeDate();
    }

    private void OnDestroy()
    {
        OnPuzzleSetDown -= UpdateGameData;
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

    public void GameStart(int Count, int Level)
    {
        CanMoveCount = Count;
        CurrentLevel = Level;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(0);
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

    public void test()
    {
        //�����������ʱ���ᴥ��OnPuzzleSetDown�¼�
        OnPuzzleSetDown?.Invoke();
    }
}
