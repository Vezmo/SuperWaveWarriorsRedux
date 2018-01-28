using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputManager : MonoBehaviour
{
  public const float DEADZONE_HORIZONTAL = 0.4f;
  public const float DEADZONE_VERTICAL = 0.4f;
  private const string INPUT_PROFILE = "InputProfile";

  private List<PlayerAction> m_inputsToQueue;

  private static InputManager m_instance;
  public static InputManager Instance { get { return m_instance; } }

  public Controller CurrentController;

  public float InputLifetime;
  public PlayerActions InputProfile;
  private PlayerActions m_defaultInputProfile;

  private Timer m_inputTimer;

  public List<PlayerAction> QueuedInput = new List<PlayerAction>();

  public bool InputLeft { get { return Mathf.Abs(InputProfile.Horizontal) > DEADZONE_HORIZONTAL && InputProfile.Horizontal < 0; } }
  public bool InputRight { get { return Mathf.Abs(InputProfile.Horizontal) > DEADZONE_HORIZONTAL && InputProfile.Horizontal > 0; } }
  public bool InputDown { get { return Mathf.Abs(InputProfile.Vertical) > DEADZONE_VERTICAL && InputProfile.Vertical < 0; } }
  public bool InputUp { get { return Mathf.Abs(InputProfile.Vertical) > DEADZONE_VERTICAL && InputProfile.Vertical > 0; } }

  private void Awake()
  {
    if (m_instance != null)
    {
      Destroy(gameObject);
      Debug.LogWarning("Tried to instantiate an InputManager but one was already existing");
      return;
    }

    m_instance = this;

    //Initialize InputProfile
    InputProfile = new PlayerActions();
    if (PlayerPrefs.HasKey(INPUT_PROFILE))
    {
      LoadBindings();
    }
    else
    {
      BindDefaultActions();
      SaveBindings();
    }

    m_inputsToQueue = new List<PlayerAction>();
    m_inputsToQueue.Add(InputProfile.Dash);
    m_inputsToQueue.Add(InputProfile.Shoot);

    m_inputTimer = new Timer(InputLifetime);
    m_inputTimer.OnComplete += InputTimer_OnComplete;
  }

  private void Start()
  {
    CurrentController = GameMaster.Instance.CharacterController;
  }
  private void InputTimer_OnComplete()
  {
    QueuedInput.Clear();
  }

  private void Update()
  {
    CurrentController.Update();

    if (InputProfile.Pause.WasPressed)
    {
      PauseSystem.TogglePause(this);
    }

    foreach (PlayerAction action in m_inputsToQueue)
    {
      HandleInputLifetime(action);
    }

    m_inputTimer.Loop(Time.deltaTime);
  }

  public PlayerActions GetInputProfile()
  {
    return InputProfile;
  }

  private void HandleInputLifetime(PlayerAction _playerAction)
  {
    if (_playerAction.WasPressed)
    {
      ManageQueueAndTimer(_playerAction);
    }
  }

  void ManageQueueAndTimer(PlayerAction _actionPressed)
  {
    if (m_inputTimer.timerState == Timer.TimerState.Playing)
    {
      m_inputTimer.Reset();
    }
    else
    {
      m_inputTimer.Start();
    }

    QueuedInput.Clear();
    QueuedInput.Add(_actionPressed);
  }

  public void ConsumeAction()
  {
    QueuedInput.Clear();
  }

  private void SaveBindings()
  {
    PlayerPrefs.SetString(INPUT_PROFILE, InputProfile.Save());
  }

  private void LoadBindings()
  {
    InputProfile.Load(PlayerPrefs.GetString(INPUT_PROFILE));
  }

  private void BindDefaultActions()
  {
    if (m_defaultInputProfile == null)
    {
      m_defaultInputProfile = new PlayerActions();

      m_defaultInputProfile.Left.AddDefaultBinding(Key.LeftArrow);
      m_defaultInputProfile.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
      m_defaultInputProfile.Left.AddDefaultBinding(InputControlType.DPadLeft);

      m_defaultInputProfile.Right.AddDefaultBinding(Key.RightArrow);
      m_defaultInputProfile.Right.AddDefaultBinding(InputControlType.LeftStickRight);
      m_defaultInputProfile.Right.AddDefaultBinding(InputControlType.DPadRight);

      m_defaultInputProfile.Down.AddDefaultBinding(Key.DownArrow);
      m_defaultInputProfile.Down.AddDefaultBinding(InputControlType.LeftStickDown);
      m_defaultInputProfile.Down.AddDefaultBinding(InputControlType.DPadDown);

      m_defaultInputProfile.Up.AddDefaultBinding(Key.UpArrow);
      m_defaultInputProfile.Up.AddDefaultBinding(InputControlType.LeftStickUp);
      m_defaultInputProfile.Up.AddDefaultBinding(InputControlType.DPadUp);

      m_defaultInputProfile.Dash.AddDefaultBinding(Key.S);
      m_defaultInputProfile.Dash.AddDefaultBinding(InputControlType.Action1);

      m_defaultInputProfile.Shoot.AddDefaultBinding(Key.D);
      m_defaultInputProfile.Shoot.AddDefaultBinding(InputControlType.Action3);

      m_defaultInputProfile.Confirm.AddDefaultBinding(Key.S);
      m_defaultInputProfile.Confirm.AddDefaultBinding(InputControlType.Action1);

      m_defaultInputProfile.Back.AddDefaultBinding(Key.D);
      m_defaultInputProfile.Back.AddDefaultBinding(InputControlType.Action3);

      m_defaultInputProfile.Pause.AddDefaultBinding(Key.Escape);
      m_defaultInputProfile.Pause.AddDefaultBinding(InputControlType.Command);

    }
    InputProfile = m_defaultInputProfile;
  }

  public void TransitionToMenuController()
  {
    CurrentController = GameMaster.Instance.MenuController;
  }

  public void TransitionToCharacterController()
  {
    CurrentController = GameMaster.Instance.CharacterController;
  }
}
