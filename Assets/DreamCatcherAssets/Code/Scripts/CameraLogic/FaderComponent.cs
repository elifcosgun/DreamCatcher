using DG.Tweening;
using UnityEngine;

namespace DreamCatcherAssets.Code.Scripts.CameraLogic
{
    public class FaderComponent : MonoBehaviour
    {
        public MeshRenderer staticMeshComponent;
        public float fadeSpeed = 3f;
        public float transparencyValue = 0.99f;

        private float _currentFadeValue;
        private Color _materialColor;

        private void Start()
        {
            _materialColor.r = 0.75f;
            _materialColor.g = 0.75f;
            _materialColor.b = 0.75f;
        }

        private void Update()
        {
            InterpolateFadeValue();
        }

        private void InterpolateFadeValue()
        {
            _currentFadeValue = Mathf.Lerp(_currentFadeValue, transparencyValue, Time.deltaTime);
            _materialColor.a = _currentFadeValue;
            staticMeshComponent.material.DOColor(_materialColor, Time.deltaTime * fadeSpeed);
        }

        public void StartFade()
        {
            transparencyValue = 0.01f;
        }
        
        public void EndFade()
        {
            transparencyValue = 0.99f;
        }
    }
}
