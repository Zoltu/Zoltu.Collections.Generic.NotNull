using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Zoltu.Collections.Generic.NotNull
{
	[ContractClass(typeof(NotNullEnumerableContracts<>))]
	public interface INotNullEnumerable<out T>
	{
		INotNullEnumerator<T> GetEnumerator();
	}

	[ContractClassFor(typeof(INotNullEnumerable<>))]
	public abstract class NotNullEnumerableContracts<T> : INotNullEnumerable<T>
	{
		public INotNullEnumerator<T> GetEnumerator()
		{
			Contract.Ensures(Contract.Result<INotNullEnumerator<T>>() != null);
			return default(INotNullEnumerator<T>);
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public sealed class EmptyEnumerable<T> : INotNullEnumerable<T>, INotNullEnumerator<T>, IEnumerable<T>, IEnumerator<T>
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static readonly EmptyEnumerable<T> Instance = new EmptyEnumerable<T>();

		private EmptyEnumerable()
		{
		}

		T IEnumerator<T>.Current
		{
			get
			{
				throw new InvalidOperationException("Attempted to access Current on empty enumerable.");
			}
		}

		Object IEnumerator.Current
		{
			get
			{
				throw new InvalidOperationException("Attempted to access Current on empty enumerable.");
			}
		}

		T INotNullEnumerator<T>.Current
		{
			get
			{
				throw new InvalidOperationException("Attempted to access Current on empty enumerable.");
			}
		}

		public void Dispose()
		{
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this;
		}

		INotNullEnumerator<T> INotNullEnumerable<T>.GetEnumerator()
		{
			return this;
		}

		Boolean IEnumerator.MoveNext()
		{
			return false;
		}

		Boolean INotNullEnumerator<T>.MoveNext()
		{
			return false;
		}

		void IEnumerator.Reset()
		{
		}
	}
}
