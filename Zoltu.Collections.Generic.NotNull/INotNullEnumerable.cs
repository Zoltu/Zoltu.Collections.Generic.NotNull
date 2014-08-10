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
}
