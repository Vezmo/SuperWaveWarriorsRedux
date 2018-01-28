using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuElement : MonoBehaviour {


  public MenuElement LeftNeighbor;
  public MenuElement RightNeighbor;
  public MenuElement DownNeighbor;
  public MenuElement UpNeighbor;


  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
  {
		
  }


  public virtual void OnHover()
  {
  }

  public virtual void OnConfirm()
  {
  }



}
