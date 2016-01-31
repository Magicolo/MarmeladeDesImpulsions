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

		if (!node.actif) return;

		if (node.counter <= 0)
			node.Sprite.color = node.SpriteActivated;
		else if (node.counter < node.Activatingtime)
			breatheHold(node, time);
		else if (node.counter < node.BreatheInTimeTotal)
			breatheIn(node, time);
		else if (node.counter < node.HoldTimeTotal)
			breatheHold(node, time);
		else if (node.counter < node.BreatheOutTimeTotal)
			breatheOut(node, time);
		else if (node.counter < node.BreatheOutHoldTimeTotal)
			breatheHold(node, time);
		else
			NextNode(node);

		node.counter += time.DeltaTime;
	}

	private void NextNode(BreathingNode node)
	{
		node.counter = 0;
		if (node.NextNode != null)
		{
			node.Sprite.color = node.SpriteDeactivated;
			node.actif = false;
			node.NextNode.actif = true;
			node.NextNode.Sprite.color = node.SpriteActivated;
		}
	}

	private void breatheIn(BreathingNode node, TimeComponent time)
	{
		var min = node.BreatheEmptyCircleScale;
		var max = node.BreatheFullCircleScale;
		var t = (node.counter - node.Activatingtime) / node.BreatheInTime;
		Debug.Log("a" + t);
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
		Debug.Log(t);
		Vector3 scale = Vector3.Lerp(new Vector3(max, max, 1), new Vector3(min, min, 1), t);
		node.IndicatorTransform.transform.localScale = scale;
	}
}
