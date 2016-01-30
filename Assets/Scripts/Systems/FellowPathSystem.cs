using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FellowPathSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(PathFellower),
			typeof(MouvementSpeedComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var pathFollower = entity.GetComponent<PathFellower>();
		var path = pathFollower.Path.GetComponent<SimpleWaypointsPath>();
		var movementSpeed = entity.GetComponent<MouvementSpeedComponent>();

		Transform wp = path.Waypoints[pathFollower.CurrentPathIndexTarget];

		TendTo(pathFollower, wp, movementSpeed.Speed);

		if ((pathFollower.transform.position - wp.position).magnitude <= 0.1f)
		{
			if (pathFollower.random)
			{
				pathFollower.CurrentPathIndexTarget = PRandom.Range(0, path.Waypoints.Length - 1);
			}
			else
			{
				pathFollower.CurrentPathIndexTarget++;
				if (pathFollower.CurrentPathIndexTarget >= path.Waypoints.Length)
					pathFollower.CurrentPathIndexTarget = 0;
			}
		}
	}

	private void TendTo(PathFellower pathFollower, Transform transform, float speed)
	{
		Vector3 direction = transform.position - pathFollower.transform.position;
		Vector3 mouvement = direction.normalized * speed * Time.deltaTime;
		pathFollower.transform.position += mouvement;
	}
}
