using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIElementController : MonoBehaviour
{
    [Header("OPTIONAL")]
    [SerializeField] private GameObject pcCanvas;
    [SerializeField] private GameObject androidCanvas;

    [Header("PC")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider objectSizeSlider;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle lightToggle;
    [SerializeField] private GameObject lightObject;
    [SerializeField] private GameObject sceneObject;
    [SerializeField] private AudioSource audioObject;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerUpText;

    [Header("ANDROID")]
    [SerializeField] private Slider volumeSliderA;
    [SerializeField] private Slider objectSizeSliderA;
    [SerializeField] private Toggle soundToggleA;
    [SerializeField] private Toggle lightToggleA;
    [SerializeField] private GameObject lightObjectA;
    [SerializeField] private GameObject sceneObjectA;
    [SerializeField] private AudioSource audioObjectA;
    [SerializeField] private TextMeshProUGUI timerTextA;
    [SerializeField] private TextMeshProUGUI timerUpTextA;



    // local shared
    private float currentTime = 0;
    private float maxTime = 70;
    private float maxSizePC = 3.70f;
    private float maxSizeAndroid = 1.90f;
    private bool timerStopped = false;

    private void Awake()
    {
#if UNITY_STANDALONE_WIN
        pcCanvas.SetActive(true);
        androidCanvas.SetActive(false);
        pcSetup();
#elif UNITY_ANDROID
       pcCanvas.SetActive(false);
        androidCanvas.SetActive(true);
        androidSetup();
#elif UNITY_EDITOR
        pcCanvas.SetActive(true);
        androidCanvas.SetActive(false);
        pcSetup();
#endif
    }


    private void Update()
    {

        if(currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
#if UNITY_STANDALONE_WIN
            timerText.text = ConvertSecondsToHHMMSS(currentTime);
#elif UNITY_ANDROID
           timerTextA.text = ConvertSecondsToHHMMSS(currentTime);
#elif UNITY_EDITOR
            timerText.text = ConvertSecondsToHHMMSS(currentTime);
#endif

        }


        if (currentTime > maxTime && timerStopped == false)
        {
            timerStopped = true;
#if UNITY_STANDALONE_WIN
            timerUpText.text = "Timer Up";
#elif UNITY_ANDROID
           timerUpTextA.text = "Timer Up";
#elif UNITY_EDITOR
            timerUpText.text = "Timer Up";
#endif

        }


    }

    string ConvertSecondsToHHMMSS(float totalSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                             time.Hours,
                                             time.Minutes,
                                             time.Seconds);

        return formattedTime;
    }

    private void androidSetup()
    {
        volumeSliderA.onValueChanged.AddListener(SliderVolumeA);
        soundToggleA.onValueChanged.AddListener(SoundToggleA);
        lightToggleA.onValueChanged.AddListener(LightToggleA);
        objectSizeSliderA.onValueChanged.AddListener(ObjectSizeSliderA);

        volumeSliderA.onValueChanged.Invoke(0.1f);
        soundToggleA.onValueChanged.Invoke(false);
        soundToggleA.isOn = false;
        lightToggleA.isOn = false;
        lightToggleA.onValueChanged.Invoke(false);
        objectSizeSliderA.onValueChanged.Invoke(0.3f);
    }

    private void pcSetup()
    {
        volumeSlider.onValueChanged.AddListener(SliderVolume);
        soundToggle.onValueChanged.AddListener(SoundToggle);
        lightToggle.onValueChanged.AddListener(LightToggle);
        objectSizeSlider.onValueChanged.AddListener(ObjectSizeSlider);

        volumeSlider.onValueChanged.Invoke(0.1f);
        soundToggle.onValueChanged.Invoke(false);
        soundToggle.isOn = false;
        lightToggle.isOn = false;
        lightToggle.onValueChanged.Invoke(false);
        objectSizeSlider.onValueChanged.Invoke(0.3f);
    }

    private void SoundToggleA(bool state)
    {
        audioObjectA.mute = !state;
    }

    private void LightToggleA(bool state)
    {
        lightObjectA.SetActive(state);
    }

    private void SliderVolumeA(float volume)
    {
        audioObjectA.volume = volume;
    }

    private void ObjectSizeSliderA(float size)
    {
        sceneObjectA.transform.localScale = new Vector3(size * maxSizeAndroid, size * maxSizeAndroid, size * maxSizeAndroid);
    }






    private void SoundToggle(bool state)
    {
        audioObject.mute = !state;
    }

    private void LightToggle(bool state)
    {
        lightObject.SetActive(state);
    }

    private void SliderVolume(float volume)
    {
        audioObject.volume = volume;
    }

    private void ObjectSizeSlider(float size)
    {
        sceneObject.transform.localScale = new Vector3(size * maxSizePC, size * maxSizePC, size * maxSizePC);
    }

    
}


