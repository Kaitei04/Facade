using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TriggerMovement : MonoBehaviour
{

    private float speed = 0;
    [SerializeField] HUD hud;
    [SerializeField] Text press;
    CanvasGroup cg;
    bool visible;

    [SerializeField] Transform pointA; // Reference to the starting point
    [SerializeField] Transform pointB; // Reference to the ending point
    [SerializeField] RectTransform safeZone; // Reference to the safe zone RectTransform
    [SerializeField] RectTransform trigg;

    //private float direction = 1f; // 1 for moving towards B, -1 for moving towards A
    private RectTransform pointerTransform;
    private Vector3 targetPosition;
    private Vector2 oriPos;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        pointerTransform = trigg.GetComponent<RectTransform>();
        targetPosition = pointB.position;
        oriPos = trigg.anchoredPosition;
    }

    void Update()
    {
        if (visible)
        {
            pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, speed * Time.deltaTime);


            if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
            {
                targetPosition = pointB.position;
                //direction = 1f;
            }
            else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
            {
                targetPosition = pointA.position;
                //direction = -1f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckSuccess();
            }
        }
    }

    void CheckSuccess()
    {
        
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            hud.accurateCheck(true);
        }
        else
        {
            hud.accurateCheck(false);
        }
        
    }

        /*void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (visible)
                {
                    hud.accurateCheck(onMark);
                }
            }
        }*/
        
    public void setSpeed(float v)
    {
        speed = v;
    }
    
    public void instruction(string teks)
    {
        press.text = "Press 'Space' to " + teks;
    }
    
    public void turnVisible()
    {
        visible = true;
        cg.alpha = 1f;
        targetPosition = pointB.position;
    }
    public void turnInvisible()
    {
        visible = false;
        cg.alpha = 0f;
        speed = 0f;
        trigg.anchoredPosition = oriPos;
    }
    public void init()
    {
        if (cg == null)
        {
            cg = GetComponent<CanvasGroup>();
        }
    }
    
}
