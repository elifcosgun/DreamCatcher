using System.Collections;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DreamCatcherAssets.Level.UI.MainMenuAssets
{
    public class ClickButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        [SerializeField] private Image img;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite pressed;
        [SerializeField] private AudioClip compressClip;
        [SerializeField] private AudioClip unCompressClip;

        [SerializeField] private AudioSource source;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            img.sprite = pressed;
            source.PlayOneShot(compressClip);

            StartCoroutine(StartLevel(0.4f));
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            img.sprite = defaultSprite;
            source.PlayOneShot(unCompressClip);
        }

        private IEnumerator StartLevel(float secs)
        {
            yield return new WaitForSeconds(secs);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}