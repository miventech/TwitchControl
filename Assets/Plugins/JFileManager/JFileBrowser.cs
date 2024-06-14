
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using static fileManagerTools;


public delegate void functionFolderRecive(string dataPath);
public class JFileBrowser : MonoBehaviour
{
    public static  string PathToFind = fileManagerTools.getPathBasicButton();
    /// <summary>
    /// this function open file browser in android, and responsed is sended 
    /// at Object unity to method "JFileBrowserData(string path)" this function return
    /// a URL Path , the form to use whit libary JWEB and read file the form async.
    /// </summary>
    /// <param name="ObjectUnity">This object is the base all script inside in MonoBehaviour, and here send message whit response of file PATH</param>
    /// <param name="filter">Example: new string[] { "text/plain", ".VAR" }</param>
    // public static void openFileAndroid(MonoBehaviour ObjectUnity, string[] filter)
    // {
    //     if (NativeFilePicker.IsFilePickerBusy()) return;

    //     string[] fileTypes = filter;
    //     string urlFile = "";
    //     NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
    //     {
    //         if (path == null)
    //             Debug.Log("Operation cancelled");
    //         else
    //             urlFile = "file:///" + path.Replace("\\", "/");
    //         ObjectUnity.SendMessage("JFileBrowserData", urlFile, SendMessageOptions.DontRequireReceiver);
    //         /// 
    //         /// 
    //     }, fileTypes);

    //     //debug.text += ("Permission result: " + permission);

