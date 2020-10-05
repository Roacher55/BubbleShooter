using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bubble;
    public Sprite blue;
    public Sprite pink;
    public Sprite crystal;
    Vector2 positionMouse;
    Vector3 positionMouseCamera;
    public static Vector2 startPosition;
    Vector2 newBubblePosition;
    public static bool shoot = false;
    public float speedShoot = 0.1f;
   
    









    void Start()
    {

        startPosition = this.gameObject.transform.position;

        
        GetSpritePositionPlayer();

        
        
        


    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<LineRenderer>().SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y), 0));

        if (Input.GetMouseButtonDown(0) && shoot == false )
        {
            shoot = true;
            positionMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            positionMouseCamera = Camera.main.ScreenToWorldPoint(positionMouse,0);
            GetComponent<LineRenderer>().enabled = false;
        }
            if(shoot == true)
        { 

            transform.position = Vector3.MoveTowards(transform.position, positionMouseCamera, speedShoot);
            if(this.transform.position == positionMouseCamera )
            {
                positionMouseCamera = positionMouseCamera * 2;
                

            }
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Column"))
        {
            positionMouseCamera.x = -(positionMouseCamera.x);
            positionMouseCamera.y = positionMouseCamera.y +  System.Math.Abs(positionMouseCamera.x)/4;

            Debug.Log(transform.position.y);

        }
        //Debug.Log("y =======" + this.gameObject.transform.position.y);
        if (col.gameObject.CompareTag("Bubble") && this.gameObject.transform.position.y < 2f && col.gameObject.GetComponent<SpriteRenderer>().sprite != this.GetComponent<SpriteRenderer>().sprite)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           // Debug.Log("Wchodzi w warunek");

        }
        /*

        if (col.gameObject.CompareTag("Bubble") && this.gameObject.GetComponent<SpriteRenderer>().sprite == col.gameObject.GetComponent<SpriteRenderer>().sprite)
        {
            shoot = false;
            GetComponent<LineRenderer>().enabled = true;

            GetSpritePositionPlayer();
            
        }
        else if ((col.gameObject.CompareTag("Bubble") && this.gameObject.GetComponent<SpriteRenderer>().sprite != col.gameObject.GetComponent<SpriteRenderer>().sprite) || col.gameObject.CompareTag("ColumnBack"))
        {
            shoot = false;
            GetComponent<LineRenderer>().enabled = true;

            NewBubble();

            GetSpritePositionPlayer();           

        }
        
    */

    }


    void GetSpritePositionPlayer()
    {

        var boolSpriteBlue = 0;
        var boolSpritePink = 0;
        var boolSpriteCrystal = 0;

       var  bubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach(GameObject bubble in bubbles)
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
            this.gameObject.GetComponent<SpriteRenderer>().sprite = blue;
            this.transform.localScale = new Vector2(Bubble.scaleBlue, Bubble.scaleBlue);
            this.gameObject.transform.position = startPosition;
            this.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeBlue, Bubble.coliderSizeBlue);

        }
        else if (randomChance == 2 && boolSpritePink >0)
        {

            this.gameObject.GetComponent<SpriteRenderer>().sprite = pink;
            this.transform.localScale = new Vector2(Bubble.scalePink, Bubble.scalePink);
            this.gameObject.transform.position = startPosition;
            this.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizePink, Bubble.coliderSizePink);



        }
        else if ((randomChance == 3 || randomChance == 4) && boolSpriteCrystal > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = crystal;
            this.transform.localScale = new Vector2(Bubble.scaleCrystal, Bubble.scaleCrystal);
            this.gameObject.transform.position = startPosition;
            this.GetComponent<BoxCollider2D>().size = new Vector2(Bubble.coliderSizeCrystal, Bubble.coliderSizeCrystal);

        }
        /*
        void NewBubble()
        {
            newBubblePosition = this.transform.position;
            GameObject instanieteBubble = Instantiate(bubble, newBubblePosition, Quaternion.identity);
            instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            if (instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite == pink)
            {
                instanieteBubble.transform.localScale = new Vector2(0.12f, 0.12f);
                instanieteBubble.transform.position = newBubblePosition;
            }
            if (instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite == crystal)
            {
                instanieteBubble.transform.localScale = new Vector2(0.3f, 0.3f);
                instanieteBubble.transform.position = newBubblePosition;
            }
            if (instanieteBubble.gameObject.GetComponent<SpriteRenderer>().sprite == blue)
            {
                instanieteBubble.transform.localScale = new Vector2(0.5f, 0.5f);
                instanieteBubble.transform.position = newBubblePosition;
            }
        }
        */
    }

    
}
