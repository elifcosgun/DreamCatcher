using System.Collections.Generic;
using UnityEngine;

namespace DreamCatcherAssets.Code.Scripts.CameraLogic
{
    public class CameraChecker : MonoBehaviour
    {
        public GameObject player;
        public float maxRange = 14;
        
        private readonly List<FaderComponent> _faderComponents = new List<FaderComponent>();

        private void FixedUpdate()
        {
            var position = transform.position;
            var playerPosition = player.transform.position;
            var hits = new RaycastHit[5];
            var foundComponents = new List<FaderComponent>();
            
            Physics.RaycastNonAlloc(position, (playerPosition - position), hits, maxRange);

            foreach (var hit in hits)
            {
                if (hit.transform == null) continue;
                if(hit.transform.tag.Contains("Player")) break;
                if (hit.transform.GetComponent<FaderComponent>() == null) continue;

                var target = hit.transform.GetComponent<FaderComponent>();
                foundComponents.Add(target);
            }
                
            SwitchNextFadeStatus(foundComponents);
        }

        private void SwitchNextFadeStatus( List<FaderComponent> foundComponents)
        {
            foreach (var hitComponent in foundComponents)
            {
                if (_faderComponents.Contains(hitComponent)) continue;
                
                hitComponent.StartFade();
                _faderComponents.Add(hitComponent);
            }

            for (var i = _faderComponents.Count - 1; i >= 0; --i)
            {
                if (foundComponents.Contains(_faderComponents[i])) continue;
                
                _faderComponents[i].EndFade();
                _faderComponents.RemoveAt(i);
            }
        }
    }
}
