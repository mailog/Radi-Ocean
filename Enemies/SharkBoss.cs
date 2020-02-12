using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBoss : MonoBehaviour
{
    public ParticleSystem bubbles;

    private ParticleSystem.EmissionModule bubblesEmission;

    public float slowMoveSpeed, moveSpeed, backupSpeed;

    public float backupCounter, backupTime;

    public float minX, maxX;

    public float bottY, topY;

    public float attackBottY, attackTopY;

    private bool bott;

    private Vector2 attackPos;
    
    public float waitCounter, waitTime;

    private SpriteRenderer sr;

    private Vector2 targetPos;

    public Vector2 spawnOffset;

    private bool atTarget;

    private Animator anim;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        bubblesEmission = bubbles.emission;
        bubblesEmission.enabled = false;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ChooseSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(atTarget)
        {
            if (Wait())
            {
                Attack();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, slowMoveSpeed * Time.deltaTime);
            if((Vector2)transform.position == targetPos)
            {
                atTarget = true;
            }
        }
    }

    bool Wait()
    {
        waitCounter += Time.deltaTime;
        if(waitCounter >= waitTime)
        {
            return true;
        }
        return false;
    }

    void Attack()
    {
        bubblesEmission.enabled = true;
        backupCounter += Time.deltaTime;
        float currSpeed = Mathf.Lerp(backupSpeed, moveSpeed, backupCounter / backupTime);
        transform.position = Vector2.MoveTowards(transform.position, attackPos, currSpeed * Time.deltaTime);
        if((Vector2)transform.position == attackPos)
        {
            ChooseSpawnPos();
        }
    }

    void ChooseSpawnPos()
    {
        atTarget = false;
        bott = !bott;
        waitCounter = 0;
        sr.flipY = !bott;
        float chosenX = 0;

        if(bott)
        {
            chosenX = Random.Range(minX, maxX);
            targetPos = new Vector2(chosenX, bottY);
            transform.position = targetPos - spawnOffset;
        }
        else
        {
            chosenX = player.position.x;
            targetPos = new Vector2(chosenX, topY);
            transform.position = targetPos + spawnOffset;
        }
        anim.Play("Start");
        ChooseAttackPos();
        backupCounter = 0;
        bubblesEmission.enabled = false;
    }

    void ChooseAttackPos()
    {
        if(bott)
        {
            attackPos = new Vector2(targetPos.x, attackTopY);
        }
        else
        {
            attackPos = new Vector2(targetPos.x, attackBottY);
        }
    }

    public void DetachBubbles()
    {
        bubbles.gameObject.GetComponent<AutoDestroyParticles>().enabled = true;
        bubbles.gameObject.transform.SetParent(null);
    }
}
