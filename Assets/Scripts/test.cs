using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public float progressValue = 0f;

    private float _progressTime = 10f;
    private GameObject _progressBar;

    void Start()
    {
        string url = Application.streamingAssetsPath + "/xmls";
        string fileName = "PlayBackXml";
        string filepath = url + "//" + fileName + ".xml";
        string targetName = "123";
        PlayBackXml xml = new PlayBackXml();
        xml.XmlCreate(filepath, targetName);
        //foreach (var item in PlayBackController.Instance.dicCommandDescribe)
        //{
        //    Dictionary<string, List<PlayBackData>> dicData = PlayBackController.Instance.dicCommandDescribe[item.Key];
        //    foreach (var data in dicData)
        //    {
        //        List<PlayBackData> dataList = dicData[data.Key];
        //        if (!string.IsNullOrEmpty(targetName))
        //        {
        //            xml.InsertXml(filepath,targetName, item.Key, dataList, item);
        //        }
        //    }
        //}
        //PlayBackLoad.Load(filepath, name);
    }
    void Update()
    {
        if (PlayBackController.Instance.isPlayBack)
        {
            if (progressValue < 1)
            {
                progressValue += Time.deltaTime * 1.0f / _progressTime;
                _progressBar.GetComponent<Image>().fillAmount = progressValue;
            }
            if (progressValue >= 1)
            {
                PlayBackController.Instance.isPlayBack = false;
            }
        }
    }
    /// <summary>
    /// 播放回放
    /// </summary>
    private void OnPlayButtonEvent()
    {
        if (progressValue >= 1)
        {
            PlayBackController.Instance.isPlayBack = false;
            return;
        }
        PlayBackController.Instance.isPlayBack = true;
        // playButton.gameObject.SetActive(false);
        // pauseBtn.gameObject.SetActive(true);
        float timeStamp = (float)((PlayBackController.Instance.deduceTime - PlayBackController.Instance.timeStamp).TotalSeconds);
        _progressTime = timeStamp;
    }
    /// <summary>
    /// 快进回放
    /// </summary>
    private void OnFastButtonEvent()
    {
        if (PlayBackController.Instance.isPlayBack)
        {
            if (progressValue >= 1)
            {
                PlayBackController.Instance.isPlayBack = false;
                return;
            }
            DateTime tempTime = PlayBackController.Instance.timeStamp;
            PlayBackController.Instance.timeStamp = PlayBackController.Instance.timeStamp.AddSeconds(10);
            PlayBackQuicken.Instance.SetQuickenContent(tempTime, PlayBackController.Instance.timeStamp);
            float timeStamp = (float)((PlayBackController.Instance.deduceTime - PlayBackController.Instance.timeStamp).TotalSeconds);
            progressValue = ((_progressTime - timeStamp) / (_progressTime));
            _progressBar.GetComponent<Image>().fillAmount = progressValue;
        }
    }
    /// <summary>
    /// 暂停回放
    /// </summary>
    private void OnPauseButtonEvent()
    {
        PlayBackController.Instance.isPlayBack = false;
    }
}
