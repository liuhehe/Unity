using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;

public class PlayBackXml
{
    public void XmlCreate(string filepath, string targetName)
    {
        if (File.Exists(filepath))
        {
            //创建XML文档实例
            XmlDocument xmlDoc = new XmlDocument();
            //创建root节点，也就是最上一层节点
            XmlElement root = xmlDoc.CreateElement("Point");
            XmlElement elmNew = xmlDoc.CreateElement("targetName");
            elmNew.SetAttribute("name", targetName);
            root.AppendChild(elmNew);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(filepath);
        }
    }
  
    public  void InsertXml(string filepath,string targetName, DateTime time, List<PlayBackData> dataList,string data)
    {
        int i = 0;
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNode root = xmlDoc.SelectSingleNode("Point");
            XmlNode elmNew = root.SelectSingleNode("targetName");
            XmlElement r = (XmlElement)elmNew;
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Point").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("name").Contains(targetName) == false)
                {
                    i = 0;
                }
                if (xe.GetAttribute("name").Contains(targetName) == true)
                {
                    i++;
                }
            }
            if (i == 0)
            {
                XmlElement target = xmlDoc.CreateElement("targetName");
                target.SetAttribute("name", targetName);
                root.AppendChild(target);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(filepath);
            }
            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("name").Contains(targetName) == true)
                {
                    createNode(filepath, time, dataList, xmlDoc, root, xe,data);
                }
            }
        }
    }

    private void createNode(string filepath, DateTime time, List<PlayBackData> dataList, XmlDocument xmlDoc, XmlNode root, XmlElement elmNew,string data)
    {
        XmlElement t = xmlDoc.CreateElement("Datatime");
        t.SetAttribute("time", time.ToString());
        XmlElement describe = null;
       
        foreach (var item in dataList)
        {
            if (item.describe == "")
            {
                t.SetAttribute("describe", item.describe);
                t.AppendChild(describe);

                XmlElement date2 = xmlDoc.CreateElement(data);
                date2.InnerText = data;
                t.AppendChild(date2);
            }
        }
        if (elmNew != null)
        {
            elmNew.AppendChild(t);
        }
        root.AppendChild(elmNew);
        xmlDoc.AppendChild(root);
        xmlDoc.Save(filepath);
    }
   
}
