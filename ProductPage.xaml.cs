using Poenariu_ElenaTeodora_Lab7.Models;
namespace Poenariu_ElenaTeodora_Lab7;

public partial class ProductPage : ContentPage
{
    ShopList sl;
    public ProductPage(ShopList slist)
    {
        InitializeComponent();
        sl = slist;
        BindingContext = new Product(); 
    }

async void OnSaveButtonClicked(object sender, EventArgs e)
{
    var product = (Product)BindingContext;
    await App.Database.SaveProductAsync(product);

    BindingContext = new Product();
    listView.ItemsSource = await App.Database.GetProductsAsync();
}

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem == null)
            return;

        var product = listView.SelectedItem as Product;
        await App.Database.DeleteProductAsync(product);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {

        Product p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;
            var lp = new ListProduct()
            {
                ShopListID = sl.ID,
                ProductID = p.ID
            };
            await App.Database.SaveListProductAsync(lp);
            p.ListProducts = new List<ListProduct> { lp };

            await Navigation.PopAsync();
        }


    }

}
