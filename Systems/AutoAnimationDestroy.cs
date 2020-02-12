using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAnimationDestroy : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        float destroyTime = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay;

        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
