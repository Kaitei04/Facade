using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject hp;

    public void SetHP(float hpnormalized)
    {
        hp.transform.localScale = new Vector3(hpnormalized, 1f);
    }
}
