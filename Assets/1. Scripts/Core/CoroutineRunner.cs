using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("CoroutineRunner");
                _instance = obj.AddComponent<CoroutineRunner>();
                GameObject.DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public Coroutine RunCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void RemoveCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
