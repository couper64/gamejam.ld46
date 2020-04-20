namespace LD46
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.SocialPlatforms.Impl;

	// Text.
	using UnityEngine.UI;

	public class Hand : MonoBehaviour
	{
		public Rigidbody2D rb;

		public int rewardBad;
		public int rewardNotBad;
		public int rewardGood;
		public int rewardPerfect;

		public bool isSliderGrowing;
		public float sliderSpeed;
		public float sliderBadGroup;
		public float sliderNotBadGroup;
		public float sliderGoodGroup;
		public float sliderPerfectGroup;
		public Slider sliderBurp;

		public BabyBurping baby;

		public AudioSource audioSourceBad;
		public AudioSource audioSourceNotBad;
		public AudioSource audioSourceGood;
		public AudioSource audioSourcePerfect;
		public AudioSource audioSourcePatting;

		private void Start()
		{
			Cursor.visible = false;
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			rb.MovePosition(mouse);

			if (isSliderGrowing)
			{
				sliderBurp.value += Time.deltaTime * sliderSpeed;
			}
			else 
			{
				sliderBurp.value -= Time.deltaTime * sliderSpeed;
			}

			if (sliderBurp.value >= sliderBurp.maxValue)
			{
				isSliderGrowing = false;
			}
			else if (sliderBurp.value <= sliderBurp.minValue) 
			{
				isSliderGrowing = true;
			}

			if (Input.GetMouseButtonUp(0)) 
			{
				// Local variables.
				MinigameBurpingManager manager;

				// Find that manager.
				manager = FindObjectOfType<MinigameBurpingManager>();

				if (sliderBurp.value < sliderBadGroup)
				{
					baby.Burp();

					baby.audioSourceBurping.Stop();
					baby.audioSourceCrying.Play();

					audioSourceBad.Play();

					audioSourcePatting.Play();

					manager.score -= rewardBad;
				}
				else if (sliderBurp.value < sliderNotBadGroup) 
				{
					baby.Burp();

					baby.audioSourceCrying.Stop();
					baby.audioSourceBurping.Play();

					audioSourcePatting.Play();

					audioSourceNotBad.Play();

					manager.score += rewardNotBad;
				}
				else if (sliderBurp.value < sliderGoodGroup)
				{
					baby.Burp();

					baby.audioSourceCrying.Stop();
					baby.audioSourceBurping.Play();

					audioSourcePatting.Play();

					audioSourceGood.Play();

					manager.score += rewardGood;
				}
				else if (sliderBurp.value < sliderPerfectGroup)
				{
					baby.Burp();

					baby.audioSourceCrying.Stop();
					baby.audioSourceBurping.Play();

					audioSourcePatting.Play();

					audioSourcePerfect.Play();

					manager.score += rewardPerfect;
				}
				else if (sliderBurp.value < sliderBurp.maxValue - sliderNotBadGroup)
				{
					baby.Burp();

					baby.audioSourceCrying.Stop();
					baby.audioSourceBurping.Play();

					audioSourcePatting.Play();

					audioSourceGood.Play();

					manager.score += rewardGood;
				}
				else if (sliderBurp.value < sliderBurp.maxValue - sliderBadGroup)
				{
					baby.Burp();

					baby.audioSourceCrying.Stop();
					baby.audioSourceBurping.Play();

					audioSourcePatting.Play();

					audioSourceNotBad.Play();

					manager.score += rewardNotBad;
				}
				else if (sliderBurp.value < sliderBurp.maxValue)
				{
					baby.Burp();

					baby.audioSourceBurping.Stop();
					baby.audioSourceCrying.Play();

					audioSourcePatting.Play();

					audioSourceBad.Play();

					manager.score -= rewardBad;
				}
			}
		}

		private void OnDestroy()
		{
			Cursor.visible = true;
		}
	}
}