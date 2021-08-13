﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Joystick joystick;
    public float speed = 2000f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (health == 0)
        {
            winLoseBG.gameObject.SetActive(true);
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            winLoseText.text = "Game Over!";
            StartCoroutine(LoadScene(3));
            //Debug.Log("Game Over!");
            
        }
    }
        // Update is called once per frame
    void FixedUpdate ()
    {
        
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || joystick.Horizontal >= .2f)
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || joystick.Horizontal <= -.2f)
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || joystick.Vertical >= .2f)
        {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || joystick.Vertical <= -.2f)
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
    }
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
            //Debug.Log("Score: " + score.ToString());
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            //Debug.Log("Health: " + health.ToString());
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            winLoseBG.gameObject.SetActive(true);
            winLoseText.color = Color.black;
            winLoseBG.color = Color.green;
            winLoseText.text = "You win!";
            StartCoroutine(LoadScene(3));
            //Debug.Log("You win!");
        }
    }
 }

