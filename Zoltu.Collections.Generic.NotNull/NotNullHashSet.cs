using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Linq.NotNull;

namespace Zoltu.Collections.Generic.NotNull
{
	public class NotNullHashSet<T> : INotNullSet<T>
	{
		private readonly HashSet<T> _backingHashSet;

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_backingHashSet != null);
			Contract.Invariant(Count == _backingHashSet.Count);
		}

		public NotNullHashSet()
		{
			_backingHashSet = new HashSet<T>();
		}

		public NotNullHashSet(INotNullEnumerable<T> collection)
		{
			Contract.Requires(collection != null);

			_backingHashSet = new HashSet<T>(collection.NotNullToNull());
		}

		public NotNullHashSet(IEqualityComparer<T> equalityComparor)
		{
			Contract.Requires(equalityComparor != null);

			_backingHashSet = new HashSet<T>(equalityComparor);
		}

		public NotNullHashSet(INotNullEnumerable<T> collection, IEqualityComparer<T> equalityComparor)
		{
			Contract.Requires(collection != null);
			Contract.Requires(equalityComparor != null);

			_backingHashSet = new HashSet<T>(collection.NotNullToNull(), equalityComparor);
		}

		public int Count
		{
			get
			{
				Contract.Ensures(Contract.Result<Int32>() == _backingHashSet.Count);
				return _backingHashSet.Count;
			}
		}

		public Boolean Add(T item)
		{
			return _backingHashSet.Add(item);
		}

		public void Clear()
		{
			_backingHashSet.Clear();
		}

		public Boolean Contains(T item)
		{
			if (item == null)
				return false;

			return _backingHashSet.Contains(item);
		}

		public INotNullEnumerator<T> GetEnumerator()
		{
			return new NullToNotNullEnumerator<T>(_backingHashSet.GetEnumerator());
		}

		public void ExceptWith(INotNullEnumerable<T> other)
		{
			_backingHashSet.ExceptWith(other.NotNullToNull());
		}

		public void IntersectWith(INotNullEnumerable<T> other)
		{
			_backingHashSet.IntersectWith(other.NotNullToNull());
		}

		public Boolean IsProperSubsetOf(INotNullEnumerable<T> other)
		{
			return _backingHashSet.IsProperSubsetOf(other.NotNullToNull());
		}

		public Boolean IsProperSupersetOf(INotNullEnumerable<T> other)
		{
			return _backingHashSet.IsProperSupersetOf(other.NotNullToNull());
		}

		public Boolean IsSubsetOf(INotNullEnumerable<T> other)
		{
			return _backingHashSet.IsSubsetOf(other.NotNullToNull());
		}

		public Boolean IsSupersetOf(INotNullEnumerable<T> other)
		{
			return _backingHashSet.IsSupersetOf(other.NotNullToNull());
		}

		public Boolean Overlaps(INotNullEnumerable<T> other)
		{
			return _backingHashSet.Overlaps(other.NotNullToNull());
		}

		public Boolean Remove(T item)
		{
			return _backingHashSet.Remove(item);
		}

		public Boolean SetEquals(INotNullEnumerable<T> other)
		{
			return _backingHashSet.SetEquals(other.NotNullToNull());
		}

		public void SymmetricExceptWith(INotNullEnumerable<T> other)
		{
			_backingHashSet.SymmetricExceptWith(other.NotNullToNull());
		}

		public void UnionWith(INotNullEnumerable<T> other)
		{
			_backingHashSet.UnionWith(other.NotNullToNull());
		}

		void INotNullCollection<T>.Add(T item)
		{
			var oldCount = Count;
			((ICollection<T>)_backingHashSet).Add(item);
			Contract.Assume(Count == oldCount + 1);
		}
	}
}
