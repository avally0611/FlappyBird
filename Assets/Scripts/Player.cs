using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    //change sprite being displayed and cycle through many sprites to make wings flapp - define actual sprite
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    //called automatically by unity - at beginning of frame rate
    private void Awake()
    {
        //look for component on object script is working on
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void onEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    // Start is called before the first frame update
    private void Start()
    {
        //calls a function - repeating makes it continously call function - repeat every 15 secs 
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = Vector3.up * strength;
        }

        // we havent applied gravity to upward direction movement - we say y because we only want gravity when it is up/down
        direction.y += gravity * Time.deltaTime;
        // Gravity is m.s so it needs to be consistent every second

        // weve only changed direction - need to actually move object 
        transform.position += direction * Time.deltaTime;
        // deltaTime is to make action independant of frame rate so it happens constantly
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        //make sure it loops to bgeinning if index is bigger than num of sprites (eg. 3 sprites - index = 4, make it repeat)
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
