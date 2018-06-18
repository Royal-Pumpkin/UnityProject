using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Text;
using System.IO;
public class FileUtil
{   
    static string GetPath(string fileName)
    {
        return Path.Combine(Application.streamingAssetsPath, fileName + ".json");
    }

    static JsonData LoadJData(string path)
    {
#if UNITY_ANDROID
        WWW file = new WWW(path);

        if (file != null)
        {
            while (!file.isDone) ;
            string jsonStr = Encoding.UTF8.GetString(file.bytes);

            //Debug.Log(jsonStr);
            JsonData json = JsonMapper.ToObject(jsonStr.Trim());
            return json;
        }

#else
        Debug.Log(File.Exists(path));
        if (File.Exists(path))
        {
            string jsonStr = Encoding.UTF8.GetString(File.ReadAllBytes(path));
            JsonData json = JsonMapper.ToObject(jsonStr);
            return json;
        }
        //if (File.Exists(path)){
        //    string jsonStr = File.ReadAllText(path);
        //    JsonData json = JsonMapper.ToObject(jsonStr);
        //    return json;
        
#endif
        return null;
    }
    /// <summary>
    /// 파일이 저장되어 있는 경로 포함
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static JsonData LoadJsonData(string fileName)
    {
        return LoadJData(GetPath(fileName));
    }


    public static List<JsonData> LoadJsonDataInFolder(string folderName)
    {

        string path = Path.Combine(Application.streamingAssetsPath, "Data/" + folderName);
        List<JsonData> jsonDatas = new List<JsonData>();
        FileInfo[] info = new DirectoryInfo(path).GetFiles();

        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].Name.Contains(".meta"))
            {
                continue;
            }
            JsonData jd = LoadJData(path + "/" + info[i].Name);
            if (jd != null)
            {
                jsonDatas.Add(jd);
            }

        }
        if (jsonDatas.Count > 0)
        {
            return jsonDatas;
        }
        else
        {
            return null;
        }
    }

    public void SaveJsonData(string fileName, JsonData data)
    {
        
    }
}
