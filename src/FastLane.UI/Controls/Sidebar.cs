using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// A sidebar navigation control that displays a vertical list of menu items and allows the user to select one.
    /// This control exposes bindable properties for the collection of menu items, the selected index and a command
    /// that is invoked when an item is selected. The control uses theme colours for background and selection styling.
    /// </summary>
    public class Sidebar : ContentView
    {
        /// <summary>
        /// Bindable property for the collection of menu item strings displayed in the sidebar.
        /// </summary>
        public static readonly BindableProperty MenuItemsProperty =
            BindableProperty.Create(
                nameof(MenuItems),
                typeof(ObservableCollection<string>),
                typeof(Sidebar),
                new ObservableCollection<string>());

        /// <summary>
        /// Gets or sets the collection of menu items.
        /// </summary>
        public ObservableCollection<string> MenuItems
        {
            get => (ObservableCollection<string>)GetValue(MenuItemsProperty);
            set => SetValue(MenuItemsProperty, value);
        }

        /// <summary>
        /// Bindable property for the command to execute when a menu item is selected.
        /// </summary>
        public static readonly BindableProperty ItemSelectedCommandProperty =
            BindableProperty.Create(
                nameof(ItemSelectedCommand),
                typeof(ICommand),
                typeof(Sidebar),
                default(ICommand));

        /// <summary>
        /// Gets or sets the command executed when a menu item is selected. The selected menu item string
        /// will be passed as the command parameter.
        /// </summary>
        public ICommand ItemSelectedCommand
        {
            get => (ICommand)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

        /// <summary>
        /// Bindable property representing the index of the selected menu item.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(Sidebar),
                -1,
                BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the index of the currently selected menu item.
        /// </summary>
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        private readonly CollectionView _collectionView;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sidebar"/> class.
        /// </summary>
        public Sidebar()
        {
            // Setup the collection view for menu items
            _collectionView = new CollectionView
            {
                SelectionMode = SelectionMode.Single,
                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                {
                    ItemSpacing = 4
                },
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label
                    {
                        Padding = new Thickness(16, 12),
                        FontSize = 16,
                        TextColor = (Color)Application.Current.Resources["PrimaryColor"]
                    };
                    label.SetBinding(Label.TextProperty, ".");

                    var container = new Grid
                    {
                        BackgroundColor = (Color)Application.Current.Resources["SecondaryColor"]
                    };
                    container.Add(label);

                                return container;
                })
            };
            _collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MenuItems), source: this));
            _collectionView.SelectionChanged += OnSelectionChanged;

            // Set styling for sidebar
            BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];

            Content = _collectionView;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Determine selected item and update SelectedIndex property
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selected = e.CurrentSelection.FirstOrDefault() as string;
                SelectedIndex = MenuItems?.IndexOf(selected) ?? -1;
                // Execute command if provided
                if (selected != null && ItemSelectedCommand?.CanExecute(selected) == true)
                {
                    ItemSelectedCommand.Execute(selected);
                }
            }
            else
            {
                SelectedIndex = -1;
            }
        }
    }
}
