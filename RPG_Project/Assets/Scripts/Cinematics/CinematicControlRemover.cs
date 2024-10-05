using RPG.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += Disabled;
            GetComponent<PlayableDirector>().stopped += Enabled;
        }

        public void Enabled(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        public void Disabled(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
