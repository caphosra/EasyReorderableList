# EasyReorderableList
You can easily use ReorderableList. This will improve the design and efficiency of the editor extension!

## Description
The ReorderableList that exists in the UnityEditorInternal namespace is very useful as an array representation when writing Editor extension code. However, it is hard to use for those who use it for the first time.
By using this, you can easily and quickly create ReorderableList.

## Demo
![Screenshot](https://github.com/capra314cabra/EasyReorderableList/blob/master/Image%20for%20GitHub/screenshot.png)
``` C#
private EasyReorderableList<int> easyList;

public override void OnInspectorGUI()
{
    var Target = target as TestScriptable;
    
    //Create new EasyReorderableList instance.
    if(easyList == null) easyList = new EasyReorderableList<int>("i", Target.i, Draw);
    
    //Draw
    easyList.DoLayoutList();
    
    //Save
    EditorUtility.SetDirty(Target);
}

//DrawFunction
public int Draw(Rect rect, int data, bool isActive, bool isFocused)
{
    return EditorGUI.IntField(rect, "int", data);
}
```

## Usage
1. Import EasyReorderableList.
2. Write ```using CPRUnitySystem;```
3. Create instance
``` C#
easyList = new EasyReorderableList<Type>([ListTitle], [Array], [DrawFunction]);
```
4. Draw EasyReorderableList
``` C#
easyList.DoLayoutList();
// or reorderableList.DoList([Rect]);
```
5. Run EditorUtility.SetDirty
``` C#
EditorUtility.SetDirty([Target]);
```

## Install
1. Download it => [ReleasePage](https://github.com/capra314cabra/EasyReorderableList/releases)
2. Import this into your unity project (If you download DLL file, you should create plugins folder.)

## Requirement
UnityEditorInternal namespace and UnityEditor namespace(Only when UNITY_EDITOR is defined)

## Contribution
1. Fork it
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request

## Licence
[MIT Licence](https://github.com/capra314cabra/EasyReorderableList/blob/master/LICENSE)

## Author
[capra314cabra](https://github.com/capra314cabra)
