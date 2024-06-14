using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecretsUI : MonoBehaviour
{
    public TMP_InputField UserName,PassOAuth,Channel;
    const string TokenID = "Token";
    const string UserID = "UserID";
    const string ChannelID = "Channel";
    // Start is called before the first frame update
    void Start()
    {
        UserName.onEndEdit.AddListener(DataUpdate);
        PassOAuth.onEndEdit.AddListener(DataUpdate);
        Channel.onEndEdit.AddListener(DataUpdate);
        ReadSaveData();
    }

    public void ReadSaveData(){
        string _Channel = "";
        string _Token = "";
        string _UserName = "";
        if(PlayerPrefs.HasKey(ChannelID)){
            _Channel = PlayerPrefs.GetString(ChannelID);
        }
        if(PlayerPrefs.HasKey(TokenID)){
            _Token = PlayerPrefs.GetString(TokenID);
        }
        if(PlayerPrefs.HasKey(UserID)){
            _UserName = PlayerPrefs.GetString(UserID);
        }



        Secrets.Channel = _Channel;
        Secrets.UserName = _UserName;
        Secrets.AccessToken= _Token;
        
        Channel.SetTextWithoutNotify(Secrets.Channel     );
        UserName.SetTextWithoutNotify(Secrets.UserName    );
        PassOAuth.SetTextWithoutNotify(Secrets.AccessToken );
    }
    public void DataUpdate(string text){
        Secrets.Channel = Channel.text;
        Secrets.UserName = UserName.text;
        Secrets.AccessToken= PassOAuth.text;
        PlayerPrefs.SetString(UserID,Secrets.UserName);
        PlayerPrefs.SetString(ChannelID,Secrets.Channel);
        PlayerPrefs.SetString(TokenID,Secrets.AccessToken );
    }
   
}
