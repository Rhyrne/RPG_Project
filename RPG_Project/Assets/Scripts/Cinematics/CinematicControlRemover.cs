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
            DisableDirector();
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += Disabled;
            GetComponent<PlayableDirector>().stopped += Enabled;
            EnableDirector();
        }

        public void Enabled(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        public void Disabled(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = false;
        }

        public void DisableDirector()
        {
            GetComponent<PlayableDirector>().enabled = false;
        }

        public void EnableDirector()
        {
            GetComponent<PlayableDirector>().enabled = true;
        }
    }
}
