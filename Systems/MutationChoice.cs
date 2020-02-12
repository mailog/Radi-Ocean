using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationChoice : MonoBehaviour
{
    public GameManager gameManager;

    public Fish fish;

    public GameObject pauseButton;

    public Text option1Text, option2Text;

    public Image option1Img, option2Img;

    public Button option1Butt, option2Butt;

    public Sprite[] mutationSprites;

    public MutationManager mutationManager;

    public int[] mutationTiers;

    public string[] mutationNames;

    //public int[] possibleMutationsTestPre, possibleMutationsTestMid, possibleMutationsTestPost;

    public Animator[] bubbles;

    public float delay;

    private float popCounter, popTime;

    private bool popping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(popping)
            CheckPopping();   
    }

    void CheckPopping()
    {
        if(popTime == 0)
            popTime = bubbles[0].GetCurrentAnimatorStateInfo(0).length + delay;

        popCounter += Time.unscaledDeltaTime;
        if(popCounter >= popTime)
        {
            gameObject.SetActive(false);
        }
    }

    void FillOptions()
    {
        List<int> possibleMutations = new List<int>();
        
        for(int i = 0; i < mutationTiers.Length; i++)
        {
            //if(mutationTiers[i] > 0)
            //{
                possibleMutations.Add(i);
            //}
        }

        //possibleMutationsTestPre = possibleMutations.ToArray();
        
        option1Butt.onClick.RemoveAllListeners();
        option2Butt.onClick.RemoveAllListeners();

        int option1Index = Random.Range(0, possibleMutations.Count);
        option1Img.sprite = mutationSprites[option1Index];
        option1Butt.onClick.AddListener(delegate { ChooseMutation(option1Index,0); });
        option1Text.text = mutationNames[option1Index];
        possibleMutations.RemoveAt(option1Index);
        //Debug.Log("Option 1: " + option1Index + " Total Options: " + possibleMutations.Count);

        //possibleMutationsTestMid = possibleMutations.ToArray();

        int option2Index = Random.Range(0, possibleMutations.Count);
        option2Img.sprite = mutationSprites[option2Index];
        option2Butt.onClick.AddListener(delegate { ChooseMutation(option2Index,1); });
        option2Text.text = mutationNames[option2Index];
        possibleMutations.RemoveAt(option2Index);
        Debug.Log("Option 2: " + option2Index + " Total Options: " + possibleMutations.Count);

        //possibleMutationsTestPost = possibleMutations.ToArray();
    }


    void ChooseMutation(int chosenMutation, int index)
    {
        mutationTiers[chosenMutation]--;
        //mutationManager.AddMutation(chosenMutation);
        
        bubbles[index].Play("Pop");
        popTime = bubbles[index].GetCurrentAnimatorStateInfo(0).length + delay;
        popping = true;
        gameManager.Flash();
    }

    private void OnEnable()
    {
        gameManager.Flash();
        pauseButton.SetActive(false);
        fish.enabled = false;
        FillOptions();
        Time.timeScale = 0f;
        popping = false;
        popCounter = 0;
        foreach(Animator anim in bubbles)
        {
            anim.Play("Bubble");
        }
    }

    private void OnDisable()
    {
        pauseButton.SetActive(true);
        fish.enabled = true;
        Time.timeScale = 1f;
    }
}
