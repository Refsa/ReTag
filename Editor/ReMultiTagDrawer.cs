using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ReMultiTag))]
public class ReMultiTagDrawer : PropertyDrawer
{
    SerializedProperty _property;
    ReorderableList _list;

    List<ReTag> tags;
    List<string> tagMenuPaths;

    int toRemove = -1;
    ReTag toAdd = null;

    bool tagsSetup = false;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (property.FindPropertyRelative("Tags").arraySize + 3) * EditorGUIUtility.singleLineHeight;
    }

    void Setup(SerializedProperty property)
    {
        _property = property.FindPropertyRelative("Tags");
        _list = new ReorderableList(_property.serializedObject, _property, true, true, true, true)
        {
            drawHeaderCallback = DrawListHeader,
            drawElementCallback = DrawListElement,
            onAddDropdownCallback = AddDropdown,
        };
    }

    void SetupTags()
    {
        tags = new List<ReTag>();
        tagMenuPaths = new List<string>();
        var guids = AssetDatabase.FindAssets("t:ReTag");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var tag = AssetDatabase.LoadAssetAtPath<ReTag>(path);
            tags.Add(tag);

            var parentFolder = Directory.GetParent(path);
            tagMenuPaths.Add(parentFolder.Name + "/" + tag.GetTag.name);
        }

        tagsSetup = true;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.serializedObject.Update();
        if (!tagsSetup) SetupTags();
        Setup(property);

        Rect listRect = new Rect(position);
        //listRect.height -= EditorGUIUtility.singleLineHeight * 2f;

        _list.DoList(listRect);

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

        property.serializedObject.ApplyModifiedProperties();
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
}