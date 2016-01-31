using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo
{
	public partial class Events
	{
		public static readonly Events Win = new Events(1);
		public static readonly Events Lose = new Events(2);
		public static readonly Events Recycle = new Events(3);
		public static readonly Events OnQuit = new Events(8);
		public static readonly Events OnLevelWasLoaded = new Events(10);
		public static readonly Events End_Any = new Events(16, 17, 18, 19, 20, 21, 22);
		public static readonly Events End_0 = new Events(16);
		public static readonly Events End_1 = new Events(17);
		public static readonly Events End_2 = new Events(18);
		public static readonly Events End_3 = new Events(19);
		public static readonly Events End_4 = new Events(20);
		public static readonly Events End_5 = new Events(21);
		public static readonly Events End_6 = new Events(22);
	}
}
