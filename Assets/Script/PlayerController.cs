using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player Speed
    float speed = 5.0f;
    float zPointA = 12.5f;
    float zPointB = 17.0f;
    int crateTriggered = 0;
    bool isForward = true;

    public Rigidbody PlayerRb;
    public Animator PlayerAnimation;

    public GameObject TurningPlatform;
    public GameObject MovingPlatform;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Collision With Crate
        if (collision.gameObject.CompareTag("Crate"))
        {
            crateTriggered = 1;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Collision With Cone
        if (other.gameObject.CompareTag("Cone"))
        {
            TurningPlatform.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //Collision With Target
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("WinScene");
        }
    }
}
