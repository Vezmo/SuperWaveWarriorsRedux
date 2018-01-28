using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
  private static GameMaster m_instance;
  public static GameMaster Instance { get { return m_instance; } }

  public MenuController MenuController;
  public CharacterController CharacterController;

  private void Awake()
  {
    if (m_instance != null)
    {
      Destroy(gameObject);
      Debug.LogWarning("Tried to instantiate a GameMaster but one was already existing");
      return;
    }

    m_instance = this;

    MenuController = new MenuController();
    CharacterController = new CharacterController();

  }

  void Start()
  {

  }

  void Update()
  {

  }

  public void SetMenu(Menu _menu)
  {
    MenuController.CurrentMenu = _menu;
  }
}
