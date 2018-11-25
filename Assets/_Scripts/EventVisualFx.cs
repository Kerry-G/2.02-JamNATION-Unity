using System;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Rendering.PostProcessing;
using Utils;
using Random = System.Random;

public class EventVisualFx : MonoBehaviour {

    public float easeSpeed = 0.5f;

    private PostProcessVolume   _volume;
    private ChromaticAberration _chromaticAberration;


    private List<PostProcessEffectSettings> _settings = new List<PostProcessEffectSettings>();

    private float _tShooting = 0.0f;


    void Start() {
        GameController.Instance.onChangePhase.AddListener(ListenOnChange);

        _chromaticAberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        _chromaticAberration.enabled.Override(true);
        _chromaticAberration.intensity.Override(1f);

        _settings.Add(_chromaticAberration);

        _volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _settings.ToArray());
    }


    private void ListenOnChange(GamePhase phase) {
        if ( phase == GamePhase.Moving ) {
            _tShooting = 0;
        }
    }


    void Update() {
        _chromaticAberration.intensity.value = EasingFunction.EaseOutCirc(1, 0, _tShooting);
        _chromaticAberration.intensity.value += 0.1f + (float)Math.Sin(Time.realtimeSinceStartup*10) * UnityEngine.Random.Range(0,0.15f);

        if (_tShooting < 1) {
            _tShooting += easeSpeed * Time.deltaTime;
        }
    }


    void OnDestroy() { RuntimeUtilities.DestroyVolume(_volume, true, true); }

}