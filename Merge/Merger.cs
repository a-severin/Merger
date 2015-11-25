using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Merge {
	public class Merger {
		private readonly string[] _source;
		private readonly string[] _version1;
		private readonly string[] _version2;

		public Merger(string[] source, string[] version1, string[] version2) {
			_source = source;
			_version1 = version1;
			_version2 = version2;
		}

		public void Merge(string outputPath) {
			File.WriteAllLines(outputPath, Merge());
		}

		public string[] Merge() {
			
			var ver1Indexes = FindLinesIndex(_source, _version1);
			var ver1Set = new HashSet<int>(ver1Indexes);
			var ver2Indexes = FindLinesIndex(_source, _version2);
			var ver2Set = new HashSet<int>(ver2Indexes);

			var result = new List<string>();
			int si = 0, v1 = 0, v2 = 0;
			while (si < _source.Length || v1 < _version1.Length || v2 < _version2.Length) {
				if (v1 < _version1.Length) {
					if (ver1Indexes[v1] >= 0) {
						v1++;
					} else {
						while (v1 < _version1.Length && ver1Indexes[v1] < 0) {
							result.Add(_version1[v1]);
							v1++;
						}
					}
				}

				if (v2 < _version2.Length) {
					if (ver2Indexes[v2] >= 0) {
						v2++;
					} else {
						while (v2 < _version2.Length && ver2Indexes[v2] < 0) {
							result.Add(_version2[v2]);
							v2++;
						}
					}
				}

				if (si < _source.Length) {
					if (ver1Set.Contains(si) && ver2Set.Contains(si)) {
						result.Add(_source[si]);
					}
					si++;
				}
			}
			
			return result.ToArray();
		}

		public static int[] FindLinesIndex(string[] src, string[] ver) {
			//value -1 means new line
			var indexes = new int[ver.Length];
			for (int i = 0; i < indexes.Length; i++) {
				indexes[i] = -1;
			}

			var j = 0;
			for (int i = 0; i < ver.Length; i++) {

				if (j >= src.Length) {
					continue;
				}

				if (src[j] == ver[i]) {
					indexes[i] = j;
					j++;
				} else {
					var k = j;
					k++;
					while (k < src.Length) {
						if (src[k] == ver[i]) {
							j = k;
							indexes[i] = j;
							break;
						}
						k++;
					} ;
				}
			}

			return indexes;
		}
	}
}