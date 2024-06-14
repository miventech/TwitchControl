using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Multplex.SXP.Utils;
using TwitchLib.Api.V5.Models.Clips;
using System;
using System.IO;

public class ButtonDynamicUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public paramButtonDynamic Params;
    public DragWindow DragWindowSystem;
    RectTransform CurrentRect;
    public GameObject[] ObjectOfEditMode;
    public Image CurrentImage;
    public Sprite NormalSprite;
    public Texture NormalTexture;
    public Sprite ClickSprite;
    public Texture ClickTexture;
    public Color Color_Normal = Color.white;
    public Color Color_Click = Color.white;
    public Color Color_Highline = Color.white;
    public void OnPointerDown(PointerEventData eventData)
    {
        // CurrentImage.sprite = ClickSprite;
        CurrentImage.color = Color_Click;
        if(BuildUIRuntime.ModeEdition){
            PropertiesButtonUI.CurrentButton = this;
            return;
        }
        
           
        ManagerConnections.Singleton.SendMessageChat(Params.CommandToSend);
        

    }
    public void SetStateObjectOfEdit(bool state){
        foreach (var _Obj in ObjectOfEditMode)
        {
            _Obj.SetActive(state);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentRect = GetComponent<RectTransform>();
        DragWindowSystem.OnEndDrag.AddListener(endDragWindow);
        BuildUIRuntime.OnChangeModeEdition += changeModeEdition;
        SetStateObjectOfEdit(false);
        Params.generateID();
        DragWindowSystem.enabled = BuildUIRuntime.ModeEdition;

    }
     private void OnDestroy() {
        BuildUIRuntime.OnChangeModeEdition -= changeModeEdition;
     }
    public void changeModeEdition(bool d){
        PropertiesButtonUI.CurrentButton = null;
        DragWindowSystem.enabled = d;
        // if (!BuildUIRuntime.ModeEdition)
        // {
        //     SetStateObjectOfEdit(BuildUIRuntime.ModeEdition);
        // }
    }
    public void endDragWindow(){
        
        Params.Position = CurrentRect.position;
        Params.Size = CurrentRect.sizeDelta;
        Params.Rotation = CurrentRect.rotation;
        Save();
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        CurrentImage.sprite = NormalSprite;
        CurrentImage.color = Color_Normal;

        // throw new System.NotImplementedException(); }
    }

    public void Save(){
        if(ClickTexture != null)
            Params.ImageClicked = Convert.ToBase64String(((Texture2D)ClickTexture).EncodeToPNG());
        if(NormalTexture != null)
            Params.ImageNoClicked = Convert.ToBase64String(((Texture2D)NormalTexture).EncodeToPNG());

        string pathSave = fileManagerTools.getPathCurrentControl() + Params.ID +".btn";

        File.WriteAllText(pathSave, JsonUtility.ToJson(Params));
    }
    public void load(string path)
    {
        Params = JsonUtility.FromJson<paramButtonDynamic>(File.ReadAllText(path));
        if (Params.ImageNoClicked != string.Empty)
        {
            byte[] bytes = Convert.FromBase64String(Params.ImageNoClicked);
            Texture2D Tex = new Texture2D(20, 20);
            Tex.filterMode = FilterMode.Point;
            Tex.LoadImage(bytes);
            this.NormalTexture = Tex;
            var _sprite = Sprite.Create(Tex, new Rect(0.0f, 0.0f, Tex.width, Tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            this.NormalSprite = _sprite;
        }
        // if (Params.ImageClicked != string.Empty)
        // {
        //     byte[] bytes = Convert.FromBase64String(Params.ImageClicked);
        //      Texture2D Tex = new Texture2D(20, 20);
        //     Tex.filterMode = FilterMode.Point;
        //     Tex.LoadImage(bytes);
        //     this.ClickTexture = Tex;
        //     var _sprite = Sprite.Create(Tex, new Rect(0.0f, 0.0f, Tex.width, Tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //     this.ClickSprite = _sprite;
        // }
        CurrentRect = GetComponent<RectTransform>();

        CurrentRect.position  = Params.Position;
        CurrentRect.sizeDelta = Params.Size ;
        CurrentRect.rotation = Params.Rotation;
        CurrentImage.sprite = NormalSprite;

    }
}