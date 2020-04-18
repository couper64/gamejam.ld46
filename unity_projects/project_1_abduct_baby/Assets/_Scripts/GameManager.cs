namespace LD46
{
    // Monobehaviour.
    using UnityEngine;

    // Text.
    using UnityEngine.UI;

    // SceneManager.
    using UnityEngine.SceneManagement;
    using System.Collections;
    using System.Threading;

    /// <summary>
    /// One script to rule them all.
    /// Days.
    /// Iterate over days.
    /// End of the game.
    /// Each day consists out of events, which lead to a minigame.
    /// Multiple type of events depend on baby's state.
    /// Mutliple type of minigames depend on the type of the event.
    /// Every minigames is time limited.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public int dayCount;
        public int dayFinal;
        public Text timerLabel;
        public Text userLog;
        public Button buttonMinigameFeeding;
        public Button buttonMinigameBoiling;
        public GameObject panelLeft;
        public GameObject panelRight;
        public GameObject panelBottom;
        public AudioSource mouseAudioSource;
        public Baby baby;

        public void GameOver() 
        {
            // Local variables.
            string message;

            // Prepare.
            message = "Game Over.";

            // Output.
            userLog.text = message;

            // Stop updating.
            enabled = false;
        }

        public void LoadMinigameFeeding() 
        {
            SceneManager.LoadSceneAsync("MinigameFeeding", LoadSceneMode.Additive);

            panelLeft.SetActive(false);
            panelRight.SetActive(false);
            panelBottom.SetActive(false);

            StartCoroutine(PlayMinigameFeeding());
        }

        public void LoadMinigameBoiling() 
        {
            SceneManager.LoadSceneAsync("MinigameBoiling", LoadSceneMode.Additive);

            panelLeft.SetActive(false);
            panelRight.SetActive(false);
            panelBottom.SetActive(false);
        }

        private IEnumerator PlayMinigameFeeding() 
        {
            float timer = 0.00f;
            float moodTimer = 0.00f;
            float delay = 60.00f;
            float moodDelay = 3.00f;
            BoxCollider2D collider = baby.GetComponent<BoxCollider2D>();
            timerLabel.gameObject.SetActive(true);

            while (true)
            {
                timer += Time.deltaTime;
                moodTimer += Time.deltaTime;

                if (moodTimer >= moodDelay) 
                {
                    collider.enabled = !collider.enabled;

                    if (collider.enabled)
                    {
                        baby.animationState = Baby.AnimationState.Idle;
                    }
                    else 
                    {
                        baby.animationState = Baby.AnimationState.Closed_Mouth;
                    }

                    moodTimer = 0.00f;
                }

                if (timer >= delay) 
                {
                    break;
                }

                timerLabel.text = (delay - timer).ToString("00");

                yield return new WaitForEndOfFrame();
            }

            SceneManager.UnloadSceneAsync("MinigameFeeding");

            panelLeft.SetActive(true);
            panelRight.SetActive(true);
            panelBottom.SetActive(true);

            collider.enabled = false;

            timerLabel.gameObject.SetActive(false);
        }

        private void Start()
        {
            buttonMinigameFeeding.onClick.AddListener(LoadMinigameFeeding);
            buttonMinigameFeeding.GetComponentInChildren<Text>().text = "Feed Baby";

            buttonMinigameBoiling.onClick.AddListener(LoadMinigameBoiling);
            buttonMinigameBoiling.GetComponentInChildren<Text>().text = "Boil Milk";

            userLog.text = string.Format("It's a happy day {0}!", ++dayCount);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                mouseAudioSource.Play();
            }

            if (dayCount >= dayFinal) 
            {
                GameOver();
            }
        }
    }
}