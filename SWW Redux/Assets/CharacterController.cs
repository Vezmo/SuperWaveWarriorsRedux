using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Controller {

  private StateMachine<CharacterController> m_stateMachine;

  private CharacterDefaultState m_characterDefaultState;

  public override void OnStart()
  {
    m_characterDefaultState = new CharacterDefaultState(this);
    m_stateMachine = new StateMachine<CharacterController>(m_characterDefaultState);
  }

  public override void OnUpdate()
  {
    if (InputManager.Instance.QueuedInput.Contains(InputManager.Instance.InputProfile.Dash))
    {
      Debug.Log("Dash");
      InputManager.Instance.QueuedInput.Clear();
    }
  }

}
