using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] HUD hud;

    //private int speed;
    private Animator enemyAnimator;
    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void setAnimSpeed(float s)
    {
        enemyAnimator.speed = s;
    }

    public void playAnim(string nama)
    {
        enemyAnimator.Play(nama);
    }

    public void Aim()
    { 
        hud.Frame4Enemy();
    }

    public void EndAttack()
    {
        hud.EnemyFin();
    }

    public void damaged()
    {
        hud.SetBattleStage(4);
    }
}
