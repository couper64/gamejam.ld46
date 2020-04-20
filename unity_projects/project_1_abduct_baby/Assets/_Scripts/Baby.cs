namespace LD46
{
    // MonoBehaviour.
    using UnityEngine;

    // Text.
    using UnityEngine.UI;

    public class Baby : MonoBehaviour
    {
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

        private void Start()
        {
            // Start game settings.
            hunger = 80.00f;
            thurst = 80.00f;
            cleanliness = 80.00f;
            burp = 80.00f;
        }

        private void Update()
        {
            // First control the range of the score.
            hunger = Mathf.Clamp
            (
                hunger, 
                sliderHunger.minValue, 
                sliderHunger.maxValue
            );
            cleanliness = Mathf.Clamp
            (
                hunger,
                sliderCleanliness.minValue,
                sliderCleanliness.maxValue
            );
            thurst = Mathf.Clamp
            (
                hunger,
                sliderThurst.minValue,
                sliderThurst.maxValue
            );
            burp = Mathf.Clamp
            (
                hunger,
                sliderBurp.minValue,
                sliderBurp.maxValue
            );

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
        }
    }
}