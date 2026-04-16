using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal sealed class GlobalExceptionWindow : Window
{
    public GlobalExceptionWindow(string source, Exception exception, string logPath, bool isTerminating, bool canContinue)
    {
        Title = isTerminating ? "Fatal Error" : "Unhandled Error";
        Width = 900;
        Height = 640;
        MinWidth = 760;
        MinHeight = 520;
        CanResize = true;
        Background = new SolidColorBrush(Color.Parse("#F6F1E8"));
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        Content = BuildContent(source, exception, logPath, isTerminating, canContinue);
    }

    private Control BuildContent(string source, Exception exception, string logPath, bool isTerminating, bool canContinue)
    {
        string headline = isTerminating
            ? "The UI hit a fatal error and needs to stop."
            : "The UI hit an unhandled error.";
        string description = canContinue
            ? "The exception was intercepted by the global handler. Review the details below and continue only if the UI still behaves correctly."
            : "The exception was logged before shutdown. The crash report path is included below.";

        Button closeButton = new Button
        {
            Content = isTerminating ? "Close" : "Dismiss",
            MinWidth = 120,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        closeButton.Click += (_, _) => Close();

        SelectableTextBlock details = new SelectableTextBlock
        {
            TextWrapping = TextWrapping.Wrap,
            FontFamily = FontFamily.Parse("Courier New, Consolas, monospace"),
            Text = exception.ToString()
        };

        ScrollViewer detailsScrollViewer = new ScrollViewer
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            Content = details
        };

        Grid metadata = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,*"),
            RowDefinitions = new RowDefinitions("Auto,Auto,Auto"),
            ColumnSpacing = 12,
            RowSpacing = 8
        };

        metadata.Children.Add(CreateLabel("Source", 0));
        metadata.Children.Add(CreateValue(source, 0));
        metadata.Children.Add(CreateLabel("Message", 1));
        metadata.Children.Add(CreateValue(exception.Message, 1));
        metadata.Children.Add(CreateLabel("Crash Log", 2));
        metadata.Children.Add(CreateValue(logPath, 2));

        StackPanel body = new StackPanel
        {
            Spacing = 16,
            Margin = new Thickness(22)
        };

        body.Children.Add(new TextBlock
        {
            Text = headline,
            FontSize = 26,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.Parse("#7F1D1D"))
        });
        body.Children.Add(new TextBlock
        {
            Text = description,
            TextWrapping = TextWrapping.Wrap,
            Foreground = new SolidColorBrush(Color.Parse("#374151"))
        });
        body.Children.Add(new Border
        {
            Background = new SolidColorBrush(Color.Parse("#FFFDF8")),
            BorderBrush = new SolidColorBrush(Color.Parse("#D6CCBC")),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(16),
            Child = metadata
        });
        body.Children.Add(new Border
        {
            Background = new SolidColorBrush(Color.Parse("#FFFDF8")),
            BorderBrush = new SolidColorBrush(Color.Parse("#D6CCBC")),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(12),
            Child = detailsScrollViewer
        });
        body.Children.Add(closeButton);

        return body;
    }

    private static TextBlock CreateLabel(string text, int row)
    {
        TextBlock label = new TextBlock
        {
            Text = text,
            FontWeight = FontWeight.SemiBold,
            Foreground = new SolidColorBrush(Color.Parse("#111827")),
            VerticalAlignment = VerticalAlignment.Top
        };
        Grid.SetRow(label, row);
        return label;
    }

    private static SelectableTextBlock CreateValue(string text, int row)
    {
        SelectableTextBlock value = new SelectableTextBlock
        {
            Text = text,
            Foreground = new SolidColorBrush(Color.Parse("#374151")),
            TextWrapping = TextWrapping.Wrap
        };
        Grid.SetRow(value, row);
        Grid.SetColumn(value, 1);
        return value;
    }
}
