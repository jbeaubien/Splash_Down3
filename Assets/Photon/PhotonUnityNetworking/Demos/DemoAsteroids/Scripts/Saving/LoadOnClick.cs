using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour
{
   

    public void ButtonClicked()
    {
    GameObject.Find("DoNotDestroyGO").GetComponent<Save>().currentName = this.GetComponentInChildren<Text>().text;
    GameObject.Find("DoNotDestroyGO").GetComponent<Save>().LoadFile();

    }
}
