using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public sealed class Void
	{
		public static readonly Void Instance = new Void();

		private Void() { }
	}
}
