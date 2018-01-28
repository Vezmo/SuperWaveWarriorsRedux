using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseSystem
{
  private static List<Object> m_pauseRequests = new List<Object>();

  public static bool IsPaused { get { return m_pauseRequests.Count > 0; } }


  public static void TogglePause(Object _requester)
  {
    if (IsPaused)
    {
      RequestResume(_requester);
    }
    else
    {
      RequestPause(_requester);
    }
  }

  public static void RequestPause(Object _requester)
  {
    m_pauseRequests.Add(_requester);
  }

  public static void RequestResume(Object _requester)
  {
    if (m_pauseRequests.Contains(_requester))
    {
      m_pauseRequests.Remove(_requester);
    }
  }

}
