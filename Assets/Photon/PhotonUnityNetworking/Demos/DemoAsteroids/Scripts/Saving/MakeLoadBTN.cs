using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class MakeLoadBTN : MonoBehaviour
{
    public GameObject LoadBTN;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        createLoadButton();
    }
    void OnDisable()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

   

    public void createLoadButton()
    {
        string[] files = System.IO.Directory.GetFiles("C:/Users/Public/");
        foreach (string file in files)
        {

            //Do work on the files here
            if (file.Contains(".dat"))
            {
                (Instantiate(LoadBTN, transform.position, transform.rotation) as GameObject).transform.parent = this.transform;
                LoadBTN.GetComponentInChildren<Text>().text = file;
                LoadBTN.GetComponentInChildren<Text>().text = LoadBTN.GetComponentInChildren<Text>().text.Replace("C:/Users/Public/", "");
                LoadBTN.GetComponentInChildren<Text>().text = LoadBTN.GetComponentInChildren<Text>().text.Replace("save.dat", "");
            }

        }
    }
}
