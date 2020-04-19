namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    // Toggle, Slider, Text.
    using UnityEngine.UI;

    public class MinigameFeedingManager : MonoBehaviour
    {
        public float timer;
        public float duration;

        public float moodTimer;
        public float moodDuration;
        public float moodMaxDuration;

        public Text timerLabel;

        private void Update()
        {
            // Minigame ticking.
            timer += Time.deltaTime;

            // Let user know.
            timerLabel.text = (duration - timer).ToString("00");

            // Is it the time to stop the game?
            if (timer >= duration)
            {
                // Local variabbles.
                GameManager gameManager;

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // Tell manager to set default mood for baby.
                gameManager.baby.SetDefaultMood();

                // And, return back to gameplay scene.
                gameManager.UnloadMinigame("MinigameFeeding");
            }

            // Mood ticking.
            moodTimer += Time.deltaTime;

            // Is it time to change the mood?
            if (moodTimer >= moodDuration) 
            {
                // Reset timer and duration.
                moodTimer = 0.00f;
                moodDuration = Random.Range(1.00f, moodMaxDuration);

                // Local variabbles.
                GameManager gameManager;

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // Tell manager to set new random mood for baby.
                gameManager.baby.SetRandomMood();
            }
        }
    }
}
