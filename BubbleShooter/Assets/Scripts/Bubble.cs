using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    //  public GameObject dead;
    Vector2 startPosition;
    public Sprite blue;
    public Sprite pink;
    public Sprite crystal;

    public GameObject dead;

    public static float coliderSizeBlue = 3.25f;
    public static float coliderSizePink = 13.25f;
    public static float coliderSizeCrystal = 5f;

    public static float scaleBlue = 0.5f;
    public static float scalePink = 0.12f;
    public static float scaleCrystal = 0.3f;

    private int collisionCount = 0;
    bool destroySingleBubble = false;

    public float speedFall = 0.8f;


    // Start is called before the first frame update
    void Start()
    {

        startPosition = this.gameObject.transform.position;
        if (this.gameObject.GetComponent<SpriteRenderer>() == null)
        {
            float randomChance = UnityEngine.Random.Range(1, 4);
            if (randomChance == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = blue;
                this.transform.localScale = new Vector2(scaleBlue, scaleBlue);
                this.gameObject.transform.position = startPosition;
                this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(coliderSizeBlue, coliderSizeBlue);
            }
            else if (randomChance == 2)
            {

                this.gameObject.GetComponent<SpriteRenderer>().sprite = pink;
                this.transform.localScale = new Vector2(Bubble.scalePink, Bubble.scalePink);
                this.gameObject.transform.position = startPosition;
                this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(coliderSizePink, coliderSizePink);


            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = crystal;
                this.transform.localScale = new Vector2(scaleCrystal, scaleCrystal);
                this.gameObject.transform.position = startPosition;
                this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(coliderSizeCrystal, coliderSizeCrystal);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DestroySingleBubble();
    }
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        var player = GameObject.Find("Gamer");
        Debug.Log(this.gameObject.name);
       // Debug.Log("Kolizja");
        if (col.gameObject.CompareTag("Gamer") && this.gameObject.GetComponent<SpriteRenderer>().sprite == player.gameObject.GetComponent<SpriteRenderer>().sprite)
        {
            Instantiate(dead, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

       
    }
    */

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bubble" && collision.gameObject.GetComponent<SpriteRenderer>().sprite == this.GetComponent<SpriteRenderer>().sprite)
        {
            Instantiate(dead, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Bubble")
        {
            collisionCount--;
        }

        if(collision.gameObject.tag != "ColumnBack" && collisionCount==0)
        {
            destroySingleBubble = true; ;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            collisionCount++;
        }
}

    void DestroySingleBubble()
    {
        if(destroySingleBubble == true)
        {
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.tag = "Untagged";
            transform.position = Vector3.MoveTowards(transform.position, new  Vector2(transform.position.x, -3), speedFall);
        if(transform.position.y <-2)
        { 
        Instantiate(dead, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
            }
    }
    }


}

 

