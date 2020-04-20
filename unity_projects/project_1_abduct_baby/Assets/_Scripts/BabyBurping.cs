namespace LD46
{
    using System.Collections;

    // MonoBehaviour.
    using UnityEngine;

    public class BabyBurping : MonoBehaviour
    {
        public enum AnimationState
        {
            Idle = 0,
            Burping = 1
        }

        public Animator animator;
        public AnimationState animationState;
        public int animatorParameterId;

        public float burpDuration;

        public AudioSource audioSourceBurping;
        public AudioSource audioSourceCrying;

        private IEnumerator BurpFor(float seconds) 
        {
            animationState = AnimationState.Burping;

            yield return new WaitForSeconds(seconds);

            animationState = AnimationState.Idle;
        }

        public void Burp() 
        {
            StartCoroutine(BurpFor(burpDuration));
        }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            animationState = AnimationState.Idle;
            animatorParameterId = Animator.StringToHash("State");
        }

        private void Update()
        {
            // Update animation state in animator component.
            animator.SetInteger(animatorParameterId, (int)animationState);

            // Reaction to animation states.
            switch (animationState)
            {
                case AnimationState.Idle:
                    break;

                case AnimationState.Burping:
                    break;
            }
        }
    }
}