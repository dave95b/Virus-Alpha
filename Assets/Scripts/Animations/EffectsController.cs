using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour {

    [SerializeField]
    private ParticleSystem destroyParticles, hackParticles, unhackParticles;

    [SerializeField]
    private ElementActionEvent destroyEvent, hackEvent, unhackEvent;

    private void OnEnable()
    {
        destroyEvent.AddListener(PlayDestroyEffect);
        hackEvent.AddListener(PlayHackEffect);
        unhackEvent.AddListener(PlayUnhackEffect);
    }

    private void OnDisable()
    {
        destroyEvent.RemoveListener(PlayDestroyEffect);
        hackEvent.RemoveListener(PlayHackEffect);
        unhackEvent.RemoveListener(PlayUnhackEffect);
    }

    private void PlayDestroyEffect(SystemElementController systemElementController, VirusValue virus)
    {
        if(destroyParticles != null)
        {
            destroyParticles.transform.position = systemElementController.transform.position;
            //int elementSize = systemElementController.SystemElement.Size;
            //destroyParticles.transform.localScale.Set();
            destroyParticles.Play();
        }
    }

    private void PlayHackEffect(SystemElementController systemElementController, VirusValue virus)
    {
        if(hackParticles != null)
        {
            hackParticles.transform.position = systemElementController.transform.position;

            //ParticleSystem.MinMaxGradient gradient = hackParticles.colorOverLifetime.color;
            //gradient.colorMin = Color.white;
            //gradient.colorMax = virus.Value.Color;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(Color.white, 0.0f), new GradientColorKey(virus.Value.Color, 0.3f)
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.8f)//, new GradientAlphaKey(0.0f, 1.0f)
                }
            );

            var col = hackParticles.colorOverLifetime;
            col.enabled = true;
            col.color = gradient;

            hackParticles.Play();
        }
    }

    private void PlayUnhackEffect(SystemElementController systemElementController, VirusValue virus)
    {
        if(unhackParticles != null)
        {
            unhackParticles.transform.position = systemElementController.transform.position;

            Gradient gradient = new Gradient();
            Color startColor = systemElementController.SystemElement.OwnerVirus.Color;
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(startColor, 0.0f), new GradientColorKey(Color.white, 0.3f)
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.8f)
                }
            );

            var col = hackParticles.colorOverLifetime;
            col.enabled = true;
            col.color = gradient;

            unhackParticles.Play();
        }
    }
}
