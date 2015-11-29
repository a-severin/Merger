using System;  
using System.Linq;

namespace Test 
{
	public class TestType 
	{
		private string var2 = "Func2";
		private int count;

		public TestType(int count) {
			this.count = count;
		}
		
		public void Func2() {
			Console.WriteLine(var2);
		}
	}
  }