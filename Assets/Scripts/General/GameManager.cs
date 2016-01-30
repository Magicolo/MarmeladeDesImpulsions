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
		Loading,
		Playing,
		Winning,
		Losing,
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

	public void LevelSuccess()
	{
		if (CurrentState == States.Playing)
			SwitchState(States.Winning);
	}

	public void LevelFailure()
	{
		if (CurrentState == States.Playing)
			SwitchState(States.Losing);
	}

	void Awake()
	{
		CurrentScene = SceneManager.GetActiveScene();
	}

	void Update()
	{
		switch (CurrentState)
		{
			case States.Loading:
				if (loadingTask == null || loadingTask.isDone)
					SwitchState(States.Playing);
				break;
			case States.Playing:
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
			case States.Loading:
				loadingTask = null;
				break;
			case States.Playing:
				break;
			case States.Winning:
				break;
			case States.Losing:
				break;
		}

		CurrentState = state;

		switch (CurrentState)
		{
			case States.Loading:
				break;
			case States.Playing:
				break;
			case States.Winning:
				counter = WinDelay;
				break;
			case States.Losing:
				counter = LoseDelay;
				break;
		}
	}
}
