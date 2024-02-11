using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FX
{
    public class SustainableEffect : MonoBehaviour
    {
        [SerializeField] private float stepTime = 1;
        [SerializeField] private ParticleSystem particleSystem;
        private float _currentTime = 0;
        private bool _isActive = false;

        private void Start()
        {
            particleSystem.Stop();
        }

        public void Sustain()
        {
            if (!_isActive)
            {
                _isActive = true;
                particleSystem.Play();
            }
            
            _currentTime = stepTime;
        }
        
        void Update()
        {
            if (_isActive)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    _isActive = false;
                    particleSystem.Stop();
                }
            }
        }
    }
}
