using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.dataPath ;

    public static void Init()
    {

        #region Check Save Folder Directory Exists
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        #endregion
    }

    public static void Save(string savestring)
    {
        File.AppendAllText(Application.dataPath + "/save.txt,", savestring);
        File.WriteAllText(Application.dataPath + "/savewritten.txt,", savestring);
        Debug.Log("Written to file");
    }

    public static string Load()
    {
        if(File.Exists(SAVE_FOLDER + "/save"))
        {
            string savestring = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            return savestring;

        }
        else
        {
            return null;
        }
       
    }
}
