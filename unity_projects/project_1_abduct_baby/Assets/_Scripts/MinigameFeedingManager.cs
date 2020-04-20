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

        public bool isScored;
        public float scoreBad;
        public float scoreNotBad;
        public float scoreGood;
        public float scorePerfect;

        public int score;
        public Slider sliderScore;

        // Whether user wants to go back to home screne,
        // after finishing the game.
        public bool isFinished;
        public GameObject panelGameOver;
        public Text gameOverLabel;

        public AudioSource audioSourceGood;
        public AudioSource audioSourceBad;

        public void SetFinished(bool finished) 
        {
            isFinished = finished;
        }

        private void Start()
        {
            // Setup slider to meet timer settings.
            sliderTimer.maxValue = duration;
            sliderTimer.value = duration;

            // Start score.
            score = 20;
        }

        private void Update()
        {
            // Limit the score.
            if (score < 0 && !isScored) 
            {
                score = 0;
            }

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

                    // Calculate score.
                    if (score < scoreBad && !isScored)
                    {
                        // Baby didn't feel full, shame.
                        score = -20;
                    }
                    else if (score < scoreNotBad && !isScored)
                    {
                        // Not bad.
                        score = 20;
                    }
                    else if (score < scoreGood && !isScored)
                    {
                        // Good.
                        score = 40;
                    }
                    else if (!isScored)
                    {
                        // Perfect!
                        score = 50;
                        score += (int)(score / scorePerfect * 1 / scorePerfect);
                    }

                    // Do scoring only once.
                    isScored = true;

                    // Prepare the message.
                    if (score > 0)
                    {
                        gameOverLabel.text = string.Format
                        (
                            "Congratulations! You gain " +
                            "<color=green>+{0}</color> points of hunger.",
                            score
                        );
                    }
                    else 
                    {
                        gameOverLabel.text = string.Format
                        (
                            "We believe in you! You gain " +
                            "<color=red>{0}</color> points of hunger.",
                            score
                        );
                    }

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
                int thrust;
                int cleanliness;
                int burp;

                // Randomise penalties.
                thrust = Random.Range(-1, -15);
                cleanliness = Random.Range(-1, -15);
                burp = Random.Range(-1, -15);

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // And, return back to gameplay scene.
                gameManager.UnloadMinigame
                (
                    "MinigameFeeding", 
                    score, 
                    cleanliness, 
                    thrust, 
                    burp
                );
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
