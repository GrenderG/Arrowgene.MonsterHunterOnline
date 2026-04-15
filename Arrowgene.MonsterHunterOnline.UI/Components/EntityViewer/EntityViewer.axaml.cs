using Avalonia.Controls;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

public partial class EntityViewer : UserControl
{
    public EntityViewer()
    {
        InitializeComponent();
        DataContext = new EntityViewerViewModel();
    }
}
