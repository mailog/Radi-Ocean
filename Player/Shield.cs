using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform playerTrans;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(playerTrans.position, axis, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy Laser"))
        {
            collision.gameObject.GetComponent<Laser>().Impact();
        }
    }
}
