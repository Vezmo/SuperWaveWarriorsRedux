using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fader : MonoBehaviour
{

  public static Fader Instance { get { return m_instance; } }
  static Fader m_instance;

  public enum FadeState
  {
    Clear = 0,
    FadingOut = 5,
    Dark = 10,
    FadingIn = 15
  }

  Timer FadeOutTimer;
  Timer FadeInTimer;

  public Image FadeTexture;

  float m_currentAlpha;

  public FadeState StartingFadeState;
  FadeState m_currentFadeState;
  Coroutine m_currentCoroutine;

  [Header("Fade Out")]
  public float FadeOutDuration;
  public AnimationCurve FadeOutCurve;

  [Header("Fade In")]
  public float FadeInDuration;
  public AnimationCurve FadeInCurve;

  void Awake()
  {
    if (m_instance != null)
    {
      Destroy(gameObject);
      Debug.LogWarning("Tried to instantiate a Fader but one was already existing");
      return;
    }

    m_instance = this;

  }

  void Start()
  {
    m_currentFadeState = StartingFadeState;

    //Disables or enables FadeTexture depending on StartingState
    //HandleFadeTextureInit();

    //Create Timers and Subscribe
    InitializeTimers();
  }

  void Update()
  {
    if (PauseSystem.IsPaused)
    {
      return;
    }

    FadeTexture.color = new Color(FadeTexture.color.r, FadeTexture.color.g, FadeTexture.color.b, m_currentAlpha);
  }

  void InitializeTimers()
  {
    FadeInTimer = new Timer(FadeInDuration);
    FadeOutTimer = new Timer(FadeOutDuration);

    FadeInTimer.OnComplete += FadeInTimer_OnComplete;
    FadeOutTimer.OnComplete += FadeOutTimer_OnComplete;
  }

  public void FadeOut()
  {
    FadeOutTimer.Start();

    m_currentAlpha = 0;
    m_currentFadeState = FadeState.FadingOut;
    FadeTexture.gameObject.SetActive(true);
    m_currentCoroutine = StartCoroutine(Fade(m_currentAlpha, 1, FadeOutCurve, FadeOutTimer));
  }

  public void FadeOut(AnimationCurve _fadeOutCurve, Timer _fadeOutTimer)
  {
    if (!FadeTexture.gameObject.activeSelf)
    {
      FadeTexture.gameObject.SetActive(true);
      m_currentAlpha = 0;
      m_currentFadeState = FadeState.FadingOut;
    }

    FadeOutTimer.Start();
    m_currentCoroutine = StartCoroutine(Fade(m_currentAlpha, 1, _fadeOutCurve, _fadeOutTimer));
  }

  public void FadeIn()
  {
    FadeIn(FadeInCurve, FadeInTimer);
  }

  public void FadeIn(AnimationCurve _fadeInCurve, Timer _fadeInTimer)
  {
    if (!FadeTexture.gameObject.activeSelf)
    {
      FadeTexture.gameObject.SetActive(true);
      m_currentAlpha = 1;
      m_currentFadeState = FadeState.FadingIn;
    }

    FadeInTimer.Start();
    StartCoroutine(Fade(m_currentAlpha, 0, _fadeInCurve, _fadeInTimer));
  }

  void FadeInTimer_OnComplete()
  {
    m_currentFadeState = FadeState.Clear;
    m_currentAlpha = 0;
    FadeTexture.gameObject.SetActive(false);
    StopCoroutine(m_currentCoroutine);
  }

  void FadeOutTimer_OnComplete()
  {
    m_currentFadeState = FadeState.Dark;
    m_currentAlpha = 1;
    StopCoroutine(m_currentCoroutine);
  }

  IEnumerator Fade(float _currentAlpha, float _targetAlpha, AnimationCurve _fadeCurve, Timer _fadeTimer)
  {
    while (_fadeTimer.timerState == Timer.TimerState.Playing)
    {
      if (!PauseSystem.IsPaused)
      {
        m_currentAlpha = Mathf.Lerp(_currentAlpha, _targetAlpha, _fadeCurve.Evaluate(_fadeTimer.GetNormalizedTime()));
        _fadeTimer.Loop(Time.deltaTime);
      }
      yield return null;
    }
  }

  void HandleFadeTextureInit()
  {
    if (m_currentFadeState == FadeState.Clear && FadeTexture.gameObject.activeSelf)
    {
      FadeTexture.gameObject.SetActive(false);
    }

    if (m_currentFadeState == FadeState.Dark && !FadeTexture.gameObject.activeSelf)
    {
      FadeTexture.gameObject.SetActive(true);
      m_currentAlpha = 1;
    }
  }

  public void HandleTestFade()
  {
    switch (m_currentFadeState)
    {
      case FadeState.Dark:
        FadeIn();
        break;

      case FadeState.Clear:
        FadeOut();
        break;
    }
  }

  void OnDestroy()
  {
    FadeInTimer.OnComplete -= FadeInTimer_OnComplete;
    FadeOutTimer.OnComplete -= FadeOutTimer_OnComplete;
  }

  public void Destroy()
  {
    OnDestroy();
  }
}
