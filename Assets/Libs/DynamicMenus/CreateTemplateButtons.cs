using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class CreateTemplateButtons : MonoBehaviour
{
    public string FolderName;
    public UnityEvent OnCreateFolder;
    public void CreateFolder(){
        string path = fileManagerTools.getPathControls() + FolderName;
        if (Directory.Exists(path)) return;
        Directory.CreateDirectory(path);
        OnCreateFolder.Invoke();
    }

    public void setFolderName(string data){
        FolderName = data;
    }
}
