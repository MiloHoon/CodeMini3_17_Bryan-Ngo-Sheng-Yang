using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = Random.Range(60.0f, 90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
