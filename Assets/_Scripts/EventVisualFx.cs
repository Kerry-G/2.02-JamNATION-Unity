using System;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Rendering.PostProcessing;
using Utils;
using Random = System.Random;

public class EventVisualFx : MonoBehaviour {

    public float easeSpeed     = 0.5f;
    public float hueShiftSpeed = 50f;

    [Range(2, 180)]
    public float hueShiftAmplitude = 50f;

    private PostProcessVolume   _volume;
    private ChromaticAberration _chromaticAberration;
    private ColorGrading        _colorGrading;

    private float _abbrationMinimumRandom = 0.08f;


    private List<PostProcessEffectSettings> _settings = new List<PostProcessEffectSettings>();

    private float _tShooting = 0.0f;
    private bool  _changeHue = false;


    void Start() {
        GameController.Instance.onChangePhase.AddListener(ListenOnChange);

        _chromaticAberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        _chromaticAberration.enabled.Override(true);
        _chromaticAberration.intensity.Override(1f);

        _colorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        _colorGrading.enabled.Override(true);

        _settings.Add(_chromaticAberration);
        _settings.Add(_colorGrading);

        _volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _settings.ToArray());
    }


    private void ListenOnChange(GamePhase phase) {
        if ( phase == GamePhase.Moving ) {
            _tShooting = 0;
            _changeHue = false;
            _abbrationMinimumRandom = 0.08f;
            _colorGrading.hueShift.Override(0f); // UnShift it
        } else {
            _changeHue = true;
            _abbrationMinimumRandom = 0.3f;
            _colorGrading.hueShift.Override(50f); // Shift it
        }
    }


    void Update() {
        _chromaticAberration.intensity.value = EasingFunction.EaseOutCirc(1, 0, _tShooting);
        _chromaticAberration.intensity.value +=
            _abbrationMinimumRandom + (float) Math.Sin(Time.realtimeSinceStartup * 10) * UnityEngine.Random.Range(0, 0.15f);

        if ( _tShooting < 1 ) _tShooting += easeSpeed * Time.deltaTime;

        if ( _changeHue ) {
            _colorGrading.hueShift.value += (float)Math.Sin(Time.realtimeSinceStartup * hueShiftSpeed) * hueShiftAmplitude;
        }
    }


    void OnDestroy() { RuntimeUtilities.DestroyVolume(_volume, true, true); }

}