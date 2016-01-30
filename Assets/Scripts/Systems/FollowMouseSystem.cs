using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FollowMouseSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(FollowMouseComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var follower = entity.GetComponent<FollowMouseComponent>();
		follower.Transform.position = Camera.main.GetMouseWorldPosition();
	}
}
