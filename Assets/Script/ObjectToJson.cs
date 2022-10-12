using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using JsonHelper = Script.JsonHelper;
using Object = Script.Object;

[Serializable]
public class ObjectToJson : MonoBehaviour
{
    
    void Start()
    {
        Object[] objArr =
        {
        //Add Your Array Here
        };
        Debug.Log(objArr[0].Type);
        string path = Application.dataPath + "/Resources";
        string fileName = "";//Add your Filename Here
        string playerToJson = JsonHelper.ToJson(objArr);
        Debug.Log(playerToJson);
        if (!Directory.Exists(path))
        {

            Directory.CreateDirectory(path);

        };
        fileName = Path.Combine(path, fileName);     //将文件名和路径合并

        if (!File.Exists(fileName))     //判断文件是否已经存在不存在就创建一个文件；
        {

            FileStream fs = File.Create(fileName);

            fs.Close();

        }
        File.WriteAllText(fileName, playerToJson, Encoding.UTF8);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
