using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ReTags))]
[CanEditMultipleObjects]
public class ReTagsInspector : Editor
{
    SerializedProperty _property;
    ReorderableList _list;

    List<ReTag> tags;
    List<string> tagMenuPaths;

    int toRemove = -1;
    ReTag toAdd = null;

    void OnEnable()
    {
        _property = serializedObject.FindProperty("setTags");
        _list = new ReorderableList(serializedObject, _property, true, true, true, true)
        {
            drawHeaderCallback = DrawListHeader,
            drawElementCallback = DrawListElement,
            onAddDropdownCallback = AddDropdown,
        };

        tags = new List<ReTag>();
        tagMenuPaths = new List<string>();
        var guids = AssetDatabase.FindAssets("t:Tag");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var tag = AssetDatabase.LoadAssetAtPath<ReTag>(path);
            tags.Add(tag);

            var parentFolder = Directory.GetParent(path);
            tagMenuPaths.Add(parentFolder.Name + "/" + tag.GetTag.name);
        }
    }

    void AddDropdown(Rect buttonRect, ReorderableList list)
    {
        var tagsDropdown = new GenericMenu();
        for (int i = 0; i < tags.Count; i++)
        {
            var tag = tags[i];
            tagsDropdown.AddItem(new GUIContent(tagMenuPaths[i]), false, () => toAdd = tag);
        }
        tagsDropdown.ShowAsContext();
    }

    bool CanAddListElement(ReorderableList list)
    {
        if (toAdd != null)
        {
            _property.InsertArrayElementAtIndex(_property.arraySize);

            var subProp = _property.GetArrayElementAtIndex(_property.arraySize - 1);
            subProp.objectReferenceValue = toAdd;

            toAdd = null;
            return true;
        }

        return false;
    }

    bool RemoveListElement(ReorderableList list)
    {
        if (toRemove != -1)
        {
            _property.DeleteArrayElementAtIndex(toRemove);
            _property.DeleteArrayElementAtIndex(toRemove);
            toRemove = -1;
            return true;
        }

        return false;
    }

    void DrawListHeader(Rect rect)
    {
        GUI.Label(rect, "Tags");
    }

    void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        var item = _property.GetArrayElementAtIndex(index);
        var propRect = new Rect(rect);
        propRect.width -= 20f;

        EditorGUI.PropertyField(propRect, item);

        var removeRect = new Rect(rect);
        removeRect.width = 20f;
        removeRect.position += Vector2.right * propRect.width;

        if (GUI.Button(removeRect, "X", EditorStyles.miniButton))
        {
            toRemove = index;
            Repaint();
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space();
        _list.DoLayoutList();

        HandleDragAndDrop();

        if (toRemove != -1)
        {
            _property.DeleteArrayElementAtIndex(toRemove);
            _property.DeleteArrayElementAtIndex(toRemove);
            toRemove = -1;
        }
        if (toAdd != null)
        {
            AddTag(toAdd);
            toAdd = null;
        }
        serializedObject.ApplyModifiedProperties();
    }

    void AddTag(ReTag tag)
    {
        _property.InsertArrayElementAtIndex(_property.arraySize);

        var subProp = _property.GetArrayElementAtIndex(_property.arraySize - 1);
        subProp.objectReferenceValue = tag;
    }

    void HandleDragAndDrop()
    {
        switch (Event.current.type)
        {
            case EventType.DragUpdated:
                if (DragAndDrop.objectReferences.All(e => e is ReTag))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                }
                else
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                }
                break;
            case EventType.DragPerform:
                if (DragAndDrop.objectReferences.All(e => e is ReTag))
                {
                    foreach (var obj in DragAndDrop.objectReferences)
                    {
                        AddTag(obj as ReTag);
                    }
                    DragAndDrop.AcceptDrag();
                }
                break;
        }
    }
}
#endif