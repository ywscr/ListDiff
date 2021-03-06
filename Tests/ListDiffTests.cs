using ListDiff;
using Xunit;

namespace ListDiffTests
{
	public class Tests
	{
		[Theory]
		[InlineData ("", "", "")]
		[InlineData ("", "a", "+(a)")]
		[InlineData ("a", "", "-(a)")]
		[InlineData ("a", "a", "a")]
		[InlineData ("a", "b", "-(a)+(b)")]
		[InlineData ("ab", "ab", "ab")]
		[InlineData ("abc", "ab", "ab-(c)")]
		[InlineData ("ab", "abc", "ab+(c)")]
		[InlineData ("ab", "zab", "+(z)ab")]
		[InlineData ("ab", "b", "-(a)b")]
		[InlineData ("abc", "ac", "a-(b)c")]
		[InlineData ("abc", "a", "a-(b)-(c)")]
		[InlineData ("abc", "c", "-(a)-(b)c")]
		[InlineData ("abc", "", "-(a)-(b)-(c)")]
		public void SimpleCases (string left, string right, string expectedDiff)
		{
			var diff = left.Diff (right);
			Assert.Equal (expectedDiff, diff.ToString ());
		}

		[Theory]
		[InlineData (1000)]
		[InlineData (10000)]
		[InlineData (100000)]
		public void DiffMiddleModificationOfLongList (int listSize)
		{
			var sb = new System.Text.StringBuilder ();
			for (int i = 0; i < listSize / 10; i++) {
				sb.Append ("0123456789");
			}

			var original = sb.ToString ();
			var middleIndex = original.Length / 2;
			var middleItem = original[middleIndex];
			var modified = sb.ToString ().Remove (middleIndex, 1);
			var expectedDiff = modified.Insert (middleIndex, string.Format("-({0})", middleItem));

			var diff = original.Diff (modified);

			Assert.Equal (expectedDiff, diff.ToString ());
		}

		[Fact]
		public void Insert ()
		{
			var source = "ac";
			var destination = "abc";
			var diff = source.Diff (destination);
		}
	}
}
