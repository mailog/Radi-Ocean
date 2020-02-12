using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorCount : MonoBehaviour
{
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void IncCount()
    {
        enemyCount++;
    }

    public void DecCount()
    {
        enemyCount--;
    }

    public int GetCount()
    {
        return enemyCount;
    }
}
