using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject columnBack;
    public GameObject bubblePrefab;
    public GameObject player;
    public GameObject dead;
    public Sprite blue;
    public Sprite crystal;
    public Sprite pink;
    Vector3 previousPosition = new  Vector3(0,0,0);
    Vector3 lastPosition = new Vector3(0, 0, 0);
    GameObject[] bubbles;
    GameObject[] forDestoyBubbles;
    public static int numberOfShoot = 0;
    public GameObject YourScore;
        




    void Start()
    {
        Application.targetFrameRate = 30;
         GenereteBubble();

        YourScore.GetComponent<TextMesh>().text = "Strzały: " + numberOfShoot;



    }

    // Update is called once per frame
    void Update()
    {

        

        bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (var oneBubble in bubbles)
        {
            if(player.GetComponent<BoxCollider2D>().IsTouching(oneBubble.gameObject.GetComponent<BoxCollider2D>()) && player.gameObject.GetComponent<SpriteRenderer>().sprite == oneBubble.gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                Debug.Log("Dziala if Destroy");
                Destroybubble(oneBubble);
                break;
            }
            else if (player.GetComponent<BoxCollider2D>().IsTouching(oneBubble.gameObject.GetComponent<BoxCollider2D>()) && player.gameObject.GetComponent<SpriteRenderer>().sprite != oneBubble.gameObject.GetComponent<SpriteRenderer>().sprite)
            {
                Debug.Log("Dziala if NewBubble");
                Debug.Log("Pozycja przed wejsciem " + " x= " + player.transform.position.x + " y= " + player.transform.position.y);
                NewBubble(oneBubble);
                break;
            }
            else if (player.GetComponent<BoxCollider2D>().IsTouching(columnBack.gameObject.GetComponent<BoxCollider2D>()))
                {

                NewBubble(columnBack.gameObject);
                break;

            }
        
        }
        EndGame();
    }

    void Destroybubble(GameObject bubble)
    {
        Debug.Log("Dziala DestroyBubble");
     //   DestroyFewBubbles(bubble);
        Instantiate(dead, player.transform.position, Quaternion.identity);
                Destroy(bubble.gameObject);
                
        
                GetSpritePositionPlayer();
            
        

        
    }

  





    void GetSpritePositionPlayer()
    {
        
        var boolSpriteBlue = 0;
        var boolSpritePink = 0;
        var boolSpriteCrystal = 0;
        bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            if (bubble.GetComponent<SpriteRenderer>().sprite == blue)
                boolSpriteBlue++;
            if (bubble.GetComponent<SpriteRenderer>().sprite == crystal)
                boolSpriteCrystal++;
            if (bubble.GetComponent<SpriteRenderer>().sprite == pink)
                boolSpritePink++;

        }
                

        float randomChance = UnityEngine.Random.Range(1, 4);
        if (randomChance == 1 && boolSpriteBlue > 0)
        {
            player.gameObject.GetComponent<SpriteRenderer>().sprite = blue;
            player.transform.localScale = new Vector2(Bubble.scaleBlue, Bubble.scaleBlue);
            player.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeBlue, Bubble.coliderSizeBlue);

        }
        else if (randomChance == 2 && boolSpritePink >0)
        {

            player.gameObject.GetComponent<SpriteRenderer>().sprite = pink;
            player.transform.localScale = new Vector2(Bubble.scalePink, Bubble.scalePink);
            player.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizePink, Bubble.coliderSizePink);



        }
        else if((randomChance == 3 || randomChance == 4) && boolSpriteCrystal > 0)
        {
            player.gameObject.GetComponent<SpriteRenderer>().sprite = crystal;
            player.transform.localScale = new Vector2(Bubble.scaleCrystal, Bubble.scaleCrystal);
            player.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeCrystal, Bubble.coliderSizeCrystal);

        }
        player.gameObject.transform.position = Player.startPosition;
        player.gameObject.GetComponent<LineRenderer>().enabled = true;
        Player.shoot = false;

        ShootsNumber();
    }


    void NewBubble(GameObject bubble)
    {

        Debug.Log("Dziala NewBubble");

        Debug.Log("Pozycja po wejsciu " + " x= " + player.transform.position.x + " y= " + player.transform.position.y);
        var tempPosition = player.transform.position;
        /*
        if(bubble.tag == "Bubble" && tempPosition.x > previousPosition.x)
        {
            Debug.Log("Dziala11111");
        tempPosition.y = bubble.transform.position.y - 1.5f;
        tempPosition.x = bubble.transform.position.x - 1.5f;

        }
        else if(bubble.tag == "Bubble" && tempPosition.x < previousPosition.x)
            {
            tempPosition.y = bubble.transform.position.y - 1.5f;
            tempPosition.x = bubble.transform.position.x + 1.5f;
            Debug.Log("Dziala222222");

        }
        else if(bubble.tag == "ColumnBack")
        {
            tempPosition.y = bubble.transform.position.y - 2.2f;
        }
        */
        
        var instanieteBubble = Instantiate(bubblePrefab, tempPosition, Quaternion.identity);

        instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite = player.gameObject.GetComponent<SpriteRenderer>().sprite;
            if (instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite == pink)
            {
            instanieteBubble.transform.localScale = new Vector2(Bubble.scalePink, Bubble.scalePink);
            instanieteBubble.transform.position = tempPosition;
            instanieteBubble.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizePink, Bubble.coliderSizePink);

        }
            if (instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite == crystal)
            {
            instanieteBubble.transform.localScale = new Vector2(Bubble.scaleCrystal, Bubble.scaleCrystal);
            instanieteBubble.transform.position = tempPosition;
            instanieteBubble.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeCrystal, Bubble.coliderSizeCrystal);

        }
            if (instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite == blue)
            {
            instanieteBubble.transform.localScale = new Vector2(Bubble.scaleBlue, Bubble.scaleBlue);
            instanieteBubble.transform.position = tempPosition;
            instanieteBubble.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeBlue, Bubble.coliderSizeBlue);

        }
            GetSpritePositionPlayer();

        
     

        
    }




    void GenereteBubble()
    {

        Vector2 automaticPrefabPositionBubble = new Vector2(-6f, 6);
        float TempX = automaticPrefabPositionBubble.x;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 9; i++)
            {

                var instanieteBubble = Instantiate(bubblePrefab, automaticPrefabPositionBubble, Quaternion.identity);

                float randomChance = UnityEngine.Random.Range(1, 4);
                if (randomChance == 1)
                {
                    instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite = blue;
                    instanieteBubble.transform.localScale = new Vector2(Bubble.scaleBlue, Bubble.scaleBlue);
                    instanieteBubble.gameObject.transform.position = automaticPrefabPositionBubble;
                    instanieteBubble.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeBlue, Bubble.coliderSizeBlue);
                }
                else if (randomChance == 2)
                {

                    instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite = pink;
                    instanieteBubble.transform.localScale = new Vector2(Bubble.scalePink, Bubble.scalePink);
                    instanieteBubble.gameObject.transform.position = automaticPrefabPositionBubble;
                    instanieteBubble.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizePink, Bubble.coliderSizePink);


                }
                else
                {
                    instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite = crystal;
                    instanieteBubble.transform.localScale = new Vector2(Bubble.scaleCrystal, Bubble.scaleCrystal);
                    instanieteBubble.gameObject.transform.position = automaticPrefabPositionBubble;
                    instanieteBubble.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeCrystal, Bubble.coliderSizeCrystal);
                }
                automaticPrefabPositionBubble.x = automaticPrefabPositionBubble.x + 1.5f;
            }
            automaticPrefabPositionBubble.x = TempX;
            automaticPrefabPositionBubble.y = automaticPrefabPositionBubble.y + 1.5f;
        }
    }


    void PreviousPosition()
    {
        if(player.transform.position != lastPosition)
        {
            previousPosition = lastPosition;
          //  Debug.Log("wykonuje sie");
        }
        lastPosition = player.transform.position;
    //    Debug.Log("previous" + previousPosition.x);
      //  Debug.Log("actual" + player.transform.position.x);
    }


    void EndGame()
    {
        if (bubbles.Length == 0)
        {
            SceneManager.LoadScene("GameEnd");
        }

        
    }

    void ShootsNumber()
    {
        numberOfShoot++;
        YourScore.GetComponent<TextMesh>().text = "Strzały: " + numberOfShoot;
    }

    
}



