using System;
using System.Diagnostics.Contracts;

namespace Zoltu.Collections.Generic.NotNull
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	[ContractClass(typeof(INotNullDictionaryContracts<,>))]
	public interface INotNullDictionary<TKey, TValue> : INotNullCollection<NotNullKeyValuePair<TKey, TValue>>
	{
		TValue this[TKey key] { get; set; }
		INotNullEnumerable<TKey> Keys { get; }
		INotNullEnumerable<TValue> Values { get; }
		Boolean ContainsKey(TKey key);
		void Add(TKey key, TValue value);
		Boolean Remove(TKey key);
		Boolean TryGetValue(TKey key, out TValue value);
	}

	[ContractClassFor(typeof(INotNullDictionary<,>))]
	public abstract class INotNullDictionaryContracts<TKey, TValue> : INotNullDictionary<TKey, TValue>
	{
		public TValue this[TKey key]
		{
			get
			{
				Contract.Ensures(Contract.Result<TValue>() != null);
				return default(TValue);
			}
			set
			{
				Contract.Requires(value != null);
			}
		}

		public INotNullEnumerable<TKey> Keys
		{
			get
			{
				Contract.Ensures(Contract.Result<INotNullEnumerable<TKey>>() != null);
				return default(INotNullEnumerable<TKey>);
			}
		}

		public INotNullEnumerable<TValue> Values
		{
			get
			{
				Contract.Ensures(Contract.Result<INotNullEnumerable<TValue>>() != null);
				return default(INotNullEnumerable<TValue>);
			}
		}

		public void Add(TKey key, TValue value)
		{
			Contract.Requires(key != null);
			Contract.Requires(value != null);
		}

		public Boolean ContainsKey(TKey key)
		{
			return default(Boolean);
		}

		public Boolean Remove(TKey key)
		{
			Contract.Ensures(Contract.Result<Boolean>() == false || Count == Contract.OldValue(Count) - 1);
			return default(Boolean);
		}

		public Boolean TryGetValue(TKey key, out TValue value)
		{
			Contract.Ensures(!Contract.Result<Boolean>() || Contract.ValueAtReturn(out value) != null);
			value = default(TValue);
			return default(Boolean);
		}

		public abstract int Count { get; }
		public abstract void Add(NotNullKeyValuePair<TKey, TValue> item);
		public abstract void Clear();
		public abstract bool Contains(NotNullKeyValuePair<TKey, TValue> item);
		public abstract INotNullEnumerator<NotNullKeyValuePair<TKey, TValue>> GetEnumerator();
		public abstract bool Remove(NotNullKeyValuePair<TKey, TValue> item);
	}
}
