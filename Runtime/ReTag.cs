using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ReTag : ScriptableObject
{
    [SerializeField, HideInInspector] ReTagIdentifier tag = ReTagIdentifier.None;
    public ReTagIdentifier GetTag => tag;

    public void SetTagName(string name) => tag.name = name;

    public void OnValidate()
    {
        SetTagName(this.name);
    }
}

[System.Serializable]
public struct ReTagIdentifier : System.IEquatable<ReTagIdentifier>, System.IEquatable<string>
{
    [SerializeField] string _name;
    int _id;

    public string name
    {
        get => _name;
        set
        {
            _id = ReTagIdentifier.GetHashCode(value);
            _name = value;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is ReTagIdentifier tag) return Equals(tag);
        if (obj is string name) return Equals(name);

        return false;
    }
    public bool Equals(ReTagIdentifier other) => _id == other._id;
    public bool Equals(string other) => _name == other;

    public override int GetHashCode() => _id;
    static int GetHashCode(string name) => name.GetHashCode();

    public static bool operator ==(ReTagIdentifier a, ReTagIdentifier b) => a._id == b._id;
    public static bool operator ==(ReTagIdentifier a, string b) => a._name == b;
    public static bool operator !=(ReTagIdentifier a, ReTagIdentifier b) => a._id != b._id;
    public static bool operator !=(ReTagIdentifier a, string b) => a._name != b;

    public static implicit operator string(ReTagIdentifier tag) => tag.name;
    public static implicit operator ReTagIdentifier(string name) => new ReTagIdentifier { name = name };

    public override string ToString() => _name;

    public static ReTagIdentifier None = new ReTagIdentifier { name = "NONE" };
}