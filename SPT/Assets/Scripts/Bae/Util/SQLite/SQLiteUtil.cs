using UnityEngine;
using System.Collections;
using System.Data.SQLite;
using System.IO;
public class SQLiteUtil
{
    void CopyDB()
    {
        string filepath = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/dbname.db";
            if (!File.Exists(filepath))
            {

                WWW loadDB = new WWW(Path.Combine(Application.streamingAssetsPath, ""));
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }
        }
        else
        {
            filepath = Application.dataPath + "/dbname.db";
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/dbname.db", filepath);
            }
        }
    }

    public void Test()
    {
        string strConn = @"Data Source=D:\testdb.db";

        using (SQLiteConnection conn = new SQLiteConnection(strConn))
        {
            conn.Open();

            string strSQL = "CREATE TABLE member (name string)";
            SQLiteCommand sqlcmd = new SQLiteCommand(strSQL, conn);
            sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();

            strSQL = "INSERT INTO member VALUES ('WestwoodForever SQLite C# Sample.')";
            sqlcmd = new SQLiteCommand(strSQL, conn);
            sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();

            strSQL = "SELECT * FROM member";
            sqlcmd = new SQLiteCommand(strSQL, conn);
            SQLiteDataReader rd = sqlcmd.ExecuteReader();
            sqlcmd.Dispose();
            while (rd.Read())
            {
                Debug.Log(rd.GetString(0));
            }
            rd.Close();

        }
    }
    
}
