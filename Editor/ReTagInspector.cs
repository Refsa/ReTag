/*using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
[CustomEditor(typeof(ReTag))]
public class ReTagInspector : Editor
{
    bool isUnique;
    List<string> sharedNames;

    void OnEnable() 
    {
        isUnique = true;
        sharedNames = new List<string>();

        foreach (var guid in AssetDatabase.FindAssets("t:Tag"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<ReTag>(path);

            if (asset.GetTag.name == ((ReTag)target).GetTag.name && asset != (ReTag)target)
            {
                isUnique = false;
                sharedNames.Add(path);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        var targetAs = target as ReTag;
        GUILayout.Label($"{targetAs.GetTag.name}");

        if (targetAs.name != targetAs.GetTag.name)
        {
            targetAs.OnValidate();
        }

        if (!isUnique)
        {
            GUILayout.Space(16f);

            GUILayout.Label("This tag name is not unique:");
            foreach (var path in sharedNames)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"\t{path}");
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif*/