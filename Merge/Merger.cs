using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Merge {
	public class Merger {
		private readonly SourceLine[] _source;
		private readonly VersionLine[] _version1;
		private readonly VersionLine[] _version2;

		public Merger(string[] source, string[] version1, string[] version2) {
			_source = new SourceLine[source.Length];
			for (var i = 0; i < source.Length; i++) {
				_source[i] = new SourceLine(source[i]);
			}

			_version1 = new VersionLine[version1.Length];
			for (var i = 0; i < version1.Length; i++) {
				_version1[i] = new VersionLine(version1[i]);
			}

			_version2 = new VersionLine[version2.Length];
			for (var i = 0; i < version2.Length; i++) {
				_version2[i] = new VersionLine(version2[i]);
			}
		}

		public void Merge(string outputPath) {
			File.WriteAllLines(outputPath, Merge());
		}

		public string[] Merge() {
			FindPositions(_source, _version1);
			FindPositions(_source, _version2);

			var removeActions = new List<MergeAction>();
			var insertActions = new List<MergeAction>();

			CollectActions(_source, _version1, _version2, removeActions, insertActions);
			DeduplicateActions(insertActions);

			var result = new List<Tuple<int, List<string>>>();

			for (var i = 0; i < _source.Length; i++) {
				result.Add(new Tuple<int, List<string>>(i, new List<string> {_source[i].Content}));
			}

			foreach (var action in removeActions) {
				result[action.MergePosition].Item2.Clear();
			}

			foreach (var action in insertActions) {
				result[action.MergePosition].Item2.AddRange(action.Lines.Select(_ => _.Content));
			}

			return result.SelectMany(_ => _.Item2).ToArray();
		}

		public static void CollectActions(SourceLine[] source,
			VersionLine[] version1,
			VersionLine[] version2,
			List<MergeAction> removeActions,
			List<MergeAction> insertActions) {
			int si = 0, v1 = 0, v2 = 0;
			while (si < source.Length || v1 < version1.Length || v2 < version2.Length) {
				if (v1 < version1.Length) {
					if (version1[v1].SourcePositon >= 0) {
						v1++;
					} else {
						var sp = v1 - 1 >= 0 ? version1[v1 - 1].SourcePositon : 0;
						var mergeAction = new MergeAction(sp);
						while (v1 < version1.Length && version1[v1].SourcePositon < 0) {
							mergeAction.Lines.Add(version1[v1]);
							v1++;
						}

						insertActions.Add(mergeAction);
					}
				}

				if (v2 < version2.Length) {
					if (version2[v2].SourcePositon >= 0) {
						v2++;
					} else {
						var sp = v2 - 1 >= 0 ? version2[v2 - 1].SourcePositon : 0;
						var mergeAction = new MergeAction(sp);
						while (v2 < version2.Length && version2[v2].SourcePositon < 0) {
							mergeAction.Lines.Add(version2[v2]);
							v2++;
						}
						insertActions.Add(mergeAction);
					}
				}

				if (si < source.Length) {
					if (source[si].ExistsFlag != 2) {
						var mergeAction = new MergeAction(si);
						mergeAction.Lines.Add(source[si]);
						removeActions.Add(mergeAction);
					}
					si++;
				}
			}
		}

		public static void DeduplicateActions(List<MergeAction> actions) {
			var i = 0;
			while (i < actions.Count - 1) {
				var ins1 = actions[i];
				var ins2 = actions[i + 1];
				if (ins1.IsEqualContents(ins2)) {
					actions.RemoveAt(i + 1);
				}
				i++;
			}
		}

		public static void FindPositions(SourceLine[] source, VersionLine[] version) {
			var j = source.Length - 1;
			for (var i = version.Length - 1; i >= 0; i--) {
				var ver = version[i];

				if (j < 0) {
					continue;
				}

				if (source[j].TrimedContent == ver.TrimedContent) {
					ver.SourcePositon = j;
					source[j].ExistsFlag += 1;
					j--;
				} else {
					var k = j;
					k--;
					while (k >= 0) {
						if (source[k].TrimedContent == ver.TrimedContent) {
							j = k;
							ver.SourcePositon = j;
							source[j].ExistsFlag += 1;
							j--;
							break;
						}
						k--;
					}
				}
			}
		}
	}
}