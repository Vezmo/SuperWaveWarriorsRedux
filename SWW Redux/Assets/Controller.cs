using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller
{
  // Use this for initialization
  public void Start()
  {
    OnStart();
  }

  public void Update()
  {
    OnUpdate();
  }

  public abstract void OnStart();

  public abstract void OnUpdate();

}
