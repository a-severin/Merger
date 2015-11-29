using System.Collections.Generic;


namespace Merge {
	public class MergeAction {
		public readonly List<Line> Lines = new List<Line>();

		public readonly int MergePosition;

		public MergeAction(int mergePosition) {
			MergePosition = mergePosition;
		}

		public bool IsEqualContents(MergeAction comp) {
			if (MergePosition != comp.MergePosition) {
				return false;
			}
			if (Lines.Count != comp.Lines.Count) {
				return false;
			}

			for (var i = 0; i < Lines.Count; i++) {
				if (Lines[i].TrimedContent != comp.Lines[i].TrimedContent) {
					return false;
				}
			}

			return true;
		}
	}
}