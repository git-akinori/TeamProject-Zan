using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
	BaseEnemyStatus status;
	
    protected float _knockbackTime = 0;
    protected bool _knockCheck = false;

    protected Animator _animator;

    static private PlayerBehaviour PB_player;
	Rigidbody2D rb2d;

    protected enum EnumStatus
    {
        MOVE,
        ATTACK,
		SPECIAL,
        DEATH
    };

    private EnumStatus _status;

    private void Update()
    {
        Debug.Log(this._status);
    }

    protected virtual void CreateStart()
    {
        /* 作成後一回だけ呼ばれる */
        _animator = GetComponent<Animator>();

        if(!PB_player) PB_player = GameManager.Member.PlayerBehaviour;
		rb2d = GetComponent<Rigidbody2D>();

		if (transform.name == "Slime") status = GameManager.Member.EnemyStatus.Slime;
		else if (transform.name == "Skeleton") status = GameManager.Member.EnemyStatus.Skeleton;
		else if (transform.name == "Ork") status = GameManager.Member.EnemyStatus.Ork;
		else if (transform.name == "Boss Slime") status = GameManager.Member.EnemyStatus.Boss_Slime;
		else if (transform.name == "Boss Skeleton") status = GameManager.Member.EnemyStatus.Boss_Skeleton;
		else if (transform.name == "Boss Ork") status = GameManager.Member.EnemyStatus.Boss_Ork;
		else Debug.LogError("not enemyName!" + transform.name);
	}

    protected virtual void MoveStart()
    {
		/* 通常移動開始時、最初に1回呼ばれる */

		rb2d.velocity = new Vector2(-status.Speed, 0);
	}

    protected virtual void MoveUpdate()
    {
		if (rb2d.IsSleeping()) MoveStart();

		/* 通常移動の処理 */
		if (_knockCheck == true)
		{
			_knockbackTime += Time.deltaTime;
			if (_knockbackTime > 1)
			{
				_knockCheck = false;
				MoveStart();
				_knockbackTime = 0;
			}
		}
	}

    protected virtual void AttackStart()
    {
        /*攻撃開始時に、一回呼ばれる*/
        HitPlayer();
    }

    protected virtual void AttackUpdate()
    {
        /*攻撃開始時の処理*/
    }

	protected virtual void SpecialStart()
	{
	}

	protected virtual void SpecialUpdate()
	{
	}

	protected virtual void DeathStart()
    {
        if (PB_player)
        {
            /* HPが0になった時最初に1回だけ呼ばれる */
            PB_player.AddExtraPoint(5);
        }

        Destroy(gameObject, 0.5f);
    }

    protected virtual void DeathUpdate()
    {
        /* HPが0になった後の処理 */
    }



    protected virtual void HitPlayer()
    {
        //敵がプレイヤーにダメージを与える

        if (PB_player)
        {
            /* プレイヤーに当たった時の処理*/
            PB_player.Damaged(2);
        }


    }

    protected virtual void Enemy_damaged()
    {
        /* 武器に当たった時の処理 */
        status.HP -= 1;
    }

    protected virtual void Knockback()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 30 / status.Weight, ForceMode2D.Impulse);
    }

    void Start()
    {
        CreateStart();
        changeStatus(EnumStatus.MOVE);
    }

    void FixedUpdate()
    {

        switch (_status)
        {
            case EnumStatus.MOVE:
                MoveUpdate();
                break;

            case EnumStatus.ATTACK:
                AttackUpdate();
                break;

			case EnumStatus.SPECIAL:
				SpecialUpdate();
				break;

            case EnumStatus.DEATH:
                DeathUpdate();
                break;

            default:
                Debug.Log("NoStatus");
                break;
        }
    }

    protected void changeStatus(EnumStatus _status)
    {
        switch (_status)
        {
            case EnumStatus.MOVE:
                MoveStart();
                break;

            case EnumStatus.ATTACK:
                HitPlayer();
                break;

			case EnumStatus.SPECIAL:
				SpecialStart();
				break;

            case EnumStatus.DEATH:
                DeathStart();
                break;

            default:
                break;
        }

        this._status = _status;

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            changeStatus(EnumStatus.ATTACK);
            HitPlayer();
        }

        if (collision.gameObject.tag == "Attack" 
            || collision.gameObject.tag == "Attack_combo2" 
            || collision.gameObject.tag == "Attack_combo3"
            || collision.gameObject.tag == "Attack_Special_Knife"
            || collision.gameObject.tag == "Attack_Special_Sword")
        {
            Debug.Log(this._status);
            Enemy_damaged();
            changeStatus(EnumStatus.MOVE);

            _knockCheck = true;
            Knockback();

            if (status.HP <= 0)
            {
                changeStatus(EnumStatus.DEATH);
            }
        }

        if(collision.gameObject.tag == "Attack_Special_shild")
        {
            changeStatus(EnumStatus.MOVE);
            _knockCheck = true;
            Knockback();

        }


    }



}
