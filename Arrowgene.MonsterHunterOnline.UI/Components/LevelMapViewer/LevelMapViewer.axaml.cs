using Avalonia.Controls;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class LevelMapViewer : UserControl
{
    public LevelMapViewer()
    {
        InitializeComponent();
        DataContext = new LevelMapViewerViewModel();
    }
}
