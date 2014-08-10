using System;
using System.Diagnostics.Contracts;

namespace Zoltu.Collections.Generic.NotNull
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	[ContractClass(typeof(NotNullCollectionContracts<>))]
	public interface INotNullCollection<T> : INotNullEnumerable<T>
	{
		Int32 Count { get; }
		void Add(T item);
		void Clear();
		Boolean Contains(T item);
		Boolean Remove(T item);
	}

	[ContractClassFor(typeof(INotNullCollection<>))]
	public abstract class NotNullCollectionContracts<T> : INotNullCollection<T>
	{
		public Int32 Count
		{
			get
			{
				Contract.Ensures(Contract.Result<Int32>() >= 0);
				return default(Int32);
			}
		}

		public void Add(T item)
		{
			Contract.Requires(item != null);
			Contract.Ensures(Count == Contract.OldValue(Count) + 1);
		}

		public void Clear()
		{
			Contract.Ensures(Count == 0);
		}

		public abstract Boolean Contains(T item);
		public abstract Boolean Remove(T item);
		public abstract INotNullEnumerator<T> GetEnumerator();
	}
}
