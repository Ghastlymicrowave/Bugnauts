//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""f006e076-eb19-4698-ad58-780f17887844"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""Value"",
                    ""id"": ""9af01148-8b96-4a73-9bce-9327b1591e30"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""cameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""22040183-32ac-4191-b3de-6de2f4c409c0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""96ad87d4-fded-4ff6-a56a-4d20e6b31ad5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""net"",
                    ""type"": ""Button"",
                    ""id"": ""9698029c-3f17-4ee0-b330-9ebe2572c83f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""shoot"",
                    ""type"": ""Button"",
                    ""id"": ""14537c39-c26c-44cf-be2e-cb3a51d37255"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d3c61991-bf02-4921-a610-11e521da3b3f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d8b12387-24ac-42f9-a4f7-d5e0c6b66e4d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b2525200-b810-47cd-b026-d52bee5425e1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""91d7ddd9-fac8-42f0-b8d5-ee87c38957bd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b111ad82-dfe5-4a13-b805-597932b7178a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a52a2677-be75-4cea-9225-3a8c876b30a3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""237f89a9-7e36-4ff1-b1bd-98365bb9c58b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=3,y=3)"",
                    ""groups"": """",
                    ""action"": ""cameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e062d98-4b23-4d59-be92-252cc0986388"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""cameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf472bb1-798d-470d-a451-0ea296d90944"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29bb1eec-170b-4105-b157-de463c1e428f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a61e91ae-7fac-454f-90bb-996010a0830d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""net"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3eeea881-a1e3-479f-a066-5abe863f85b7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_movement = m_Main.FindAction("movement", throwIfNotFound: true);
        m_Main_cameraMovement = m_Main.FindAction("cameraMovement", throwIfNotFound: true);
        m_Main_jump = m_Main.FindAction("jump", throwIfNotFound: true);
        m_Main_net = m_Main.FindAction("net", throwIfNotFound: true);
        m_Main_shoot = m_Main.FindAction("shoot", throwIfNotFound: true);
    }

    public void Dispose()
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_movement;
    private readonly InputAction m_Main_cameraMovement;
    private readonly InputAction m_Main_jump;
    private readonly InputAction m_Main_net;
    private readonly InputAction m_Main_shoot;
    public struct MainActions
    {
        private @PlayerActions m_Wrapper;
        public MainActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_Main_movement;
        public InputAction @cameraMovement => m_Wrapper.m_Main_cameraMovement;
        public InputAction @jump => m_Wrapper.m_Main_jump;
        public InputAction @net => m_Wrapper.m_Main_net;
        public InputAction @shoot => m_Wrapper.m_Main_shoot;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @movement.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @movement.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @movement.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @cameraMovement.started -= m_Wrapper.m_MainActionsCallbackInterface.OnCameraMovement;
                @cameraMovement.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnCameraMovement;
                @cameraMovement.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnCameraMovement;
                @jump.started -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @jump.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @jump.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @net.started -= m_Wrapper.m_MainActionsCallbackInterface.OnNet;
                @net.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnNet;
                @net.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnNet;
                @shoot.started -= m_Wrapper.m_MainActionsCallbackInterface.OnShoot;
                @shoot.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnShoot;
                @shoot.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @movement.started += instance.OnMovement;
                @movement.performed += instance.OnMovement;
                @movement.canceled += instance.OnMovement;
                @cameraMovement.started += instance.OnCameraMovement;
                @cameraMovement.performed += instance.OnCameraMovement;
                @cameraMovement.canceled += instance.OnCameraMovement;
                @jump.started += instance.OnJump;
                @jump.performed += instance.OnJump;
                @jump.canceled += instance.OnJump;
                @net.started += instance.OnNet;
                @net.performed += instance.OnNet;
                @net.canceled += instance.OnNet;
                @shoot.started += instance.OnShoot;
                @shoot.performed += instance.OnShoot;
                @shoot.canceled += instance.OnShoot;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnNet(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
