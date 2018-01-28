using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T>
{
  protected T Target;

  public BaseState(T _target)
  {
    Target = _target;

  }

  /// <summary>
  /// What's executed upon entering the state
  /// </summary>
  public abstract void OnEnter();

  /// <summary>
  /// What's executed inside of the state
  /// </summary>
  public abstract void OnUpdate();

  /// <summary>
  /// What's executed upon leaving the state
  /// </summary>
  public abstract void OnExit();

}
