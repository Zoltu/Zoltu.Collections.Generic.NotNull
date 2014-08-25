using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Linq.NotNull;

namespace Zoltu.Collections.Generic.NotNull
{
	public class NotNullList<T> : INotNullList<T>
	{
		private readonly IList<T> _list = new List<T>();

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_list != null);
			Contract.Invariant(Count == _list.Count);
		}

		public T this[Int32 index]
		{
			get
			{
				var result = _list[index];
				Contract.Assume(result != null);
				return result;
			}

			set
			{
				_list[index] = value;
			}
		}

		public Int32 Count
		{
			get
			{
				Contract.Ensures(Contract.Result<Int32>() == _list.Count);
				return _list.Count;
			}
		}

		public void Add(T item)
		{
			var oldCount = Count;
			_list.Add(item);
			Contract.Assume(Count == oldCount + 1);
		}

		public void Clear()
		{
			_list.Clear();
		}

		public Boolean Contains(T item)
		{
			return _list.Contains(item);
		}

		public INotNullEnumerator<T> GetEnumerator()
		{
			return _list.NotNull().GetEnumerator();
		}

		public Int32 IndexOf(T item)
		{
			return _list.IndexOf(item);
		}

		public void Insert(Int32 index, T item)
		{
			var oldCount = Count;
			_list.Insert(index, item);
			Contract.Assume(Count == oldCount + 1);
		}

		public Boolean Remove(T item)
		{
			return _list.Remove(item);
		}

		public void RemoveAt(Int32 index)
		{
			_list.RemoveAt(index);
		}
	}
}
