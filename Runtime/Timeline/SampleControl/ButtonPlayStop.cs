using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class ButtonPlayStop : MonoBehaviour
{
    PlayableDirector Director;
    // Start is called before the first frame update

    public KeyCode ButtonKey = KeyCode.Space;
    void Start()
    {
        Director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Director == null){
            return;
        }

        if(Input.GetKeyDown(ButtonKey)){
            var state = Director.state;
            switch(state){
                case PlayState.Playing:
                    Director.Pause();
                    break;
                case PlayState.Paused:
                    Director.Play();
                    break;
            }
        }

    }
}
