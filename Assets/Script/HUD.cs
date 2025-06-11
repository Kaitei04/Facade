using TMPro;
using UnityEngine;
using System.Threading;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] HPBar playerhpbar;
    [SerializeField] HPBar enemyhpbar;
    [SerializeField] TextMeshProUGUI enemyhpNum;
    [SerializeField] TextMeshProUGUI playerhpNum;
    [SerializeField] TextMeshProUGUI spNum;
    [SerializeField] EnemyAction enemyAction;
    [SerializeField] AttackButton btn1;
    [SerializeField] MakeButton btn2;
    [SerializeField] MakeButton btn3;
    [SerializeField] MakeButton btn4;
    [SerializeField] MakeButton btn5;
    [SerializeField] TriggerMovement trigger;
    [SerializeField] Text condition;
    [SerializeField] CanvasGroup exit;
    [SerializeField] CanvasGroup spUi;
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] EnemyAnimator enemyAnimator;

    private float power = 0;
    private int heal = 0;
    private int currentSP = 5;
    private int turnCount = 0;
    private int reduc = 0;
    private int efficiency = 0;
    private int AtkCard = 0;
    private int StatusCard = 0;
    private float speed;
    private int BattleStage;
    private string EnemyMove;
    private float enemyPower;
    private string move;
    private int cdm = 100;
    //private Animator playerAnimator;
    //private Animator enemyAnimator;
    public float playermaxHp = 100;
    public float enemymaxHp = 350;
    public float currentPlayerhp;
    public float currentEnemyhp;

    void Start()
    {
        trigger.init();
        currentPlayerhp = playermaxHp;
        playerhpNum.text = currentPlayerhp.ToString() + "/" + playermaxHp.ToString();
        if (playerhpNum == null)
        {
            Debug.LogError("hpNum is not assigned in the Inspector!");
        }
        currentEnemyhp = enemymaxHp;
        enemyhpNum.text = currentEnemyhp.ToString() + "/" + enemymaxHp.ToString();
        trigger.turnInvisible();
        SetBattleStage(0);
        exit.alpha = 0f;
    }

    

    public void BStage1()
    {
        //playerAnimator.Play("PlayerIdle");
        //enemyAnimator.Play("EnemyIdle");
        spNum.text = "SP : " + currentSP.ToString() + "/5";
        spUi.alpha = 1f;
        trigger.turnInvisible();
        btn1.enableActionBtn();
        btn2.enableActionBtn();
        btn3.enableActionBtn();
        btn4.enableActionBtn();
        btn5.enableActionBtn();
        playerhpNum.text = currentPlayerhp.ToString() + "/" + playermaxHp.ToString();
        enemyhpNum.text = currentEnemyhp.ToString() + "/" + enemymaxHp.ToString();
    }

    

    public void BStage2()
    {
        turnCount += 1;
        speedCount();
        btn1.disableActionBtn();
        btn2.disableActionBtn();
        btn3.disableActionBtn();
        btn4.disableActionBtn();
        btn5.disableActionBtn();
        spUi.alpha = 0f;
        //enemyAnimator.speed = 0;
        enemyAnimator.setAnimSpeed(0);
        playerAnimator.playAnim(move);
    }

    public void Frame4Attack()
    {
        playerAnimator.setAnimSpeed(0f);
        SetBattleStage(3);
    }

    public void BStage3()
    {
        trigger.instruction("Deal Most Damage");
        trigger.setSpeed(speed);
        trigger.turnVisible();
    }

    public void accurateCheck(bool mark)
    {
        if (BattleStage == 3)
        {
            if (mark == false)
            {
                heal = 0;
                power = power * 0.8f;
            }
            else
            {
                power = power * (cdm / 100f);
            }
            trigger.turnInvisible();
            currentPlayerhp = Mathf.Min(playermaxHp, currentPlayerhp + heal);
            currentEnemyhp = Mathf.Max(0, currentEnemyhp - power);
            playerhpNum.text = currentPlayerhp.ToString() + "/" + playermaxHp.ToString();
            enemyhpNum.text = currentEnemyhp.ToString() + "/" + enemymaxHp.ToString();
            playerhpbar.SetHP(currentPlayerhp / playermaxHp);
            enemyhpbar.SetHP(currentEnemyhp / enemymaxHp);
            if (power > 0)
            {
                enemyAnimator.playAnim("Enemy_Attacked");
                enemyAnimator.setAnimSpeed(1f);
            }
            else
            {
                playerAnimator.setAnimSpeed(1f);
            }
        }

        if (BattleStage == 5)
        {
            if (mark == true)
            {
                enemyPower = enemyPower * (1f - (reduc / 100f));
                playerAnimator.setAnimSpeed(1f);
                playerAnimator.playAnim("Player_Guard");
            }
            else
            {
                enemyPower = enemyPower * 1.1f;
                playerAnimator.setAnimSpeed(1f);
                playerAnimator.playAnim("Player_Attacked");
            }
            trigger.turnInvisible();
            //enemyAnimator.speed = 1;
            currentPlayerhp = Mathf.Max(0, currentPlayerhp - enemyPower);
            playerhpNum.text = currentPlayerhp.ToString() + "/" + playermaxHp.ToString();
            playerhpbar.SetHP(currentPlayerhp / playermaxHp);
        }
    }

    

    public void BStage4()
    {
        //playerAnimator.speed = 1;
        playerAnimator.setAnimSpeed(1f);
    }

    public void EndAttack()
    {
        if (BattleStage == 3)
        {
            BattleStage = 4;
        }
        Condition();
    }

    public void Frame4Enemy()
    {
        speedCount();
        //enemyAnimator.speed = 0;
        enemyAnimator.setAnimSpeed(0);
        SetBattleStage(5);
    }

    public void BStage5()
    {
        trigger.instruction("Avoid Critical Hit");
        trigger.setSpeed(speed);
        trigger.turnVisible();
    }

    public void BStage6()
    {
        //enemyAnimator.speed = 1f;
        enemyAnimator.setAnimSpeed(1f);
        //StartCoroutine(turn0());
    }

    

    public void EnemyFin()
    {
        Condition();
    }

    public void BStage0()
    {
        power = 0;
        heal = 0;
        currentSP = 5;
        reduc = 0;
        efficiency = 0;
        AtkCard = 0;
        playerAnimator.setAnimSpeed(1f);
        enemyAnimator.setAnimSpeed(1f);
        StatusCard = 0;
        move = null;
        cdm = 100;
        playerAnimator.playAnim("Player_Idle");
        enemyAnimator.playAnim("Enemy_Idle");
        SetBattleStage(1);
    }

    public void Condition()
    {
        if (BattleStage == 4)
        {
            if (currentEnemyhp <= 0)
            {
                SetBattleStage(7);
            }
            else
            {
                EnemyAttack();
                playerAnimator.setAnimSpeed(0f);
                enemyAnimator.setAnimSpeed(1f);
                enemyAnimator.playAnim("Enemy_Attack");
            }
        }
        if (BattleStage == 6)
        {
            if (currentPlayerhp <= 0)
            {
                SetBattleStage(8);
            }
            else
            {
                SetBattleStage(0);
            }
        }
    }

    public void BStageW()
    {
        condition.text = "VICTORY!";
        condition.color = Color.green;
        exit.alpha = 1f;
    }

    public void BStageL()
    {
        condition.text = "DEFEAT";
        condition.color = Color.red;
        exit.alpha = 1f;
    }

    

    public void Update()
    {
        if (BattleStage == 7 || BattleStage == 8)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main_Menu");
            }
        }
    }

    public void SetBattleStage(int n)
    {
        BattleStage = n;

        switch (BattleStage)
        {
            case 0:
                BStage0(); break;
            case 1:
                BStage1(); break;
            case 2:
                BStage2(); 
                break;
            case 3:
                BStage3(); break;
            case 4:
                BStage4(); 
                break;
            case 5:
                BStage5(); break;
            case 6:
                BStage6(); break;
            case 7:
                BStageW(); break;
            case 8:
                BStageL(); break;
            default:
                Debug.Log("Something is wrong. Battle Stage: " + BattleStage); break;
        }
    }

    public void useSkill(int sp, int pwr, int hl, int rdc, int eff, int n, int cd)
    {
        currentSP = currentSP - sp;
        power = power + pwr;
        heal = heal + hl;
        reduc = reduc + rdc;
        cdm = cdm + cd;
        efficiency = efficiency + eff;
        if (n == 0)
        {
            StatusCard += 1; 
        }
        AtkCard = AtkCard + n;
        addMove(n, sp);
        spNum.text = "SP : " + currentSP.ToString() + "/5";
    }

    public void addMove(int n, int sp)
    {
        if (move == null)
        {
            if (n > 0)
            {
                move = "Range_Attack";
            }
            else
            {
                move = "Status_Move";
            }
        }
        if (sp < 0)
        {
            move = null;
        }
    }

    public void speedCount()
    {
        if (BattleStage == 2)
        {
            if (AtkCard == 0)
            {
                AtkCard = 1;
            }
            speed = 45f / (10f - ((float)efficiency / AtkCard));
        }
        if (BattleStage == 4)
        {
            
            float EnemyEff = 5f;
            if (reduc > 0)
            {
                EnemyEff = EnemyEff + (3.5f * (reduc / 100f));
            }
            speed = 62f / (10f - EnemyEff);
        }
    }

    public void EnemyAttack()
    {
        EnemyMove = enemyAction.actionName(turnCount, currentEnemyhp, enemymaxHp);
        Debug.Log(EnemyMove);
        int Damage = enemyAction.Act(turnCount, currentEnemyhp, enemymaxHp);
        enemyPower = Damage;
    }

    public bool isEnough(int sp)
    {
        if (currentSP >= sp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checkAtk()
    {
        if (AtkCard > 0 || heal > 0)
        {
            SetBattleStage(2);
        }
    }
}
