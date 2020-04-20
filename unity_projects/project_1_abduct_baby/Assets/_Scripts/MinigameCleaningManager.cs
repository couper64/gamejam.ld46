namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    // Toggle, Slider, Text.
    using UnityEngine.UI;

    public class MinigameCleaningManager : MonoBehaviour
    {
        public float timer;
        public float duration;
        public Slider sliderTimer;

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
        public Text textHunger;
        public Text textThurst;
        public Text textCleanliness;
        public Text textBurp;

        public Text timerLabel;

        public int penaltyThurst;
        public int penaltyHunger;
        public int penaltyBurp;

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

            // Randomise penalties.
            penaltyThurst = Random.Range(-1, -15);
            penaltyHunger = Random.Range(-1, -15);
            penaltyBurp = Random.Range(-1, -15);
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
                    Sponge sponge = FindObjectOfType<Sponge>();

                    // Destroy only if exists.
                    if (sponge != null) 
                    {
                        Destroy(sponge.gameObject);
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
                        textCleanliness.text = string.Format
                        (
                            "<color=#ffffff>Cleanliness</color>" +
                            " <color=#00FF00>+{0}</color>",
                            score
                        );
                    }
                    else
                    {
                        textCleanliness.text = string.Format
                        (
                            "<color=#ffffff>Cleanliness</color>" +
                            " <color=red>{0}</color>",
                            score
                        );
                    }

                    textThurst.text = string.Format
                    (
                        "<color=#ffffff>Thurst</color> <color=red>{0}</color>",
                        penaltyThurst
                    );

                    textHunger.text = string.Format
                    (
                        "<color=#ffffff>Hunger</color> <color=red>{0}</color>",
                        penaltyHunger
                    );

                    textBurp.text = string.Format
                    (
                        "<color=#ffffff>Burp</color> <color=red>{0}</color>",
                        penaltyBurp
                    );

                    return;
                }

                // Local variabbles.
                GameManager gameManager;

                // Find.
                gameManager = FindObjectOfType<GameManager>();

                // And, return back to gameplay scene.
                gameManager.UnloadMinigame
                (
                    "MinigameCleaning",
                    penaltyHunger,
                    score,
                    penaltyThurst,
                    penaltyBurp
                );
            }
        }
    }
}
