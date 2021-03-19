using System.Collections.Generic;

[System.Serializable]
public struct ReMultiTag
{
    public List<ReTag> Tags;
    public bool RequireAll;

    public static implicit operator ReMultiTag(List<ReTag> from)
    {
        return new ReMultiTag {Tags = from};
    }

    public static implicit operator List<ReTag>(ReMultiTag from)
    {
        return from.Tags;
    }
}