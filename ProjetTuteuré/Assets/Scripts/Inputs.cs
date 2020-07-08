// GENERATED AUTOMATICALLY FROM 'Assets/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""9ed2d23c-d126-4ee0-829f-8620197e3d38"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2441e16b-9c83-4e00-a395-5f6dd7f8c16e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch Weapon"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d9d4698b-1781-44ba-99ae-b3b8aed0b37b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action Key"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1240bbe4-c895-484a-adf0-78956c167441"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire Key"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cdb1bd45-b91c-4656-b34e-23f22c259327"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""957294dd-755f-44a4-8a58-995851a2e536"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""973c14e7-bbd5-4b4d-be71-258e0a5ec6e8"",
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
                    ""id"": ""c0eb5633-a8c4-4e8b-aa60-d43bdd20cdef"",
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
                    ""id"": ""27e12cbc-09c9-45f5-87aa-42e5c8fcc8da"",
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
                    ""id"": ""84f56ca9-c763-4e60-b3e8-69dde758a4df"",
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
                    ""id"": ""fa7ed391-8064-4a59-a162-e50a685db4a9"",
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
                    ""id"": ""10fe48c6-cf4f-42c0-80bf-246378222a90"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f98a45e-740c-487f-9351-7a00f38f1235"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9b66b8e-f0af-4066-91c5-4564f90070c5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f99cb54-9465-4cdf-9e8a-057c7ea3b19e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu Controls"",
            ""id"": ""f7ec9ce0-163a-480a-b1a0-c973530565ef"",
            ""actions"": [
                {
                    ""name"": ""Pause Menu"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7e3ec849-524c-402b-924c-e110cc6b4971"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Open Inventory"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0612f405-3a48-4794-b1bc-700f25d441b4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5e409b52-1c42-4ed5-83cf-683206f9e33c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86c06610-9c78-4fa5-bd84-28ccb607a7cb"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_SwitchWeapon = m_PlayerControls.FindAction("Switch Weapon", throwIfNotFound: true);
        m_PlayerControls_ActionKey = m_PlayerControls.FindAction("Action Key", throwIfNotFound: true);
        m_PlayerControls_FireKey = m_PlayerControls.FindAction("Fire Key", throwIfNotFound: true);
        m_PlayerControls_MousePosition = m_PlayerControls.FindAction("MousePosition", throwIfNotFound: true);
        // Menu Controls
        m_MenuControls = asset.FindActionMap("Menu Controls", throwIfNotFound: true);
        m_MenuControls_PauseMenu = m_MenuControls.FindAction("Pause Menu", throwIfNotFound: true);
        m_MenuControls_OpenInventory = m_MenuControls.FindAction("Open Inventory", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_SwitchWeapon;
    private readonly InputAction m_PlayerControls_ActionKey;
    private readonly InputAction m_PlayerControls_FireKey;
    private readonly InputAction m_PlayerControls_MousePosition;
    public struct PlayerControlsActions
    {
        private @Inputs m_Wrapper;
        public PlayerControlsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @SwitchWeapon => m_Wrapper.m_PlayerControls_SwitchWeapon;
        public InputAction @ActionKey => m_Wrapper.m_PlayerControls_ActionKey;
        public InputAction @FireKey => m_Wrapper.m_PlayerControls_FireKey;
        public InputAction @MousePosition => m_Wrapper.m_PlayerControls_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @SwitchWeapon.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchWeapon;
                @ActionKey.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActionKey;
                @ActionKey.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActionKey;
                @ActionKey.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActionKey;
                @FireKey.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFireKey;
                @FireKey.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFireKey;
                @FireKey.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFireKey;
                @MousePosition.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
                @ActionKey.started += instance.OnActionKey;
                @ActionKey.performed += instance.OnActionKey;
                @ActionKey.canceled += instance.OnActionKey;
                @FireKey.started += instance.OnFireKey;
                @FireKey.performed += instance.OnFireKey;
                @FireKey.canceled += instance.OnFireKey;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // Menu Controls
    private readonly InputActionMap m_MenuControls;
    private IMenuControlsActions m_MenuControlsActionsCallbackInterface;
    private readonly InputAction m_MenuControls_PauseMenu;
    private readonly InputAction m_MenuControls_OpenInventory;
    public struct MenuControlsActions
    {
        private @Inputs m_Wrapper;
        public MenuControlsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseMenu => m_Wrapper.m_MenuControls_PauseMenu;
        public InputAction @OpenInventory => m_Wrapper.m_MenuControls_OpenInventory;
        public InputActionMap Get() { return m_Wrapper.m_MenuControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuControlsActions instance)
        {
            if (m_Wrapper.m_MenuControlsActionsCallbackInterface != null)
            {
                @PauseMenu.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPauseMenu;
                @OpenInventory.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenInventory;
            }
            m_Wrapper.m_MenuControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
            }
        }
    }
    public MenuControlsActions @MenuControls => new MenuControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
        void OnActionKey(InputAction.CallbackContext context);
        void OnFireKey(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface IMenuControlsActions
    {
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
    }
}
