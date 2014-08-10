using System;
using System.Diagnostics.Contracts;

namespace Zoltu.Collections.Generic.NotNull
{
	[ContractClass(typeof(NotNullListContracts<>))]
	public interface INotNullList<T> : INotNullCollection<T>
	{
		T this[Int32 index] { get; set; }
		Int32 IndexOf(T item);
		void Insert(Int32 index, T item);
		void RemoveAt(Int32 index);
	}

	[ContractClassFor(typeof(INotNullList<>))]
	public abstract class NotNullListContracts<T> : INotNullList<T>
	{
		public T this[Int32 index]
		{
			get
			{
				Contract.Requires(index >= 0);
				Contract.Requires(index < Count);
				Contract.Ensures(Contract.Result<T>() != null);
				return default(T);
			}
			set
			{
				Contract.Requires(index >= 0);
				Contract.Requires(index < Count);
				Contract.Requires(value != null);
			}
		}

		[Pure]
		public Int32 IndexOf(T item)
		{
			Contract.Ensures(Contract.Result<Int32>() >= -1);
			Contract.Ensures(Contract.Result<Int32>() < Count);
			return default(Int32);
		}

		public void Insert(Int32 index, T item)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index <= Count);
			Contract.Requires(item != null);
			Contract.Ensures(Count == Contract.OldValue(Count) + 1);
		}

		public abstract bool Remove(T item);

		public void RemoveAt(Int32 index)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index < Count);
			Contract.Ensures(Count == Contract.OldValue(Count) - 1);
		}

		public abstract int Count { get; }
		public abstract void Add(T item);
		public abstract void Clear();
		public abstract bool Contains(T item);
		public abstract INotNullEnumerator<T> GetEnumerator();
	}
}
