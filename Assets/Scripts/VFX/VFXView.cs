using CosmicCuration.VFX;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXView : MonoBehaviour
    {
        [SerializeField] private List<VFXData> vFXDatas = new List<VFXData>();

        private VFXController controller;
        private ParticleSystem currentVFXPlaying;

        public void SetController(VFXController controllerToSet) => controller = controllerToSet;

        public void ConfigureAndPlay(VFXType type, Vector2 playPosition)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = playPosition; 

            foreach (VFXData item in vFXDatas)
            {
                if (item.type == type)
                {
                    item.particleSystem.gameObject.SetActive(true);
                    currentVFXPlaying = item.particleSystem;
                }
                else
                    item.particleSystem.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (currentVFXPlaying != null)
            {
                if (currentVFXPlaying.isStopped)
                {
                    currentVFXPlaying.gameObject.SetActive(false);
                    currentVFXPlaying = null;
                    controller.OnParticleEffectCompleted();
                    gameObject.SetActive(false);
                }
            }
        }
    }

    [Serializable]
    public struct VFXData
    {
        public VFXType type;
        public ParticleSystem particleSystem;
    }
}