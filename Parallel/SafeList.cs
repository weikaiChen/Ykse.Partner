// Copyright (c) Yuanta Securities. All rights reserved.
// Modified By      YYYY-MM-DD
// CK               2015-03-31 - Creation

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Ykse.Partner
{
	[Serializable, DataContract]
	public class SafeList<T>
		: IProducerConsumerCollection<T>
	{
		private object _Lock = new object();
		private Queue<T> _Queue = null;

		public SafeList()
		{
			_Queue = new Queue<T>();
		}

		public SafeList(IEnumerable<T> collection)
		{
			_Queue = new Queue<T>(collection);
		}

		public void Push(T item)
		{
			lock(_Lock) {
				_Queue.Enqueue(item);
			}
		}

		public bool TryPop(out T item)
		{
			var rval = true;
			lock(_Lock) {
				if(_Queue.Count == 0) {
					item = default(T);
					rval = false;
				} else {
					item = _Queue.Dequeue();
				}
			}
			return rval;
		}

		public bool TryTake(out T item)
		{
			return TryPop(out item);
		}

		public bool TryAdd(T item)
		{
			Push(item);
			return true;
		}

		public T[] ToArray()
		{
			T[] rval = null;
			lock(_Lock) {
				rval = _Queue.ToArray();
			}
			return rval;
		}

		public void CopyTo(T[] array, int index)
		{
			lock(_Lock) {
				_Queue.CopyTo(array, index);
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			Queue<T> clone = null;
			lock(_Lock) {
				clone = new Queue<T>(_Queue);
			}
			return clone.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		public object SyncRoot
		{
			get
			{
				return _Lock;
			}
		}

		public int Count
		{
			get
			{
				return _Queue.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(_Lock) {
				((ICollection)_Queue).CopyTo(array, index);
			}
		}

		public SafeList<T> Remove(Func<T, bool> predicate)
		{
			lock(_Lock) {
				var list = _Queue.Where(x => !predicate(x));
				_Queue = new Queue<T>(list);
			}
			return this;
		}

		public SafeList<T> Insert(int index, T one)
		{
			lock(_Lock) {
				var newQueue = new Queue<T>();
				bool added = false;
				int count = _Queue.Count;
				for(int i = 0; i < count; i++) {
					if(!added) {
						if(index == i) {
							newQueue.Enqueue(one);
							added = true;
						}
					}
					newQueue.Enqueue(_Queue.Dequeue());
				}
				if(!added) {
					newQueue.Enqueue(one);
					added = true;
				}
				_Queue = newQueue;
			}
			return this;
		}

		public T this[int index]
		{
			get
			{
				var array = ToArray();
				if(array.Length > index) {
					return ToArray()[index];
				}
				throw new IndexOutOfRangeException(
					"Index excced SafeList length is {0}"
				);
			}
		}

		public bool Exists(Func<T, bool> predicate)
		{
			lock(_Lock) {
				var list = _Queue.Where(predicate);
				return list.Count() > 0;
			}
		}

		public SafeList<T> Add(params T[] ones)
		{
			foreach(var one in ones) {
				Push(one);
			}
			return this;
		}

		public SafeList<T> AddRange(IEnumerable<T> ones)
		{
			foreach(var one in ones) {
				Push(one);
			}
			return this;
		}
	}
}
