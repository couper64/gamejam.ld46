namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    // Toggle, Slider, Text.
    using UnityEngine.UI;

    public class MinigameBurpingManager : MonoBehaviour
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

        public Hand hand;
        public float difficultyRate;

        public int penaltyThurst;
        public int penaltyCleanliness;
        public int penaltyHunger;

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
            penaltyCleanliness = Random.Range(-1, -15);
            penaltyHunger = Random.Range(-1, -15);
        }

        private void Update()
        {
            // Limit the score.
            if (score < 0 && !isScored)
            {
                score = 0;
            }

            // Burping slider depends on the success of score.
            hand.sliderSpeed = hand.sliderBurp.maxValue * score / difficultyRate;

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
                    Hand hand = FindObjectOfType<Hand>();

                    // Destroy only if exists.
                    if (hand != null) 
                    {
                        Destroy(hand.gameObject);
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
                        textBurp.text = string.Format
                        (
                            "<color=#ffffff>Burp</color>" +
                            " <color=#00FF00>+{0}</color>",
                            score
                        );
                    }
                    else
                    {
                        textBurp.text = string.Format
                        (
                            "<color=#ffffff>Burp</color>" +
                            " <color=red>{0}</color>",
                            score
                        );
                    }

                    textThurst.text = string.Format
                    (
                        "<color=#ffffff>Thurst</color> <color=red>{0}</color>",
                        penaltyThurst
                    );

                    textCleanliness.text = string.Format
                    (
                        "<color=#ffffff>Cleanliness</color> <color=red>{0}</color>",
                        penaltyCleanliness
                    );

                    textHunger.text = string.Format
                    (
                        "<color=#ffffff>Hunger</color> <color=red>{0}</color>",
                        penaltyHunger
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
                    "MinigameBurping",
                    penaltyHunger,
                    penaltyCleanliness,
                    penaltyThurst,
                    score
                );
            }
        }
    }
}
