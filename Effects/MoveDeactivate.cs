using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDeactivate : MonoBehaviour
{
    public Vector2 endPos;

    public Vector2 startPos;

    public float moveSpeed;

    public bool disappears, reset;

    public MonoBehaviour[] finishEnable; 

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, endPos, moveSpeed * Time.unscaledDeltaTime);

        if ((Vector2)transform.localPosition == endPos)
        {
            if(finishEnable.Length > 0)
            {
                foreach(MonoBehaviour comp in finishEnable)
                {
                    comp.enabled = true;
                }
            }

            if(disappears)
            {
                if (transform.parent != null)
                {
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else if(reset)
            {

            }
            else
            {
                enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        foreach (MonoBehaviour comp in finishEnable)
        {
            comp.enabled = false;
        }

        if (reset && startPos != Vector2.zero)
            transform.localPosition = startPos;
    }
}
