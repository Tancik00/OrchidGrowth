using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class WeaponXITools
{
	private static AudioListener mListener;

	private static bool mLoaded = false;

	private static float mGlobalVolume = 1f;

	private static float mLastTimestamp = 0f;

	private static AudioClip mLastClip;

	private static Vector3[] mSides = new Vector3[4];

	public static KeyCode[] keys = new KeyCode[]
	{
		KeyCode.Backspace,
		KeyCode.Tab,
		KeyCode.Clear,
		KeyCode.Return,
		KeyCode.Pause,
		KeyCode.Escape,
		KeyCode.Space,
		KeyCode.Exclaim,
		KeyCode.DoubleQuote,
		KeyCode.Hash,
		KeyCode.Dollar,
		KeyCode.Ampersand,
		KeyCode.Quote,
		KeyCode.LeftParen,
		KeyCode.RightParen,
		KeyCode.Asterisk,
		KeyCode.Plus,
		KeyCode.Comma,
		KeyCode.Minus,
		KeyCode.Period,
		KeyCode.Slash,
		KeyCode.Alpha0,
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
		KeyCode.Colon,
		KeyCode.Semicolon,
		KeyCode.Less,
		KeyCode.Equals,
		KeyCode.Greater,
		KeyCode.Question,
		KeyCode.At,
		KeyCode.LeftBracket,
		KeyCode.Backslash,
		KeyCode.RightBracket,
		KeyCode.Caret,
		KeyCode.Underscore,
		KeyCode.BackQuote,
		KeyCode.A,
		KeyCode.B,
		KeyCode.C,
		KeyCode.D,
		KeyCode.E,
		KeyCode.F,
		KeyCode.G,
		KeyCode.H,
		KeyCode.I,
		KeyCode.J,
		KeyCode.K,
		KeyCode.L,
		KeyCode.M,
		KeyCode.N,
		KeyCode.O,
		KeyCode.P,
		KeyCode.Q,
		KeyCode.R,
		KeyCode.S,
		KeyCode.T,
		KeyCode.U,
		KeyCode.V,
		KeyCode.W,
		KeyCode.X,
		KeyCode.Y,
		KeyCode.Z,
		KeyCode.Delete,
		KeyCode.Keypad0,
		KeyCode.Keypad1,
		KeyCode.Keypad2,
		KeyCode.Keypad3,
		KeyCode.Keypad4,
		KeyCode.Keypad5,
		KeyCode.Keypad6,
		KeyCode.Keypad7,
		KeyCode.Keypad8,
		KeyCode.Keypad9,
		KeyCode.KeypadPeriod,
		KeyCode.KeypadDivide,
		KeyCode.KeypadMultiply,
		KeyCode.KeypadMinus,
		KeyCode.KeypadPlus,
		KeyCode.KeypadEnter,
		KeyCode.KeypadEquals,
		KeyCode.UpArrow,
		KeyCode.DownArrow,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.Insert,
		KeyCode.Home,
		KeyCode.End,
		KeyCode.PageUp,
		KeyCode.PageDown,
		KeyCode.F1,
		KeyCode.F2,
		KeyCode.F3,
		KeyCode.F4,
		KeyCode.F5,
		KeyCode.F6,
		KeyCode.F7,
		KeyCode.F8,
		KeyCode.F9,
		KeyCode.F10,
		KeyCode.F11,
		KeyCode.F12,
		KeyCode.F13,
		KeyCode.F14,
		KeyCode.F15,
		KeyCode.Numlock,
		KeyCode.CapsLock,
		KeyCode.ScrollLock,
		KeyCode.RightShift,
		KeyCode.LeftShift,
		KeyCode.RightControl,
		KeyCode.LeftControl,
		KeyCode.RightAlt,
		KeyCode.LeftAlt,
		KeyCode.Mouse3,
		KeyCode.Mouse4,
		KeyCode.Mouse5,
		KeyCode.Mouse6,
		KeyCode.JoystickButton0,
		KeyCode.JoystickButton1,
		KeyCode.JoystickButton2,
		KeyCode.JoystickButton3,
		KeyCode.JoystickButton4,
		KeyCode.JoystickButton5,
		KeyCode.JoystickButton6,
		KeyCode.JoystickButton7,
		KeyCode.JoystickButton8,
		KeyCode.JoystickButton9,
		KeyCode.JoystickButton10,
		KeyCode.JoystickButton11,
		KeyCode.JoystickButton12,
		KeyCode.JoystickButton13,
		KeyCode.JoystickButton14,
		KeyCode.JoystickButton15,
		KeyCode.JoystickButton16,
		KeyCode.JoystickButton17,
		KeyCode.JoystickButton18,
		KeyCode.JoystickButton19
	};

	public static float soundVolume
	{
		get
		{
			if (!WeaponXITools.mLoaded)
			{
				WeaponXITools.mLoaded = true;
				WeaponXITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return WeaponXITools.mGlobalVolume;
		}
		set
		{
			if (WeaponXITools.mGlobalVolume != value)
			{
				WeaponXITools.mLoaded = true;
				WeaponXITools.mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	public static bool fileAccess
	{
		get
		{
			return Application.platform != RuntimePlatform.WebGLPlayer && Application.platform != RuntimePlatform.WebGLPlayer;
		}
	}

	public static string clipboard
	{
		get
		{
			TextEditor textEditor = new TextEditor();
			textEditor.Paste();
			return textEditor.content.text;
		}
		set
		{
			TextEditor textEditor = new TextEditor();
			textEditor.content = new GUIContent(value);
			textEditor.OnFocus();
			textEditor.Copy();
		}
	}

	public static Vector2 screenSize
	{
		get
		{
			return new Vector2((float)Screen.width, (float)Screen.height);
		}
	}

	public static AudioSource PlaySound(AudioClip clip)
	{
		return WeaponXITools.PlaySound(clip, 1f, 1f);
	}

	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return WeaponXITools.PlaySound(clip, volume, 1f);
	}

	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		float time = RealTime.time;
		if (WeaponXITools.mLastClip == clip && WeaponXITools.mLastTimestamp + 0.1f > time)
		{
			return null;
		}
		WeaponXITools.mLastClip = clip;
		WeaponXITools.mLastTimestamp = time;
		volume *= WeaponXITools.soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (WeaponXITools.mListener == null || !WeaponXITools.GetActive(WeaponXITools.mListener))
			{
				AudioListener[] array = UnityEngine.Object.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (WeaponXITools.GetActive(array[i]))
						{
							WeaponXITools.mListener = array[i];
							break;
						}
					}
				}
				if (WeaponXITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = (UnityEngine.Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (camera != null)
					{
						WeaponXITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (WeaponXITools.mListener != null && WeaponXITools.mListener.enabled && WeaponXITools.GetActive(WeaponXITools.mListener.gameObject))
			{
				AudioSource audioSource = WeaponXITools.mListener.GetComponent<AudioSource>();
				if (audioSource == null)
				{
					audioSource = WeaponXITools.mListener.gameObject.AddComponent<AudioSource>();
				}
				audioSource.priority = 50;
				audioSource.pitch = pitch;
				audioSource.PlayOneShot(clip, volume);
				return audioSource;
			}
		}
		return null;
	}

	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return UnityEngine.Random.Range(min, max + 1);
	}

	public static string GetHierarchy(GameObject obj)
	{
		if (obj == null)
		{
			return string.Empty;
		}
		string text = obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			text = obj.name + "\\" + text;
		}
		return text;
	}

	public static T[] FindActive<T>() where T : Component
	{
		return UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
	}

	public static string GetTypeName<T>()
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static string GetTypeName(UnityEngine.Object obj)
	{
		if (obj == null)
		{
			return "Null";
		}
		string text = obj.GetType().ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static void RegisterUndo(UnityEngine.Object obj, string name)
	{
	}

	public static void SetDirty(UnityEngine.Object obj)
	{
	}

	public static GameObject AddChild(GameObject parent)
	{
		return WeaponXITools.AddChild(parent, true);
	}

	public static GameObject AddChild(GameObject parent, bool undo)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
		if (gameObject != null && parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	public static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			child.gameObject.layer = layer;
			WeaponXITools.SetChildLayer(child, layer);
		}
	}

	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject gameObject = WeaponXITools.AddChild(parent);
		gameObject.name = WeaponXITools.GetTypeName<T>();
		return gameObject.AddComponent<T>();
	}

	public static T AddChild<T>(GameObject parent, bool undo) where T : Component
	{
		GameObject gameObject = WeaponXITools.AddChild(parent, undo);
		gameObject.name = WeaponXITools.GetTypeName<T>();
		return gameObject.AddComponent<T>();
	}

	public static GameObject GetRoot(GameObject go)
	{
		Transform transform = go.transform;
		while (true)
		{
			Transform parent = transform.parent;
			if (parent == null)
			{
				break;
			}
			transform = parent;
		}
		return transform.gameObject;
	}

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return (T)((object)null);
		}
		T component = go.GetComponent<T>();
		if (component == null)
		{
			Transform parent = go.transform.parent;
			while (parent != null && component == null)
			{
				component = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return component;
	}

	public static T FindInParents<T>(Transform trans) where T : Component
	{
		if (trans == null)
		{
			return (T)((object)null);
		}
		return trans.GetComponentInParent<T>();
	}

	public static void Destroy(UnityEngine.Object obj)
	{
		if (obj)
		{
			if (obj is Transform)
			{
				Transform transform = obj as Transform;
				GameObject gameObject = transform.gameObject;
				if (Application.isPlaying)
				{
					transform.parent = null;
					UnityEngine.Object.Destroy(gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
			}
			else if (obj is GameObject)
			{
				GameObject gameObject2 = obj as GameObject;
				Transform transform2 = gameObject2.transform;
				if (Application.isPlaying)
				{
					transform2.parent = null;
					UnityEngine.Object.Destroy(gameObject2);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(gameObject2);
				}
			}
			else if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}

	public static void DestroyChildren(this Transform t)
	{
		bool isPlaying = Application.isPlaying;
		while (t.childCount != 0)
		{
			Transform child = t.GetChild(0);
			if (isPlaying)
			{
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(child.gameObject);
			}
		}
	}

	public static void DestroyImmediate(UnityEngine.Object obj)
	{
		if (obj != null)
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			else
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
	}

	public static void Broadcast(string funcName)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	public static bool IsChild(Transform parent, Transform child)
	{
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.parent;
		}
		return false;
	}

	private static void Activate(Transform t)
	{
		WeaponXITools.Activate(t, false);
	}

	private static void Activate(Transform t, bool compatibilityMode)
	{
		WeaponXITools.SetActiveSelf(t.gameObject, true);
		if (compatibilityMode)
		{
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				Transform child = t.GetChild(i);
				if (child.gameObject.activeSelf)
				{
					return;
				}
				i++;
			}
			int j = 0;
			int childCount2 = t.childCount;
			while (j < childCount2)
			{
				Transform child2 = t.GetChild(j);
				WeaponXITools.Activate(child2, true);
				j++;
			}
		}
	}

	private static void Deactivate(Transform t)
	{
		WeaponXITools.SetActiveSelf(t.gameObject, false);
	}

	public static void SetActiveChildren(GameObject go, bool state)
	{
		Transform transform = go.transform;
		if (state)
		{
			int i = 0;
			int childCount = transform.childCount;
			while (i < childCount)
			{
				Transform child = transform.GetChild(i);
				WeaponXITools.Activate(child);
				i++;
			}
		}
		else
		{
			int j = 0;
			int childCount2 = transform.childCount;
			while (j < childCount2)
			{
				Transform child2 = transform.GetChild(j);
				WeaponXITools.Deactivate(child2);
				j++;
			}
		}
	}

	[Obsolete("Use WeaponXITools.GetActive instead")]
	public static bool IsActive(Behaviour mb)
	{
		return mb != null && mb.enabled && mb.gameObject.activeInHierarchy;
	}

	[DebuggerStepThrough]
	public static bool GetActive(Behaviour mb)
	{
		return mb && mb.enabled && mb.gameObject.activeInHierarchy;
	}

	[DebuggerStepThrough]
	public static bool GetActive(GameObject go)
	{
		return go && go.activeInHierarchy;
	}

	[DebuggerStepThrough]
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	public static void SetLayer(GameObject go, int layer)
	{
		go.layer = layer;
		Transform transform = go.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			Transform child = transform.GetChild(i);
			WeaponXITools.SetLayer(child.gameObject, layer);
			i++;
		}
	}

	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	public static bool Save(string fileName, byte[] bytes)
	{
		if (!WeaponXITools.fileAccess)
		{
			return false;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (bytes == null)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			return true;
		}
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(path);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
			return false;
		}
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
		return true;
	}

	public static byte[] Load(string fileName)
	{
		if (!WeaponXITools.fileAccess)
		{
			return null;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}
		return null;
	}

	public static Color ApplyPMA(Color c)
	{
		if (c.a != 1f)
		{
			c.r *= c.a;
			c.g *= c.a;
			c.b *= c.a;
		}
		return c;
	}

	public static T AddMissingComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	public static Vector3[] GetSides(this Camera cam)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), null);
	}

	public static Vector3[] GetSides(this Camera cam, float depth)
	{
		return cam.GetSides(depth, null);
	}

	public static Vector3[] GetSides(this Camera cam, Transform relativeTo)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	public static Vector3[] GetSides(this Camera cam, float depth, Transform relativeTo)
	{
		if (cam.orthographic)
		{
			float orthographicSize = cam.orthographicSize;
			float num = -orthographicSize;
			float num2 = orthographicSize;
			float y = -orthographicSize;
			float y2 = orthographicSize;
			Rect rect = cam.rect;
			Vector2 screenSize = WeaponXITools.screenSize;
			float num3 = screenSize.x / screenSize.y;
			num3 *= rect.width / rect.height;
			num *= num3;
			num2 *= num3;
			Transform transform = cam.transform;
			Quaternion rotation = transform.rotation;
			Vector3 position = transform.position;
			int num4 = Mathf.RoundToInt(screenSize.x);
			int num5 = Mathf.RoundToInt(screenSize.y);
			if ((num4 & 1) == 1)
			{
				position.x -= 1f / screenSize.x;
			}
			if ((num5 & 1) == 1)
			{
				position.y += 1f / screenSize.y;
			}
			WeaponXITools.mSides[0] = rotation * new Vector3(num, 0f, depth) + position;
			WeaponXITools.mSides[1] = rotation * new Vector3(0f, y2, depth) + position;
			WeaponXITools.mSides[2] = rotation * new Vector3(num2, 0f, depth) + position;
			WeaponXITools.mSides[3] = rotation * new Vector3(0f, y, depth) + position;
		}
		else
		{
			WeaponXITools.mSides[0] = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, depth));
			WeaponXITools.mSides[1] = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, depth));
			WeaponXITools.mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth));
			WeaponXITools.mSides[3] = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, depth));
		}
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				WeaponXITools.mSides[i] = relativeTo.InverseTransformPoint(WeaponXITools.mSides[i]);
			}
		}
		return WeaponXITools.mSides;
	}

	public static Vector3[] GetWorldCorners(this Camera cam)
	{
		float depth = Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f);
		return cam.GetWorldCorners(depth, null);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, float depth)
	{
		return cam.GetWorldCorners(depth, null);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, Transform relativeTo)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, float depth, Transform relativeTo)
	{
		if (cam.orthographic)
		{
			float orthographicSize = cam.orthographicSize;
			float num = -orthographicSize;
			float num2 = orthographicSize;
			float y = -orthographicSize;
			float y2 = orthographicSize;
			Rect rect = cam.rect;
			Vector2 screenSize = WeaponXITools.screenSize;
			float num3 = screenSize.x / screenSize.y;
			num3 *= rect.width / rect.height;
			num *= num3;
			num2 *= num3;
			Transform transform = cam.transform;
			Quaternion rotation = transform.rotation;
			Vector3 position = transform.position;
			WeaponXITools.mSides[0] = rotation * new Vector3(num, y, depth) + position;
			WeaponXITools.mSides[1] = rotation * new Vector3(num, y2, depth) + position;
			WeaponXITools.mSides[2] = rotation * new Vector3(num2, y2, depth) + position;
			WeaponXITools.mSides[3] = rotation * new Vector3(num2, y, depth) + position;
		}
		else
		{
			WeaponXITools.mSides[0] = cam.ViewportToWorldPoint(new Vector3(0f, 0f, depth));
			WeaponXITools.mSides[1] = cam.ViewportToWorldPoint(new Vector3(0f, 1f, depth));
			WeaponXITools.mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 1f, depth));
			WeaponXITools.mSides[3] = cam.ViewportToWorldPoint(new Vector3(1f, 0f, depth));
		}
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				WeaponXITools.mSides[i] = relativeTo.InverseTransformPoint(WeaponXITools.mSides[i]);
			}
		}
		return WeaponXITools.mSides;
	}

	public static string GetFuncName(object obj, string method)
	{
		if (obj == null)
		{
			return "<null>";
		}
		string text = obj.GetType().ToString();
		int num = text.LastIndexOf('/');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		return (!string.IsNullOrEmpty(method)) ? (text + "/" + method) : text;
	}

	public static void Execute<T>(GameObject go, string funcName) where T : Component
	{
		T[] components = go.GetComponents<T>();
		T[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			T t = array[i];
			MethodInfo method = t.GetType().GetMethod(funcName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(t, null);
			}
		}
	}

	public static void ExecuteAll<T>(GameObject root, string funcName) where T : Component
	{
		WeaponXITools.Execute<T>(root, funcName);
		Transform transform = root.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			WeaponXITools.ExecuteAll<T>(transform.GetChild(i).gameObject, funcName);
			i++;
		}
	}

	public static string KeyToCaption(KeyCode key)
	{
		switch (key)
		{
		case KeyCode.Keypad0:
			return "K0";
		case KeyCode.Keypad1:
			return "K1";
		case KeyCode.Keypad2:
			return "K2";
		case KeyCode.Keypad3:
			return "K3";
		case KeyCode.Keypad4:
			return "K4";
		case KeyCode.Keypad5:
			return "K5";
		case KeyCode.Keypad6:
			return "K6";
		case KeyCode.Keypad7:
			return "K7";
		case KeyCode.Keypad8:
			return "K8";
		case KeyCode.Keypad9:
			return "K9";
		case KeyCode.KeypadPeriod:
			return ".";
		case KeyCode.KeypadDivide:
			return "/";
		case KeyCode.KeypadMultiply:
			return "*";
		case KeyCode.KeypadMinus:
			return "-";
		case KeyCode.KeypadPlus:
			return "+";
		case KeyCode.KeypadEnter:
			return "NT";
		case KeyCode.KeypadEquals:
			return "=";
		case KeyCode.UpArrow:
			return "UP";
		case KeyCode.DownArrow:
			return "DN";
		case KeyCode.RightArrow:
			return "LT";
		case KeyCode.LeftArrow:
			return "RT";
		case KeyCode.Insert:
			return "Ins";
		case KeyCode.Home:
			return "Home";
		case KeyCode.End:
			return "End";
		case KeyCode.PageUp:
			return "PU";
		case KeyCode.PageDown:
			return "PD";
		case KeyCode.F1:
			return "F1";
		case KeyCode.F2:
			return "F2";
		case KeyCode.F3:
			return "F3";
		case KeyCode.F4:
			return "F4";
		case KeyCode.F5:
			return "F5";
		case KeyCode.F6:
			return "F6";
		case KeyCode.F7:
			return "F7";
		case KeyCode.F8:
			return "F8";
		case KeyCode.F9:
			return "F9";
		case KeyCode.F10:
			return "F10";
		case KeyCode.F11:
			return "F11";
		case KeyCode.F12:
			return "F12";
		case KeyCode.F13:
			return "F13";
		case KeyCode.F14:
			return "F14";
		case KeyCode.F15:
			return "F15";
		case (KeyCode)297:
		case (KeyCode)298:
		case (KeyCode)299:
		case KeyCode.RightCommand:
		case KeyCode.LeftCommand:
		case KeyCode.LeftWindows:
		case KeyCode.RightWindows:
		case KeyCode.AltGr:
		case (KeyCode)314:
		case KeyCode.Help:
		case KeyCode.Print:
		case KeyCode.SysReq:
		case KeyCode.Break:
		case KeyCode.Menu:
		case (KeyCode)320:
		case (KeyCode)321:
		case (KeyCode)322:
			
			case KeyCode.Ampersand:
				return "&";
			case KeyCode.Quote:
				return "'";
			case KeyCode.LeftParen:
				return "(";
			case KeyCode.RightParen:
				return ")";
			case KeyCode.Asterisk:
				return "*";
			case KeyCode.Plus:
				return "+";
			case KeyCode.Comma:
				return ",";
			case KeyCode.Minus:
				return "-";
			case KeyCode.Period:
				return ".";
			case KeyCode.Slash:
				return "/";
			case KeyCode.Alpha0:
				return "0";
			case KeyCode.Alpha1:
				return "1";
			case KeyCode.Alpha2:
				return "2";
			case KeyCode.Alpha3:
				return "3";
			case KeyCode.Alpha4:
				return "4";
			case KeyCode.Alpha5:
				return "5";
			case KeyCode.Alpha6:
				return "6";
			case KeyCode.Alpha7:
				return "7";
			case KeyCode.Alpha8:
				return "8";
			case KeyCode.Alpha9:
				return "9";
			case KeyCode.Colon:
				return ":";
			case KeyCode.Semicolon:
				return ";";
			case KeyCode.Less:
				return "<";
			case KeyCode.Equals:
				return "=";
			case KeyCode.Greater:
				return ">";
			case KeyCode.Question:
				return "?";
			case KeyCode.At:
				return "@";
			case KeyCode.LeftBracket:
				return "[";
			case KeyCode.Backslash:
				return "\\";
			case KeyCode.RightBracket:
				return "]";
			case KeyCode.Caret:
				return "^";
			case KeyCode.Underscore:
				return "_";
			case KeyCode.BackQuote:
				return "`";
			case KeyCode.A:
				return "A";
			case KeyCode.B:
				return "B";
			case KeyCode.C:
				return "C";
			case KeyCode.D:
				return "D";
			case KeyCode.E:
				return "E";
			case KeyCode.F:
				return "F";
			case KeyCode.G:
				return "G";
			case KeyCode.H:
				return "H";
			case KeyCode.I:
				return "I";
			case KeyCode.J:
				return "J";
			case KeyCode.K:
				return "K";
			case KeyCode.L:
				return "L";
			case KeyCode.M:
				return "M";
			case KeyCode.N:
				return "N0";
			case KeyCode.O:
				return "O";
			case KeyCode.P:
				return "P";
			case KeyCode.Q:
				return "Q";
			case KeyCode.R:
				return "R";
			case KeyCode.S:
				return "S";
			case KeyCode.T:
				return "T";
			case KeyCode.U:
				return "U";
			case KeyCode.V:
				return "V";
			case KeyCode.W:
				return "W";
			case KeyCode.X:
				return "X";
			case KeyCode.Y:
				return "Y";
			case KeyCode.Z:
				return "Z";
			case KeyCode.Delete:
				return "Del";
			
			//goto IL_30D;
		case KeyCode.Numlock:
			return "Num";
		case KeyCode.CapsLock:
			return "Cap";
		case KeyCode.ScrollLock:
			return "Scr";
		case KeyCode.RightShift:
			return "RS";
		case KeyCode.LeftShift:
			return "LS";
		case KeyCode.RightControl:
			return "RC";
		case KeyCode.LeftControl:
			return "LC";
		case KeyCode.RightAlt:
			return "RA";
		case KeyCode.LeftAlt:
			return "LA";
		case KeyCode.Mouse0:
			return "M0";
		case KeyCode.Mouse1:
			return "M1";
		case KeyCode.Mouse2:
			return "M2";
		case KeyCode.Mouse3:
			return "M3";
		case KeyCode.Mouse4:
			return "M4";
		case KeyCode.Mouse5:
			return "M5";
		case KeyCode.Mouse6:
			return "M6";
		case KeyCode.JoystickButton0:
			return "(A)";
		case KeyCode.JoystickButton1:
			return "(B)";
		case KeyCode.JoystickButton2:
			return "(X)";
		case KeyCode.JoystickButton3:
			return "(Y)";
		case KeyCode.JoystickButton4:
			return "(RB)";
		case KeyCode.JoystickButton5:
			return "(LB)";
		case KeyCode.JoystickButton6:
			return "(Back)";
		case KeyCode.JoystickButton7:
			return "(Start)";
		case KeyCode.JoystickButton8:
			return "(LS)";
		case KeyCode.JoystickButton9:
			return "(RS)";
		case KeyCode.JoystickButton10:
			return "J10";
		case KeyCode.JoystickButton11:
			return "J11";
		case KeyCode.JoystickButton12:
			return "J12";
		case KeyCode.JoystickButton13:
			return "J13";
		case KeyCode.JoystickButton14:
			return "J14";
		case KeyCode.JoystickButton15:
			return "J15";
		case KeyCode.JoystickButton16:
			return "J16";
		case KeyCode.JoystickButton17:
			return "J17";
		case KeyCode.JoystickButton18:
			return "J18";
		case KeyCode.JoystickButton19:
			return "J19";
		}
		
        return "Esc";
    }
}
