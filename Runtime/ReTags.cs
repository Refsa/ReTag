using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class ReTags : MonoBehaviour
{
    [SerializeField] List<ReTag> setTags;
    HashSet<ReTagIdentifier> tags;

    void Awake()
    {
        tags = new HashSet<ReTagIdentifier>();

        if (setTags != null)
        {
            foreach (var t in setTags)
            {
                if (!tags.Contains(t.GetTag))
                {
                    tags.Add(t.GetTag);
                }
            }
        }
    }

    public bool HasTag(string tag) => tags.Contains(tag);
    public bool SetTag(string tag) => tags.Add(tag);
    public bool RemoveTag(string tag) => tags.Remove(tag);

    public bool HasTag(ReTagIdentifier tag) => tags.Contains(tag);
    public bool SetTag(ReTagIdentifier tag) => tags.Add(tag);
    public bool RemoveTag(ReTagIdentifier tag) => tags.Remove(tag);

    public bool HasTag(ReTag tag) => tags.Contains(tag.GetTag);
    public bool SetTag(ReTag tag) => tags.Add(tag.GetTag);
    public bool RemoveTag(ReTag tag) => tags.Remove(tag.GetTag);
}
