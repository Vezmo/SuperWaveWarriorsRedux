using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : Controller
{
  public enum DirectionalInputType { Null, Left, Right, Down, Up }
  private DirectionalInputType m_currentDirectionalInput;

  public PlayerActions Input;

  private Timer m_menuFirstNavigationCooldownTimer;
  private float m_menuFirstNavigationCooldownDuration = 0.25f;

  private Timer m_menuContinuedNavigationCooldownTimer;
  private float m_menuContinuedNavigationCooldownDuration = 0.05f;

  public bool CanMove = true;
  public Menu CurrentMenu;

  public override void OnStart()
  {
    m_menuFirstNavigationCooldownTimer = new Timer(m_menuFirstNavigationCooldownDuration);
    m_menuFirstNavigationCooldownTimer.OnComplete += MenuFirstNavigationCooldownTimer_OnComplete;

    m_menuContinuedNavigationCooldownTimer = new Timer(m_menuContinuedNavigationCooldownDuration);
    m_menuContinuedNavigationCooldownTimer.OnComplete += MenuContinuedNavigationCooldownTimer_OnComplete;

    Input = InputManager.Instance.GetInputProfile();
  }

  public override void OnUpdate()
  {
    m_menuFirstNavigationCooldownTimer.Loop(Time.deltaTime);
    m_menuContinuedNavigationCooldownTimer.Loop(Time.deltaTime);

    HandleDirectionalInput();
  }

  private void HandleDirectionalInput()
  {
    DirectionalInputType lastDirectionalInput = m_currentDirectionalInput;

    if (InputManager.Instance.InputLeft)
    {
      m_currentDirectionalInput = DirectionalInputType.Left;
    }
    else if (InputManager.Instance.InputRight)
    {
      m_currentDirectionalInput = DirectionalInputType.Right;
    }
    else if (InputManager.Instance.InputDown)
    {
      m_currentDirectionalInput = DirectionalInputType.Down;
    }
    else if (InputManager.Instance.InputUp)
    {
      m_currentDirectionalInput = DirectionalInputType.Up;
    }
    else
    {
      m_currentDirectionalInput = DirectionalInputType.Null;
      m_menuFirstNavigationCooldownTimer.Stop();
      CanMove = true;
    }

    if (CanMove && lastDirectionalInput == DirectionalInputType.Null && lastDirectionalInput != m_currentDirectionalInput)
      Move(m_currentDirectionalInput, true);
    else if (CanMove)
      Move(m_currentDirectionalInput, false);
  }

  private void Move(DirectionalInputType _directionalInput, bool _firstDirectionalInput)
  {
    switch (_directionalInput)
    {
      case DirectionalInputType.Left:
        CurrentMenu.MoveLeft();
        break;

      case DirectionalInputType.Right:
        CurrentMenu.MoveRight();
        break;

      case DirectionalInputType.Down:
        CurrentMenu.MoveDown();
        break;

      case DirectionalInputType.Up:
        CurrentMenu.MoveUp();
        break;

      case DirectionalInputType.Null:

        break;
    }

    if (_directionalInput != DirectionalInputType.Null && _firstDirectionalInput)
    {
      m_menuFirstNavigationCooldownTimer.Start();
      CanMove = false;
    }
    else if (_directionalInput != DirectionalInputType.Null)
    {
      m_menuContinuedNavigationCooldownTimer.Start();
      CanMove = false;
    }
  }

  private void MenuContinuedNavigationCooldownTimer_OnComplete()
  {
    CanMove = true;
  }

  private void MenuFirstNavigationCooldownTimer_OnComplete()
  {
    CanMove = true;
  }
}
