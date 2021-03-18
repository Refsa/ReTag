using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class ReTags : MonoBehaviour
{
    [SerializeField] ReMultiTag tags;
    
    HashSet<ReTagIdentifier> cachedTags;

    void Awake()
    {
        cachedTags = new HashSet<ReTagIdentifier>();

        if (tags.Tags != null)
        {
            foreach (var t in tags.Tags)
            {
                if (!cachedTags.Contains(t.GetTag))
                {
                    cachedTags.Add(t.GetTag);
                }
            }
        }
    }

    public bool HasTag(string tag) => cachedTags.Contains(tag);
    public bool SetTag(string tag) => cachedTags.Add(tag);
    public bool RemoveTag(string tag) => cachedTags.Remove(tag);

    public bool HasTag(ReTagIdentifier tag) => cachedTags.Contains(tag);
    public bool SetTag(ReTagIdentifier tag) => cachedTags.Add(tag);
    public bool RemoveTag(ReTagIdentifier tag) => cachedTags.Remove(tag);

    public bool HasTag(ReTag tag) => cachedTags.Contains(tag.GetTag);
    public bool SetTag(ReTag tag) => cachedTags.Add(tag.GetTag);
    public bool RemoveTag(ReTag tag) => cachedTags.Remove(tag.GetTag);
}
