using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace pr5
{
    public partial class MainWindow : Window
    {
        private magazin_productov_pr55Entities _context;
        private string _currentTable;
        private object _selectedItem;

        public MainWindow()
        {
            InitializeComponent();
            _context = new magazin_productov_pr55Entities();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _context.categories.Load();
            _context.suppliers.Load();

            CategoryCbx.ItemsSource = _context.categories.Local;
            SupplierCbx.ItemsSource = _context.suppliers.Local;

            ShowProducts();
        }

        private void ShowProducts_Click(object sender, RoutedEventArgs e) => ShowProducts();
        private void ShowSuppliers_Click(object sender, RoutedEventArgs e) => ShowSuppliers();
        private void ShowCategories_Click(object sender, RoutedEventArgs e) => ShowCategories();

        private void ShowProducts()
        {
            _currentTable = "products";
            ConfigureUIForTable();

            _context.products.Load();
            MainDataGrid.ItemsSource = _context.products.Local;
        }

        private void ShowSuppliers()
        {
            _currentTable = "suppliers";
            ConfigureUIForTable();

            _context.suppliers.Load();
            MainDataGrid.ItemsSource = _context.suppliers.Local;
        }

        private void ShowCategories()
        {
            _currentTable = "categories";
            ConfigureUIForTable();

            _context.categories.Load();
            MainDataGrid.ItemsSource = _context.categories.Local;
        }

        private void ConfigureUIForTable()
        {
            MainDataGrid.Columns.Clear();

            ProductFields.Visibility = _currentTable == "products" ? Visibility.Visible : Visibility.Collapsed;
            SupplierFields.Visibility = _currentTable == "suppliers" ? Visibility.Visible : Visibility.Collapsed;
            CategoryFields.Visibility = _currentTable == "categories" ? Visibility.Visible : Visibility.Collapsed;

            switch (_currentTable)
            {
                case "products":
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("ID_product") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("title") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Описание", Binding = new Binding("description_pr") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена", Binding = new Binding("cost") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Категория", Binding = new Binding("categories.title") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Поставщик", Binding = new Binding("suppliers.name_of_company") });
                    break;

                case "suppliers":
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("ID_supplier") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Компания", Binding = new Binding("name_of_company") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Телефон", Binding = new Binding("number") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Email", Binding = new Binding("mail") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Адрес", Binding = new Binding("address_company") });
                    break;

                case "categories":
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("ID_category") });
                    MainDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("title") });
                    break;
            }

            ClearInputFields();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (_currentTable)
                {
                    case "products":
                        if (string.IsNullOrWhiteSpace(ProductNameTbx.Text))
                            throw new Exception("Введите название продукта");
                        if (CategoryCbx.SelectedItem == null)
                            throw new Exception("Выберите категорию");
                        if (SupplierCbx.SelectedItem == null)
                            throw new Exception("Выберите поставщика");

                        var newProduct = new products
                        {
                            title = ProductNameTbx.Text,
                            description_pr = DescriptionTbx.Text,
                            cost = decimal.Parse(PriceTbx.Text),
                            category_ID = ((categories)CategoryCbx.SelectedItem).ID_category,
                            supplier_ID = ((suppliers)SupplierCbx.SelectedItem).ID_supplier
                        };
                        _context.products.Add(newProduct);
                        break;

                    case "suppliers":
                        if (string.IsNullOrWhiteSpace(CompanyTbx.Text))
                            throw new Exception("Введите название компании");
                        if (string.IsNullOrWhiteSpace(PhoneTbx.Text))
                            throw new Exception("Введите телефон");

                        var newSupplier = new suppliers
                        {
                            name_of_company = CompanyTbx.Text,
                            number = int.Parse(PhoneTbx.Text),
                            mail = EmailTbx.Text,
                            address_company = AddressTbx.Text
                        };
                        _context.suppliers.Add(newSupplier);
                        break;

                    case "categories":
                        if (string.IsNullOrWhiteSpace(CategoryNameTbx.Text))
                            throw new Exception("Введите название категории");

                        var newCategory = new categories
                        {
                            title = CategoryNameTbx.Text
                        };
                        _context.categories.Add(newCategory);
                        break;
                }

                _context.SaveChanges();
                MainDataGrid.Items.Refresh();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования");
                return;
            }

            try
            {
                switch (_currentTable)
                {
                    case "products":
                        var product = (products)_selectedItem;
                        product.title = ProductNameTbx.Text;
                        product.description_pr = DescriptionTbx.Text;
                        product.cost = decimal.Parse(PriceTbx.Text);
                        product.category_ID = ((categories)CategoryCbx.SelectedItem).ID_category;
                        product.supplier_ID = ((suppliers)SupplierCbx.SelectedItem).ID_supplier;
                        break;

                    case "suppliers":
                        var supplier = (suppliers)_selectedItem;
                        supplier.name_of_company = CompanyTbx.Text;
                        supplier.number = int.Parse(PhoneTbx.Text);
                        supplier.mail = EmailTbx.Text;
                        supplier.address_company = AddressTbx.Text;
                        break;

                    case "categories":
                        var category = (categories)_selectedItem;
                        category.title = CategoryNameTbx.Text;
                        break;
                }

                _context.SaveChanges();
                MainDataGrid.Items.Refresh();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления");
                return;
            }

            try
            {
                switch (_currentTable)
                {
                    case "products":
                        _context.products.Remove((products)_selectedItem);
                        break;

                    case "suppliers":
                        _context.suppliers.Remove((suppliers)_selectedItem);
                        break;

                    case "categories":
                        _context.categories.Remove((categories)_selectedItem);
                        break;
                }

                _context.SaveChanges();
                MainDataGrid.Items.Refresh();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = MainDataGrid.SelectedItem;

            if (_selectedItem != null)
            {
                switch (_currentTable)
                {
                    case "products":
                        var product = (products)_selectedItem;
                        ProductNameTbx.Text = product.title;
                        DescriptionTbx.Text = product.description_pr;
                        PriceTbx.Text = product.cost.ToString();
                        CategoryCbx.SelectedItem = _context.categories.Local
                            .FirstOrDefault(c => c.ID_category == product.category_ID);
                        SupplierCbx.SelectedItem = _context.suppliers.Local
                            .FirstOrDefault(s => s.ID_supplier == product.supplier_ID);
                        break;

                    case "suppliers":
                        var supplier = (suppliers)_selectedItem;
                        CompanyTbx.Text = supplier.name_of_company;
                        PhoneTbx.Text = supplier.number.ToString();
                        EmailTbx.Text = supplier.mail;
                        AddressTbx.Text = supplier.address_company;
                        break;

                    case "categories":
                        var category = (categories)_selectedItem;
                        CategoryNameTbx.Text = category.title;
                        break;
                }
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            _selectedItem = null;

            ProductNameTbx.Clear();
            DescriptionTbx.Clear();
            PriceTbx.Clear();
            CategoryCbx.SelectedItem = null;
            SupplierCbx.SelectedItem = null;

            CompanyTbx.Clear();
            PhoneTbx.Clear();
            EmailTbx.Clear();
            AddressTbx.Clear();

            CategoryNameTbx.Clear();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context?.Dispose();
        }
    }
}