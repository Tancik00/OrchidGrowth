using System;
using UnityEngine;

namespace TapticPlugin
{
	[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
	public class SampleBall : MonoBehaviour
	{
		public AudioClip se;

		private Vector3 startPosition;

		private AudioSource audioSource;

		private void OnEnable()
		{
			this.startPosition = base.transform.position;
			this.audioSource = base.GetComponent<AudioSource>();
		}

		private void OnCollisionEnter(Collision collision)
		{
			float magnitude = collision.relativeVelocity.magnitude;
			this.audioSource.PlayOneShot(this.se, magnitude);
			UnityEngine.Debug.LogFormat("hit :{0}", new object[]
			{
				magnitude
			});
			ImpactFeedback feedback;
			if (magnitude < 2f)
			{
				feedback = ImpactFeedback.Light;
			}
			else if (magnitude < 5f)
			{
				feedback = ImpactFeedback.Midium;
			}
			else
			{
				feedback = ImpactFeedback.Heavy;
			}
			TapticManager.Impact(feedback);
		}

		public void Retey()
		{
			base.transform.position = this.startPosition;
		}
	}
}
