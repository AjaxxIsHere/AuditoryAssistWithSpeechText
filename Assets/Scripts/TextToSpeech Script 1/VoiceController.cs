using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    [SerializeField]
    Text uiText;
    void Start() {
        Setup(LANG_CODE);
#if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
        
#endif
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnStopSpeaking;

        CheckPermission();
    }

    void CheckPermission() {
#if UNITY_ANDROID
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone)) {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    #region Text to Speech
    public void StartSpeaking(string message) {
        TextToSpeech.Instance.StartSpeak(message);
    }
    public void StopSpeaking() {
        TextToSpeech.Instance.StopSpeak();
    }

    void OnSpeakStart() {
        Debug.Log("Talking Started...");
    }

    void OnStopSpeaking() {
        Debug.Log("Stopped Speaking");
    }
    #endregion

    #region Speech to text

    public void StartListening() {
        SpeechToText.Instance.StopRecording();
    }

    public void StopListening() {
        SpeechToText.Instance.StopRecording();
        
    }

    void OnFinalSpeechResult(string result) {
        uiText.text = result;
    }

    void OnPartialSpeechResult(string result) {
        uiText.text = result;
    }

    #endregion
    void Setup(string code) {
        TextToSpeech.Instance.Setting(code, 1, 1);
        SpeechToText.Instance.Setting(code);
    }
}
