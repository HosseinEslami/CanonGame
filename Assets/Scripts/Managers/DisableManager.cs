using System;
using System.Collections;
using UnityEngine;

public class DisableManager : MonoBehaviour
{
    [SerializeField] private bool disableByTime;
    [SerializeField] private float timer;

    private void OnEnable()
    {
        if (disableByTime)
            StartCoroutine(DisableTimer(timer));
    }

    private IEnumerator DisableTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    public  void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
