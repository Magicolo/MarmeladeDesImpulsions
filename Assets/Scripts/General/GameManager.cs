using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class GameManager : MonoBehaviour
{
	public enum States
	{
		Idle,
		Playing,
		Winning,
		Losing,
		Loading
	}

	public float WinDelay = 3f;
	public float LoseDelay = 3f;

	public States CurrentState { get; private set; }
	public Scene CurrentScene { get; private set; }

	AsyncOperation loadingTask;
	float counter;

	public void LoadScene(string scene)
	{
		SwitchState(States.Loading);
		CurrentScene = SceneManager.GetSceneByName(scene);
		loadingTask = SceneManager.LoadSceneAsync(scene);
	}

	void Update()
	{
		switch (CurrentState)
		{
			case States.Playing:
				break;
			case States.Loading:
				if (loadingTask == null || loadingTask.isDone)
					SwitchState(States.Playing);
				break;
			case States.Winning:
				counter -= TimeManager.Game.DeltaTime;

				if (counter <= 0f)
					LoadScene("Main");
				break;
			case States.Losing:
				counter -= TimeManager.Game.DeltaTime;

				if (counter <= 0f)
					ReloadCurrentLevel();
				break;
		}
	}

	void ReloadCurrentLevel()
	{
		LoadScene(CurrentScene.name);
	}

	void SwitchState(States state)
	{
		switch (CurrentState)
		{
			case States.Idle:
				break;
			case States.Playing:
				break;
			case States.Winning:
				break;
			case States.Losing:
				break;
			case States.Loading:
				loadingTask = null;
				break;
		}

		CurrentState = state;

		switch (CurrentState)
		{
			case States.Idle:
				break;
			case States.Playing:
				break;
			case States.Winning:
				counter = WinDelay;
				break;
			case States.Losing:
				counter = LoseDelay;
				break;
			case States.Loading:
				break;
		}
	}
}
