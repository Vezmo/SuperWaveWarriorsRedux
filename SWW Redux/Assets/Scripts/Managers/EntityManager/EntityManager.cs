using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
  private HashSet<BaseEntity> _entities;

  private static EntityManager m_instance;
  public static EntityManager Instance { get { return m_instance; } }

  private void Awake()
  {
    if (m_instance != null)
    {
      Destroy(gameObject);
      Debug.LogError("Tried to instantiate an EntityManager but one was already existing");
      return;
    };

    _entities = new HashSet<BaseEntity>();
    m_instance = this;
  }

  /// <summary>
  /// Registers an entity to the entity manager
  /// </summary>
  /// <param name="entity"></param>
  public void RegisterEntity(BaseEntity entity)
  {
    _entities.Add(entity);
  }

  /// <summary>
  /// Unregister an entity to the entity manager
  /// </summary>
  /// <param name="entity"></param>
  public void UnregisterEntity(BaseEntity entity)
  {
    _entities.Remove(entity);
  }
}
