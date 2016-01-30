using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class DraggableSystem : SystemBase
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(DraggableComponent)
		});
	}

	public override void OnActivate()
	{
		base.OnActivate();

		EventManager.Subscribe(UIEvents.OnBeginDrag, (Action<IEntity>)OnBeginDrag);
		EventManager.Subscribe(UIEvents.OnEndDrag, (Action<IEntity>)OnEndDrag);
		EventManager.Subscribe(UIEvents.OnDrag, (Action<IEntity>)OnDrag);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.Unsubscribe(UIEvents.OnBeginDrag, (Action<IEntity>)OnBeginDrag);
		EventManager.Unsubscribe(UIEvents.OnEndDrag, (Action<IEntity>)OnEndDrag);
		EventManager.Unsubscribe(UIEvents.OnDrag, (Action<IEntity>)OnDrag);
	}

	void OnBeginDrag(IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var draggable = entity.GetComponent<DraggableComponent>();
		draggable.Locked = false;
		draggable.Dragging = true;
	}

	void OnEndDrag(IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var draggable = entity.GetComponent<DraggableComponent>();
		draggable.Dragging = false;
	}

	void OnDrag(IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var draggable = entity.GetComponent<DraggableComponent>();

		if (!draggable.Dragging || draggable.Locked)
			return;

		draggable.CachedTransform.position = Camera.main.GetMouseWorldPosition();
	}
}
