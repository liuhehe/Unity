using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
public class PlayBackLoad
{
    public static void Load(string filepath, string readName)
    {
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNode nodeName = xmlDoc.FirstChild;
            XmlNodeList nodeNameList = nodeName.ChildNodes;
            
            foreach (XmlElement node in nodeNameList)
            {
                if (node.GetAttribute("name") == readName)
                {
                    XmlNodeList playBackList = node.ChildNodes;
                    foreach (XmlElement xe in playBackList)
                    {
                        PlayBackData data = new PlayBackData();
                        data.time = DateTime.Parse(xe.GetAttribute("time"));
                        data.describe = xe.GetAttribute("describe");
                        XmlNodeList playBackAttributeList = xe.ChildNodes;
                        foreach (XmlElement x in playBackAttributeList)
                        {
                            XmlAttributeCollection col = x.Attributes;
                            if (x.Name == "state")
                            {
                                XmlAttribute stateAttribute = col["value"];
                                data.state = int.Parse(stateAttribute.Value);
                            }
                        }
                        if (PlayBackController.Instance.dicPlaybackData.Count > 0)
                        {
                            PlayBackController.Instance.dicPlaybackData.Clear();
                        }
                        if (PlayBackController.Instance.dicPlayBack.ContainsKey(data.time))
                        {
                            PlayBackController.Instance.dicPlayBack[data.time].Add(data);
                        }
                        else
                        {
                            List<PlayBackData> ls = new List<PlayBackData>();
                            ls.Add(data);
                            PlayBackController.Instance.dicPlayBack.Add(data.time, ls);
                        }
                    }
                }
            }
        }
    }
}
public struct PlayBackData
{
    public DateTime time;
    public string describe;
    public int state;
}
