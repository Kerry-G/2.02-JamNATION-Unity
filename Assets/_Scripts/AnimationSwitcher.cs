using System;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class AnimationSwitcher : MonoBehaviour {

    private Animation _animation;
    private List<String> animsNames = new List<string>();

    // Use this for initialization
    void Start() {
        _animation = GetComponent<Animation>();
        GameController.Instance.onChangePhase.AddListener(ChangeAnim);

        foreach ( AnimationState animState in _animation )
            animsNames.Add(animState.name);
    }


    public void ChangeAnim(GamePhase phase) {
        if ( phase == GamePhase.Moving ) {
            _animation.CrossFade(animsNames[0], 0.1f);
        } else {
            _animation.CrossFade(animsNames[1], 0.1f);
        }
    }

}