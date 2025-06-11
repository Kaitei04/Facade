using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private bool physical;
    private GameObject detective;
    //[SerializeField] PlayerHUD hud;
    [SerializeField] HUD HeadsUp;
    [SerializeField] MakeButton b1;
    [SerializeField] MakeButton b2;
    [SerializeField] MakeButton b3;
    [SerializeField] MakeButton b4;
    private Image img;


    void Start()
    {
        img = gameObject.GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnClick);
        //detective = GameObject.FindGameObjectWithTag("detective");
    }

    void OnClick()
    {
        //detective.GetComponent<FighterAction>().SelectAttack("Attack");
        //StartCoroutine(hud.EndMyTurn());
        HeadsUp.checkAtk();
        
        b1.setFalse();
        b2.setFalse();
        b3.setFalse();
        b4.setFalse();
    }
    public void disableActionBtn()
    {
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
}