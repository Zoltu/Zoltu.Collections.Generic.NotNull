using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Collections.Generic.NotNull;

namespace Zoltu.Linq.NotNull
{
	internal class NullToNotNullEnumerable<T> : INotNullEnumerable<T>
	{
		private readonly IEnumerable<T> _source;

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_source != null);
		}

		public NullToNotNullEnumerable(IEnumerable<T> source)
		{
			_source = source ?? EmptyEnumerable<T>.Instance;
		}

		public INotNullEnumerator<T> GetEnumerator()
		{
			return new Enumerator(_source.GetEnumerator());
		}
		private sealed class Enumerator : INotNullEnumerator<T>
		{
			private readonly IEnumerator<T> _source;
			private T _current;
			private States _state = States.BeforeFirst;

			[ContractInvariantMethod]
			private void ContractInvariants()
			{
				Contract.Invariant(_source != null);
				Contract.Invariant(_current != null || _state != States.Enumerating);
			}

			public Enumerator(IEnumerator<T> source)
			{
				Contract.Requires(source != null);
				_source = source;
			}

			public T Current
			{
				get
				{
					Contract.Ensures(Contract.Result<T>() != null);
					switch (_state)
					{
						case States.Enumerating:
							return _current;
						case States.BeforeFirst:
							throw new InvalidOperationException("Attempted to access Current before the first iteration.");
						case States.AfterLast:
							throw new InvalidOperationException("Attempted to access Current after the last iteration.");
						case States.Disposed:
							throw new InvalidOperationException("Attempted to access Current after disposal.");
						default:
							throw new InvalidOperationException("Invalid state detected: " + _state.ToString());
					}
				}
			}

			public void Dispose()
			{
				_state = States.Disposed;
			}

			public Boolean MoveNext()
			{
				if (_state == States.AfterLast)
					return false;
				if (_state == States.Disposed)
					return false;

				while (_source.MoveNext())
				{
					_current = _source.Current;
					if (_current == null)
						continue;

					_state = States.Enumerating;
					return true;
				}

				_state = States.AfterLast;
				return false;
			}

			private enum States
			{
				BeforeFirst,
				Enumerating,
				AfterLast,
				Disposed,
			}
		}
	}
}
