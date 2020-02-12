using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public AudioManager audioManager;

    public float[] realMoneyCosts;
    public int[] skinCosts;

    public Sprite[] skinSprites;
    public RuntimeAnimatorController[] skinAnims;
    public string[] skinDescriptions;


    public Button[] skinButtons;
    public bool[] isRealMoney;
    public Animator[] skinBubbles;

    public Image[] skinImgs;
    public Text[] skinCostTexts;

    public GameObject[] equippedImgs;

    public MutationManager mutationManager;
    public SpriteRenderer sr;
    public Animator anim;

    public GameObject confirmWindow, confirmBubble;

    public Image confirmImage;
    public Button confirmButton;
    public Text confirmCost, confirmDescription;
    public Animator confirmAnim;

    public TextFlash radCountFlash;
    public WhiteFlash radFoodFlash;

    private void Awake()
    {
        PlayerPrefs.SetInt("Skin:0", 1);
        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < skinCosts.Length; i++)
        {
            skinImgs[i].sprite = skinSprites[i];
            skinCostTexts[i].text = isRealMoney[i] ? realMoneyCosts[i] + "$" : skinCosts[i] + "";
        }

        LoadSkin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadSkin()
    {
        ChangeSkin(PlayerPrefs.GetInt("CurrSkin", 0));
    }

    public void CheckUnlocked()
    {
        for(int i = 0; i < skinCosts.Length; i++)
        {
            if(PlayerPrefs.GetInt("Skin:" + i, 0) == 1)
            {
                SkinUnlocked(i);
            }
        }
    }

    public void ChangeSkin(int skinIndex)
    {
        sr.sprite = skinSprites[skinIndex];
        anim.runtimeAnimatorController = skinAnims[skinIndex];
        confirmWindow.SetActive(false);
        PlayerPrefs.SetInt("CurrSkin", skinIndex);
        PlayerPrefs.Save();
        for(int i = 0; i < equippedImgs.Length;i++)
        {
            equippedImgs[i].SetActive(i == skinIndex);
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        if(isRealMoney[skinIndex])
        {

        }
        else
        {
            if (mutationManager.GetExp() >= skinCosts[skinIndex])
            {
                mutationManager.AddExp(-1 * skinCosts[skinIndex]);
                PlayerPrefs.SetInt("Skin:" + skinIndex, 1);
                PlayerPrefs.Save();

                audioManager.PlaySound(5);
                skinBubbles[skinIndex].Play("Pop");
                skinCostTexts[skinIndex].text = "";

                ChangeSkin(skinIndex);
            }
            else
            {
                radCountFlash.Flash();
                radFoodFlash.SelfFlashWhite();
            }
        }
    }

    void SkinUnlocked(int skinIndex)
    {
        skinBubbles[skinIndex].GetComponent<Image>().enabled = false;
        skinBubbles[skinIndex].gameObject.SetActive(false);
        skinCostTexts[skinIndex].gameObject.SetActive(false);
    }

    public void OpenConfirmWindow(int skinIndex)
    {
        confirmWindow.SetActive(true);
        confirmAnim.runtimeAnimatorController = null;
        confirmImage.sprite = skinSprites[skinIndex];
        confirmDescription.text = skinDescriptions[skinIndex];
        if (!confirmButton.gameObject.activeSelf)
            confirmButton.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("Skin:" + skinIndex, 0) == 0)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(delegate { UnlockSkin(skinIndex); });
            if (!confirmBubble.activeSelf)
                confirmBubble.SetActive(true);

            confirmCost.text = isRealMoney[skinIndex] ? realMoneyCosts[skinIndex] + "$" : "-" + skinCosts[skinIndex];
        } 
        else
        {
            confirmCost.text = "";
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(delegate { ChangeSkin(skinIndex); });
            if (confirmBubble.activeSelf)
                confirmBubble.SetActive(false);
        }
    }
}
