using UnityEngine;
using UnityEditor;

public class ParticleSizeMatchStaticStrategy : AbstractStaticStrategy<Vector3>
{
    [MenuItem("Tools/MyTool/Do It in C#")]
    static void DoIt()
    {
        EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
    }
}