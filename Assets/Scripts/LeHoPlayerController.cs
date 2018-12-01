using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LeHoPlayerController : MonoBehaviour {
   
    public float speed;
    public Text countText;
    public Text timerText;
    public Image lossImage;
    public Image winImage;

    private bool gameOn;
    private float timer = 0.0f;
    private Rigidbody2D rb2d;
    private int timercount = 12;
    private int count;
    private int win;

    //private double xsize = 0.82;
	// Use this for initialization


	void Start () {
        rb2d = GetComponent <Rigidbody2D>();
        rb2d.freezeRotation = true;
        gameOn = true;

        count = 0;
        win = 0;
        SetCountText();

        winImage.enabled = false;

        lossImage.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (rb2d.velocity.x >= 0)
        {
            transform.localScale = new Vector2(2, transform.localScale.y);
        }

        else
        {
            transform.localScale = new Vector2(-2, transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        timer += Time.deltaTime;
        for (float i = Convert.ToInt32(timer % 60); i <= 12; i++)
        {

            float seconds = Convert.ToInt32(timer % 60);
            timerText.text = "Time: " + seconds.ToString();

        }

        if (timer >= 12 && count < 10)
        {
            lossImage.enabled = true;
            count = 0;
            StartCoroutine(ByeAfterDelay(2));

        }
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Application.Quit();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Skull", if it is...
        if (other.gameObject.CompareTag("Skull"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();

        }

        if (other.gameObject.CompareTag("Finish"))
        {

            other.gameObject.SetActive(false);
            win += 1;

        }
    }

    void SetCountText()
    {
        //Set the text property of countText object
        countText.text = "Skulls Recovered: " + count.ToString();


        if (count >= 10 && timer < 12)
        {
            //... then set the text property of our winText object to win screen
            winImage.enabled = true;
            StartCoroutine(ByeAfterDelay(2));
        }
           
    }
}
