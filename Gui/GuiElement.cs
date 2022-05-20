using System;
using System.Runtime.CompilerServices;

namespace ST_ASCII.Gui;

/// <summary>
/// Experimental feature
/// </summary>
[Obsolete("This class is WIP")]
public abstract class GuiElement
{
    public bool IsVisible { get; set; }
    public bool IsFocused { get; set; }
}