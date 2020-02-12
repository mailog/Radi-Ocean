using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text hiScoreText;

    public float waitTime;

    public GameObject[] actives;

    public Button pauseButton;

    public GameObject[] spawners;

    public MutationManager mutationManager;

    public ExpandShrink adSize, retrySize;

    // Start is called before the first frame update
    void Start()
    {
        CheckHiScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WatchAd()
    {
        mutationManager.AddExp(500);
        ObjectPooler.laserPooler.ExpExplode(30, Camera.main.ScreenToWorldPoint(adSize.gameObject.transform.position));

        adSize.gameObject.SetActive(false);
        CheckHiScore();
    }

    IEnumerator TurnOnActives()
    {
        yield return new WaitForSeconds(waitTime);

        foreach(GameObject obj in actives)
        {
            obj.SetActive(true);
        }
    }

    void CheckHiScore()
    {
        int hiScoreExp = PlayerPrefs.GetInt("HiScore", 0);
        int sessionExp = mutationManager.GetSessionExp();
        int currHiScore = hiScoreExp >= sessionExp ? hiScoreExp : sessionExp;
        hiScoreText.text = "" + currHiScore;
        PlayerPrefs.SetInt("HiScore", currHiScore);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        pauseButton.interactable = false;
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }
        StartCoroutine(TurnOnActives());
    }
}
