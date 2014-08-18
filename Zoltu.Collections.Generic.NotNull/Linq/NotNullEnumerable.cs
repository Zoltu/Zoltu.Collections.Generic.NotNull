using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Zoltu.Collections.Generic.NotNull;

namespace Zoltu.Linq.NotNull
{
	public static partial class NotNullEnumerable
	{
		public static INotNullEnumerable<T> NotNull<T>(this IEnumerable<T> source)
		{
			Contract.Ensures(Contract.Result<INotNullEnumerable<T>>() != null);

			if (source == null)
				return EmptyEnumerable<T>.Instance;

			return new NullToNotNullEnumerable<T>(source);
		}

		public static IEnumerable<T> NotNullToNull<T>(this INotNullEnumerable<T> source)
		{
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			if (source == null)
				return EmptyEnumerable<T>.Instance;

			return new NotNullToNullEnumerable<T>(source);
		}
	}
}
