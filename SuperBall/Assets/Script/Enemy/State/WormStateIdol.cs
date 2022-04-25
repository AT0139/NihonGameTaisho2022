using UnityEngine;

//毛虫　通常(移動)状態定義用partialスクリプト

public partial class EnemyManager
{


    public class WormStateIdol : EnemyStateBase
    {
        enum MOVE_DIR
        {
            RIGHT,
            LEFT,
            UP,
        }

        private MOVE_DIR dir;

        int MOVE_SPEED = 5;
        int ATTACK_INTERVAL = 300;
        int atkCnt = 0;

        public override void OnEnter(EnemyManager owner)
        {
        }

        public override void OnUpdate(EnemyManager owner)
        {
            Move(owner);

            atkCnt++;
            if (atkCnt >= ATTACK_INTERVAL)
            {
                owner.StateTransition(owner.stateAttack);
                atkCnt = 0;
            }
        }
        public override void OnCollision(EnemyManager owner)
        {
            owner.Destroy();
        }

        private void Move(EnemyManager owner)
        {
            if (!IsGround(owner))
            {
                SwitchDir();
            }

            switch (dir)
            {
                case MOVE_DIR.RIGHT:
                    owner.transform.localScale = new Vector3(-1, 1, 1);
                    owner.rigidbody2D.velocity = new Vector2(MOVE_SPEED, owner.rigidbody2D.velocity.y);
                    break;

                case MOVE_DIR.LEFT:
                    owner.transform.localScale = new Vector3(1, 1, 1);
                    owner.rigidbody2D.velocity = new Vector2(-MOVE_SPEED, owner.rigidbody2D.velocity.y);
                    break;

                case MOVE_DIR.UP:
                    break;
            }
        }

        bool IsGround(EnemyManager owner)
        {
            Vector3 startPoint = owner.transform.position + owner.transform.right * -0.8f * owner.transform.localScale.x;
            Vector3 endPoint = startPoint - owner.transform.up * 0.5f;

            Debug.DrawLine(startPoint, endPoint);
            return Physics2D.Linecast(startPoint, endPoint);
        }

        void SwitchDir()
        {
            if (dir == MOVE_DIR.RIGHT)
            {
                dir = MOVE_DIR.LEFT;
            }
            else if (dir == MOVE_DIR.LEFT)
            {
                dir = MOVE_DIR.RIGHT;
            }
        }
    }
}
