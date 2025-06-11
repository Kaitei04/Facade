using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] HUD hud;

    //private int speed;
    private Animator playerAnimator;
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    

    public void setAnimSpeed(float s)
    {
        playerAnimator.speed = s;
    }

    public void playAnim(string nama)
    {
        playerAnimator.Play(nama);
    }

    public void Aim()
    { 
        hud.Frame4Attack();
    }

    public void EndAttack()
    {
        hud.EndAttack();
    }

    public void damaged()
    {
        hud.SetBattleStage(6);
    }
}
