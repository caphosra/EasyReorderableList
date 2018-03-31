using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CPRUnitySystem;
using System.Linq;

[CreateAssetMenu]
public class TestScriptable : ScriptableObject
{
    public List<int> i = new List<int>();
    
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TestScriptable))]
    public class TestEx : Editor
    {
        private EasyReorderableList<int> easyList;

        public override void OnInspectorGUI()
        {
            var Target = target as TestScriptable;
            if(easyList == null) easyList = new EasyReorderableList<int>("i", Target.i, Draw, 10);
            easyList.DoLayoutList();
            EditorUtility.SetDirty(Target);
        }

        public int Draw(Rect rect, int data, bool isActive, bool isFocused)
        {
            return EditorGUI.IntField(rect, "int", data);
        }
    }
}
