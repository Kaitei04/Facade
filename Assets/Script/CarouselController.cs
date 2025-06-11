using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarouselController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public int jumlahItem = 6;
    public RectTransform content;
    public float scrollStep;
    private string namaSceneGame = "Tutorial_Battle";
    [SerializeField] Button next;
    [SerializeField] Button prev;
    [SerializeField] Button skip;
    [SerializeField] Text skipTxt;
    private Image nextImg;
    private Image prevImg;
    private int slide = 1;

    public void Start()
    {
        scrollStep = 1f / (jumlahItem - 1);
        next.onClick.AddListener(ScrollKanan);
        prev.onClick.AddListener(ScrollKiri);
        nextImg = next.GetComponent<Image>();
        prevImg = prev.GetComponent<Image>();
        skip.onClick.AddListener(SkipTutorial);
        ButtonVisibility();
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene(namaSceneGame);
    }

    public void ScrollKiri()
    {
        float target = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition - scrollStep);
        scrollRect.horizontalNormalizedPosition = target;
        slide -= 1;
        if (slide < 1)
        {
            slide = 1;
        }
        ButtonVisibility();
    }

    public void ScrollKanan()
    {
        float target = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition + scrollStep);
        scrollRect.horizontalNormalizedPosition = target;
        slide += 1;
        if (slide > jumlahItem)
        {
            slide = jumlahItem;
        }
        ButtonVisibility();
    }

    public void ButtonVisibility()
    {
        if  (slide <= 1)
        {
            
            Color temp = prevImg.color;
            temp.a = 0f;
            prevImg.color = temp;
        }
        else if (slide > 1)
        {
            
            Color temp = prevImg.color;
            temp.a = 0.85f;
            prevImg.color = temp;
        }
        if (slide >= jumlahItem)
        {
            skipTxt.text = "Finish Tutorial";
            Color temp = nextImg.color;
            temp.a = 0f;
            nextImg.color = temp;
        }
        else if (slide < jumlahItem)
        {
            skipTxt.text = "Skip";
            Color temp = nextImg.color;
            temp.a = 0.85f;
            nextImg.color = temp;
        }
    }
}
