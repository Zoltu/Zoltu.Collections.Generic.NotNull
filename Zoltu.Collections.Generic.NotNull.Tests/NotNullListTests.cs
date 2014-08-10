using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zoltu.Collections.Generic.NotNull.Tests
{
	[TestClass]
	public class NotNullListTests
	{
		[TestMethod]
		public void when_add_item_then_it_is_reachable()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			foreach (var item in list)
			{
				if (item == "foo")
					return;
			}
			Assert.Fail("Expected to find 'foo' in the list but didn't.");
		}

		[TestMethod]
		public void when_remove_item_then_it_is_not_reachable()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			list.Remove("foo");
			foreach (var item in list)
			{
				if (item == "foo")
					Assert.Fail("Expected to not find 'foo' in the list but did.");
			}
		}

		[TestMethod]
		public void when_list_is_empty_then_count_is_0()
		{
			var list = new NotNullList<String>();
			Assert.AreEqual(0, list.Count);
		}

		[TestMethod]
		public void when_list_has_1_item_then_count_is_1()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			Assert.AreEqual(1, list.Count);
		}

		[TestMethod]
		public void when_list_has_item_added_then_removed_then_count_is_0()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			list.Remove("foo");
			Assert.AreEqual(0, list.Count);
		}

		[TestMethod]
		public void when_get_item_by_index_then_gets_correct_item()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			list.Add("bar");
			Assert.AreEqual("bar", list[1]);
		}

		[TestMethod]
		public void when_item_inserted_in_middle_then_it_is_at_correct_index()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			list.Add("bar");
			list.Insert(1, "baz");
			Assert.AreEqual("baz", list[1]);
		}

		[TestMethod]
		public void when_removing_by_index_then_list_is_shifted()
		{
			var list = new NotNullList<String>();
			list.Add("foo");
			list.Add("bar");
			list.Add("baz");
			list.RemoveAt(1);
			Assert.AreEqual("baz", list[1]);
		}
	}
}
