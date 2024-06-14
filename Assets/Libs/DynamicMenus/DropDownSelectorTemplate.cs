using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using UnityEngine.Events;

public class DropDownSelectorTemplate : MonoBehaviour
{
    public TMP_Dropdown DropDownBase;
    public UnityEvent EventChangeFolder; 
    // Start is called before the first frame update
    void Start()
    {
        DropDownBase.onValueChanged.AddListener(OnDropDownValueChanged);
        UpdateFoldersList();
    }

    private void OnDropDownValueChanged(int arg0)
    {
        fileManagerTools.CurrentControlFolder = DropDownBase.options[arg0].text;
        EventChangeFolder.Invoke();
    }

    public void UpdateFoldersList(){
        if (DropDownBase == null) return;

        string[] folders = Directory.GetDirectories(fileManagerTools.getPathControls());
        List<string> FoldersShot = new List<string>();
        foreach (var folder in folders)
        {
            FoldersShot.Add(folder.Replace(fileManagerTools.getPathControls(), ""));
            // Debug.Log(folder);
        }
        DropDownBase.options.Clear();
        DropDownBase.AddOptions(FoldersShot);
        DropDownBase.SetValueWithoutNotify(0);
    }
}
