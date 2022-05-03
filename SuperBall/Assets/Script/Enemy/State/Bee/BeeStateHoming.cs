using UnityEngine;

//ハチ　ホーミング状態定義用EnemyManagerのparctialスクリプト

public partial class EnemyManager
{
    public class BeeStateHoming : EnemyStateBase
    {
        float HOMING_SPEED = 3;
        float ATTACK_DISTANCE = 10;

        public override void OnEnter(EnemyManager owner)
        {
            owner.SetAnimation(owner.moveAnimationName);

            owner.GetComponentInChildren<ParticleSystem>().Play();
            owner.GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material.SetFloat("_Green", 3.0f);
        }

        public override void OnUpdate(EnemyManager owner)
        {
            Homing(owner);

            //敵が画面外にいったら待機に遷移
            if(!owner.isEnterScreen)
            {
                owner.StateTransition(owner.stateIdol);
            }
        }

        //プレイヤーを追いかける
        private void Homing(EnemyManager owner)
        {
            owner.transform.position = Vector2.MoveTowards(owner.transform.position,
                new Vector2(owner.player.transform.position.x, owner.player.transform.position.y), HOMING_SPEED * Time.deltaTime);

            //プレイヤーとの距離が近くなったら攻撃に遷移
            if (Vector2.Distance(owner.player.transform.position, owner.transform.position) < ATTACK_DISTANCE)
            {
                owner.StateTransition(owner.stateAttack);
            }
        }
        public override void OnCollision(EnemyManager owner)
        {
            owner.Destroy();
        }
    }
}   