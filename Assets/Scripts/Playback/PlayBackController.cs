using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackController : MonoBehaviour
{
    public string targetName;       //任务名称
    public DateTime deduceTime;     //推演时间
    public DateTime timeStamp;      //时间戳
    public bool isPlayBack = false;
   
    public Dictionary<DateTime,Dictionary<string,List<PlayBackData>>> dicPlaybackData = new Dictionary<DateTime, Dictionary<string, List<PlayBackData>>>();
    public Dictionary<DateTime, List<PlayBackData>> dicPlayBack = new Dictionary<DateTime, List<PlayBackData>>();

    public static PlayBackController Instance;

    private float _nexTime = 0;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (isPlayBack)
        {
            if (_nexTime > 1)
            {
                timeStamp = timeStamp.AddSeconds(1);
                _nexTime = 0;
                List<PlayBackData> dataList = null;
                if (dicPlayBack.TryGetValue(timeStamp, out dataList))
                {
                    if (dataList == null)
                        return;

                    foreach (PlayBackData data in dataList)
                    {
                        if (data.describe == "")
                        {
                           //Todo
                        }
                    }
                }
            }
            _nexTime += Time.deltaTime;
        }
    }

    public void SavePlayBackData(string objName,string describe)
    {
        if (PlayBackController.Instance.dicPlaybackData.ContainsKey(deduceTime))
        {
            List<PlayBackData> dataList = new List<PlayBackData>();
            PlayBackData data = new PlayBackData();
            //if (PlayBackController.Instance.dicCommandDescribe[deduceTime].ContainsKey(data.JzjName))
            //{
            //    PlayBackController.Instance.dicCommandDescribe[deduceTime][data.JzjName].Add(data);
            //}
            //else
            //{
            //    dataList.Add(data);
            //    PlayBackController.Instance.dicCommandDescribe[deduceTime].Add(data.JzjName, dataList);
            //}
        }
        else
        {
            Dictionary<string, List<PlayBackData>> dicData = new Dictionary<string, List<PlayBackData>>();
            List<PlayBackData> dataList = new List<PlayBackData>();
            PlayBackData data = new PlayBackData();
            data.describe = describe;
            //if (dicData.ContainsKey(data.JzjName))
            //{
            //    dicData[data.JzjName].Add(data);
            //}
            //else
            //{
            //    dataList.Add(data);
            //    dicData.Add(data.JzjName, dataList);
            //}
            PlayBackController.Instance.dicPlaybackData.Add(deduceTime, dicData);
        }
    }
}

