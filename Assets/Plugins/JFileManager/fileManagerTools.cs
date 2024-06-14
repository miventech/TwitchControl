using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Windows;
using System;

public class fileManagerTools : MonoBehaviour
{
    // public static string PROJECT = "PROJECT";
    // public static string SESSION = "SESSION";
    public static string PathROOT = "";
    public const string PlayerPrefName = "PathRoot";
    public static bool DoneLoadPath = false;
    public static string CurrentControlFolder = "DefaultControl";
    public static string SEP
    {
        get
        {
            return Path.DirectorySeparatorChar.ToString();
        }
    }

    /// <summary>
    /// Get directory commons in all device;
    /// </summary>
    /// <returns></returns>
    public static string getHomePath()
    {
        if (!DoneLoadPath)
        {
            if (PlayerPrefs.HasKey(PlayerPrefName))
            {
                PathROOT = PlayerPrefs.GetString(PlayerPrefName);
            }
            DoneLoadPath = true;
        }

        if (string.IsNullOrEmpty(PathROOT))
        {
            string r = "";
#if UNITY_EDITOR_WIN
            r = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + SEP + "Miv Control Twitch";
#elif UNITY_STANDALONE_WIN
            r = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + SEP + "Miv Control Twitch" ;
#elif UNITY_EDITOR_LINUX
            r = Application.persistentDataPath + SEP + "Miv Control Twitch" ;
#elif UNITY_STANDALONE_LINUX
            r = Application.persistentDataPath + SEP + "Miv Control Twitch" ;
#elif UNITY_EDITOR_OSX
            r = Application.persistentDataPath + SEP + "Miv Control Twitch" ;
#elif UNITY_STANDALONE_OSX
            r = Application.persistentDataPath + SEP + "Miv Control Twitch" ;
#elif UNITY_ANDROID
            r =  Application.persistentDataPath + SEP + "Miv Control Twitch" ;
#elif UNITY_IOS
            r =  Application.persistentDataPath + SEP + "Miv Control Twitch" ;
#endif
            if (!Directory.Exists(r)) Directory.CreateDirectory(r);
            return r;
        }else{
            if (!Directory.Exists(PathROOT)) Directory.CreateDirectory(PathROOT);
            return PathROOT;
        }
    }
    /// <summary>
    /// Get Path  "Miv Control Twitch/0_SYSTEM_FILE/" 
    /// </summary>
    /// <returns></returns>
    public static string getPathControls()
    {
        string path = getHomePath() + $"{SEP}Controls";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path + SEP;
    }
    public static string getPathBasicButton()
    {
        string path = getHomePath() + $"{SEP}BasicButton";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path + SEP;
    }
    public static string getPathCurrentControl()
    {
        string path = getPathControls() + CurrentControlFolder;
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path + SEP;
    }
}
