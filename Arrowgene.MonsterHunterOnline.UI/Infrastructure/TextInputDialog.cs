using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal sealed class TextInputDialog : Window
{
    private readonly TextBox _textBox;

    public TextInputDialog(string title, string prompt, string initialValue, string confirmLabel = "OK")
    {
        Title = title;
        Width = 520;
        Height = 220;
        MinWidth = 420;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        Background = new SolidColorBrush(Color.Parse("#F5F4F0"));

        _textBox = new TextBox
        {
            Text = initialValue
        };
        _textBox.AttachedToVisualTree += (_, _) =>
        {
            _textBox.Focus();
            _textBox.CaretIndex = _textBox.Text?.Length ?? 0;
        };
        _textBox.KeyDown += TextBoxOnKeyDown;

        Button cancelButton = new Button
        {
            Content = "Cancel",
            MinWidth = 96
        };
        cancelButton.Click += (_, _) => Close(null);

        Button confirmButton = new Button
        {
            Content = confirmLabel,
            MinWidth = 96
        };
        confirmButton.Click += (_, _) => Close(TrimmedValueOrNull());

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 16,
            Children =
            {
                new TextBlock
                {
                    Text = prompt,
                    FontSize = 14,
                    FontWeight = FontWeight.SemiBold,
                    Foreground = new SolidColorBrush(Color.Parse("#111827")),
                    TextWrapping = TextWrapping.Wrap
                },
                _textBox,
                new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Spacing = 10,
                    Children =
                    {
                        cancelButton,
                        confirmButton
                    }
                }
            }
        };
    }

    private void TextBoxOnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Close(TrimmedValueOrNull());
            e.Handled = true;
        }
    }

    private string? TrimmedValueOrNull()
    {
        string value = _textBox.Text?.Trim() ?? string.Empty;
        return value.Length == 0 ? null : value;
    }
}
