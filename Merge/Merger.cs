using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace Merge {
	public class Merger {
		private readonly SourceLine[] _source;
		private readonly VersionLine[] _version1;
		private readonly VersionLine[] _version2;
		private int _si;
		private int _v1;
		private int _v2;

		public Merger(string[] source, string[] version1, string[] version2) {

			_source = new SourceLine[source.Length];
			for (int i = 0; i < source.Length; i++) {
				_source[i] = new SourceLine(source[i]);
			}

			_version1 = new VersionLine[version1.Length];
			for (int i = 0; i < version1.Length; i++) {
				_version1[i] = new VersionLine(version1[i]);
			}

			_version2 = new VersionLine[version2.Length];
			for (int i = 0; i < version2.Length; i++) {
				_version2[i] = new VersionLine(version2[i]);
			}

			_si = 0;
			_v1 = 0;
			_v2 = 0;
		}

		private bool _next() {
			var result = false;
			if (_si + 1 < _source.Length) {
				_si++;
				result = true;
			}
			if (_v1 + 1 < _version1.Length) {
				_v1++;
				result = true;
			}
			if (_v2 + 1 < _version2.Length) {
				_v2++;
				result = true;
			}

			return result;
		}

		private void _reset() {
			_si = 0;
			_v1 = 0;
			_v2 = 0;
		}

		public void Merge(string outputPath) {
			File.WriteAllLines(outputPath, Merge());
		}

		public string[] Merge() {
			FindPositions(_source, _version1);
			FindPositions(_source, _version2);

			var result = new List<string>();
			int si = 0, v1 = 0, v2 = 0;
			while (si < _source.Length || v1 < _version1.Length || v2 < _version2.Length) {
				if (v1 < _version1.Length) {
					if (_version1[v1].SourcePositon >= 0) {
						v1++;
					} else {
						while (v1 < _version1.Length && _version1[v1].SourcePositon < 0) {
							result.Add(_version1[v1].Content);
							v1++;
						}
					}
				}

				if (v2 < _version2.Length) {
					if (_version2[v2].SourcePositon >= 0) {
						v2++;
					} else {
						while (v2 < _version2.Length && _version2[v2].SourcePositon < 0) {
							result.Add(_version2[v2].Content);
							v2++;
						}
					}
				}

				if (si < _source.Length) {
					if (_source[si].ExistsFlag == 2) {
						result.Add(_source[si].Content);
					}
					si++;
				}
			}

			return result.ToArray();
		}

		public static void FindPositions(SourceLine[] source, VersionLine[] version) {
			var j = 0;
			foreach (VersionLine ver in version) {
				if (j >= source.Length) {
					continue;
				}

				if (source[j].Content == ver.Content) {
					ver.SourcePositon = j;
					source[j].ExistsFlag += 1;
					j++;
				} else {
					var k = j;
					k++;
					while (k < source.Length) {
						if (source[k].Content == ver.Content) {
							j = k;
							ver.SourcePositon = j;
							source[j].ExistsFlag += 1;
							break;
						}
						k++;
					}
				}
			}
		}
	}

	public class Line {

		public Line(string content) {
			Content = content;
			TrimedContent = content.Trim();
		}

		public readonly string Content;
		public readonly string TrimedContent;
	}

	public class VersionLine: Line {
		public VersionLine(string content): base(content) {
		}

		public int SourcePositon = -1;
	}

	public class SourceLine : Line {
		public SourceLine(string content) : base(content) {
		}

		public int ExistsFlag = 0;
	}
}