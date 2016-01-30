using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ConcentrationColorizeMaterialColorSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(ConcentrationColorizeMaterialColor)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var colorize = entity.GetComponent<ConcentrationColorizeMaterialColor>();
		colorize.Material.color = colorize.Gradient.Evaluate(colorize.Concentration.T);
	}
}
