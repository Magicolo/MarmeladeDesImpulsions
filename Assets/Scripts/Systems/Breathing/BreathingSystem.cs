using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class BreathingSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(BreathingNode)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var node = entity.GetComponent<BreathingNode>();
		var time = entity.GetComponent<TimeComponent>();

		node.counter += time.DeltaTime;

		if (node.counter < node.BreatheInTime)
			breatheIn(node, time);
		else if (node.counter < node.BreatheInTime + node.HoldTime)
			breatheHold(node, time);
		else if (node.counter < node.BreatheInTime + node.HoldTime + node.BreatheOutTime)
			breatheOut(node, time);
		else
			NextNode(node);

	}

	private void NextNode(BreathingNode node)
	{
		node.counter = 0;
		//		if (node.NextNode != null)
	}

	private void breatheIn(BreathingNode node, TimeComponent time)
	{
		var min = node.BreatheEmptyCircleScale;
		var max = node.BreatheFullCircleScale;
		var t = node.counter / node.BreatheInTime;
		Vector3 scale = Vector3.Lerp(new Vector3(min, min, 1), new Vector3(max, max, 1), t);
		node.IndicatorTransform.transform.localScale = scale;
	}

	private void breatheHold(BreathingNode node, TimeComponent time)
	{

	}

	private void breatheOut(BreathingNode node, TimeComponent time)
	{
		var min = node.BreatheEmptyCircleScale;
		var max = node.BreatheFullCircleScale;
		var t = (node.counter - node.HoldTimeTotal) / node.BreatheOutTime;
		Vector3 scale = Vector3.Lerp(new Vector3(max, max, 1), new Vector3(min, min, 1), t);
		node.IndicatorTransform.transform.localScale = scale;
	}
}
