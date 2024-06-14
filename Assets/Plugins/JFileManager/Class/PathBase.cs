using UnityEngine;

[CreateAssetMenu(menuName = "FileManager/BasePath")]
public class PathBase : ScriptableObject
{
    public string NamePath;
    public OriginPath Origin = OriginPath.HOME;
    public string[] Paths;
    
    public enum OriginPath{
        HOME,SYSTEM_FILES,PROJECT
    }
}