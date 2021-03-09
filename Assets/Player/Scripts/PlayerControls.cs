// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""WorldActions"",
            ""id"": ""4f5575e8-e11f-4986-b295-ff0e9ec32a56"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""228639f4-87b5-4253-97a7-0616b0d475ca"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""65454454-3df5-4014-89c3-570f4de497ba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""238a734a-ece2-4a62-9e1f-a2aa3fa4564d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Prone"",
                    ""type"": ""Button"",
                    ""id"": ""2b2a9672-4eb1-4d03-b38e-f55ffa0c4183"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""2dedbe6c-6a79-4a19-87ce-35a2f376d70f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""5508b963-3239-4063-825b-c337832167a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""cd7005d1-da49-413e-a939-32a328e64e4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""1723e112-67b8-4833-8809-ec9f96f34a9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""cfb00916-631a-47a8-a447-9d7ca5a0ff12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""144c4eed-dccb-4330-94a5-a168178a21d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""b12b1621-9427-42a6-a31f-9feb5b37c830"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""96d1b2aa-2648-4d6a-afd0-bcaffd392016"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34b426e7-1d14-485f-8e3c-71534e0e508c"",
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
                    ""id"": ""92e1bd53-0569-4586-be0e-eb295d0f7f10"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d64e96da-821e-4fd9-a282-724d06170fb2"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f954a7a-62b0-46c6-91e2-d2f95e4e0380"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""0ce3eae5-df79-4abf-bdae-ef03b902ba64"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3c3402db-8954-4f56-b093-30a4270928a1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a32a28ad-48b9-4520-bb6d-da301babb1e0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""88ccb92b-a887-4b30-b22c-8d6da590ad4a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aab97c47-a00a-446e-90e8-aa8f5fd4ce1c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e5fb1220-daf5-4bab-a7e1-59dd6c1a2439"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c32fdf86-d2d5-433a-ae81-07905fb4aea8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""239c210b-9bcc-421a-b776-c729dc223764"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd1b9fe5-e4fc-4455-be53-2bd785fe839b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fbee383-c3fe-4548-9f9d-3f5061c2a015"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // WorldActions
        m_WorldActions = asset.FindActionMap("WorldActions", throwIfNotFound: true);
        m_WorldActions_Look = m_WorldActions.FindAction("Look", throwIfNotFound: true);
        m_WorldActions_Move = m_WorldActions.FindAction("Move", throwIfNotFound: true);
        m_WorldActions_Jump = m_WorldActions.FindAction("Jump", throwIfNotFound: true);
        m_WorldActions_Prone = m_WorldActions.FindAction("Prone", throwIfNotFound: true);
        m_WorldActions_Crouch = m_WorldActions.FindAction("Crouch", throwIfNotFound: true);
        m_WorldActions_Interact = m_WorldActions.FindAction("Interact", throwIfNotFound: true);
        m_WorldActions_Shoot = m_WorldActions.FindAction("Shoot", throwIfNotFound: true);
        m_WorldActions_Aim = m_WorldActions.FindAction("Aim", throwIfNotFound: true);
        m_WorldActions_Sprint = m_WorldActions.FindAction("Sprint", throwIfNotFound: true);
        m_WorldActions_Reload = m_WorldActions.FindAction("Reload", throwIfNotFound: true);
        m_WorldActions_Drop = m_WorldActions.FindAction("Drop", throwIfNotFound: true);
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

    // WorldActions
    private readonly InputActionMap m_WorldActions;
    private IWorldActionsActions m_WorldActionsActionsCallbackInterface;
    private readonly InputAction m_WorldActions_Look;
    private readonly InputAction m_WorldActions_Move;
    private readonly InputAction m_WorldActions_Jump;
    private readonly InputAction m_WorldActions_Prone;
    private readonly InputAction m_WorldActions_Crouch;
    private readonly InputAction m_WorldActions_Interact;
    private readonly InputAction m_WorldActions_Shoot;
    private readonly InputAction m_WorldActions_Aim;
    private readonly InputAction m_WorldActions_Sprint;
    private readonly InputAction m_WorldActions_Reload;
    private readonly InputAction m_WorldActions_Drop;
    public struct WorldActionsActions
    {
        private @PlayerControls m_Wrapper;
        public WorldActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_WorldActions_Look;
        public InputAction @Move => m_Wrapper.m_WorldActions_Move;
        public InputAction @Jump => m_Wrapper.m_WorldActions_Jump;
        public InputAction @Prone => m_Wrapper.m_WorldActions_Prone;
        public InputAction @Crouch => m_Wrapper.m_WorldActions_Crouch;
        public InputAction @Interact => m_Wrapper.m_WorldActions_Interact;
        public InputAction @Shoot => m_Wrapper.m_WorldActions_Shoot;
        public InputAction @Aim => m_Wrapper.m_WorldActions_Aim;
        public InputAction @Sprint => m_Wrapper.m_WorldActions_Sprint;
        public InputAction @Reload => m_Wrapper.m_WorldActions_Reload;
        public InputAction @Drop => m_Wrapper.m_WorldActions_Drop;
        public InputActionMap Get() { return m_Wrapper.m_WorldActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WorldActionsActions set) { return set.Get(); }
        public void SetCallbacks(IWorldActionsActions instance)
        {
            if (m_Wrapper.m_WorldActionsActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnLook;
                @Move.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnJump;
                @Prone.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnProne;
                @Prone.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnProne;
                @Prone.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnProne;
                @Crouch.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnCrouch;
                @Interact.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnInteract;
                @Shoot.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnShoot;
                @Aim.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnAim;
                @Sprint.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnSprint;
                @Reload.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnReload;
                @Drop.started -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_WorldActionsActionsCallbackInterface.OnDrop;
            }
            m_Wrapper.m_WorldActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Prone.started += instance.OnProne;
                @Prone.performed += instance.OnProne;
                @Prone.canceled += instance.OnProne;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
            }
        }
    }
    public WorldActionsActions @WorldActions => new WorldActionsActions(this);
    public interface IWorldActionsActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnProne(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
    }
}
