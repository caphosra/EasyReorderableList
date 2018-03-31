#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditorInternal;

namespace CPRUnitySystem
{
    public class EasyReorderableList<T>
    {
        private ReorderableList reorderableList;
        private T t_default;
        private ElementCallback<T> elementDrawCallback;

        public List<T> Data { get; private set; }
        public event OnListChangedEventHandler<T> OnListChanged;

        public EasyReorderableList(string title, List<T> data, ElementCallback<T> drawCallback, T init = default(T))
        {
            Data = data;
            t_default = init;
            reorderableList = new ReorderableList(data, typeof(T));

            reorderableList.drawHeaderCallback += (rect) =>
            {
                EditorGUI.LabelField(rect, title);
            };

            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                Data[index] = elementDrawCallback(rect, Data[index], isActive, isFocused);
            };
            elementDrawCallback = drawCallback;

            reorderableList.onAddCallback += OnAdd;
            reorderableList.onRemoveCallback += OnRemove;
            reorderableList.onChangedCallback += OnChangedCallback;
        }

        public void DoList(Rect rect)
        {
            reorderableList.DoList(rect);
        }

        public void DoLayoutList()
        {
            reorderableList.DoLayoutList();
        }

        private void OnAdd(ReorderableList list)
        {
            Data.Add(t_default);
        }

        private void OnRemove(ReorderableList list)
        {
            var remove = Data.Count;
            Data.RemoveAt(remove - 1);
        }

        private void OnChangedCallback(ReorderableList list)
        {
            if (OnListChanged != null)
                OnListChanged(Data);
        }
    }

    public delegate void OnListChangedEventHandler<T>(List<T> list);

    public delegate T ElementCallback<T>(Rect rect, T data, bool isActive, bool isFocused);

    [System.Obsolete("This interface is not used.")]
    public interface IReorderableSetting
    {
        
    }
}

#endif
