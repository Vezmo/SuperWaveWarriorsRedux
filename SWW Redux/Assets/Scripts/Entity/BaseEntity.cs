using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
  private void Awake()
  {
    EntityManager.Instance.RegisterEntity(this);
    OnAwake();
  }

  private void Start()
  {
    OnStart();
  }

  private void Update()
  {
    if (IsPausable && PauseSystem.IsPaused) return;
    OnUpdate();
  }


  protected abstract void OnAwake();

  /// <summary>
  /// Method called when method `Start` from Unity is called.
  /// </summary>
  protected abstract void OnStart();

  /// <summary>
  /// Method called when method `Update` from Unity is called.
  /// </summary>
  protected abstract void OnUpdate();

  /// <summary>
  /// Sets if the current entity is affected by pause
  /// </summary>
  protected virtual bool IsPausable
  {
    get
    {
      return true;
    }
  }

  public virtual void OnActivate(Vector2 _position)
  {
  }

  public virtual void OnDeactivate()
  {
  }

  private void OnDestroy()
  {
    EntityManager.Instance.UnregisterEntity(this);
  }
}

