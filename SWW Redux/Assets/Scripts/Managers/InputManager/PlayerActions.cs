using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerActions : PlayerActionSet {

  public PlayerAction Left;
  public PlayerAction Right;
  public PlayerOneAxisAction Horizontal;

  public PlayerAction Down;
  public PlayerAction Up;
  public PlayerOneAxisAction Vertical;

  public PlayerAction Shoot;
  public PlayerAction Special;
  public PlayerAction Dash;

  public PlayerAction Confirm;
  public PlayerAction Back;

  public PlayerAction Pause;

  public PlayerActions()
  {
    Left = CreatePlayerAction("Left");
    Right = CreatePlayerAction("Right");
    Horizontal = CreateOneAxisPlayerAction(Left, Right);

    Down = CreatePlayerAction("Down");
    Up = CreatePlayerAction("Up");
    Vertical = CreateOneAxisPlayerAction(Down, Up);

    Shoot = CreatePlayerAction("Shoot");
    Special = CreatePlayerAction("Special");
    Dash = CreatePlayerAction("Dash");

    Confirm = CreatePlayerAction("Confirm");
    Back = CreatePlayerAction("Back");

    Pause = CreatePlayerAction("Pause");
  }
}
