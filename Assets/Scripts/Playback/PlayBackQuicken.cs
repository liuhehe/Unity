using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayBackQuicken : MonoBehaviour
{
    public bool isQuicken = false;
    public static PlayBackQuicken Instance;

    void Awake()
    {
        Instance = this;
    }
    public void SetQuickenContent(DateTime currentTime, DateTime timeStamp)
    {
        List<DateTime> tempTime = new List<DateTime>();

        foreach (DateTime item in PlayBackController.Instance.dicPlayBack.Keys)
        {
            if (item <= currentTime)
            {
                continue;
            }
            if (timeStamp <= item)
            {
                PlayBackController.Instance.isPlayBack = true;
                break;
            }
            SetQuicken(item);
        }
    }
    void SetQuicken(DateTime time)
    {
        List<PlayBackData> dataList = null;
        if (PlayBackController.Instance.dicPlayBack.TryGetValue(time, out dataList))
        {
            if (dataList == null)
                return;
            foreach (PlayBackData data in dataList)
            {
                if (data.describe == GlobalParameter.JZJ_STATE)
                {

                }
            }
        }
    }
}

