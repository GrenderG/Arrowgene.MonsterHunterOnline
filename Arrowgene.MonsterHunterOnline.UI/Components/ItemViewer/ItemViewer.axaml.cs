using Avalonia.Controls;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class ItemViewer : UserControl
{
    public ItemViewer()
    {
        InitializeComponent();
        DataContext = new ItemViewerViewModel();
    }
}
