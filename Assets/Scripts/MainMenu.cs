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

	void Start ()
  {

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

	public void AddSE(AudioSource newSource)
	{
		Debug.Log("sound!");
		SEaudioList.Add(newSource);
  }
}
