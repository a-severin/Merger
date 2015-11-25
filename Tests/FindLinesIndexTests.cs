using System;
using System.Diagnostics;
using Merge;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests {
	[TestClass]
	public class FindLinesIndexTests {

		[TestMethod]
		public void TestCase1() {
			var src = new[] {"line1", "line2", "line3"};
			var ver = new[] {"line1", "line2", "line3"};

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(0, indexes[0]);
			Assert.AreEqual(1, indexes[1]);
			Assert.AreEqual(2, indexes[2]);

		}

		[TestMethod]
		public void TestCase2() {
			var src = new[] {"line1", "line2", "line3"};
			var ver = new[] {"line1", "line2", "line3", "line4"};

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(0, indexes[0]);
			Assert.AreEqual(1, indexes[1]);
			Assert.AreEqual(2, indexes[2]);
			Assert.AreEqual(-1, indexes[3]);

		}

		[TestMethod]
		public void TestCase3() {
			var src = new[] {"line1", "line3", "line4"};
			var ver = new[] {"line1", "line2", "line3", "line4"};

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(0, indexes[0]);
			Assert.AreEqual(-1, indexes[1]);
			Assert.AreEqual(1, indexes[2]);
			Assert.AreEqual(2, indexes[3]);
		}

		[TestMethod]
		public void TestCase4() {
			var src = new[] {"line1", "line2", "line3", "line4"};
			var ver = new[] {"line1", "line2", "line01", "line02", "line3", "line4"};

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(0, indexes[0]);
			Assert.AreEqual(1, indexes[1]);
			Assert.AreEqual(-1, indexes[2]);
			Assert.AreEqual(-1, indexes[3]);
			Assert.AreEqual(2, indexes[4]);
			Assert.AreEqual(3, indexes[5]);
		}

		[TestMethod]
		public void TestCase5() {
			var src = Array.Empty<string>();
			var ver = new[] {"line1", "line2"};

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(-1, indexes[0]);
			Assert.AreEqual(-1, indexes[1]);
		}

		[TestMethod]
		public void TestCase6() {
			var src = new[] { "line1", "line2" };
			var ver = Array.Empty<string>();

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(0, indexes.Length);
		}

		[TestMethod]
		public void TestCase7() {
			var src = new[] { "line1", "line2", "line3" };
			var ver = new[] { "line01", "line2", "line3" };

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(-1, indexes[0]);
			Assert.AreEqual(1, indexes[1]);
			Assert.AreEqual(2, indexes[2]);
		}

		[TestMethod]
		public void TestCase8() {
			var src = new[] { "line1", "line2", };
			var ver = new[] { "line1", "line2", "line2" };

			var indexes = Merger.FindLinesIndex(src, ver);

			Assert.AreEqual(0, indexes[0]);
			Assert.AreEqual(1, indexes[1]);
			Assert.AreEqual(-1, indexes[2]);
		}
	}
}