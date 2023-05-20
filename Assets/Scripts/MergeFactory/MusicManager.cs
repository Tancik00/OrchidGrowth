using System;
using UnityEngine;

namespace MergeFactory
{
	public class MusicManager : MonoBehaviour
	{
		public static MusicManager instance;

		private AudioSource audioSource;

		public AudioClip[] musicClips;

		private void OnEnable()
		{
			if (UnityEngine.Object.FindObjectsOfType<MusicManager>().Length > 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			if (!MusicManager.instance)
			{
				MusicManager.instance = this;
			}
			this.audioSource = base.GetComponent<AudioSource>();
		}

		public void Mute(bool mute)
		{
			this.audioSource.mute = mute;
		}

		public void PlayMusic(int index)
		{
			this.audioSource.clip = this.musicClips[index];
			this.audioSource.Play();
		}
	}
}
