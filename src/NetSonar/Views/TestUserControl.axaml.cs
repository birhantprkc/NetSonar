using System.Collections.ObjectModel;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NetSonar.Avalonia.Views;

public partial class TestUserControl : UserControl
{
    public class TestClass
    {
        public int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }

    public ObservableCollection<TestClass> TestCollection { get; } =
    [
        new() { Index = 1, Name = "Test 1", Description = "Description 1", Group = "Group 1"},
        new() { Index = 2, Name = "Test 2", Description = "Description 2", Group = "Group 1" },
        new() { Index = 3, Name = "Test 3", Description = "Description 3", Group = "Group 2" },
        new() { Index = 4, Name = "Test 4", Description = "Description 4", Group = "Group 2" },
    ];

    public int? ItemCount { get; set; } = 0;


    public TestUserControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        var groupView = new DataGridCollectionView(TestCollection);
        groupView.GroupDescriptions.Add(new DataGridPathGroupDescription("Group"));
        MyGrid.ItemsSource = groupView;
    }
}