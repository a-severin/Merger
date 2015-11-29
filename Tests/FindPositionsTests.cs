using System;
using System.Linq;
using Merge;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests {
	[TestClass]
	public class FindPositionsTests {
		private SourceLine[] _createSourceLines(string[] src) {
			return src.Select(_ => new SourceLine(_)).ToArray();
		}

		private VersionLine[] _createVersionLines(string[] ver) {
			return ver.Select(_ => new VersionLine(_)).ToArray();
		}

		[TestMethod]
		public void TestCase1() {
			var src = _createSourceLines(new[] {"line1", "line2", "line3"});
			var ver = _createVersionLines(new[] {"line1", "line2", "line3"});

			Merger.FindPositions(src, ver);

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(2, ver[2].SourcePositon);
		}

		[TestMethod]
		public void TestCase2() {
			var src = _createSourceLines(new[] {"line1", "line2", "line3"});
			var ver = _createVersionLines(new[] {"line1", "line2", "line3", "line4"});

			Merger.FindPositions(src, ver);

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(2, ver[2].SourcePositon);
			Assert.AreEqual(-1, ver[3].SourcePositon);
		}

		[TestMethod]
		public void TestCase3() {
			var src = _createSourceLines(new[] {"line1", "line3", "line4"});
			var ver = _createVersionLines(new[] {"line1", "line2", "line3", "line4"});

			Merger.FindPositions(src, ver);

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(-1, ver[1].SourcePositon);
			Assert.AreEqual(1, ver[2].SourcePositon);
			Assert.AreEqual(2, ver[3].SourcePositon);
		}

		[TestMethod]
		public void TestCase4() {
			var src = _createSourceLines(new[] {"line1", "line2", "line3", "line4"});
			var ver = _createVersionLines(new[] {"line1", "line2", "line01", "line02", "line3", "line4"});

			Merger.FindPositions(src, ver);

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(-1, ver[2].SourcePositon);
			Assert.AreEqual(-1, ver[3].SourcePositon);
			Assert.AreEqual(2, ver[4].SourcePositon);
			Assert.AreEqual(3, ver[5].SourcePositon);
		}

		[TestMethod]
		public void TestCase5() {
			var src = _createSourceLines(Array.Empty<string>());
			var ver = _createVersionLines(new[] {"line1", "line2"});

			Merger.FindPositions(src, ver);

			Assert.AreEqual(-1, ver[0].SourcePositon);
			Assert.AreEqual(-1, ver[1].SourcePositon);
		}

		[TestMethod]
		public void TestCase6() {
			var src = _createSourceLines(new[] {"line1", "line2", "line3"});
			var ver = _createVersionLines(new[] {"line01", "line2", "line3"});

			Merger.FindPositions(src, ver);

			Assert.AreEqual(-1, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(2, ver[2].SourcePositon);
		}

		[TestMethod]
		public void TestCase7() {
			var src = _createSourceLines(new[] {"line1", "line2"});
			var ver = _createVersionLines(new[] {"line1", "line2", "line2"});

			Merger.FindPositions(src, ver);

			Console.WriteLine(string.Join("\n", ver.AsEnumerable()));

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(-1, ver[1].SourcePositon);
			Assert.AreEqual(1, ver[2].SourcePositon);
		}

		[TestMethod]
		public void TestCase8() {
			var src = _createSourceLines(new[] { "{", "class Test{", "\n", "}", "}" });
			var ver = _createVersionLines(new[] { "{", "class Test{", "public void Func1(){}", "}", "}" });

			Merger.FindPositions(src, ver);

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(-1, ver[2].SourcePositon);
			Assert.AreEqual(3, ver[3].SourcePositon);
			Assert.AreEqual(4, ver[4].SourcePositon);
		}

		[TestMethod]
		public void TestCase9() {
			var src = _createSourceLines(new[] { "{", "\t{", "\t}", "}" });
			var ver = _createVersionLines(new[] { "{", "\t{", "something", "\t}", "}" });

			Merger.FindPositions(src, ver);

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(-1, ver[2].SourcePositon);
			Assert.AreEqual(2, ver[3].SourcePositon);
			Assert.AreEqual(3, ver[4].SourcePositon);
		}

		[TestMethod]
		public void TestCase10() {
			var src = _createSourceLines(new[] {
				"{",
				"class Test{",
				"\n",
				"}",
				"}" });
			var ver = _createVersionLines(new[] {
				"{",
				"class Test{",
				"public void Func1(){",
				"}",
				"}",
				"}"
			});

			Merger.FindPositions(src, ver);

			Console.WriteLine(string.Join("\n", ver.AsEnumerable()));

			Assert.AreEqual(0, ver[0].SourcePositon);
			Assert.AreEqual(1, ver[1].SourcePositon);
			Assert.AreEqual(-1, ver[2].SourcePositon);
			Assert.AreEqual(-1, ver[3].SourcePositon);
			Assert.AreEqual(3, ver[4].SourcePositon);
			Assert.AreEqual(4, ver[5].SourcePositon);
		}
	}
}