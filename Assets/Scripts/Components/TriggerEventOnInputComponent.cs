using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TriggerEventOnInputComponent : ComponentBehaviour
{
	public enum InputTypes
	{
		Pressed,
		Down,
		Up
	}

	public InputManager.Players Player;
	public string InputAction;
	public InputTypes InputType = InputTypes.Down;
	public Events Event;
}