using System;
using System.Diagnostics.Contracts;

namespace Zoltu.Collections.Generic.NotNull
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
	[Serializable]
	public struct NotNullKeyValuePair<TKey, TValue>
	{
		private readonly TKey _key;
		private readonly TValue _value;

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_key != null);
			Contract.Invariant(_value != null);
		}

		public TKey Key
		{
			get
			{
				Contract.Ensures(Contract.Result<TKey>() != null);
				return _key;
			}
		}
		public TValue Value
		{
			get
			{
				Contract.Ensures(Contract.Result<TValue>() != null);
				return _value;
			}
		}

		public NotNullKeyValuePair(TKey key, TValue value)
		{
			Contract.Requires(key != null);
			Contract.Requires(value != null);
			_key = key;
			_value = value;
		}

		public override String ToString()
		{
			Contract.Ensures(Contract.Result<String>() != null);
			return String.Format("[{0}, {1}]", Key.ToString(), Value.ToString());
		}
	}
}
