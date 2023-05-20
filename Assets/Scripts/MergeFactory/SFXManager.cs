using System;
using UnityEngine;

namespace MergeFactory
{
	public class SFXManager : MonoBehaviour
	{
		public static SFXManager instance;

		public AudioSource coinPopAudioSource;

		public AudioSource[] audios;

		private AudioSource audioSource;

		public AudioClip[] soundClips;

		public void Mute(bool mute)
		{
			for (int i = 0; i < this.audios.Length; i++)
			{
				this.audios[i].mute = mute;
			}
		}

		private void OnEnable()
		{
			if (UnityEngine.Object.FindObjectsOfType<SFXManager>().Length > 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			if (!SFXManager.instance)
			{
				SFXManager.instance = this;
			}
			this.audioSource = base.GetComponent<AudioSource>();
		}

		public void PlayCoinPopSound()
		{
			if (!this.coinPopAudioSource.isPlaying)
			{
				this.coinPopAudioSource.PlayOneShot(this.coinPopAudioSource.clip);
			}
		}

		public void PlaySound(Sound sound)
		{
			this.PlaySound((int)sound);
		}

		public void PlaySound(int index)
		{
			if (index < this.soundClips.Length && this.audioSource != null)
			{
				this.audioSource.PlayOneShot(this.soundClips[index]);
			}
		}
	}
}
