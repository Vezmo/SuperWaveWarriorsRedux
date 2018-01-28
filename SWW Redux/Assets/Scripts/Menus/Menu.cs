using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

  public List<MenuElement> MenuElements = new List<MenuElement>();

  public MenuElement HoveredElement { get { return m_hoveredElement; } }
  private MenuElement m_hoveredElement;
  public MenuElement StartingHoveredElement;

  // Use this for initialization
  void Start()
  {
    m_hoveredElement = StartingHoveredElement;
  }

  // Update is called once per frame
  void Update()
  {
    if (m_hoveredElement != null)
    {
      m_hoveredElement.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    foreach (MenuElement mE in MenuElements)
    {
      if (mE != m_hoveredElement)
      {
        mE.GetComponent<SpriteRenderer>().color = Color.white;
      }
    }
  }

  public void MoveLeft()
  {
    if (m_hoveredElement.LeftNeighbor != null)
    {
      m_hoveredElement = m_hoveredElement.LeftNeighbor;
    }
  }

  public void MoveRight()
  {
    if (m_hoveredElement.RightNeighbor != null)
    {
      m_hoveredElement = m_hoveredElement.RightNeighbor;
    }
  }

  public void MoveDown()
  {
    if (m_hoveredElement.DownNeighbor != null)
    {
      m_hoveredElement = m_hoveredElement.DownNeighbor;
    }
  }

  public void MoveUp()
  {
    if (m_hoveredElement.UpNeighbor != null)
    {
      m_hoveredElement = m_hoveredElement.UpNeighbor;
    }
  }

  public void Confirm()
  {
    if (m_hoveredElement != null)
    {
      m_hoveredElement.OnConfirm();
    }
  }

  public virtual void OnBack()
  {

  }
}
