using System;

namespace Zoltu.Collections.Generic.NotNull
{
	public interface INotNullSet<T> : INotNullCollection<T>
	{
		//Add item to the set, return true if added, false if duplicate
		new Boolean Add(T item);

		//Transform this set into its union with the INotNullEnumerable<T> other
		void UnionWith(INotNullEnumerable<T> other);

		//Transform this set into its intersection with the INotNullEnumberable<T> other
		void IntersectWith(INotNullEnumerable<T> other);

		//Transform this set so it contains no elements that are also in other
		void ExceptWith(INotNullEnumerable<T> other);

		//Transform this set so it contains elements initially in this or in other, but not both
		void SymmetricExceptWith(INotNullEnumerable<T> other);

		//Check if this set is a subset of other
		bool IsSubsetOf(INotNullEnumerable<T> other);

		//Check if this set is a superset of other
		bool IsSupersetOf(INotNullEnumerable<T> other);

		//Check if this set is a subset of other, but not the same as it
		bool IsProperSupersetOf(INotNullEnumerable<T> other);

		//Check if this set is a superset of other, but not the same as it
		bool IsProperSubsetOf(INotNullEnumerable<T> other);

		//Check if this set has any elements in common with other
		bool Overlaps(INotNullEnumerable<T> other);

		//Check if this set contains the same and only the same elements as other
		bool SetEquals(INotNullEnumerable<T> other);
	}
}
