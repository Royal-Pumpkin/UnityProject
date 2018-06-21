using UnityEngine;
//using System.Data.SQLite;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class DBManager
{
    //private static DBManager instance = null;
    //public static DBManager Instance
    //{
    //    get {
    //        if(instance == null)
    //        {
    //            instance = new DBManager();
    //        }
    //        return instance;
    //    }
    //}
    //string[] selectQuerys = {
    //    "select gold,key, gem, key_recovery_time from player",
    //    "select num, difficulty, clear, star from stage_clear ",
    //    "select num, usable, need_num, next_num, get_tower from tower_tree;"
    //};
    //static string dbName = "data.db";
    //static string filepath = string.Empty;
    //string dataSource = "Data Source=" + Path.GetFullPath(filepath);
    //SQLiteConnection connection = null;

    //public static void CopyDB()
    //{
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        filepath = Application.persistentDataPath + "/"+ dbName;
    //        filepath.Trim();
    //        if (!File.Exists(filepath))
    //        {

    //            WWW loadDB = new WWW(Path.Combine(Application.streamingAssetsPath, dbName));
    //            loadDB.bytesDownloaded.ToString();
    //            while (!loadDB.isDone) ;
    //            File.WriteAllBytes(filepath, loadDB.bytes);
    //        }
    //    }
    //    else
    //    {
    //        filepath = Application.dataPath+"/"+dbName;
    //        filepath.Trim();

    //        if (!File.Exists(filepath))
    //        {
    //            File.Copy(Path.Combine(Application.streamingAssetsPath, dbName), filepath);
    //        }
    //    }
    //}
    
    //public Dictionary<string, object> SelectOne(int queryNumber,string[] keys)
    //{
    //    Dictionary<string, object> pairs = new Dictionary<string, object>();
    //    using (SQLiteConnection conn = GetConnection())
    //    {
    //        conn.Open();
    //        SQLiteCommand sqlcmd = new SQLiteCommand(selectQuerys[queryNumber], conn);
    //        SQLiteDataReader rd = sqlcmd.ExecuteReader();
    //        sqlcmd.Dispose();
    //        if (rd.Read())
    //        {                
    //            for(int i = 0; i < keys.Length; i++)
    //            {                    
    //                pairs[keys[i]] = rd[keys[i]];
    //            }
    //        }
    //        rd.Close();
    //    }
    //    return pairs;
    //}
    //public List<Dictionary<string, object>> SelectList(int queryNumber, string[] keys)
    //{
    //    List<Dictionary<string, object>> pairsList = new List<Dictionary<string, object>>();
    //    using (SQLiteConnection conn = GetConnection())
    //    {
    //        conn.Open();
    //        SQLiteCommand sqlcmd = new SQLiteCommand(selectQuerys[queryNumber], conn);
    //        SQLiteDataReader rd = sqlcmd.ExecuteReader();
    //        sqlcmd.Dispose();
    //        while (rd.Read())
    //        {
    //            Dictionary<string, object> pairs = new Dictionary<string, object>();
    //            for (int i = 0; i < keys.Length; i++)
    //            {
    //                pairs[keys[i]] = rd[keys[i]];
    //            }
    //            pairsList.Add(pairs);
    //        }
    //        rd.Close();
    //    }
    //    return pairsList;
    //}


    //public void Select()
    //{
    //    using (SQLiteConnection conn=GetConnection())
    //    {
    //        conn.Open();

    //        string strSQL = "CREATE TABLE member (name string)";
    //        SQLiteCommand sqlcmd = new SQLiteCommand(strSQL, conn);
    //        sqlcmd.ExecuteNonQuery();
    //        sqlcmd.Dispose();

    //        strSQL = "INSERT INTO member VALUES ('WestwoodForever SQLite C# Sample.')";
    //        sqlcmd = new SQLiteCommand(strSQL, conn);
    //        sqlcmd.ExecuteNonQuery();
    //        sqlcmd.Dispose();

    //        strSQL = "SELECT * FROM member";
    //        sqlcmd = new SQLiteCommand(strSQL, conn);
    //        SQLiteDataReader rd = sqlcmd.ExecuteReader();
    //        sqlcmd.Dispose();
    //        while (rd.Read())
    //        {
    //            Debug.Log(rd.GetString(0));
    //        }
    //        rd.Close();

    //    }
    //}

    //public SQLiteConnection GetConnection()
    //{
    //    return new SQLiteConnection(dataSource); ;
    //}    
}