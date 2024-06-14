using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Secrets
{
    #if UNITY_EDITOR
        public static string Channel = "danyk2";
        public static string UserName = "miventech_jcode";
        public static string AccessToken = "oauth:9ahf7vkoeid3d8x26s7rnp8g98u1rs";
    #else
        public static string Channel = "";
        public static string UserName = "";
        public static string AccessToken = "";
    #endif
}
