using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    //�ؼ��Ĳ���
    [SerializeField] private Text stepCountText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text levelText;

    //�ؼ����޸ķ���
    public void UpdateInfo(int stepCount, int level)
    {
        stepCountText.text = "ʣ�ಽ����" + stepCount.ToString();
        levelText.text = "�ؿ���" + level.ToString();
    }

    public void UpdateTime(float time)
    {
        timeText.text = "ʱ�䣺" + time.ToString("F0");
    }
}
