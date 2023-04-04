//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/PlayerActions.inputactions
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
                },
                {
                    ""name"": ""buff"",
                    ""type"": ""Button"",
                    ""id"": ""92725e2b-6fd9-45a2-a0eb-f6618f0a7ddc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""db112cfe-5738-4d55-a77c-940fa72278d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""pause"",
                    ""type"": ""Button"",
                    ""id"": ""c0c4bb00-f0f5-4bd1-8e87-926df7627539"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""controls"",
                    ""type"": ""Button"",
                    ""id"": ""bfba741b-876c-466e-a116-e929a096f032"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""field guide"",
                    ""type"": ""Button"",
                    ""id"": ""ae555814-de89-4396-b13c-eb2cc2c1e15e"",
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
                    ""processors"": ""ScaleVector2(x=0.001,y=0.001)"",
                    ""groups"": """",
                    ""action"": ""cameraMovement"",
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
                    ""id"": ""f73caf24-0a74-4be6-a3a3-df803300a8b0"",
                    ""path"": ""<Gamepad>/leftTrigger"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""16290d6b-33b0-48d5-814a-c4628f9c2f89"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""926f3025-7890-4e38-b179-c87f7cfe3454"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fd1b819-24fa-4631-926b-8f6f45fbddf0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36347d04-a6ac-43cf-9722-4ee8bc0a31d1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa94d456-1703-4a61-9c0a-68f92b9544a7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd60b3c3-4888-4627-876d-b41ed9e70051"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""968eb810-e6b5-4006-b800-e66e4a169f88"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ba85076-0e65-4e40-86ac-04db833865e1"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""controls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec825daa-e2cf-4f2f-9030-a163992ec5f3"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""controls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b98af559-f109-4d98-a81d-467b9b4d002e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""field guide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71b63234-d80e-47a5-b029-921fad5f58dd"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""field guide"",
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
        m_Main_buff = m_Main.FindAction("buff", throwIfNotFound: true);
        m_Main_interact = m_Main.FindAction("interact", throwIfNotFound: true);
        m_Main_pause = m_Main.FindAction("pause", throwIfNotFound: true);
        m_Main_controls = m_Main.FindAction("controls", throwIfNotFound: true);
        m_Main_fieldguide = m_Main.FindAction("field guide", throwIfNotFound: true);
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
    private readonly InputAction m_Main_buff;
    private readonly InputAction m_Main_interact;
    private readonly InputAction m_Main_pause;
    private readonly InputAction m_Main_controls;
    private readonly InputAction m_Main_fieldguide;
    public struct MainActions
    {
        private @PlayerActions m_Wrapper;
        public MainActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_Main_movement;
        public InputAction @cameraMovement => m_Wrapper.m_Main_cameraMovement;
        public InputAction @jump => m_Wrapper.m_Main_jump;
        public InputAction @net => m_Wrapper.m_Main_net;
        public InputAction @shoot => m_Wrapper.m_Main_shoot;
        public InputAction @buff => m_Wrapper.m_Main_buff;
        public InputAction @interact => m_Wrapper.m_Main_interact;
        public InputAction @pause => m_Wrapper.m_Main_pause;
        public InputAction @controls => m_Wrapper.m_Main_controls;
        public InputAction @fieldguide => m_Wrapper.m_Main_fieldguide;
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
                @buff.started -= m_Wrapper.m_MainActionsCallbackInterface.OnBuff;
                @buff.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnBuff;
                @buff.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnBuff;
                @interact.started -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @pause.started -= m_Wrapper.m_MainActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnPause;
                @controls.started -= m_Wrapper.m_MainActionsCallbackInterface.OnControls;
                @controls.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnControls;
                @controls.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnControls;
                @fieldguide.started -= m_Wrapper.m_MainActionsCallbackInterface.OnFieldguide;
                @fieldguide.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnFieldguide;
                @fieldguide.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnFieldguide;
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
                @buff.started += instance.OnBuff;
                @buff.performed += instance.OnBuff;
                @buff.canceled += instance.OnBuff;
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
                @controls.started += instance.OnControls;
                @controls.performed += instance.OnControls;
                @controls.canceled += instance.OnControls;
                @fieldguide.started += instance.OnFieldguide;
                @fieldguide.performed += instance.OnFieldguide;
                @fieldguide.canceled += instance.OnFieldguide;
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
        void OnBuff(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnControls(InputAction.CallbackContext context);
        void OnFieldguide(InputAction.CallbackContext context);
    }
}
