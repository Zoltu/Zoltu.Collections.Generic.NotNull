using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Linq.NotNull;

namespace Zoltu.Collections.Generic.NotNull
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class NotNullDictionary<TKey, TValue> : INotNullDictionary<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> _backingDictionary = new Dictionary<TKey, TValue>();

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_backingDictionary != null);
			Contract.Invariant(Count == _backingDictionary.Count);
		}

		public TValue this[TKey key]
		{
			get
			{
				var result = _backingDictionary[key];
				if (result == null)
					throw new KeyNotFoundException();
				return result;
			}

			set
			{
				_backingDictionary[key] = value;
			}
		}

		public Int32 Count
		{
			get
			{
				Contract.Ensures(Contract.Result<Int32>() == _backingDictionary.Count);
				return _backingDictionary.Count;
			}
		}

		public INotNullEnumerable<TKey> Keys
		{
			get
			{
				return _backingDictionary.Keys.NotNull();
			}
		}

		public INotNullEnumerable<TValue> Values
		{
			get
			{
				return _backingDictionary.Values.NotNull();
			}
		}

		public void Add(NotNullKeyValuePair<TKey, TValue> item)
		{
			var oldCount = Count;
			_backingDictionary.Add(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
			Contract.Assume(Count == oldCount + 1);
		}

		public void Add(TKey key, TValue value)
		{
			_backingDictionary.Add(key, value);
		}

		public void Clear()
		{
			_backingDictionary.Clear();
		}

		public Boolean Contains(NotNullKeyValuePair<TKey, TValue> item)
		{
			return _backingDictionary.Contains(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
		}

		public Boolean ContainsKey(TKey key)
		{
			return _backingDictionary.ContainsKey(key);
		}

		public INotNullEnumerator<NotNullKeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new Enumerator(_backingDictionary.GetEnumerator());
		}

		public Boolean Remove(NotNullKeyValuePair<TKey, TValue> item)
		{
			return _backingDictionary.Remove(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
		}

		public Boolean Remove(TKey key)
		{
			var oldCount = Count;
			var result = _backingDictionary.Remove(key);
			Contract.Assume(!result || (Count == oldCount - 1));
			return result;
		}

		public Boolean TryGetValue(TKey key, out TValue value)
		{
			if (!_backingDictionary.TryGetValue(key, out value))
				return false;
			if (value == null)
				return false;

			return true;
		}

		private class Enumerator : INotNullEnumerator<NotNullKeyValuePair<TKey, TValue>>
		{
			private readonly IEnumerator<KeyValuePair<TKey, TValue>> _backingEnumerator;

			[ContractInvariantMethod]
			private void ContractInvariants()
			{
				Contract.Invariant(_backingEnumerator != null);
			}

			public Enumerator(IEnumerator<KeyValuePair<TKey, TValue>> backingEnumerator)
			{
				Contract.Requires(backingEnumerator != null);
				_backingEnumerator = backingEnumerator;
			}

			public NotNullKeyValuePair<TKey, TValue> Current
			{
				get
				{
					var backingCurrent = _backingEnumerator.Current;
					Contract.Assume(backingCurrent.Key != null);
					Contract.Assume(backingCurrent.Value != null);
					return new NotNullKeyValuePair<TKey, TValue>(backingCurrent.Key, backingCurrent.Value);
				}
			}

			public bool MoveNext()
			{
				while (_backingEnumerator.MoveNext())
				{
					if (_backingEnumerator.Current.Key != null && _backingEnumerator.Current.Value != null)
						return true;
				}

				return false;
			}

			public void Dispose() { }
		}
	}
}
