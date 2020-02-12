using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRadiation : MonoBehaviour
{
    private GameManager gameManager;

    public float minYPos;

    public float radiation;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckYPos();
    }


    void CheckYPos()
    {
        if (transform.position.y < minYPos)
        {
            gameManager.AddRadiation();
        }
    }
}
