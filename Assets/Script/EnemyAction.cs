using UnityEngine;
using UnityEngine.UI;

public class EnemyAction : MonoBehaviour
{
    //[SerializeField] PlayerHUD playerHUD;

    float thres = 8f / 35;

    public int Act(int t, float cHp, float mHp)
    {
        
        if ((float)cHp / mHp <= thres)
        {

            return 999;
        }
        else
        {
            if (t % 2 == 0)
            {

                return 35;
            }
            else
            {

                return 30;
            }
        }
    }

    public string actionName(int t, float cHp, float mHp)
    {
        
        if ((float)cHp / mHp <=thres)
        {
            return "Annilihation";
        }
        else
        {
            if (t % 2 == 0)
            {
                return "Strong Punch!";
            }
            else
            {
                return "Strong Kick!";
            }
        }
    }
}
