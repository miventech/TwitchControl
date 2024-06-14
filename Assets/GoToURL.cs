using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToURL : MonoBehaviour
{
    public string URL;
    // Start is called before the first frame update
    public void Go()
    {
         Application.OpenURL(URL);
    }

   
}
