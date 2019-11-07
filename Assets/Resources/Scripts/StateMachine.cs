using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IStateMachine { }

[SerializeField]
public class StateMachine<TState> : IStateMachine where TState : struct
{
	public TState currentState { get; private set; }
	public TState prevState { get; private set; }

	public void ChangeState(TState state)
	{
		prevState = currentState;
		currentState = state;
	}

	public void ChangePrevState()
	{
		TState prev = currentState;
		currentState = prevState;
		prevState = prev;
	}

	public void Dispose()
	{
		Debug.Log("StateMachine Disposed !");
	}

	public StateMachine()
	{
	}

}
