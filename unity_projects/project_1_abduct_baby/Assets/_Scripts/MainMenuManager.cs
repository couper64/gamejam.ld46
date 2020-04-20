namespace LD46
{
	// MonoBehaviour.
	using UnityEngine;

	// SceneManager.
	using UnityEngine.SceneManagement;

	public class MainMenuManager : MonoBehaviour
	{
		public AudioSource audioSourceMouse;

		public void StartGame() 
		{
			SceneManager.LoadSceneAsync("GamePlay", LoadSceneMode.Single);
		}

		public void ExitGame() 
		{
			Application.Quit();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				audioSourceMouse.Play();
			}
		}
	}
}