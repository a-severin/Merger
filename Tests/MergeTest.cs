using System;
using Merge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
	[TestClass]
	public class MergeTest {
		[TestMethod]
		public void TestCase1() {
			var src = new[] { "line1", "line2", "line3" };
			var ver1 = new[] { "line1", "line2", "line3" };
			var ver2 = new[] { "line1", "line2", "line3" };

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Assert.AreEqual("line1", result[0]);
			Assert.AreEqual("line2", result[1]);
			Assert.AreEqual("line3", result[2]);
		}

		[TestMethod]
		public void TestCase2() {
			var src = new[] { "line1", "line2", "line3" };
			var ver1 = new[] { "line1", "line2", "line3", "line4" };
			var ver2 = new[] { "line1", "line2", "line3" };

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Assert.AreEqual("line1", result[0]);
			Assert.AreEqual("line2", result[1]);
			Assert.AreEqual("line3", result[2]);
			Assert.AreEqual("line4", result[3]);
		}

		[TestMethod]
		public void TestCase3() {
			var src = new[] { "line1", "line2", "line3" };
			var ver1 = new[] { "line1", "line2", "line3", "line4" };
			var ver2 = new[] { "line1", "line2", "line3", "line5", "line6" };

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Assert.AreEqual("line1", result[0]);
			Assert.AreEqual("line2", result[1]);
			Assert.AreEqual("line3", result[2]);
			Assert.AreEqual("line4", result[3]);
			Assert.AreEqual("line5", result[4]);
			Assert.AreEqual("line6", result[5]);
		}

		[TestMethod]
		public void TestCase4() {
			var src = new[] { "line1", "line2", "line3" };
			var ver1 = new[] { "line7", "line2", "line3", "line4" };
			var ver2 = new[] { "line1", "line2", "line3", "line5", "line6" };

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Console.WriteLine(string.Join("\n", result));

			Assert.AreEqual("line7", result[0]);
			Assert.AreEqual("line2", result[1]);
			Assert.AreEqual("line3", result[2]);
			Assert.AreEqual("line4", result[3]);
			Assert.AreEqual("line5", result[4]);
			Assert.AreEqual("line6", result[5]);
		}

		[TestMethod]
		public void TestCase5() {
			var src = new[] { "line1", "line2" };
			var ver1 = new[] { "line3", "line4" };
			var ver2 = new[] { "line5", "line6" };

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Console.WriteLine(string.Join("\n", result));

			Assert.AreEqual("line3", result[0]);
			Assert.AreEqual("line4", result[1]);
			Assert.AreEqual("line5", result[2]);
			Assert.AreEqual("line6", result[3]);
		}

		[TestMethod]
		public void TestCase6() {
			var src = new[] { "line1", "line2" };
			var ver1 = new[] { "line1", " line3", "line2", "line4 " };
			var ver2 = new[] { "line1", "line3 ", "line2", " line4" };

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Console.WriteLine(string.Join("\n", result));

			Assert.AreEqual("line1", result[0]);
			Assert.AreEqual(" line3", result[1]);
			Assert.AreEqual("line2", result[2]);
			Assert.AreEqual("line4 ", result[3]);
		}

		[TestMethod]
		public void TestCase7() {
			var src = new[] {
				"{",
				"class Test{",
				"\n",
				"}",
				"}" };
			var ver1 = new[] {
				"{",
				"class Test{",
				"public void Func1(){",
				"}",
				"}",
				"}"
			};
			var ver2 = new[] {
				"{",
				"class Test{",
				"public void Func2(){",
				"}",
				"}",
				"}"
			};

			var merger = new Merger(src, ver1, ver2);
			var result = merger.Merge();

			Console.WriteLine(string.Join("\n", result));

			Assert.AreEqual("{", result[0]);
			Assert.AreEqual("class Test{", result[1]);
			Assert.AreEqual("public void Func1(){", result[2]);
			Assert.AreEqual("}", result[3]);
			Assert.AreEqual("public void Func2(){", result[4]);
			Assert.AreEqual("}", result[5]);
			Assert.AreEqual("}", result[6]);
			Assert.AreEqual("}", result[7]);
		}
	}
}
