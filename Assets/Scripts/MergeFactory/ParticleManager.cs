using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MergeFactory
{
	public class ParticleManager : MonoBehaviour
	{
		private sealed class _DestroyParticle_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal GameObject obj;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _DestroyParticle_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					UnityEngine.Object.Destroy(this.obj);
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		public static ParticleManager instance;

		public GameObject levelPointsAddParticle;

		public GameObject boxOpenedParticle;

		public GameObject boxOpenedParticle_Small;

		public GameObject newLand;

		public Transform particleParentTransform;

		private void Awake()
		{
			ParticleManager.instance = this;
		}

		public void LevelIncShowParticle(Vector3 pos, int _inc)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.levelPointsAddParticle, this.levelPointsAddParticle.gameObject.transform.parent);
			gameObject.transform.localScale = Vector3.one;
			gameObject.GetComponent<TweenPosition>().from = pos;
			gameObject.GetComponent<TweenPosition>().enabled = true;
			gameObject.GetComponent<ScorePointsToAdd>().pointsToAdd = _inc;
			gameObject.SetActive(true);
		}

		public void ShowNewLandPartilecs(Vector3 pos)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.newLand, this.particleParentTransform);
			gameObject.transform.position = pos;
			gameObject.SetActive(true);
			UnityEngine.Object.Destroy(gameObject, 3f);
		}

		public void ShowBoxOpenedParticle(Vector3 pos)
		{
			GameObject gameObject;
			if (DataProvider.instance.gameData.maxGridSlots > 16)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.boxOpenedParticle_Small, this.particleParentTransform);
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.boxOpenedParticle, this.particleParentTransform);
			}
			gameObject.transform.position = pos;
			gameObject.SetActive(true);
			base.StartCoroutine(this.DestroyParticle(gameObject));
		}

		private IEnumerator DestroyParticle(GameObject obj)
		{
			ParticleManager._DestroyParticle_c__Iterator0 _DestroyParticle_c__Iterator = new ParticleManager._DestroyParticle_c__Iterator0();
			_DestroyParticle_c__Iterator.obj = obj;
			return _DestroyParticle_c__Iterator;
		}

		public void OnParticleEnd(GameObject obj)
		{
			obj.SetActive(false);
			LevelManager.instance.AnimateLevelNo();
			LevelManager.instance.IncLevelValue(obj.GetComponent<ScorePointsToAdd>().pointsToAdd);
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}
}
