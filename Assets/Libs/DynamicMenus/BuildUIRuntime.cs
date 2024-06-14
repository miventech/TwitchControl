using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuildUIRuntime : MonoBehaviour
{

    public GameObject PrefabButton;
    static bool _ModeEdition = false;
    public RectTransform ContainerButton;
    public static bool ModeEdition{
        get{
            return _ModeEdition;
        }
        set{
            _ModeEdition = value;
            OnChangeModeEdition.Invoke(_ModeEdition);
        }
    }
    
    public delegate void ChangeModeEdition(bool state);

    public static event ChangeModeEdition OnChangeModeEdition;

    
    public void addButton(){
        var obj = Instantiate(PrefabButton ,ContainerButton);
        var RectT = obj.GetComponent<RectTransform>();
        RectT.position = new Vector3(100, 100);
    }
    [ContextMenu("Find Control Folder")]
    public void FindControlFolder(){
        JFileBrowser.OpenFolderBrowser(this, "Select Folder Control Template");
    }

    public void JFileBrowserPathRaw(string path){
        string[] sepPath = path.Split(fileManagerTools.SEP);
        string folder = sepPath[sepPath.Length - 1];
        Debug.Log(folder);
        fileManagerTools.CurrentControlFolder = folder;
    }
    //Convert.ToBase64String(byte[] data)
    //Convert.FromBase64String(string data)
    public void ToggleModeEdition(){
        ModeEdition = !ModeEdition;
    }
    public void LoadButtons(){
        string[] btns = Directory.GetFiles(fileManagerTools.getPathCurrentControl(), "*.btn");
        foreach(Transform t in ContainerButton){
            Destroy(t.gameObject);
        }
        foreach (var btn in btns)
        {
            var obj = Instantiate(PrefabButton ,ContainerButton);
            var RectT = obj.GetComponent<RectTransform>();
            RectT.position = new Vector3(100, 100);
            obj.GetComponent<ButtonDynamicUI>().load(btn);
        }
    }
    private void Start() {
        LoadButtons();
        CheckCopy();
    }


    public void CheckCopy(){
        if (Directory.Exists(fileManagerTools.getPathBasicButton() + "Check")) return;
        CopyAll(new DirectoryInfo(Application.streamingAssetsPath), new DirectoryInfo(fileManagerTools.getPathBasicButton()));
        Directory.CreateDirectory(fileManagerTools.getPathBasicButton() + "Check");
    }
    public void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        try
        {
            //check if the target directory exists
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            //copy all the files into the new directory

            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }


            //copy all the sub directories using recursion

            foreach (DirectoryInfo diSourceDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetDir = target.CreateSubdirectory(diSourceDir.Name);
                CopyAll(diSourceDir, nextTargetDir);
            }
            //success here
        }
        catch (IOException ie)
        {
            //handle it here
        }
    }
}
