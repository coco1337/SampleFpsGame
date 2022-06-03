#define DEBUG_LEVEL_LOG
#define DEBUG_LEVEL_WARN
#define DEBUG_LEVEL_ERROR

using UnityEngine;
using System.Collections;

public class D
{
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void log(string format, params object[] paramList)
	{
		Debug.Log(string.Format(format, paramList));
	}

	//[System.Diagnostics.Conditional("DEBUG_LEVEL_WARN")]
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void warn(string format, params object[] paramList)
	{
		Debug.LogWarning(string.Format(format, paramList));
	}

	//[System.Diagnostics.Conditional("DEBUG_LEVEL_ERROR")]
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void error(string format, params object[] paramList)
	{
		Debug.LogError(string.Format(format, paramList));
	}

	//[System.Diagnostics.Conditional("UNITY_EDITOR")]
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void assert(bool condition)
	{
		assert(condition, string.Empty, true);
	}

	//[System.Diagnostics.Conditional("UNITY_EDITOR")]
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void assert(bool condition, string assertString)
	{
		assert(condition, assertString, false);
	}

	//[System.Diagnostics.Conditional("UNITY_EDITOR")]
	//[System.Diagnostics.Conditional("DEBUG_LEVEL_LOG")]
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void assert(bool condition, string assertString, bool pauseOnFail)
	{
		if (!condition)
		{
			Debug.LogError("assert failed! " + assertString);

			if (pauseOnFail)
				Debug.Break();
		}
	}
}
