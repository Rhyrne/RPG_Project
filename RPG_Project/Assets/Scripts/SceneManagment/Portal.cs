using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeOutIn = 1f;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutIn);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return fader.FadeIn(fadeOutIn);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                {
                    continue;
                }
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = portal.spawnPoint.position;
            player.transform.rotation = portal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

