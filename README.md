# ReTag
### Tag system for unity
![image](https://user-images.githubusercontent.com/4514574/102297203-1dcef200-3f4f-11eb-9e6c-642c76dac828.png)

Simple Tag system to mark GameObjects with one or many tags.

## Features
- ScriptableObject based
- Custom Inspector for easier management
- Extension methods to call on GameObject or MonoBehaviour
- Add and Remove tags during runtime
- Implicit conversion between string and Tag

## Example
Step 1: Create a tag  
Step 2: Add the Tags component to a GameObject  
Step 3: Click the "+" icon or drag the Tag asset onto the component  
Step 4: Call gameObject.HasTag("TagName") or gameObject.HasTag(tagAsset)  

## Methods:
`GameObject::HasTag(ReTag tag)`  
`GameObject::HasTag(ReTagIdentifier tag)`  
`GameObject::HasTag(string tag)`  

`GameObject::RemoveTag(ReTag tag)`  
`GameObject::RemoveTag(ReTagIdentifier tag)`  
`GameObject::RemoveTag(string tag)`  

`GameObject::SetTag(ReTag tag)`  
`GameObject::SetTag(ReTagIdentifier tag)`  
`GameObject::SetTag(string tag)`  