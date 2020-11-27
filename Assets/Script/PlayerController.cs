using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float speed = 5.0f;
    float gravityMoldifier = 2.0f;
    float zPointA = 12.5f;
    float zPointB = 17.0f;
    float currentTime = 0.0f;
    float startingTime = 10.0f;

    int totalCoin = 4;
    int maxJump = 0;
    int crateTriggered = 0;

    bool isForward = true;
    bool timeTrigger = true;

    public Rigidbody PlayerRb;
    public Animator PlayerAnimation;

    public GameObject TurningPlatform;
    public GameObject MovingPlatform;
    public Text TimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMoldifier;

        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Move Forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayerAnimation.SetBool("isMove", true);
        }
        //Player Move Backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerAnimation.SetBool("isMove", true);
        }
        //Player Change Running To Idle
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            PlayerAnimation.SetBool("isMove", false);
        }
        //Player Move Left
            if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            PlayerAnimation.SetBool("isMove", true);
        }
        //Player Move Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            PlayerAnimation.SetBool("isMove", true);
        }
        //Player Change Running To Idle
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            PlayerAnimation.SetBool("isMove", false);
        }

        //Trigger MovingPlatform
        if (crateTriggered == 1)
        if (MovingPlatform.transform.position.z < zPointB && isForward)
        {
            MovingPlatform.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);
        }
        else if (MovingPlatform.transform.position.z > zPointA && !isForward)
        {
            MovingPlatform.transform.Translate(Vector3.back * Time.deltaTime * 2.5f);
        }
        else
        {
            isForward = !isForward;
        }

        //Player Fall Off The Level
        if (transform.position.y < -5)
        {
            SceneManager.LoadScene("LoseScene");
        }

        //Trigger For CountDown
        if (!timeTrigger)
        {
            TimeLeft.GetComponent<Text>().text = "Time Left : " + currentTime.ToString("0");
            currentTime -= 1 * Time.deltaTime;
        }

        //If CurrentTime Is 0 Second Then The Platform Will Dissappear
        if (currentTime <= 0)
        {
            TurningPlatform.transform.rotation = Quaternion.Euler(0, 180, 0);
            currentTime = 10.0f;
            timeTrigger = !timeTrigger;
        }

        Jumping();
    }

    private void Jumping()
    {
        //Player Jump
        if (maxJump == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerRb.AddForce(Vector3.up * speed * gravityMoldifier, ForceMode.Impulse);
                PlayerAnimation.SetBool("isJumping", true);
                ++maxJump;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //From Jump State To Idle State
        if (collision.gameObject.CompareTag("Plane") || collision.gameObject.CompareTag("Cone") || collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.CompareTag("TurningPlatform"))
        {
            PlayerAnimation.SetBool("isJumping", false);
            maxJump = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Collision With Coin
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            --totalCoin;
        }
        //Check Total Coin Is 0
        if (totalCoin == 0)
        {
            //Collision With Cone
            if (other.gameObject.CompareTag("Cone"))
            {
                //Check If Timer is 10 Second, Brings Out The Platform
                if (currentTime == 10)
                {
                    TurningPlatform.transform.rotation = Quaternion.Euler(0, 0, 0);
                    timeTrigger = !timeTrigger;
                }
            }
        }
        //Collision With Crate
        if (other.gameObject.CompareTag("Crate"))
        {
            crateTriggered = 1;
        }
        //Collision With Target
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("WinScene");
        }
    }
}
