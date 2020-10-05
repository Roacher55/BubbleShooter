using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject YourScore;
    public GameObject buttonNick;
    public GameObject inputNick;
    public GameObject text;
    public GameObject previousScore;
    bool save = true;
    
    void Start()
    {
        Application.targetFrameRate = 30;
      //  DataDatabase();

        YourScore.GetComponent<TextMesh>().text = "Liczba twoich strzałów: " + GameHandler.numberOfShoot;
        previousScore.GetComponent<TextMesh>().text = "Poprzedni Gracz " + PlayerPrefs.GetString("PoprzedniNick") + " " + PlayerPrefs.GetInt("PoprzedniStrzaly") + " strzały";


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Gra");
        }

        buttonNick.GetComponent<Button>().onClick.AddListener(SaveData);

     //   ReadGraczeData();
    }
   void ReadGraczeData()
    {
       //  Gamer gamers = JsonUtility.FromJson<Gamer>("Assets/Json" + "/Gracze.json");

        
          //  Debug.Log("Nick " + gamers.nick + " " + gamers.score);
        
    }
    void SaveData()
    {
        inputNick.SetActive(false);
        buttonNick.SetActive(false);
        if(save == true)
        { 
        PlayerPrefs.SetString("PoprzedniNick", text.GetComponent<Text>().text);
        PlayerPrefs.SetInt("PoprzedniStrzaly", GameHandler.numberOfShoot);
     //   Debug.Log(PlayerPrefs.GetString("Poprzedni"));
            save = false;
        }

        /*
         Gamer g = new Gamer();
        g.nick = text.GetComponent<Text>().text;
        g.score = GameHandler.numberOfShoot;

        if(save == true)
        { 
        string jsonNick = JsonUtility.ToJson(g);
        System.IO.File.AppendAllText("Assets/Json" + "/Gracze.json", jsonNick);
            save = false;
        }
        */
    }
    /*
    void DataDatabase()
    {
        string con = "URI=file:" + Application.dataPath + "Plugins/NicksScores.s3db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(con);
        dbconn.Open();
        IDbCommand dbcm = dbconn.CreateCommand();
        string sqlQuerry = "Select * From Scores;";
        dbcm.CommandText = sqlQuerry;
        IDataReader reader = dbcm.ExecuteReader();
        while(reader.Read())
        {
            int id = reader.GetInt32(0);
            string nick = reader.GetString(1);
            int score = reader.GetInt32(2);
            Debug.Log("Id: " + id + " Nick: " + nick + "  Score: " + score);
        }
        reader.Close();
        reader = null;
        dbcm.Dispose();
        dbcm = null;
    }
    */

    
}


public class Gamer
{
   public string nick;
   public int score;
    
}


