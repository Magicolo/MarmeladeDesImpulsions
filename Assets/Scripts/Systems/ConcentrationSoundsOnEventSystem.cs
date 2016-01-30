using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class ConcentrationSoundsOnEventSystem : SystemBase
{
	[Inject]
	GameManager gameManager = null;
	[Inject]
	IPuzzleLevel level = null;

	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(ConcentrationSoundsOnEventComponent)
		});
	}

	public override void OnActivate()
	{
		base.OnActivate();

		EventManager.SubscribeAll((Action<BehaviourEvents, IEntity>)OnEvent);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.UnsubscribeAll((Action<BehaviourEvents, IEntity>)OnEvent);
	}

	void OnEvent(BehaviourEvents identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var concentrate = entity.GetComponent<ConcentrationSoundsOnEventComponent>();

		if (concentrate.Event.HasAll(identifier))
		{
			for (int i = 0; i < concentrate.Sounds.Length; i++)
				PlaySound(concentrate.Sounds[i]);
		}
	}

	void PlaySound(ConcentrationSoundsOnEventComponent.SoundData sound)
	{
		if (sound == null)
			return;

		var item = AudioManager.CreateItem(sound.Sound);
		var scale = sound.Scale;

		item.SetVolumeScale(scale.Min);
		item.OnUpdate += i =>
		{
			float volume = level.Concentration * (scale.Max - scale.Min) + scale.Min;
			i.SetVolumeScale(Mathf.Clamp01(volume));

			if (gameManager.CurrentState == GameManager.States.Winning)
				i.Stop();
			else if (gameManager.CurrentState != GameManager.States.Playing)
				i.StopImmediate();
		};

		item.Play();
	}
}
