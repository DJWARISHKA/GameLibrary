using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassHomework
{
    public class MyList<T> : IList<T>, IEnumerator<T>
    //where T : auto
    {
        private class ListItem
        {
            public ListItem _next;
            public ListItem _prev;
            public T _value;

            public ListItem(T value, ListItem next = null, ListItem prev = null)
            {
                _value = value;
                _next = next;
                _prev = prev;
            }
        }

        private ListItem _head = null;
        private ListItem _tail = null;
        private int _size = 0;
        private ListItem _currentItem = null;

        private ListItem GetItem(int index)
        {
            if (index < 0 || index >= _size) return null;
            ListItem item = _head;
            while (index-- > 0)
            {
                item = item._next;
            }
            return item;
        }

        private ListItem GetItem(T value)
        {
            ListItem item = _head;
            while (item != null)
            {
                if (item._value.Equals(value))
                {
                    return item;
                }
                item = item._next;
            }
            return null;
        }

        private void RemoveItem(ListItem item)
        {
            if (item == _head) _head = _head._next;
            if (item == _tail) _tail = _tail._prev;

            if (item._next != null)
            {
                item._next._prev = item._prev;
            }
            if (item._prev != null)
            {
                item._prev._next = item._next;
            }
            //облегчаем работу GarbageCollector
            item._next = item._prev = null;

            --_size;
        }

        public T this[int index]
        {
            get
            {
                ListItem item = GetItem(index);
                if (item == null) throw new IndexOutOfRangeException("Index out of range");
                return item._value;
            }

            set
            {
                if (index == _size)
                {
                    Add(value);
                }
                else
                {
                    ListItem item = GetItem(index);
                    if (item == null) throw new IndexOutOfRangeException("Index out of range");
                    item._value = value;
                }
            }
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        public T Current
        {
            get
            {
                return _currentItem._value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _currentItem._value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T value)
        {
            ++_size;

            if (_head == null && _tail == null)
            {
                _head = _tail = new ListItem(value);
            }
            else
            {
                _tail = _tail._next = new ListItem(value, null, _tail);
            }
        }

        public void AddHead(T value)
        {
            ++_size;

            if (_head == null && _tail == null)
            {
                _head = _tail = new ListItem(value);
            }
            else
            {
                _head = _head._prev = new ListItem(value, _head);
            }
        }

        public void Clear()
        {
            while (_head != null)
            {
                ListItem tmpItem = _head;
                _head = _head._next;

                tmpItem._prev = null;
                tmpItem._next = null;
            }

            _head = _tail = null;
            _size = 0;
        }

        public bool Contains(T value)
        {
            ListItem item = GetItem(value);
            return item != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ListItem item = _head;
            while (item != null)
            {
                array[arrayIndex++] = item._value;
                item = item._next;
            }
        }

        public int IndexOf(T value)
        {
            int index = 0;
            for (ListItem item = _head;
                item != null;
                item = item._next, ++index)
            {
                if (item._value.Equals(value))
                {
                    return index;
                }
            }
            return -1;
        }

        public void Insert(int index, T value)
        {
            if (index == 0)
            {
                AddHead(value);
                return;
            }
            if (index == _size)
            {
                Add(value);
                return;
            }

            ListItem item = GetItem(index);
            if (item == null) throw new IndexOutOfRangeException();
            item._prev._next = item._prev = new ListItem(value, item, item._prev);
            _size++;
        }

        public bool Remove(T value)
        {
            ListItem item = GetItem(value);
            if (item == null) return false;

            RemoveItem(item);

            return true;
        }

        public void RemoveAt(int index)
        {
            ListItem item = GetItem(index);
            if (item == null) throw new IndexOutOfRangeException();

            RemoveItem(item);
        }

        public bool MoveNext()
        {
            if (_currentItem == null)
            {
                _currentItem = _head;
            }
            else
            {
                _currentItem = _currentItem._next;
            }

            return _currentItem != null;
        }

        public void Reset()
        {
            _currentItem = null;
        }

        public void Dispose()
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < Count; i++)
                tmp += this[i].ToString();
            return tmp;
        }
    }
}