using System;
using System.Diagnostics.Contracts;

namespace Zoltu.Collections.Generic.NotNull
{
	[ContractClass(typeof(NotNullEnumeratorContracts<>))]
	public interface INotNullEnumerator<out T> : IDisposable
	{
		T Current { get; }
		Boolean MoveNext();
	}

	[ContractClassFor(typeof(INotNullEnumerator<>))]
	public abstract class NotNullEnumeratorContracts<T> : INotNullEnumerator<T>
	{
		public T Current
		{
			get
			{
				Contract.Ensures(Contract.Result<T>() != null);
				return default(T);
			}
		}

		public abstract void Dispose();

		public abstract Boolean MoveNext();
	}
}
