using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merge {
	class Program {
		static void Main(string[] args) {

			if (args.Length != 4) {
				Console.WriteLine("Required 4 arguments: <source-file>, <version1-file>, <version2-file>, <output-file>");
				return;
			}

			var sourcePath = args[0];
			var version1Path = args[1];
			var version2Path = args[2];
			var outputPath = args[3];

			if (!File.Exists(sourcePath)) {
				Console.WriteLine($"Source File not found: {sourcePath}");
				return;
			}

			if (!File.Exists(version1Path)) {
				Console.WriteLine($"Version 1 File not found: {version1Path}");
				return;
			}

			if (!File.Exists(version2Path)) {
				Console.WriteLine($"Version 2 File not found: {version1Path}");
				return;
			}

			var source = File.ReadAllLines(sourcePath);
			var version1 = File.ReadAllLines(version1Path);
			var version2 = File.ReadAllLines(version2Path);

			var merger = new Merger(source, version1, version2);
			try {
				merger.Merge(outputPath);
			} catch (IOException e) {
				Console.Error.WriteLine(e.Message);
			}
			
		}
	}
}
