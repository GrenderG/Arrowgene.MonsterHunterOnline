using Avalonia.Controls;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class CraftViewer : UserControl
{
    public CraftViewer()
    {
        InitializeComponent();
        DataContext = new CraftViewerViewModel();
    }
}
