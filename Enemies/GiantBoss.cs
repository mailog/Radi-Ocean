using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBoss : MonoBehaviour
{
    /*private GameManager gameManager;

    private Spawner trashSpawner;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        trashSpawner = GameObject.FindWithTag("Trash Spawner").GetComponent<Spawner>();

        trashSpawner.SetSpedUp(true);
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
        MoveToPos();   
    }

    void Shake()
    {
        gameManager.Shake(0.1f, 0.025f);
    }

    void MoveToPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector2.zero, moveSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        trashSpawner.SetSpedUp(false);
    }*/
}
