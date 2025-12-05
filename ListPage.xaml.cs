using Poenariu_ElenaTeodora_Lab7.Models;

namespace Poenariu_ElenaTeodora_Lab7;

public partial class ListPage : ContentPage
{
    Product selectedItem;

    public ListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext));
    }

    void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedItem = e.SelectedItem as Product;
    }

    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        if (selectedItem == null)
            return;

        var sl = (ShopList)BindingContext;

        await App.Database.DeleteListProductByIdsAsync(sl.ID, selectedItem.ID);

        listView.ItemsSource = await App.Database.GetListProductsAsync(sl.ID);

        selectedItem = null;
    }
}
