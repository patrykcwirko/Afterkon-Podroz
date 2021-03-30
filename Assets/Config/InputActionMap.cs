// GENERATED AUTOMATICALLY FROM 'Assets/Config/InputActionMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActionMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionMap"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""338bbfac-f9d8-4bfd-a8c1-355f2fcc877c"",
            ""actions"": [
                {
                    ""name"": ""Move1"",
                    ""type"": ""Value"",
                    ""id"": ""bc6c2fbe-1f95-407b-a5cd-8aeedc7da3be"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""ab26556b-d92e-42f0-ac41-04bcf0fc6988"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stomp"",
                    ""type"": ""Button"",
                    ""id"": ""c9ed48cf-d24d-4da7-88d6-4383c093c922"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heal"",
                    ""type"": ""Button"",
                    ""id"": ""65fc8637-1ca6-4dab-967c-a976770f5b16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PushPull"",
                    ""type"": ""Button"",
                    ""id"": ""e017eccc-b933-41f1-88c4-c0f27f401a0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrow"",
                    ""id"": ""5be99590-ed2c-4d57-a32d-8b717585b78b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1b0c6c88-c8c6-4a28-9b96-aa35dd7001de"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b834f40b-45f3-4c23-bb4c-c30b0b9bd5c7"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0036b4c3-e2ac-4dd5-8f6f-85dc6d8aa176"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stomp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""448b5d51-af55-4124-a5c3-d9addbf5b483"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44638c56-221e-4667-9bf6-50214f516101"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1ef3146-f2fa-4aa4-96fa-e90c5218c530"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26f46119-7669-468f-bb30-3bbca28c6771"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushPull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move1 = m_Player.FindAction("Move1", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Stomp = m_Player.FindAction("Stomp", throwIfNotFound: true);
        m_Player_Heal = m_Player.FindAction("Heal", throwIfNotFound: true);
        m_Player_PushPull = m_Player.FindAction("PushPull", throwIfNotFound: true);
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

    // Player
    private readonly UnityEngine.InputSystem.InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move1;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Stomp;
    private readonly InputAction m_Player_Heal;
    private readonly InputAction m_Player_PushPull;
    public struct PlayerActions
    {
        private @InputActionMap m_Wrapper;
        public PlayerActions(@InputActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move1 => m_Wrapper.m_Player_Move1;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Stomp => m_Wrapper.m_Player_Stomp;
        public InputAction @Heal => m_Wrapper.m_Player_Heal;
        public InputAction @PushPull => m_Wrapper.m_Player_PushPull;
        public UnityEngine.InputSystem.InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator UnityEngine.InputSystem.InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove1;
                @Move1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove1;
                @Move1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove1;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Stomp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStomp;
                @Stomp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStomp;
                @Stomp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStomp;
                @Heal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @PushPull.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPushPull;
                @PushPull.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPushPull;
                @PushPull.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPushPull;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move1.started += instance.OnMove1;
                @Move1.performed += instance.OnMove1;
                @Move1.canceled += instance.OnMove1;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Stomp.started += instance.OnStomp;
                @Stomp.performed += instance.OnStomp;
                @Stomp.canceled += instance.OnStomp;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
                @PushPull.started += instance.OnPushPull;
                @PushPull.performed += instance.OnPushPull;
                @PushPull.canceled += instance.OnPushPull;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove1(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnStomp(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
        void OnPushPull(InputAction.CallbackContext context);
    }
}
