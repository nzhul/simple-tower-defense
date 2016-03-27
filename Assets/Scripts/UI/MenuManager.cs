using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;

	public Slider[] volumeSliders;

	void Start()
	{
		volumeSliders[0].onValueChanged.AddListener(SetMasterVolume);
		volumeSliders[1].onValueChanged.AddListener(SetMusicVolume);
		volumeSliders[2].onValueChanged.AddListener(SetSfxVolume);

		volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
		volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
		volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
	}

	public void Play()
	{
		SceneManager.LoadScene("Level01");
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void OptionsMenu()
	{
		mainMenuHolder.SetActive(false);
		optionsMenuHolder.SetActive(true);
	}

	public void Back()
	{
		optionsMenuHolder.SetActive(false);
		mainMenuHolder.SetActive(true);
	}

	public void SetMasterVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
	}

	public void SetMusicVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
	}

	public void SetSfxVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
	}
}
