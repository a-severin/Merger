namespace Merge {
	/// <summary>
	/// Represent line of code
	/// </summary>
	public class Line {
		public readonly string Content;
		public readonly string TrimedContent;

		public Line(string content) {
			Content = content;
			TrimedContent = content.Trim();
		}
	}
}