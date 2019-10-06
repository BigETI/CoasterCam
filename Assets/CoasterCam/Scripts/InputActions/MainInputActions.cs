// GENERATED AUTOMATICALLY FROM 'Assets/CoasterCam/InputActions/MainInputActions.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace CoasterCam.ActionInputs
{
    public class MainInputActions : IInputActionCollection
    {
        private InputActionAsset asset;
        public MainInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInputActions"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""0c28b47b-fa5a-4a19-8744-a5c92317cdcd"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""88262464-d951-4a57-8c38-97e6006b00b5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc50b322-d824-436e-9e3f-0658df7c0ff9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0264b24c-9981-4303-af34-507434769531"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fef59ee1-11a4-40cb-b8ad-e7f4b2c42752"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // ActionMap
            m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
            m_ActionMap_Interact = m_ActionMap.FindAction("Interact", throwIfNotFound: true);
        }

        ~MainInputActions()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // ActionMap
        private readonly InputActionMap m_ActionMap;
        private IActionMapActions m_ActionMapActionsCallbackInterface;
        private readonly InputAction m_ActionMap_Interact;
        public struct ActionMapActions
        {
            private MainInputActions m_Wrapper;
            public ActionMapActions(MainInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Interact => m_Wrapper.m_ActionMap_Interact;
            public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
            public void SetCallbacks(IActionMapActions instance)
            {
                if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
                {
                    Interact.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnInteract;
                    Interact.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnInteract;
                    Interact.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnInteract;
                }
                m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Interact.started += instance.OnInteract;
                    Interact.performed += instance.OnInteract;
                    Interact.canceled += instance.OnInteract;
                }
            }
        }
        public ActionMapActions @ActionMap => new ActionMapActions(this);
        public interface IActionMapActions
        {
            void OnInteract(InputAction.CallbackContext context);
        }
    }
}
