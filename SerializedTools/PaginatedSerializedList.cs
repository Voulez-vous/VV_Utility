using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace VV.Utility.SerializedTools
{
    [Serializable]
    public class PaginatedSerializedList<T> : IEnumerable<T>, IEnumerable
    {
        [NonSerialized] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private  List<T> items = new();
        [SerializeField] private  List<T> visibleItems = new();

        [SerializeField] private bool foldout = false;
        [SerializeField] private int page;
        [SerializeField] private int itemsPerPage = 10;

        public List<T> Items => items;
        
        public int Page
        {
            get => page;
            set
            {
                page = Math.Clamp(value, 0, Count / itemsPerPage);
                UpdateVisibleItems();
            }
        }

        public int ItemsPerPage
        {
            get => itemsPerPage;
            set
            {
                itemsPerPage = value;
                Page = Math.Clamp(Page, 0, Count);
            }
        }

        [DebuggerHidden]
        public int Count => items.Count;
        public int TotalPages => Mathf.Max(1, Mathf.CeilToInt((float)items.Count / ItemsPerPage));
        
        private void UpdateVisibleItems()
        {
            visibleItems.Clear();
            int start = Page * ItemsPerPage;
            int count = Math.Min(ItemsPerPage, Count - start);
            visibleItems.AddRange(items.GetRange(start, count));
            foldout = true;
        }

        public IEnumerable<T> GetPageItems()
        {
            int start = Page * ItemsPerPage;
            int end = Mathf.Min(start + ItemsPerPage, items.Count);

            for (int i = start; i < end; i++)
                yield return items[i];
        }

        public void NextPage()
        {
            if (Page < TotalPages - 1)
                Page++;
        }

        public void PreviousPage()
        {
            if (Page > 0)
                Page--;
        }

        public void Add(T item)
        {
            items.Add(item);
            UpdateVisibleItems();
        }

        public void Insert(int index, T item)
        {
            items.Insert(index, item);
            UpdateVisibleItems();
        }

        public bool Remove(T item)
        {
            bool success = items.Remove(item);
            UpdateVisibleItems();
            return success;
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
            UpdateVisibleItems();
        } 
        public void Clear() => items.Clear();

        public bool Contains(T item) => items.Contains(item);
        public int IndexOf(T item) => items.IndexOf(item);
        
        public int FindIndex(Predicate<T> match) => items.FindIndex(0, items.Count, match);

        public int FindIndex(int startIndex, Predicate<T> match) => items.FindIndex(startIndex, match);

        public int FindIndex(int startIndex, int count, Predicate<T> match) => items.FindIndex(startIndex,  count, match);
        public List<T> GetRange(int index, int count) => items.GetRange(index, count);

        public T this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public PaginatedSerializedList() {}
        
        public PaginatedSerializedList(List<T> newList)
        {
            items = newList;
            UpdateVisibleItems();
        }
        
        public static implicit operator PaginatedSerializedList<T>(List<T> newList)
        {
            return new PaginatedSerializedList<T>(newList);
        }
    }
}

