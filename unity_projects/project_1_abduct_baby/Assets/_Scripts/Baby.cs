namespace LD46
{
    using System;

    // MonoBehaviour.
    using UnityEngine;

    // Text.
    using UnityEngine.UI;

    public class Baby : MonoBehaviour
    {
        public enum AnimationState 
        {
            Idle = 0,
            Closed_Mouth = 1
        }

        public float hunger;
        public float cleanliness;
        public float toilet;

        public Text hungerLabel;
        public Text cleanlinessLabel;
        public Text toiletLabel;

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
            animator = GetComponentInChildren<Animator>();
            animationState = AnimationState.Idle;
            animatorParameterId = Animator.StringToHash("State");

            mouthCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            hungerLabel.text = string.Format("Hunger: {0}", hunger.ToString("0.0"));
            cleanlinessLabel.text = string.Format
            (
                "Cleanliness: {0}", 
                cleanliness.ToString("0.0")
            );
            toiletLabel.text = string.Format("Toilet: {0}", toilet.ToString("0.0"));

            animator.SetInteger(animatorParameterId, (int)animationState);

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            hunger++;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
        }
    }
}