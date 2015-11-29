using System;
using System.IO;
using System.Linq;

namespace Test 
{
	public class TestType 
	{
		private string var1 = "Func1";
		private string param;
		private string var2 = "Func2";
		private int count;
		
		public TestType(string param) {
			this.param = param;
		}
		public TestType(int count) {
			this.count = count;
		}
		
		public void Func1() {
			Console.WriteLine(var1);
		}
		public void Func2() {
			Console.WriteLine(var2);
		}
	}
}
