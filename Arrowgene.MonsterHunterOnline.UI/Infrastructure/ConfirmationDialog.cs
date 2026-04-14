using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal sealed class ConfirmationDialog : Window
{
    public ConfirmationDialog(string title, string message, string confirmLabel = "Confirm")
    {
        Title = title;
        Width = 460;
        Height = 210;
        MinWidth = 400;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        Background = new SolidColorBrush(Color.Parse("#F5F4F0"));

        Button cancelButton = new Button
        {
            Content = "Cancel",
            MinWidth = 96
        };
        cancelButton.Click += (_, _) => Close(false);

        Button confirmButton = new Button
        {
            Content = confirmLabel,
            MinWidth = 96
        };
        confirmButton.Click += (_, _) => Close(true);

        Content = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 16,
            Children =
            {
                new TextBlock
                {
                    Text = message,
                    FontSize = 14,
                    FontWeight = FontWeight.SemiBold,
                    Foreground = new SolidColorBrush(Color.Parse("#111827")),
                    TextWrapping = TextWrapping.Wrap
                },
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
}
