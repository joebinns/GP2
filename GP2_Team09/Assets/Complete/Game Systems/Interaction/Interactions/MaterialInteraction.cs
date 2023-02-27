using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.Rendering.UI;

namespace GameProject.Interactions {
    public class MaterialInteraction : BaseInteraction
    {
        [Header("Albedo Settings")]
        [SF] private bool _setAlbedo = false;
        [Space]
        [SF] private string _albedoColourParam   = "_BaseColor";
        [SF] private Color _albedoColour = Color.white;

        [Header("Emission Settings")]
        [SF] private bool _setEmission = false;
        [Space]
        [SF] private bool _enableEmission = false;
        [SF] private string _emissionKeyParam    = "_EMISSION";
        [Space]
        [SF] private bool _setEmissionColour = false;
        [SF] private string _emissionColourParam = "_EmissionColor";
        [SF] private Color _emissiveColour = Color.black;
        
        [Header("Material Settings")]
        [SF] private bool _global = false;
        [SF] private int _materialIndex = 0;
        [SF] private MeshRenderer _renderer = null;

        private Color _defAlbedoColour     = Color.white;
        private Color _defEmissionColour   = Color.white;
        private bool  _defIsEmissive = false;

// INITIALISATION

        /// <summary>
        /// Initialises the default material properties
        /// </summary>
        private void Awake(){
            var material = _global ?
                _renderer.sharedMaterials[_materialIndex] :
                _renderer.materials[_materialIndex]; 

            if (_setAlbedo){
                _defAlbedoColour = material.GetColor(
                    _albedoColourParam
                );
            }
            if (_setEmission){
                if (_setEmissionColour){ 
                    _defEmissionColour = material.GetColor(
                        _emissionColourParam
                    );
                }
                _defIsEmissive = material.IsKeywordEnabled(
                    _emissionKeyParam
                );
            }
        }

#if UNITY_EDITOR

        /// <summary>
        /// Resets shared material
        /// </summary>
        private void OnDestroy(){
            if (!_global) return;
            var material = _renderer.sharedMaterial;

            SetAlbedo(material, false);
            SetEmission(material, false);
        }

#endif

// MATERIAL HANDLING

        /// <summary>
        /// Changes the material properties
        /// </summary>
        public override void Trigger() => UpdateMaterial(true);

        /// <summary>
        /// Resets the material properties
        /// </summary>
        public override void Restore() => UpdateMaterial(false);


        /// <summary>
        /// Assigns and resets the material properties
        /// </summary>
        private void UpdateMaterial(bool change){
            var material = _global ? 
                _renderer.sharedMaterials[_materialIndex] :
                _renderer.materials[_materialIndex];

            SetAlbedo(material, change);
            SetEmission(material, change);
        }

        /// <summary>
        /// Changes the material albedo
        /// </summary>
        private void SetAlbedo(Material material, bool change){
            if (!_setAlbedo) return;

            material.SetColor(_albedoColourParam,
                change ? _albedoColour : _defAlbedoColour
            );
        }

        /// <summary>
        /// Changes the material emission
        /// </summary>
        private void SetEmission(Material material, bool change){
            if (!_setEmission) return;

            if (_setEmissionColour){
                material.SetColor(_emissionColourParam,
                    change ? _emissiveColour : _defEmissionColour
                );
            }
            if (change){
                if (_enableEmission)
                     material.EnableKeyword(_emissionKeyParam);
                else material.DisableKeyword(_emissionKeyParam);
                
            } else if (_defIsEmissive){ 
                   material.EnableKeyword(_emissionKeyParam);
            } else material.DisableKeyword(_emissionKeyParam);
        }
    }
}