using UnityEngine;
using UnityEngine.SceneManagement;

//毛虫　攻撃状態定義用partialスクリプト

public partial class EnemyManager
{
    public class WormStateAttack : EnemyStateBase
    {
        int ATTACK_TIME = 60;
        int atkCnt = 0;

        public override void OnEnter(EnemyManager owner)
        {
            
        }

        public override void OnUpdate(EnemyManager owner)
        {
            atkCnt++;
            if(atkCnt >= ATTACK_TIME)
            {
                owner.StateTransition(owner.stateIdol);
            }
        }

        public override void OnCollision(EnemyManager owner)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}