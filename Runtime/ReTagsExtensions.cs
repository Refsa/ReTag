using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ReTagsExtensions
{
    static Dictionary<GameObject, ReTags> tagsCache = new Dictionary<GameObject, ReTags>();

    static readonly ReTagIdentifier AnyTag = "ANY";

    #region Has Tag

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasTag(this GameObject target, string tag)
    {
        return target.HasTag((ReTagIdentifier) tag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasTag(this MonoBehaviour target, string tag)
    {
        return target.HasTag((ReTagIdentifier) tag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasTag(this GameObject target, ReTag tag)
    {
        return target.HasTag(tag.GetTag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasTag(this MonoBehaviour target, ReTag tag)
    {
        return target.HasTag(tag.GetTag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasTag(this MonoBehaviour target, ReTagIdentifier tag)
    {
        return target.gameObject.HasTag(tag);
    }

    public static bool HasTag(this GameObject target, ReTagIdentifier tag)
    {
        if (!tagsCache.TryGetValue(target, out var _))
        {
            if (target.GetComponent<ReTags>() is ReTags tags)
            {
                tagsCache.Add(target, tags);
            }
            else
            {
                return false;
            }
        }

        if (tag.Equals(AnyTag))
        {
            return true;
        }

        return tagsCache[target].HasTag(tag);
    }

    #endregion

    #region Set Tag

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SetTag(this GameObject target, string tag)
    {
        return target.SetTag((ReTagIdentifier) tag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SetTag(this MonoBehaviour target, string tag)
    {
        return target.SetTag((ReTagIdentifier) tag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SetTag(this GameObject target, ReTag tag)
    {
        return target.SetTag(tag.GetTag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SetTag(this MonoBehaviour target, ReTag tag)
    {
        return target.SetTag(tag.GetTag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SetTag(this MonoBehaviour target, ReTagIdentifier tag)
    {
        return target.gameObject.SetTag(tag);
    }

    public static bool SetTag(this GameObject target, ReTagIdentifier tag)
    {
        if (tagsCache.TryGetValue(target, out var ctag))
        {
            return ctag.SetTag(tag);
        }

        if (target.GetComponent<ReTags>() is ReTags tags)
        {
            tagsCache.Add(target, tags);
            return tags.SetTag(tag);
        }

        return false;
    }

    #endregion

    #region Remove Tag

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RemoveTag(this GameObject target, string tag)
    {
        return target.RemoveTag((ReTagIdentifier) tag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RemoveTag(this MonoBehaviour target, string tag)
    {
        return target.RemoveTag((ReTagIdentifier) tag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RemoveTag(this GameObject target, ReTag tag)
    {
        return target.RemoveTag(tag.GetTag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RemoveTag(this MonoBehaviour target, ReTag tag)
    {
        return target.RemoveTag(tag.GetTag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RemoveTag(this MonoBehaviour target, ReTagIdentifier tag)
    {
        return target.gameObject.RemoveTag(tag);
    }

    public static bool RemoveTag(this GameObject target, ReTagIdentifier tag)
    {
        if (tagsCache.TryGetValue(target, out var ctag))
        {
            return ctag.RemoveTag(tag);
        }

        if (target.GetComponent<ReTags>() is ReTags tags)
        {
            tagsCache.Add(target, tags);
            return tags.RemoveTag(tag);
        }

        return false;
    }

    #endregion
}