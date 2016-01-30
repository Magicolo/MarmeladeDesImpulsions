using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TriggerEventOnInputSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(void)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var trigger = entity.GetComponent<TriggerEventOnInputComponent>();
		bool shouldTrigger = false;

		switch (trigger.InputType)
		{
			case TriggerEventOnInputComponent.InputTypes.Pressed:
				shouldTrigger = InputManager.GetKey(trigger.Player, trigger.InputAction);
				break;
			case TriggerEventOnInputComponent.InputTypes.Down:
				shouldTrigger = InputManager.GetKeyDown(trigger.Player, trigger.InputAction);
				break;
			case TriggerEventOnInputComponent.InputTypes.Up:
				shouldTrigger = InputManager.GetKeyUp(trigger.Player, trigger.InputAction);
				break;
		}

		if (shouldTrigger)
			EventManager.Trigger(trigger.Event, entity);
	}
}
