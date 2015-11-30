namespace Merge {
	/// <summary>
	/// Represent line of changed version code 
	/// </summary>
	public class VersionLine : Line {
		public int SourcePositon = -1;

		public VersionLine(string content) : base(content) {
		}
	}
}