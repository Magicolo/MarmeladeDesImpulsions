using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ObjectiveSystem : SystemBase
{
	IEntityGroup recipients;
	IEntityGroup buttons;

	public override IEntityGroup GetEntities()
	{
		recipients = EntityManager.Entities.Filter(new[]
		{
			typeof(ObjectiveRecipientComponent)
		});

		buttons = EntityManager.Entities.Filter(typeof(ObjectiveButtonComponent));

		return EntityManager.Entities.Filter(new[]
		{
			typeof(DraggableComponent),
			typeof(ObjectiveItemComponent)
		});
	}

	public override void OnActivate()
	{
		base.OnActivate();

		EventManager.Subscribe(UIEvents.OnBeginDrag, (Action<IEntity>)OnBeginDrag);
		EventManager.Subscribe(UIEvents.OnPointerClick, (Action<IEntity>)OnPointerClick);
		EventManager.Subscribe(UIEvents.OnDrag, (Action<IEntity>)OnDrag);
		UpdateButtons();
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.Unsubscribe(UIEvents.OnBeginDrag, (Action<IEntity>)OnBeginDrag);
		EventManager.Unsubscribe(UIEvents.OnPointerClick, (Action<IEntity>)OnPointerClick);
		EventManager.Unsubscribe(UIEvents.OnDrag, (Action<IEntity>)OnDrag);
	}

	void OnBeginDrag(IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var item = entity.GetComponent<ObjectiveItemComponent>();

		for (int i = 0; i < recipients.Count; i++)
		{
			var recipient = recipients[i].GetComponent<ObjectiveRecipientComponent>();

			if (recipient.Item == item)
				recipient.Item = null;
		}
	}

	void OnDrag(IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var item = entity.GetComponent<ObjectiveItemComponent>();
		var draggable = entity.GetComponent<DraggableComponent>();

		for (int i = 0; i < recipients.Count; i++)
		{
			var recipient = recipients[i].GetComponent<ObjectiveRecipientComponent>();
			float distance = Vector3.Distance(recipient.CachedTransform.position, Camera.main.GetMouseWorldPosition());

			if (distance < 0.5f)
			{
				recipient.Item = item;
				draggable.Locked = true;
				draggable.CachedTransform.position = recipient.CachedTransform.position;
				break;
			}
			else if (recipient.Item == item)
			{
				recipient.Item = null;
				draggable.Locked = false;
			}
		}

		UpdateButtons();
	}

	void OnPointerClick(IEntity entity)
	{
		if (!buttons.Contains(entity))
			return;

		bool filled = RecipientsAreFilled();

		if (!filled)
			return;

		var items = new List<ObjectiveItemComponent>();

		for (int i = 0; i < recipients.Count; i++)
		{
			var recipient = recipients[i].GetComponent<ObjectiveRecipientComponent>();
			items.Add(recipient.Item);
		}

		ValidateItems(items);
	}

	void UpdateButtons()
	{
		bool filled = RecipientsAreFilled();

		for (int i = 0; i < buttons.Count; i++)
		{
			var button = buttons[i].GetComponent<ObjectiveButtonComponent>();
			button.Button.interactable = filled;
		}
	}

	bool RecipientsAreFilled()
	{
		bool filled = true;

		for (int i = 0; i < recipients.Count; i++)
		{
			var recipient = recipients[i].GetComponent<ObjectiveRecipientComponent>();

			filled &= recipient.Item != null;
		}

		return filled;
	}

	void ValidateItems(List<ObjectiveItemComponent> items)
	{
		PDebug.LogMethod(items);
	}
}
