using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Collections.Generic.NotNull;

namespace Zoltu.Linq.NotNull
{
	internal class NotNullToNullEnumerable<T> : IEnumerable<T>
	{
		private readonly INotNullEnumerable<T> _source;

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_source != null);
		}

		public NotNullToNullEnumerable(INotNullEnumerable<T> source)
		{
			Contract.Requires(source != null);

			_source = source;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new NotNullToNullEnumerator<T>(_source.GetEnumerator());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	internal class NotNullToNullEnumerator<T> : IEnumerator<T>
	{
		private INotNullEnumerator<T> _source;
		private State _state = State.BeforeIterating;

		[ContractInvariantMethod]
		private void ContractInvarians()
		{
			Contract.Invariant(_source != null);
		}

		public NotNullToNullEnumerator(INotNullEnumerator<T> source)
		{
			Contract.Requires(source != null);

			_source = source;
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
				return _current;
			}
			protected set
			{
				_current = value;
			}
		}
		private T _current;

		Object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		protected virtual void Dispose(Boolean disposing)
		{
			_state = State.Disposed;
			Current = default(T);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public Boolean MoveNext()
		{
			_state = State.Iterating;

			if (_source.MoveNext())
			{
				_current = _source.Current;
				return true;
			}

			_state = State.AfterIterating;
			return false;
		}

		public void Reset()
		{
			throw new NotSupportedException();
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
