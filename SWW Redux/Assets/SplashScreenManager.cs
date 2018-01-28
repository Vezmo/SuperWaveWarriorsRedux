using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{

  public List<SplashScreen> SplashScreens = new List<SplashScreen>();

  SplashScreen m_firstSplashScreen { get { return SplashScreens[0]; } }

  Timer m_fadeInTimer;
  Timer m_splashScreenTimer;
  Timer m_fadeOutTimer;

  int index = 0;

  // Use this for initialization
  void Start()
  {
    m_fadeInTimer = new Timer();
    m_fadeInTimer.OnComplete += FadeInTimer_OnComplete;

    m_splashScreenTimer = new Timer();
    m_splashScreenTimer.OnComplete += SplashScreenTimer_OnComplete;

    m_fadeOutTimer = new Timer();
    m_fadeOutTimer.OnComplete += FadeOutTimer_OnComplete;

    foreach (SplashScreen splashScreen in SplashScreens)
    {
      if (splashScreen.enabled)
      {
        splashScreen.enabled = false;
      }
    }

    LaunchSplashScreen(SplashScreens[index]);
  }

  private void FadeInTimer_OnComplete()
  {
    m_splashScreenTimer.Start();
  }

  private void SplashScreenTimer_OnComplete()
  {
    m_fadeOutTimer.Start();
    Fader.Instance.FadeOut(Fader.Instance.FadeOutCurve, m_fadeOutTimer);
  }

  private void FadeOutTimer_OnComplete()
  {
    SplashScreens[index].gameObject.SetActive(false);
    index++;

    if (index != SplashScreens.Count)
      LaunchSplashScreen(SplashScreens[index]);
    else
    {
      LastSplashScreenCompleted();
    }

      
  }

  // Update is called once per frame
  void Update()
  {
    m_fadeInTimer.Loop(Time.deltaTime);
    m_splashScreenTimer.Loop(Time.deltaTime);
    m_fadeOutTimer.Loop(Time.deltaTime);
  }

  void LaunchSplashScreen(SplashScreen _splashScreen)
  {
    _splashScreen.gameObject.SetActive(true);

    m_fadeInTimer.TargetTime = _splashScreen.FadeInDuration;
    m_splashScreenTimer.TargetTime = _splashScreen.Duration;
    m_fadeOutTimer.TargetTime = _splashScreen.FadeOutDuration;

    m_fadeInTimer.Start();
    Fader.Instance.FadeIn(Fader.Instance.FadeInCurve, m_fadeInTimer);
  }

  void LastSplashScreenCompleted()
  {

  }
}
