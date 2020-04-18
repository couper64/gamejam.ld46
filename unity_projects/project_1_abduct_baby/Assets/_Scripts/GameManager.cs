namespace LD46
{
    // Coroutines.
    using System.Collections;

    // List.
    using System.Collections.Generic;

    // Monobehaviour.
    using UnityEngine;

    // Text.
    using UnityEngine.UI;

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
        public Text userLogLabel;
        public List<Day> days;

        private IEnumerator LoopOverDays() 
        {
            foreach (Day day in days)
            {
                // Local variables.
                string message;

                // Prepare.
                message = string.Format("It's day {0}.", day.dayNumber);

                // Output.
                userLogLabel.text = message;

                // Wait till the end of this day.
                yield return new WaitForSeconds(2.00f);
            }

            // End of the game.
            userLogLabel.text = "Game Over.";
        }

        private void Start()
        {
            // Entry point.
            StartCoroutine(LoopOverDays());
        }
    }

    [System.Serializable]
    public class Day 
    {
        // Global day counter. Counts how many times 
        // this object was created.
        private static int dayCount;

        public int dayNumber;

        public Day() 
        {
            dayNumber = dayCount++;
        }
    }
}