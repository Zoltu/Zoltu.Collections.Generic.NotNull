using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Collections.Generic.NotNull;

namespace Zoltu.Linq.NotNull
{
	public static partial class NotNullEnumerable
	{
		public static INotNullEnumerable<T> NotNull<T>(this IEnumerable<T> source)
		{
			return new NullToNotNullEnumerable<T>(source);
		}

		private class NullToNotNullEnumerable<T> : INotNullEnumerable<T>
		{
			private IEnumerable<T> _backingEnumerable;

			[ContractInvariantMethod]
			private void ContractInvariants()
			{
				Contract.Invariant(_backingEnumerable != null);
			}

			public NullToNotNullEnumerable(IEnumerable<T> backingEnumerable)
			{
				Contract.Requires(backingEnumerable != null);
				_backingEnumerable = backingEnumerable;
			}

			public INotNullEnumerator<T> GetEnumerator()
			{
				return new NullToNotNullEnumerator<T>(_backingEnumerable.GetEnumerator());
			}
		}

		private class NullToNotNullEnumerator<T> : INotNullEnumerator<T>
		{
			private readonly IEnumerator<T> _backingEnumerator;

			[ContractInvariantMethod]
			private void ContractInvariants()
			{
				Contract.Invariant(_backingEnumerator != null);
			}

			public NullToNotNullEnumerator(IEnumerator<T> backingEnumerator)
			{
				Contract.Requires(backingEnumerator != null);
				_backingEnumerator = backingEnumerator;
			}

			public T Current
			{
				get
				{
					var current = _backingEnumerator.Current;
					Contract.Assume(current != null);
					return current;
				}
			}

			public Boolean MoveNext()
			{
				while (_backingEnumerator.MoveNext())
				{
					if (_backingEnumerator.Current != null)
						return true;
				}

				return false;
			}

			public void Dispose() { }
		}
	}
}
