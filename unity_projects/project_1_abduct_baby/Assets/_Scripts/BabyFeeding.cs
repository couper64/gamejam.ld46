namespace LD46
{
    using System;

    // MonoBehaviour.
    using UnityEngine;

    public class BabyFeeding : MonoBehaviour
    {
        public enum AnimationState
        {
            Idle = 0,
            Closed_Mouth = 1
        }

        public Animator animator;
        public AnimationState animationState;
        public int animatorParameterId;

        public BoxCollider2D mouthCollider;

        public void SetDefaultMood()
        {
            animationState = AnimationState.Idle;
        }

        public void SetRandomMood()
        {
            // Convert into an array to pick up randomly from.
            Array states = Enum.GetValues(typeof(AnimationState));

            // Generate random index to pick.
            int randomIndex = UnityEngine.Random.Range(0, states.Length);

            // New animation state.
            animationState = (AnimationState)states.GetValue(randomIndex);
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            animationState = AnimationState.Idle;
            animatorParameterId = Animator.StringToHash("State");

            mouthCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            // Update animation state in animator component.
            animator.SetInteger(animatorParameterId, (int)animationState);

            // Reaction to animation states.
            switch (animationState)
            {
                case AnimationState.Idle:
                    mouthCollider.enabled = true;
                    break;

                case AnimationState.Closed_Mouth:
                    mouthCollider.enabled = false;
                    break;
            }
        }
    }
}