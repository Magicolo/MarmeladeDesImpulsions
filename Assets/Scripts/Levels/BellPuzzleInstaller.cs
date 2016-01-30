using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class BellPuzzleInstaller : SceneInstaller
{
	public override void InstallBindings()
	{
		base.InstallBindings();

		Container.Bind<IPuzzleLevel>().ToSingle<BellPuzzleLevel>();
	}
}