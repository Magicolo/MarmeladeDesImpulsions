using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ObjectiveSystem : SystemBase
{
	struct ItemCombo : IEquatable<ItemCombo>
	{
		public ObjectiveItemComponent.Types Item0;
		public ObjectiveItemComponent.Types Item1;
		public ObjectiveItemComponent.Types Item2;

		public ObjectiveItemComponent.Types this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return Item0;
					case 1:
						return Item1;
					case 2:
						return Item2;
					default:
						return ObjectiveItemComponent.Types.None;
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						Item0 = value;
						break;
					case 1:
						Item1 = value;
						break;
					case 2:
						Item2 = value;
						break;
				}
			}
		}

		public int Similarity(ItemCombo other)
		{
			int score = 0;

			if (Item0 == other.Item0)
				score += 2;
			else if (other.Contains(Item0))
				score += 1;

			if (Item1 == other.Item1)
				score += 2;
			else if (other.Contains(Item1))
				score += 1;

			if (Item2 == other.Item2)
				score += 2;
			else if (other.Contains(Item2))
				score += 1;

			return score;
		}

		public bool Contains(ObjectiveItemComponent.Types item)
		{
			return
				Item0 == item ||
				Item1 == item ||
				Item2 == item;
		}

		public override int GetHashCode()
		{
			return
				Item0.GetHashCode() ^
				Item1.GetHashCode() ^
				Item2.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ItemCombo))
				return false;

			return Equals((ItemCombo)obj);
		}

		public bool Equals(ItemCombo other)
		{
			return
				Item0 == other.Item0 &&
				Item1 == other.Item1 &&
				Item2 == other.Item2;
		}

		public override string ToString()
		{
			return string.Format("{0}({1}, {2}, {3})", GetType().Name, Item0, Item1, Item2);
		}
	}

	static readonly Dictionary<int, string> similarityToResult = new Dictionary<int, string>
	{
		{ 0, "What? Nothing! Errrrrr." },
		{ 1, "Oh... hmmm... I hope nobody was watching that." },
		{ 2, "No! That's not right! Hrm." },
		{ 3, "I thought that was going to be it, but clearly I'm totaly wrong." },
		{ 4, "I can tell I'm on the right track but not in the right direction... if you know what I mean." },
		{ 5, "Ooohhh I'm sooo close I can FEEL IT! Ahhh!" },
		{ 6, "Ahhh rain... sweet rain... My father would be proud." },
	};

	static readonly ItemCombo PerfectCombo = new ItemCombo
	{
		Item0 = ObjectiveItemComponent.Types.CrystalGreen,
		Item1 = ObjectiveItemComponent.Types.Encens,
		Item2 = ObjectiveItemComponent.Types.PlumeGroupe
	};

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

		EventManager.Subscribe(UIEvents.OnPointerClick, (Action<IEntity>)OnPointerClick);
		EventManager.Subscribe(UIEvents.OnDrag, (Action<IEntity>)OnDrag);
		UpdateButtons();
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.Unsubscribe(UIEvents.OnPointerClick, (Action<IEntity>)OnPointerClick);
		EventManager.Unsubscribe(UIEvents.OnDrag, (Action<IEntity>)OnDrag);
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
				if (recipient.Item == null)
				{
					recipient.Item = item;
					draggable.Locked = true;
					draggable.CachedTransform.SetPosition(recipient.CachedTransform.position, Axes.XY);
					break;
				}
			}
			else if (recipient.Item == item)
			{
				recipient.Item = null;
				draggable.Locked = false;
				break;
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

		var combo = new ItemCombo();

		for (int i = 0; i < recipients.Count; i++)
		{
			var recipient = recipients[i].GetComponent<ObjectiveRecipientComponent>();
			combo[i] = recipient.Item.Type;
		}

		ValidateItems(combo, entity);
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
		if (recipients.Count == 0)
			return false;

		bool filled = true;

		for (int i = 0; i < recipients.Count; i++)
		{
			var recipient = recipients[i].GetComponent<ObjectiveRecipientComponent>();

			filled &= recipient.Item != null;
		}

		return filled;
	}

	void ValidateItems(ItemCombo combo, IEntity entity)
	{
		int similarity = PerfectCombo.Similarity(combo);
		string result = similarityToResult[similarity];

		var button = entity.GetComponent<ObjectiveButtonComponent>();
		button.Result.text = result;

		switch (similarity)
		{
			case 0:
				EventManager.Trigger(Events.End_0, entity);
				break;
			case 1:
				EventManager.Trigger(Events.End_1, entity);
				break;
			case 2:
				EventManager.Trigger(Events.End_2, entity);
				break;
			case 3:
				EventManager.Trigger(Events.End_3, entity);
				break;
			case 4:
				EventManager.Trigger(Events.End_4, entity);
				break;
			case 5:
				EventManager.Trigger(Events.End_5, entity);
				break;
			case 6:
				button.Rain.gameObject.SetActive(true);
				button.Rain.Play(true);
				EventManager.Trigger(Events.End_6, entity);
				button.StartCoroutine(TriggerAfterDelay(entity));
				break;
		}
	}

	IEnumerator TriggerAfterDelay(IEntity entity)
	{
		for (float counter = 0; counter < 20f; counter += TimeManager.Game.DeltaTime)
			yield return null;

		EventManager.Trigger(Events.Win, entity);
	}
}
