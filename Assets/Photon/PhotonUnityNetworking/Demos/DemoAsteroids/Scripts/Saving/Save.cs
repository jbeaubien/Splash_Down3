using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour
{

    int currentScore = 0;
    public string currentName;
    private string currentFileName;
    float currentTimePlayed = 5.0f;
    public InputField PlayerNameInput;
    public Text Char_txt;
    public GameObject ContinueBTN;
    public GameObject ToLoadBTN;
    public string currentCharacter;


    void Start()
    {
        // SaveFile();
        //  LoadFile();
        //  Char_txt.text = currentName;
        DontDestroyOnLoad(this.gameObject);
        UpdateLoadButton();
    }

    void Update()
    {
        if (currentName == "")
        {
            ContinueBTN.SetActive(false);
        }
        if (currentName != "")
        {
            ContinueBTN.SetActive(true);
        }

    }

    public void updateCurrentName()
    {
        Char_txt.text = currentName;
    }

    public void clearTxtInput()
    {
        PlayerNameInput.text = "";
    }

    public void SaveFile()
    {
        currentName = PlayerNameInput.text;
        string destination = "C:/Users/Public/" + currentName + "save.dat";
        FileStream file;
        Debug.Log("File saved");
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(currentScore, currentName, currentTimePlayed);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

        Char_txt.text = currentName;
        UpdateLoadButton();
    }

    public void LoadFile()
    {

        
        string destination = "C:/Users/Public//" + currentName + "save.dat";
        FileStream file;
        Debug.Log("File loaded");
        Debug.Log(currentName);

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        currentScore = data.score;
        data.name = currentName;
        currentTimePlayed = data.timePlayed;

        //currentName = PlayerNameInput.text;
        Char_txt.text = currentName;
        Debug.Log(data.name);
        Debug.Log(data.score);
        Debug.Log(data.timePlayed);

       // GameObject.Find("startPanel").SetActive(true);
       // GameObject.Find("LoadUserPanel").SetActive(false);
    }

    public void loadSalmon()
    {
        currentCharacter = "Spaceship_salmon";
    }
    public void loadBass()
    {
        currentCharacter = "Spaceship";
    }

    public void UpdateLoadButton()
    {
        string[] files = System.IO.Directory.GetFiles("C:/Users/Public/");
        foreach (string file in files)
        {

            //Do work on the files here
            if (file.Contains(".dat"))
            {
                ToLoadBTN.SetActive(true);
            }

        }
    }

}