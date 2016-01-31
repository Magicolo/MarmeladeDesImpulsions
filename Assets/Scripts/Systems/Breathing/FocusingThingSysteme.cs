using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FocusingThingSysteme : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(FocusingThingComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var focus = entity.GetComponent<FocusingThingComponent>();
		var breathing = entity.GetComponent<BreathingNode>();
		var time = entity.GetComponent<TimeComponent>();

		var player = EntityManager.Entities.Filter(typeof(PlayerConsentrationComponent))[0].GetComponent<PlayerConsentrationComponent>();
		var focusCircle = focus.zone.WorldCircle;

		if (focusCircle.Contains(Camera.main.GetMouseWorldPosition()) && breathing.actif)
		{
			focus.Sprite.color = focus.FocusedColor;
			handleFocus(focus, breathing, time);
		}
		else
		{
			focus.Sprite.color = focus.UnfocusedColor;
			focus.Sprite.transform.SetScale(new Vector3(1, 1, 1));
		}

		if (breathing.actif && breathing.counter > breathing.Activatingtime)
		{
			var scaleFocus = focus.Sprite.transform.localScale.x;
			var scaleBreathing = breathing.IndicatorTransform.localScale.x;

			if (Mathf.Abs(scaleFocus - scaleBreathing) >= focus.precisionNeeded)
				player.Consentration -= focus.damage * time.DeltaTime;
			else
				player.Consentration += focus.regen * time.DeltaTime;

		}
	}

	private void handleFocus(FocusingThingComponent focus, BreathingNode breathing, TimeComponent time)
	{
		float scale = focus.Sprite.transform.localScale.x;
		float breathingScale = breathing.Sprite.transform.localScale.x;
		float diff = Mathf.Abs(breathingScale - scale);
		float growthSpeed = Mathf.Max(3.5f, diff * 2);

		float growth = growthSpeed * time.DeltaTime;

		if (Input.GetMouseButton(0))
			focus.Sprite.transform.SetScale(new Vector3(scale + growth, scale + growth, 1));
		else if (Input.GetMouseButton(1))
			focus.Sprite.transform.SetScale(new Vector3(scale - growth, scale - growth, 1));
	}
}
