using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Input;

public class MouseInfo
{
    /// <summary>
    /// The state of mouse input during the previous update cycle.
    /// </summary>
    public MouseState PreviousState { get; private set; }

    /// <summary>
    /// The state of mouse input during the current update cycle.
    /// </summary>
    public MouseState CurrentState { get; private set; }

    /// <summary>
    /// Gets or Sets the current position of the mouse cursor in screen space.
    /// </summary>
    public Point Position
    {
        get => CurrentState.Position;
        set => SetPosition(value.X, value.Y);
    }

    /// <summary>
    /// Gets or Sets the current x-coordinate position of the mouse cursor in screen space.
    /// </summary>
    public int X
    {
        get => CurrentState.X;
        set => SetPosition(value, CurrentState.Y);
    }

    /// <summary>
    /// Gets or Sets the current y-coordinate position of the mouse cursor in screen space.
    /// </summary>
    public int Y
    {
        get => CurrentState.Y;
        set => SetPosition(CurrentState.X, value);
    }

    /// <summary>
    /// Gets the difference in the mouse cursor position between the previous and current frame.
    /// </summary>
    public Point PositionDelta => CurrentState.Position - PreviousState.Position;

    /// <summary>
    /// Gets the difference in the mouse cursor x-position between the previous and current frame.
    /// </summary>
    public int XDelta => CurrentState.X - PreviousState.X;

    /// <summary>
    /// Gets the difference in the mouse cursor y-position between the previous and current frame.
    /// </summary>
    public int YDelta => CurrentState.Y - PreviousState.Y;

    /// <summary>
    /// Gets a value that indicates if the mouse cursor moved between the previous and current frames.
    /// </summary>
    public bool WasMoved => PositionDelta != Point.Zero;

    /// <summary>
    /// Gets the cumulative value of the mouse scroll wheel since the start of the game.
    /// </summary>
    public int ScrollWheel => CurrentState.ScrollWheelValue;

    /// <summary>
    /// Gets the value of the scroll wheel between the previous and current frame.
    /// </summary>
    public int ScrollWheelDelta => CurrentState.ScrollWheelValue - PreviousState.ScrollWheelValue;

    /// <summary>
    /// Creates a new MouseInfo.
    /// </summary>
    public MouseInfo()
    {
        PreviousState = new MouseState();
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    /// Updates the state information about mouse input.
    /// </summary>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    /// Returns a value that indicates whether the specified mouse button is currently down.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>true if the specified mouse button is currently down; otherwise, false.</returns>
    public bool IsButtonDown(MouseButton button)
    {
        return IsButtonInState(button, ButtonState.Pressed);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified mouse button is current up.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>true if the specified mouse button is currently up; otherwise, false.</returns>
    public bool IsButtonUp(MouseButton button)
    {
        return IsButtonInState(button, ButtonState.Released);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified mouse button was just pressed on the current frame.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>true if the specified mouse button was just pressed on the current frame; otherwise, false.</returns>
    public bool WasButtonJustPressed(MouseButton button)
    {
        return IsButtonStateChanged(button, ButtonState.Released, ButtonState.Pressed);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified mouse button was just released on the current frame.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>true if the specified mouse button was just released on the current frame; otherwise, false.</returns>
    public bool WasButtonJustReleased(MouseButton button)
    {
        return IsButtonStateChanged(button, ButtonState.Pressed, ButtonState.Released);
    }

    private bool IsButtonStateChanged(MouseButton button, ButtonState fromState, ButtonState toState)
    {
        if (fromState == toState) return false;

        return GetButtonState(CurrentState, button) == toState &&
               GetButtonState(PreviousState, button) == fromState;
    }

    private bool IsButtonInState(MouseButton button, ButtonState buttonState)
    {
        return GetButtonState(CurrentState, button) == buttonState;
    }

    private ButtonState GetButtonState(MouseState mouseState, MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton;
            case MouseButton.Middle:
                return CurrentState.MiddleButton;
            case MouseButton.Right:
                return CurrentState.RightButton;
            case MouseButton.XButton1:
                return CurrentState.XButton1;
            case MouseButton.XButton2:
                return CurrentState.XButton2;
            default:
                return ButtonState.Released;
        }
    }

    /// <summary>
    /// Sets the current position of the mouse cursor in screen space and updates the CurrentState with the new position.
    /// </summary>
    /// <param name="x">The x-coordinate location of the mouse cursor in screen space.</param>
    /// <param name="y">The y-coordinate location of the mouse cursor in screen space.</param>
    public void SetPosition(int x, int y)
    {
        Mouse.SetPosition(x, y);
        CurrentState = new MouseState(
            x,
            y,
            CurrentState.ScrollWheelValue,
            CurrentState.LeftButton,
            CurrentState.MiddleButton,
            CurrentState.RightButton,
            CurrentState.XButton1,
            CurrentState.XButton2
        );
    }
}