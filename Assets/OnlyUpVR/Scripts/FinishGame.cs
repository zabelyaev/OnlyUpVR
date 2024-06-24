using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private GameObject _vfxPrefab;
    private GameObject _vfx;

    private bool _isFinish = false;

    private Stopwatch _stopwatch;
    [SerializeField] private GameObject _finishUIPrefab;
    private GameObject _finishUI;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _stopwatch = GameObject.FindAnyObjectByType<Stopwatch>();

        if (_stopwatch == null)
        {
            Debug.LogError("Stopwatch doesn't exist on scene!");
        }
    }

    public void Finish()
    {
        if (!_isFinish)
        {
            _audioSource.Play();
            PlayVFX();
            ShowResult();
            Invoke(nameof(StopVFX), 3f);
            _isFinish = true;
        }
    }

    private void PlayVFX()
    {
        _vfx = Instantiate(_vfxPrefab, this.transform.position + new Vector3(0, 0.5f, 4f), _vfxPrefab.transform.rotation);
    }

    private void StopVFX()
    {
        _audioSource.Stop();
        Destroy(_vfx);
    }

    public void ShowResult()
    {
        _finishUI = Instantiate(_finishUIPrefab, this.transform.position + new Vector3(0, 1f, 3f), _finishUIPrefab.transform.rotation);
        _finishUI.GetComponentInChildren<TextMeshProUGUI>().text = "Ваш результат:\n" + _stopwatch.FormattedTime;
        _stopwatch.StopCounting();
    }
}
