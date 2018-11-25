using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Video;

public class OnVideoEnd : MonoBehaviour {

    private VideoPlayer      _player;
    VideoPlayer.EventHandler _handleEndingEvent;


    void Start() {
        // _handleEndingEvent       =  new VideoPlayer.EventHandler();
        _player                  =  GetComponent<VideoPlayer>();
        _player.loopPointReached += EndReached;
        // VideoPlayer.loopPointReached
    }


    void EndReached(VideoPlayer vp) {
        //
        GameController.Instance.SceneController.FadeAndLoadScene("Prototype");
    }

}