    // }
    // /// <summary>
    /// this function open file browser in windown intro unity, and responsed is sended at Object unity to method
    ///  "JFileBrowserData(string path)" this function return
    /// a URL Path , the form to use whit libary JWEB and read file the form async.
    /// </summary>
    /// <param name="ObjectUnity">This object is the base all script inside in MonoBehaviour, and here send message whit response of file PATH </param>
    /// <param name="Filter">Example: new FileBrowser.Filter("VariatsModel3D", ".VAR")</param>
    /// <param name="defaulFilter"></param>
    public static void openFileWindows(MonoBehaviour ObjectUnity, FileBrowser.Filter Filter, string defaulFilter = "*")
    {
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filters
        FileBrowser.SetFilters(false, Filter);
        //FileBrowser.SetFilters(false, new FileBrowser.Filter("VariatsModel3D", ".VAR"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(defaulFilter);

        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        //FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Main Folder", PathToFind, null);

        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        ObjectUnity.StartCoroutine(ShowLoadDialogCoroutine(ObjectUnity));
    }
    public static IEnumerator ShowLoadDialogCoroutine(MonoBehaviour ObjectUnity)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, getPathBasicButton(), "Load Files VAR", "Load");
        //FileBrowser.WaitForLoadDialog()
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                PathToFind = FileBrowser.Result[i];
                //string NoURLPath = FileBrowser.Result[i];
                string urlFile = "file:///" + FileBrowser.Result[i].Replace("\\", "/");
                //processStringFile();
                ObjectUnity.SendMessage("JFileBrowserData", urlFile, SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    public static void OpenFileBrowser(MonoBehaviour ObjectUnity, FileBrowser.Filter Filter, string TitleBrowser = "Select File", string TitleBtn = "Select", string defaulFilter = "*")
    {
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filters
        FileBrowser.SetFilters(false, Filter);
        //FileBrowser.SetFilters(false, new FileBrowser.Filter("VariatsModel3D", ".VAR"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(defaulFilter);

        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        //FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Main Folder", PathToFind, null);

        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        ObjectUnity.StartCoroutine(ShowFileBrowser(ObjectUnity, TitleBrowser, TitleBtn));
    }
    public static IEnumerator ShowFileBrowser(MonoBehaviour ObjectUnity, string TitleBrowser = "Select File", string TitleBtn = "Select")
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, getPathBasicButton(), TitleBrowser, TitleBtn);
        //FileBrowser.WaitForLoadDialog()
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                //string NoURLPath = FileBrowser.Result[i];
                string urlFile = "file:///" + FileBrowser.Result[i].Replace("\\", "/");
                //processStringFile();
                PathToFind = FileBrowser.Result[i];
                ObjectUnity.SendMessage("JFileBrowserPathURL", urlFile, SendMessageOptions.DontRequireReceiver);
                ObjectUnity.SendMessage("JFileBrowserPathRaw", FileBrowser.Result[i], SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    public static void OpenFileBrowserAction(functionFolderRecive OnSuccess, MonoBehaviour ObjectUnity, FileBrowser.Filter Filter, string TitleBrowser = "Select File", string TitleBtn = "Select", string defaulFilter = "*")
    {
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filters
        FileBrowser.SetFilters(false, Filter);
        //FileBrowser.SetFilters(false, new FileBrowser.Filter("VariatsModel3D", ".VAR"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(defaulFilter);

        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        //FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Main Folder", PathToFind, null);

        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        ObjectUnity.StartCoroutine(ShowFileBrowserEvent(ObjectUnity, TitleBrowser, TitleBtn, (string d) =>
        {
            OnSuccess.Invoke(d);
        }));
    }
    public static IEnumerator ShowFileBrowserEvent(MonoBehaviour ObjectUnity, string TitleBrowser = "Select File", string TitleBtn = "Select", functionFolderRecive OnSuccess = null)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        Debug.Log("wait File Browser");
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, getPathBasicButton(), TitleBrowser, TitleBtn);
        //FileBrowser.WaitForLoadDialog()
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                //string NoURLPath = FileBrowser.Result[i];
                // string urlFile= "file:///" + FileBrowser.Result[i].Replace("\\", "/");
                // //processStringFile();
                // ObjectUnity.SendMessage("JFileBrowserPathURL" ,  urlFile, SendMessageOptions.DontRequireReceiver);
                // ObjectUnity.SendMessage("JFileBrowserPathRaw" ,  FileBrowser.Result[i], SendMessageOptions.DontRequireReceiver);
                Debug.Log("Internal:" + FileBrowser.Result[i]);
                PathToFind =  FileBrowser.Result[i];
                OnSuccess.Invoke(FileBrowser.Result[i]);
            }
        }
    }

    public static void OpenFolderBrowser(MonoBehaviour ObjectUnity, string TitleFolder = "")
    {
        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        //FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Main Folder", PathToFind, null);
        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        ObjectUnity.StartCoroutine(ShowFolderBrowser(ObjectUnity, TitleFolder));
    }
    public static IEnumerator ShowFolderBrowser(MonoBehaviour ObjectUnity, string TitleFolder = "")
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        Debug.Log("Start Loop File browser");

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, getPathBasicButton() , TitleFolder, "Select");

        
        Debug.Log("End Loop File browser");
        //FileBrowser.WaitForLoadDialog()
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                //string NoURLPath = FileBrowser.Result[i];
                string urlFile = "file:///" + FileBrowser.Result[i].Replace("\\", "/");
                //processStringFile();
                PathToFind = FileBrowser.Result[i];
                ObjectUnity.SendMessage("JFileBrowserPathURL", urlFile, SendMessageOptions.DontRequireReceiver);
                ObjectUnity.SendMessage("JFileBrowserPathRaw", FileBrowser.Result[i], SendMessageOptions.DontRequireReceiver);
            }
        }
    }



    public static void OpenFolderBrowser(MonoBehaviour ObjectUnity, string TitleFolder, functionFolderRecive OnSuccess, functionFolderRecive OnFailed)
    {
        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        //FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Main Folder", PathToFind, null);
        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        ObjectUnity.StartCoroutine(ShowFolderBrowserEvent(TitleFolder, OnSuccess, OnFailed));
    }
    public static IEnumerator ShowFolderBrowserEvent(MonoBehaviour ObjectUnity, string TitleFolder, functionFolderRecive OnSuccess, functionFolderRecive OnFailed)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        Debug.Log("Start Loop File browser");

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, PathToFind, TitleFolder, "Select");
        Debug.Log("End Loop File browser");
        //FileBrowser.WaitForLoadDialog()
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                //string NoURLPath = FileBrowser.Result[i];
                string urlFile = "file:///" + FileBrowser.Result[i].Replace("\\", "/");
                //processStringFile();
                OnSuccess?.Invoke(FileBrowser.Result[i]);
                ObjectUnity.SendMessage("JFileBrowserPathURL", urlFile, SendMessageOptions.DontRequireReceiver);
                ObjectUnity.SendMessage("JFileBrowserPathRaw", FileBrowser.Result[i], SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            
            OnFailed?.Invoke("Fail");
        }
    }

    public static IEnumerator ShowFolderBrowserEvent(string TitleFolder, functionFolderRecive OnSuccess, functionFolderRecive OnFailed)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        Debug.Log("Start Loop File browser");

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, PathToFind, TitleFolder, "Select");
        Debug.Log("End Loop File browser");
        //FileBrowser.WaitForLoadDialog()
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                //string NoURLPath = FileBrowser.Result[i];
                string urlFile = "file:///" + FileBrowser.Result[i].Replace("\\", "/");
                //processStringFile();
                OnSuccess?.Invoke(FileBrowser.Result[i]);
            }
        }
        else
        {
            OnFailed?.Invoke("Fail");
        }
    }

}
