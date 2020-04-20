namespace LD46
{
    using System.Collections;

    // MonoBehaviour.
    using UnityEngine;

    public class BabyCleaning : MonoBehaviour
    {
        public float timer;
        public float duration;

        public AudioSource audioSourceCleanUp;

        public Sprite[] spriteDirts;

        private void Start()
        {
        }

        private void Update()
        {
            // Tick.
            timer += Time.deltaTime;

            // Is time to spawn a new dirty spot?
            if (timer >= duration) 
            {
                // Reset timer.
                timer = 0.00f;

                // Spawn new dirty spot.
                GameObject dirtyObject = new GameObject();
                dirtyObject.transform.parent = transform;

                dirtyObject.transform.localPosition = Random.insideUnitCircle * 2f;

                Vector3 eulerAngles = dirtyObject.transform.localEulerAngles;

                eulerAngles.z = Random.Range(0.00f, 360.00f);

                dirtyObject.transform.localEulerAngles = eulerAngles;

                DirtySpot spot = dirtyObject.AddComponent<DirtySpot>();

                spot.audioSourceCleanUp = audioSourceCleanUp;
                

                dirtyObject.AddComponent<BoxCollider2D>();

                SpriteRenderer sprite = dirtyObject.AddComponent<SpriteRenderer>();

                spot.spriteRenderer = sprite;

                sprite.sprite = spriteDirts[Random.Range(0, spriteDirts.Length)];

                sprite.sortingLayerName = "Foreground";
            }
        }
    }
}