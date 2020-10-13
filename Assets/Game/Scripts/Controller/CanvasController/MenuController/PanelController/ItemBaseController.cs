using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemBaseController : MonoBehaviour {

	public virtual void CarregaItens(){}

	public virtual void ConfigButton (){}

	public virtual void SetItemInPanel (string nomeItem){}

	public virtual void actionItem (Transform button){}

	public virtual void actionUsar (Transform button){}

	public virtual void actionDescartar (Transform button){}
}

