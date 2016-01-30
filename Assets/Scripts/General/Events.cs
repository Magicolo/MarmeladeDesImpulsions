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
		public static readonly Events OnQuit = new Events(8);
	}
}
