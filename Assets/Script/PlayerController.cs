using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Speed
    float speed = 5.0f;
    public Rigidbody PlayerRb;
    public Animator PlayerAnimation;

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
    }
}
