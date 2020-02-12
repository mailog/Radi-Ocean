using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationShop : MonoBehaviour
{

    public MutationManager mutationManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        mutationManager.UpdateShop();
    }

}
