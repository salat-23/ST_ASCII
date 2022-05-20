using System;
using System.Collections.Generic;
using ST_ASCII.Geometry;

namespace ST_ASCII.Gui;

[Obsolete("This class is WIP")]
public class SelectBox : GuiElement, IDrawable
{
    
    public class Button
    {
        public string Text { get; set; } 
        public object Data { get; set; }

        public Button(string text, object data)
        {
            Text = text;
            Data = data;
        }
    }
    
    public int Height { get; }
    public int Width { get; }
    private int SelectedIndex { get; set; }
    private List<Button> Buttons { get; }
    public int Gap { get; set; }
    public string Selector { get; set; }
    public char Border { get; set; }
    public bool IsBordered { get; set; }

    private Rectangle border;
    
    public SelectBox(List<Button> buttons, int width, int height)
    {
        Buttons = buttons;
        Height = height;
        Width = width;
        Gap = 0;
        Selector = ">";
        Border = '#';
        IsVisible = true;
        IsBordered = true;
        border = new Rectangle(width + 1, height + 1, "#");
    }
    
    public void MoveDown()
    {
        SelectedIndex++;
        if (SelectedIndex > Buttons.Count - 1) SelectedIndex = 0;
        if (SelectedIndex < 0) SelectedIndex = Buttons.Count - 1;
    }
    
    public void MoveUp()
    {
        SelectedIndex--;
        if (SelectedIndex > Buttons.Count - 1) SelectedIndex = 0;
        if (SelectedIndex < 0) SelectedIndex = Buttons.Count - 1;
    }
    
    public Button GetSelected()
    {
        return Buttons[SelectedIndex];
    }

    public void DrawSelf(Ascii ascii, int x, int y, ConsoleColor foreground, ConsoleColor background)
    {
        if (!IsVisible) return;
        int selected = SelectedIndex;
        int takenChars = Buttons.Count + Gap * Buttons.Count;
        int ableToDraw = takenChars > Height ? Height : takenChars; 
        int[] drawingIndexes = GetSlice(Buttons.ToArray(), selected, ableToDraw);

        int drawed = 0;
        foreach (var index in drawingIndexes)
        {
            int xdraw = x;
            int ydraw = y + drawed + (Gap * drawed /*- (drawingIndexes[^1] == index ? Gap : 0)*/);
            if (selected == index)
            {
                xdraw+=Selector.Length;
                ascii.Draw(x, ydraw, Selector);
            }
            ascii.Draw(xdraw, ydraw, Buttons[index].Text);
            drawed++;
        }
        ascii.Draw(x - 1, y - 1, border);
    }
    private int[] GetSlice(Button[] array, int selectedIndex, int capacity)
    {
        if (capacity > array.Length) capacity = array.Length;
        int offset = capacity % 2 == 0 ? capacity / 2 : (capacity - 1) / 2;
        int startIndex = selectedIndex - offset;

        while (startIndex + capacity > array.Length)
        {
            startIndex--;
        }
        while (startIndex < 0)
        {
            startIndex++;
        }
        int[] result = new int[capacity];
        int resultIndex = 0;
        for (int i = startIndex; i < startIndex + capacity; i++)
        {
            result[resultIndex] = i;
            resultIndex++;
        }
        return result;
    }
}