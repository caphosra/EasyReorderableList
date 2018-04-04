#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditorInternal;

namespace CPRUnitySystem
{
    public class EasyReorderableList<T> : System.IDisposable
    {
        private ReorderableList reorderableList;
        private T t_default;

        /// <summary>
        /// List value
        /// </summary>
        public List<T> Data { get; private set; }

        /// <summary>
        /// Event callback function when List value changed
        /// </summary>
        public event OnListChangedEventHandler<T> OnListChanged;

        /// <summary>
        /// Create EasyReorderableList instance
        /// </summary>
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
                Data[index] = drawCallback(rect, Data[index], isActive, isFocused);
            };

            reorderableList.onAddCallback += OnAdd;
            reorderableList.onRemoveCallback += OnRemove;
            reorderableList.onChangedCallback += OnChangedCallback;
        }

        /// <summary>
        /// Create EasyReorderableList instance
        /// </summary>
        public EasyReorderableList(string title, List<T> data, ElementCallbackWithIndex<T> drawCallback, T init = default(T))
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
                Data[index] = drawCallback(rect, index, Data[index], isActive, isFocused);
            };

            reorderableList.onAddCallback += OnAdd;
            reorderableList.onRemoveCallback += OnRemove;
            reorderableList.onChangedCallback += OnChangedCallback;
        }

        /// <summary>
        /// Draw EasyReorderableList
        /// </summary>
        public void DoList(Rect rect)
        {
            reorderableList.DoList(rect);
        }

        /// <summary>
        /// Draw EasyReorderableList (auto layout)
        /// </summary>
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

        /// <summary>
        /// Dispose this
        /// </summary>
        public void Dispose()
        {
            reorderableList = null;
            t_default = default(T);
            Data = null;
            OnListChanged = null;
        }
    }

    /// <summary>
    /// Event handler when List value changed
    /// </summary>
    public delegate void OnListChangedEventHandler<T>(List<T> list);

    /// <summary>
    /// Drawing function
    /// </summary>
    public delegate T ElementCallback<T>(Rect rect, T data, bool isActive, bool isFocused);

    /// <summary>
    /// Drawing function
    /// </summary>
    public delegate T ElementCallbackWithIndex<T>(Rect rect, int index, T data, bool isActive, bool isFocused);

    [System.Obsolete("This interface is not used.")]
    public interface IReorderableSetting
    {
        
    }
}

#endif
