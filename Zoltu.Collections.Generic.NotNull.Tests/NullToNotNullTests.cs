using System;
using System.Collections.Generic;
using Xunit;
using Zoltu.Linq.NotNull;

namespace Zoltu.Collections.Generic.NotNull.Tests
{
	public class NullToNotNullTests
	{
		[Fact]
		public void when_source_is_empty_then_destination_is_empty()
		{
			var list = new List<String> { };
			var enumerable = list.NotNull();

			Assert.False(enumerable.GetEnumerator().MoveNext());
		}

		[Fact]
		public void when_source_contains_null_only_then_destination_is_empty()
		{
			var list = new List<String> { null };
			var enumerable = list.NotNull();

			Assert.False(enumerable.GetEnumerator().MoveNext());
		}

		[Fact]
		public void when_source_contains_null_at_end_then_destination_contains_only_not_null_items()
		{
			var list = new List<String> { "foo", null };
			var enumerable = list.NotNull();

			var enumerator = enumerable.GetEnumerator();
			enumerator.MoveNext();
			Assert.Equal("foo", enumerator.Current);
			Assert.False(enumerator.MoveNext());
		}

		[Fact]
		public void when_source_contains_null_at_beginning_then_destination_contains_only_not_null_items()
		{
			var list = new List<String> { null, "foo" };
			var enumerable = list.NotNull();

			var enumerator = enumerable.GetEnumerator();
			enumerator.MoveNext();
			Assert.Equal("foo", enumerator.Current);
			Assert.False(enumerator.MoveNext());
		}

		[Fact]
		public void when_source_contains_only_not_null_items_then_destination_contains_them()
		{
			var list = new List<String> { "foo" };
			var enumerable = list.NotNull();

			var enumerator = enumerable.GetEnumerator();
			enumerator.MoveNext();
			Assert.Equal("foo", enumerator.Current);
		}
	}
}
