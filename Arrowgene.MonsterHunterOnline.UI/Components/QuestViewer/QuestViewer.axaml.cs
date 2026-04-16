using System;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class QuestViewer : UserControl
{
    // Flag backgrounds
    private static readonly IBrush FlagOnBrush = SolidColorBrush.Parse("#DDD9CF");
    private static readonly IBrush FlagOffBrush = SolidColorBrush.Parse("#EDEAE4");

    public static readonly IValueConverter FlagBgConverter =
        new FuncValueConverter<bool, IBrush>(v => v ? FlagOnBrush : FlagOffBrush);

    // Chain graph node styling
    private static readonly IBrush ChainBgDefault = SolidColorBrush.Parse("#FFFFFF");
    private static readonly IBrush ChainBgSelected = SolidColorBrush.Parse("#E8E5DE");
    private static readonly IBrush ChainBorderDefault = SolidColorBrush.Parse("#D7D3CC");
    private static readonly IBrush ChainBorderSelected = SolidColorBrush.Parse("#0F172A");

    public static readonly IValueConverter ChainBgConverter =
        new FuncValueConverter<bool, IBrush>(v => v ? ChainBgSelected : ChainBgDefault);

    public static readonly IValueConverter ChainBorderConverter =
        new FuncValueConverter<bool, IBrush>(v => v ? ChainBorderSelected : ChainBorderDefault);

    public static readonly IValueConverter ChainWeightConverter =
        new FuncValueConverter<bool, FontWeight>(v => v ? FontWeight.Bold : FontWeight.Regular);

    // Toggle button label
    public static readonly IValueConverter ViewToggleLabelConverter =
        new FuncValueConverter<bool, string>(v => v ? "Show Details" : "Show Chain");

    public QuestViewer()
    {
        InitializeComponent();
        DataContext = new QuestViewerViewModel();
    }
}
