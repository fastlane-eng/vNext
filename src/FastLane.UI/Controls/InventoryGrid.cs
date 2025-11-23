namespace FastLane.UI.Controls
{
    using System.Collections.ObjectModel;
    using FastLane.Core.Models;
    using Microsoft.Maui.Controls;

    /// <summary>
    /// A grid control that binds to a collection of SKU items and displays them in a tabular layout.
    /// </summary>
    public class InventoryGrid : ContentView
    {
        /// <summary>
        /// Bindable property for the collection of SKUs displayed by this control.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(ObservableCollection<Sku>), typeof(InventoryGrid), default(ObservableCollection<Sku>));

        /// <summary>
        /// Gets or sets the collection of <see cref="Sku"/> items displayed in the grid.
        /// </summary>
        public ObservableCollection<Sku> ItemsSource
        {
            get => (ObservableCollection<Sku>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private readonly CollectionView _collectionView;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryGrid"/> class.
        /// Builds a collection view with templated cells to display SKU properties.
        /// </summary>
        public InventoryGrid()
        {
            _collectionView = new CollectionView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                SelectionMode = SelectionMode.None,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                        },
                        Padding = new Thickness(8, 4)
                    };

                    // SKU code
                    var codeLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = (Color)Application.Current.Resources["PrimaryColor"]
                    };
                    codeLabel.SetBinding(Label.TextProperty, new Binding(nameof(Sku.SkuCode)));

                    // Description
                    var descLabel = new Label
                    {
                        FontSize = 14,
                        TextColor = (Color)Application.Current.Resources["PrimaryColor"]
                    };
                    descLabel.SetBinding(Label.TextProperty, new Binding(nameof(Sku.Description)));

                    // Quantity on hand
                    var qtyLabel = new Label
                    {
                        FontSize = 14,
                        HorizontalTextAlignment = TextAlignment.End,
                        TextColor = (Color)Application.Current.Resources["PrimaryColor"]
                    };
                    qtyLabel.SetBinding(Label.TextProperty, new Binding(nameof(Sku.QuantityOnHand), stringFormat: "{0:N0}"));

                    // Unit price
                    var priceLabel = new Label
                    {
                        FontSize = 14,
                        HorizontalTextAlignment = TextAlignment.End,
                        TextColor = (Color)Application.Current.Resources["PrimaryColor"]
                    };
                    priceLabel.SetBinding(Label.TextProperty, new Binding(nameof(Sku.UnitPrice), stringFormat: "{0:C}"));

                    // Add children to grid with appropriate column assignments
                    grid.Children.Add(codeLabel);
                    Grid.SetColumn(descLabel, 1);
                    grid.Children.Add(descLabel);
                    Grid.SetColumn(qtyLabel, 2);
                    grid.Children.Add(qtyLabel);
                    Grid.SetColumn(priceLabel, 3);
                    grid.Children.Add(priceLabel);

                    return grid;
                })
            };

            // Bind ItemsSource of the collection view to this control's ItemsSource property
            _collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(ItemsSource), source: this));

            Content = _collectionView;
        }

        /// <summary>
        /// Adds a new SKU to the grid.
        /// </summary>
        public void AddItem(Sku sku)
        {
            if (ItemsSource == null)
            {
                ItemsSource = new ObservableCollection<Sku>();
            }
            ItemsSource.Add(sku);
        }

        /// <summary>
        /// Removes an existing SKU from the grid.
        /// </summary>
        public void RemoveItem(Sku sku)
        {
            ItemsSource?.Remove(sku);
        }
    }
}
