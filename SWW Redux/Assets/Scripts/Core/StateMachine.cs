using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
  private BaseState<T> m_currentState;
  private BaseState<T> m_transitionState;
  private bool m_isNewState;

  public StateMachine(BaseState<T> m_startingState)
  {
    m_currentState = m_startingState;
    m_currentState.OnEnter();
  }

  public void Update()
  {
    if (m_isNewState)
    {
      m_currentState.OnEnter();
      m_isNewState = false;
    }
    m_currentState.OnUpdate();
    ValidateNewState();
  }

  public void SwitchState(BaseState<T> _newState)
  {
    m_transitionState = _newState;
  }

  private void ValidateNewState()
  {
    if (m_transitionState != null)
    {
      m_currentState.OnExit();
      m_currentState = m_transitionState;
      m_transitionState = null;
      m_isNewState = true;
    }
  }
}
