using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.IO;

public class PropertiesButtonUI : MonoBehaviour
{
    static ButtonDynamicUI _CurrentButton;
    public static ButtonDynamicUI CurrentButton{

        set{
            if (_CurrentButton == value) return;
            if(_CurrentButton != null) _CurrentButton.SetStateObjectOfEdit(false);
            if(value != null)  value.SetStateObjectOfEdit(true);
            _CurrentButton = value;
            OnChangeCurrentButton.Invoke(_CurrentButton);
        }
        get{
            return _CurrentButton;
        }
        
    }
    public delegate void ChangeCurrentButton(ButtonDynamicUI button);
    public static ChangeCurrentButton OnChangeCurrentButton;

    public TMP_InputField NameID;
    public TMP_InputField CommandToSend;
    // public TMP_InputField CommandToSend;
    // Start is called before the first frame update
    void Start()
    {
        NameID.onEndEdit.AddListener((string dat) => {
            sendDataToObject();
        });
        CommandToSend.onEndEdit.AddListener((string dat) =>
        {
            sendDataToObject();
        });

        OnChangeCurrentButton += (ButtonDynamicUI button) =>
        {
            UpdateUI();
        };

        BuildUIRuntime.OnChangeModeEdition += changeModeEdition;
        gameObject.SetActive(false);

    }

    public void UpdateUI(){
        if (_CurrentButton == null) return;
        NameID.SetTextWithoutNotify(_CurrentButton.Params.Name);
        CommandToSend.SetTextWithoutNotify(_CurrentButton.Params.CommandToSend);
        
    }
    private void OnDestroy() {
        BuildUIRuntime.OnChangeModeEdition -= changeModeEdition;
    }
    public void changeModeEdition(bool d){
        gameObject.SetActive(BuildUIRuntime.ModeEdition);
    }
    public void FindImageClick(){
        JFileBrowser.OpenFileBrowserAction(ReceiveImageNormal, this, new SimpleFileBrowser.FileBrowser.Filter("*.png", ".png"), "Select Image Has Click", "Select", ".png");
    }
    // void ReceiveImageClick(string path) {
    //     if (_CurrentButton == null) return;
    //     byte[] bytes = File.ReadAllBytes(path);
    //     Texture2D Tex = new Texture2D(20, 20);
    //     Tex.filterMode = FilterMode.Point;
    //     Tex.LoadImage(bytes);
    //     _CurrentButton.ClickTexture = Tex;
    //     var _sprite = Sprite.Create(Tex, new Rect(0.0f, 0.0f, Tex.width, Tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    //     _CurrentButton.ClickSprite = _sprite;
    //     _CurrentButton.Save();

    // }
    public void FindImageNormal(){
        JFileBrowser.OpenFileBrowserAction(ReceiveImageNormal, this, new SimpleFileBrowser.FileBrowser.Filter("*.png", ".png"), "Select Image Has Click", "Select", ".png");
    }
    void ReceiveImageNormal(string path) {
        if (_CurrentButton == null) return;
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D Tex = new Texture2D(20, 20);
        Tex.filterMode = FilterMode.Point;

        Tex.LoadImage(bytes);
        _CurrentButton.NormalTexture = Tex;
        var _sprite = Sprite.Create(Tex, new Rect(0.0f, 0.0f, Tex.width, Tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        _CurrentButton.NormalSprite = _sprite;
        _CurrentButton.CurrentImage.sprite = _sprite;
        _CurrentButton.Save();
    }

    public void sendDataToObject(){
        if (_CurrentButton == null) return;
        _CurrentButton.Params.Name = NameID.text;
        _CurrentButton.Params.CommandToSend = CommandToSend.text;
        _CurrentButton.Save();
    }

    public void DeleteButton(){
        if (_CurrentButton == null) return;
        string path = fileManagerTools.getPathCurrentControl() + _CurrentButton.Params.ID + ".btn";
        if(File.Exists(path)){
            File.Delete(path);
        }
        Destroy(_CurrentButton.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
