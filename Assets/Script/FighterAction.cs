using UnityEngine;
using UnityEngine.UI;

public class FighterAction : MonoBehaviour
{
    private GameObject detective;
    private GameObject enemy;
    

    [SerializeField] private GameObject preShotPrefab;
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private GameObject evadePrefab;

    private GameObject currentAttack;
    private GameObject preShot;
    private GameObject heal;
    private GameObject evade;

    public void SelectAttack(string btn)
    {
        GameObject victim = detective;
        if (tag == "detective")
        {
            victim = enemy;
        }
        if (btn.CompareTo("Precise Shot") == 0)
        {
            Debug.Log("Precise Shot");
        } 
        else if (btn.CompareTo("Heal") == 0)
        {
            Debug.Log("Heal");
        }
        else if (btn.CompareTo("Shield Bash") == 0)
        {
            Debug.Log("Shield Bash");
        }
        else if (btn.CompareTo("Heavy Swing") == 0)
        {
            Debug.Log("Heavy Swing");
        }
        else
        {
            Debug.Log("Nothing");
        }
    }
}
