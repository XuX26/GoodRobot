using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liar.Gameplay
{
	public class PlayerController : MonoBehaviour, Rules.IGravityChange
	{
		private const string k_deathAnimTrigger = "Die";
		private const string k_reviveAnimTrigger = "Revive";
		private const string k_winAnimTrigger = "Win";

		public bool IsDying { get { return m_dyingRoutine != null; } }
		public bool IsWinning { get { return m_winningRoutine != null; } }

		[SerializeField] private bool m_pauseWithInitialization = true;
		[SerializeField] private float m_respawnDelay = 0.5f;
		[SerializeField] private float m_winTransitionDelay = 0.5f;

		[Header( "Movement Modifiers" )]
		[SerializeField] private EDirection m_moveDirection = EDirection.Right;

		[Space]
		[SerializeField] private float m_moveSpeed = 1f;
		[SerializeField] private float m_rotationSmoothTime = 0.35f;

		[Header( "Collision Modifiers" )]
		[SerializeField] private float m_wallCheckFrequency = 1f;
		[SerializeField] private float m_wallCheckRadius = 0.35f;
		[SerializeField] private float m_wallCheckDistance = 0.25f;
		[SerializeField] private LayerMask m_wallCheckLayer = 0;

		[Space]
		[SerializeField] private float m_collisionBounceTimespan = 0.3f;
		[SerializeField] private float m_collisionBounceScale = 0.15f;

		[Header( "SFX References" )]
		[SerializeField] private Utility.SfxClip m_dieSfx = default;
		[SerializeField] private Utility.SfxClip m_reviveSfx = default;
		[SerializeField] private Utility.SfxClip m_wallChangeSfx = default;

		private Animator m_animator = null;
		private OrientationRecorder m_orientationRecorder = null;
		private Rigidbody2D m_rigidbody = null;
		private SpriteRenderer m_renderer = null;
		private Utility.BounceFx m_bounceFx = null;
		private RespawnEssence m_respawnEssence = default;

		private bool m_initialized = false;
		private EDirection m_gravityDirection = EDirection.Down;
		private EDirection m_initialMoveDirection = EDirection.None;
		private float m_rotationAngleVelocity = 0;
		private float m_wallCheckResetTime = 0;
		private Coroutine m_dyingRoutine = null;
		private Coroutine m_winningRoutine = null;

		public void Apply( Rules.Traits.DirectionTrait newDirection )
		{
			m_gravityDirection = newDirection.Value;


			SetWallCheckCooldown();
		}

		public void AddNounTag()
		{
			NounContainer.AddNounThing<Rules.IGravityChange>( this );
		}

		public void RemoveNounTag()
		{
			NounContainer.RemoveNounThing<Rules.IGravityChange>( this );
		}

		public void Win()
		{
			if ( IsWinning ) { return; }

			m_winningRoutine = StartCoroutine( Win_Coroutine() );
		}

		public void Lose()
		{
			if ( IsDying ) { return; }

			m_dyingRoutine = StartCoroutine( Die_Coroutine() );
		}

		private void FixedUpdate()
		{
			if ( !m_initialized ) { return; }

			UpdateRotation();
			UpdateMovement();
		}

		private bool IsCollidingWith( Vector2 direction, float distance )
		{
			RaycastHit2D hitInfo = Physics2D.CircleCast( transform.position, m_wallCheckRadius, direction, distance, m_wallCheckLayer );

			Debug.DrawRay( transform.position, direction * (distance + m_wallCheckRadius), Color.yellow );
			
			return hitInfo.transform != null;
		}

		private void UpdateRotation()
		{
			// Update rotation to orientate with world's up direction ...
			Vector2 gravityDir = Physics2D.gravity.normalized;

			float currentAngle = Vector2.SignedAngle( Vector2.right, transform.right );
			float targetAngle = Vector2.SignedAngle( Vector2.up, -gravityDir );

			float smoothAngle = Mathf.SmoothDampAngle( currentAngle, targetAngle, ref m_rotationAngleVelocity, m_rotationSmoothTime );

			// Apply!
			transform.localEulerAngles = Vector3.forward * smoothAngle;
		}

		private void UpdateMovement()
		{
			SetSpriteFlip( m_moveDirection );

			Vector2 moveDir = GameUtility.GetDirection( m_moveDirection ) * EDirectionToSign( m_gravityDirection );
			moveDir = transform.TransformDirection( moveDir );


			// Handle wall collisions ...
			if ( m_wallCheckResetTime < Time.timeSinceLevelLoad && IsCollidingWith( moveDir, m_wallCheckDistance ) )
			{
				m_wallChangeSfx?.PlaySfx();

				SetWallCheckCooldown();

				m_bounceFx.Play( m_collisionBounceTimespan, m_collisionBounceScale );
				m_moveDirection = GetOpposite( m_moveDirection );
				return;
			}


			// Apply!
			m_rigidbody.AddForce( moveDir * m_moveSpeed, ForceMode2D.Force );
		}

		private void Awake()
		{
			m_animator = GetComponentInChildren<Animator>();
			m_rigidbody = GetComponentInChildren<Rigidbody2D>();
			m_orientationRecorder = GetComponent<OrientationRecorder>();
			m_renderer = GetComponentInChildren<SpriteRenderer>();
			m_bounceFx = GetComponentInChildren<Utility.BounceFx>();
			m_respawnEssence = GetComponentInChildren<RespawnEssence>( true );

			AddNounTag();
		}

		private IEnumerator Start()
		{
			m_respawnEssence.Stop();

			if ( m_pauseWithInitialization )
			{
				m_rigidbody.isKinematic = true;
			}


			while ( !Utility.LevelInitializer.IsInitialized ) { yield return null; }
			m_initialized = true;

			m_orientationRecorder.RecordOrientation();

			m_rigidbody.isKinematic = false;
			m_initialMoveDirection = m_moveDirection;
		}

		private void OnDestroy()
		{
			RemoveNounTag();
		}

		private void SetWallCheckCooldown()
		{
			m_wallCheckResetTime = Time.timeSinceLevelLoad + m_wallCheckFrequency;
		}

		private float EDirectionToSign( EDirection dir )
		{
			switch ( dir )
			{
				default:
				case EDirection.None: return 0;

				case EDirection.Right:
				case EDirection.Down:
					return 1;

				case EDirection.Left:
				case EDirection.Up:
					return -1;
			}
		}

		private void SetSpriteFlip( EDirection horizontal )
		{
			if ( horizontal == EDirection.Left || horizontal == EDirection.Right )
			{
				if ( m_gravityDirection == EDirection.Down || m_gravityDirection == EDirection.Right )
				{
					m_renderer.flipX = (horizontal == EDirection.Left);
				}
				else if ( m_gravityDirection == EDirection.Up || m_gravityDirection == EDirection.Left )
				{
					m_renderer.flipX = !(horizontal == EDirection.Left);
				}
			}
		}

		private EDirection GetOpposite( EDirection direction )
		{
			switch ( direction )
			{
				default:
				case EDirection.None: return EDirection.None;

				case EDirection.Up: return EDirection.Down;
				case EDirection.Down: return EDirection.Up;
				case EDirection.Left: return EDirection.Right;
				case EDirection.Right: return EDirection.Left;
			}
		}

		private IEnumerator Die_Coroutine()
		{
			m_dieSfx.PlaySfx();

			// Disable player movement ...
			m_rigidbody.isKinematic = true;
			m_rigidbody.velocity = Vector2.zero;

			// Play death anim ...
			m_animator.SetTrigger( k_deathAnimTrigger );

			// Start our respawn essence flying ...
			float flyDuration = 0;
			if ( m_respawnEssence != null )
			{
				m_respawnEssence.transform.SetParent( null, true );
				flyDuration = m_respawnEssence.FlyToTarget( transform.position, m_orientationRecorder.RecordedPosition );
			}

			// Wait a little for animations and such to play ...
			float timer = 0;
			while ( timer < 1 )
			{
				timer += Time.deltaTime / m_respawnDelay;
				yield return new WaitForFixedUpdate();
			}


			m_reviveSfx?.PlaySfx();


			// Move to respawn position and play revive!
			m_animator.SetTrigger( k_reviveAnimTrigger );
			m_orientationRecorder.ResetOrientation();

			// Wait until our respawn essence has arrived ...
			if ( m_respawnEssence != null )
			{
				flyDuration -= m_respawnDelay;
				if ( flyDuration > 0 ) { yield return new WaitForSeconds( flyDuration ); }

				m_respawnEssence.transform.SetParent( transform, false );
				m_respawnEssence.Stop();
			}


			// Activate player movement!
			m_rigidbody.isKinematic = false;
			m_moveDirection = m_initialMoveDirection;


			// Tell level we're ready to play!
			Rules.RulesManager.Instance.ResetAllRules();

			m_dyingRoutine = null;
		}

		private IEnumerator Win_Coroutine()
		{
			m_rigidbody.isKinematic = true;
			m_rigidbody.velocity = Vector2.zero;
			m_animator.SetTrigger( k_winAnimTrigger );

			yield return new WaitForSeconds( m_winTransitionDelay );

			GameManager.Instance.LoadNextLevel();

			m_winningRoutine = null;
		}
	}
}