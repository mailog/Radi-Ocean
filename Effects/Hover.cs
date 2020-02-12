using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public bool setStart;

    private Vector2 ogPosition;
    public float change;

    public float time, timeFactor = 1f;

    // Use this for initialization
    void Start()
    {
        ogPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(new Vector3(ogPosition.x, ogPosition.y + change, transform.localPosition.z), new Vector3(ogPosition.x, ogPosition.y - change, transform.localPosition.z), Mathf.PingPong(time, 1));
        time += Time.unscaledDeltaTime * timeFactor;
    }

    void OnEnable()
    {
        if (ogPosition != Vector2.zero)
            transform.localPosition = ogPosition;
        if (!setStart)
            time = 0.5f;
    }
}
