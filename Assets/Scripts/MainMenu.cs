using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour {


	public Slider masterSlider;
	public Slider effectsSlider;
	public Slider ambienceSlider;
	public Slider musicSlider;
  public float volume;
	public List<AudioSource> SEaudioList;
	public AudioSource[] AMBaudioList;
	public AudioSource[] MUSaudioList;
	public GameObject[] pauseObjects;
	public GameObject[] unpauseObjects;

	void Start ()
  {
		Time.timeScale = 1;
		// pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
	}

	// Update is called once per frame
	void Update () {

		//uses the p button to pause and unpause the game
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
		{
			pauseControl();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		Time.timeScale = 0;
		foreach(GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
		foreach(GameObject g in unpauseObjects)
		{
			g.SetActive(false);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		Time.timeScale = 1;
		foreach(GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
		foreach(GameObject g in unpauseObjects)
		{
			g.SetActive(true);
		}
	}

	public void pauseControl(){
		if(Time.timeScale == 1)
		{
			showPaused();
		} else if (Time.timeScale == 0){
			hidePaused();
		}
	}

	public void MMUpdateSound ()
	{
		//GameObject.FindGameObjectWithTag ("GameController").GetComponent<AudioSource> ().volume = PlayerPrefsManager.GetMasterVolume () * PlayerPrefsManager.GetMusicVolume ();
		//GameObject.Find ("WavesSounds").GetComponent<AudioSource> ().volume = PlayerPrefsManager.GetMasterVolume () * PlayerPrefsManager.GetAmbienceVolume ();
		//GameObject.Find ("FireCrackleSounds").GetComponent<AudioSource> ().volume = PlayerPrefsManager.GetMasterVolume () * PlayerPrefsManager.GetSoundEffectVolume ();
		//GameObject.Find ("Misc Sounds").GetComponent<AudioSource> ().volume = PlayerPrefsManager.GetMasterVolume () * PlayerPrefsManager.GetSoundEffectVolume ();

		for (int i = 0; i < SEaudioList.Count; i++){
			SEaudioList[i].volume = masterSlider.value ;
		}
		foreach (AudioSource source in AMBaudioList) {
			source.volume = masterSlider.value ;
		}
		foreach (AudioSource source in MUSaudioList) {
			source.volume = masterSlider.value;
		}

	}



	public void OnMasterSliderChange ()
	{
    volume = masterSlider.value;
		MMUpdateSound ();
	}
	public void OnEffectsSliderChange ()
	{
		MMUpdateSound ();
	}
	public void OnMusicSliderChange ()
	{
		MMUpdateSound ();
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}

	public void GameOverClick()
	{
		SceneManager.LoadScene("SplashScreen");
	}

	public void AddSE(AudioSource newSource)
	{
		Debug.Log("sound!");
		SEaudioList.Add(newSource);
  }
}
