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
        public float thurst;
        public float cleanliness;
        public float burp;

        public Slider sliderHunger;
        public Slider sliderThurst;
        public Slider sliderCleanliness;
        public Slider sliderBurp;

        public Text textHunger;
        public Text textThurst;
        public Text textCleanliness;
        public Text textBurp;

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

            // Start game settings.
            hunger = 80.00f;
            thurst = 80.00f;
            cleanliness = 80.00f;
            burp = 80.00f;
        }

        private void Update()
        {
            // Update Baby's HUD Slider Stats.
            sliderHunger.value = hunger;
            sliderThurst.value = thurst;
            sliderCleanliness.value = cleanliness;
            sliderBurp.value = burp;

            // Update Baby's HUD Text Stats.
            textHunger.text = hunger.ToString("00");
            textThurst.text = thurst.ToString("00");
            textCleanliness.text = cleanliness.ToString("00");
            textBurp.text = burp.ToString("00");

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