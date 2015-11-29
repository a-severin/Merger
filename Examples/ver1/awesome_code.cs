using System;
using System.IO;

  namespace Test 
{
	public class TestType 
	{
		private string var1 = "Func1";
		private string param;

		public TestType(string param) {
			this.param = param;
		}
		
		public void Func1() {
			Console.WriteLine(var1);
		}
	}
}