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
			_source = (source != null)
				? source
				: EmptyEnumerable<T>.Instance;
		}

		public INotNullEnumerator<T> GetEnumerator()
		{
			return new NullToNotNullEnumerator<T>(_source.GetEnumerator());
		}
	}

	internal class NullToNotNullEnumerator<T> : INotNullEnumerator<T>
	{
		private readonly IEnumerator<T> _sourceEnumerator;
		private State _state = State.BeforeIterating;

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_sourceEnumerator != null);
		}

		public NullToNotNullEnumerator(IEnumerator<T> source)
		{
			_sourceEnumerator = source;
		}

		public virtual T Current
		{
			get
			{
				if (_state == State.BeforeIterating)
					throw new InvalidOperationException("Attempted to access enumerator's current value before the first iteration.");
				if (_state == State.AfterIterating)
					throw new InvalidOperationException("Attempted to access enumerator's current value after the last iteration.");
				if (_state == State.Disposed)
					throw new InvalidOperationException("Attempted to access enumerator's current value after it has been disposed.");

				Contract.Assume(_current != null);
				return _current;
			}
			protected set
			{
				_current = value;
			}
		}
		private T _current;

		protected virtual void Dispose(Boolean disposing)
		{
			_state = State.Disposed;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public Boolean MoveNext()
		{
			_state = State.Iterating;

			while (_sourceEnumerator.MoveNext())
			{
				Current = _sourceEnumerator.Current;
				if (Current == null)
					continue;

				return true;
			}

			_state = State.AfterIterating;
			return false;
		}

		private enum State
		{
			BeforeIterating,
			Iterating,
			AfterIterating,
			Disposed,
		}
	}
}
