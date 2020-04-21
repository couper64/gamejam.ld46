namespace LD46
{
	using System.Collections;

	using UnityEngine;

	public class Spoon : MonoBehaviour
	{
		public enum AnimationState 
		{
			Food = 0,
			NoFood = 1
		}

		public float timer;
		public float duration;

		public Rigidbody2D rb;

		public Animator animator;
		public int animatorParameterId;
		public AnimationState animationState;

		public BoxCollider2D spoonCollider;

		public AudioSource audioSourceGood;
		public AudioSource audioSourceBad;

		public MinigameBoilingManager boilingManager;
		public MinigameFeedingManager feedingManager;

		private void Start()
		{
			Cursor.visible = false;
			rb = GetComponent<Rigidbody2D>();
			animator = GetComponentInChildren<Animator>();
			animatorParameterId = Animator.StringToHash("State");
			animationState = AnimationState.Food;
			spoonCollider = GetComponent<BoxCollider2D>();
		}

		private void Update()
		{
			Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			rb.MovePosition(mouse);

			// Update animation state in animator component.
			animator.SetInteger(animatorParameterId, (int)animationState);

			// Reaction to animation states.
			switch (animationState)
			{
				case AnimationState.Food:
					spoonCollider.enabled = true;
					break;

				case AnimationState.NoFood:
					spoonCollider.enabled = false;
					break;
			}

			// Tick.
			timer += Time.deltaTime;

			// When we reach the point?
			if (timer >= duration) 
			{
				// Freeze timer.
				timer = duration;

				// Give food.
				animationState = AnimationState.Food;
			}
		}

		private void OnDestroy()
		{
			Cursor.visible = true;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			BoxCollider2D box = collision as BoxCollider2D;

			if (box)
			{
				audioSourceGood.Play();

				// Reactive music.
				if (boilingManager) 
				{
					boilingManager.score += 15;
					boilingManager.audioSourceGood.Play();
				}
				if (feedingManager)
				{
					feedingManager.score += 15;
					feedingManager.audioSourceGood.Play();
				}
			}
			else 
			{
				audioSourceBad.Play();

				// Reactive music.
				if (boilingManager)
				{
					boilingManager.score -= 5;
					boilingManager.audioSourceBad.Play();
				}
				if (feedingManager)
				{
					feedingManager.score -= 5;
					feedingManager.audioSourceBad.Play();
				}
			}

			animationState = AnimationState.NoFood;

			// Keep reseting until not on mouth.
			timer = 0.00f;
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			// Keep reseting until not on mouth.
			timer = 0.00f;
		}
	}
}