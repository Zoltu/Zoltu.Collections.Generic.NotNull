using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zoltu.Collections.Generic.NotNull.Tests
{
	[TestClass]
	public class NotNullDictionary
	{
		[TestMethod]
		public void when_add_pair_then_can_look_it_up()
		{
			var dictionary = new NotNullDictionary<String, String>();
			dictionary.Add("foo", "bar");

			Assert.AreEqual("bar", dictionary["foo"]);
		}

		[TestMethod]
		public void when_lookup_key_that_does_not_exist_then_throws_exception()
		{
			try
			{
				var dictionary = new NotNullDictionary<String, String>();
				var result = dictionary["foo"];
				Assert.Fail("Exception should have been thrown.");
			}
			catch (Exception)
			{ }
		}

		[TestMethod]
		public void when_TryGet_value_that_does_not_exist_then_returns_false()
		{
			var dictionary = new NotNullDictionary<String, String>();
			String value;
			var result = dictionary.TryGetValue("foo", out value);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void when_TryGet_value_that_exists_then_returns_true()
		{
			var dictionary = new NotNullDictionary<String, String>();
			dictionary.Add("foo", "bar");
			String value;
			var result = dictionary.TryGetValue("foo", out value);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void when_ContainsKey_that_exists_then_returns_true()
		{
			var dictionary = new NotNullDictionary<String, String>();
			dictionary.Add("foo", "bar");

			var result = dictionary.ContainsKey("foo");

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void when_ContainsKey_that_does_not_exist_then_returns_false()
		{
			var dictionary = new NotNullDictionary<String, String>();

			var result = dictionary.ContainsKey("foo");

			Assert.IsFalse(result);
		}

	}
}
