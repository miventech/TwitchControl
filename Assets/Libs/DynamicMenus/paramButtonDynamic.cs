using System;
using UnityEngine;


[System.Serializable]
public class paramButtonDynamic {
    public Vector2 Size;
    public Vector3 Position;
    public Quaternion Rotation;
    public string Name;
    public string ImageNoClicked;
    public string ImageClicked;
    public string CommandToSend;
    public string ID;
    public bool FillCenter = true;

    public paramButtonDynamic(){
    }
    public void generateID()
    {
        if (ID == string.Empty)
        {
            ID = DateTime.Now.ToString("yyyyMddmmHHss");
        }
    }
}