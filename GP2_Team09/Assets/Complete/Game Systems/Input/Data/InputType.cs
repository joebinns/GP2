namespace GameProject.Inputs {
    public enum InputType {
        // Other
        Pause = 0,
        Save = 1,
        Load = 2,
        Screenshot = 3,

        // Actions
        Cursor = 10,
        Pointer,
        Use,
        Primary,
        Secondary,
        Switch,

        // Interfaces
        Character = 30,
        Inventory,
        Skills,
        Spells,
        Journal,
        Map,

        // Movement
        Turn = 50,
        Tilt,
        Roll,
        Lean,

        Move,
        Crouch,
        Sneak,
        Walk,
        Run,

        Jump,
        Dodge,
    }
}