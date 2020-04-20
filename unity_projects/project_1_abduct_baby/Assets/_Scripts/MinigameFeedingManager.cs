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
        public Slider sliderTimer;

        public float moodTimer;
        public float moodDuration;
        public float moodMaxDuration;

        public int score;
        public Slider sliderScore;

        // Whether user wants to go back to home screne,
        // after finishing the game.
        public bool isFinished;
        public GameObject panelGameOver;
        public Text gameOverLabel;

        public void SetFinished(bool finished) 
        {
            isFinished = finished;
        }

        private void Start()
        {
            // Setup slider to meet timer settings.
            sliderTimer.maxValue = duration;
            sliderTimer.value = duration;
        }

        private void Update()
        {
            // Update user with a new score.
            sliderScore.value = score;

            // Minigame ticking.
            timer += Time.deltaTime;

            // Let user know.
            sliderTimer.value = duration - timer;

            // Is it the time to stop the game?
            if (timer >= duration)
            {
                // Freeze timer.
                timer = duration;

                // Prevent mood changes and scene unloading.
                if (!isFinished) 
                {
                    // Find and destroy spoon.
                    Spoon spoon = FindObjectOfType<Spoon>();

                    // Destroy only if exists.
                    if (spoon != null) 
                    {
                        Destroy(spoon.gameObject);
                    }

                    // Show cursor.
                    Cursor.visible = true;

                    // Show GameOver panel.
                    panelGameOver.SetActive(true);

                    // Prepare the message.
                    gameOverLabel.text = string.Format
                    (
                        "Congratulations! You gain " + 
                        "<color=green>+{0}</color> points of hunger.",
                        score
                    );

                    return;
                }

                // Local variabbles.
                BabyFeeding babyFeeding;

                // Find.
                babyFeeding = FindObjectOfType<BabyFeeding>();

                // Tell manager to set default mood for baby.
                babyFeeding.SetDefaultMood();

                // Local variabbles.
                GameManager gameManager;

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // And, return back to gameplay scene.
                gameManager.UnloadMinigame("MinigameFeeding", score, 0, 0, 0);
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
                BabyFeeding babyFeeding;

                // Find.
                babyFeeding = FindObjectOfType<BabyFeeding>();

                // Tell manager to set new random mood for baby.
                babyFeeding.SetRandomMood();
            }
        }
    }
}
