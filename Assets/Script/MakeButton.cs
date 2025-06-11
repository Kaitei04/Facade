using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MakeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool physical;
    [SerializeField] FighterAction detective;
    [SerializeField] HUD hud;
    public bool state1 = false;
    private string s;
    private Image img;

    public string nama;
    public int dmg;
    public int spUse;
    public int heal;
    public int reduc;
    public int eff;
    public int cdm;
    public bool AtkCard;
    private Vector3 originalPosition;
    public float hoverOffsetY = 10f;

    void Start()
    {
        img = gameObject.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => att(nama));
        
        originalPosition = transform.localPosition;
    }

    public void setFalse()
    {
        state1 = false;
    }

    void att(string btn)
    {
        detective.SelectAttack(nama);
        state1 = !state1;
        bool enough = hud.isEnough(spUse);
        int n = 0;
        if(AtkCard)
        {
            n = 1;
        }
        if (state1 && enough)
        {
            hud.useSkill(spUse, dmg, heal, reduc, eff, n, cdm);
            s = "Selected";
            transform.localPosition = originalPosition + new Vector3(0, hoverOffsetY, 0);
        }
        else if (state1 == false)
        {
            hud.useSkill(spUse * -1, dmg * -1, heal * -1, reduc * -1, eff * -1, n * -1, cdm * -1);
            s = "Deselected";
        }
        else if (!enough)
        {
            state1 = false;
            s = "Exceeds Current SP";
        }
        Debug.Log(nama + " " + s);
    }


    public void disableActionBtn()
    {
        transform.localPosition = originalPosition;
        Color tempColor = img.color;
        tempColor.a = 0f;
        img.color = tempColor;
        img.enabled = false;
    }

    public void enableActionBtn()
    {
        Color tempColor = img.color;
        tempColor.a = 1f;
        img.color = tempColor;
        img.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered image!");
        transform.localPosition = originalPosition + new Vector3(0, hoverOffsetY, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (s != "Selected")
        {
            Debug.Log("Pointer exited image!");
            transform.localPosition = originalPosition;
        }
    }


}
