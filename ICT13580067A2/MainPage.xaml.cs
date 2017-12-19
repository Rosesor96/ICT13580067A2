using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ICT13580067A2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            newButton.Clicked += NewButton_Clicked;
        }
        protected override void OnAppearing()
        {
            LoadData();
        }
        void LoadData()
        {
            productListView.ItemsSource = App.DbHelper.GetProducts();
        }

        void NewButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProductNewPage());
        }

        void Edit_Clicked(object sender, System.EventArgs e)
        {
            var button = sender as MenuItem;
            var product = button.CommandParameter as Product;
            Navigation.PushModalAsync(new ProductNewPage(product));
        }

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            var isOk = await DisplayAlert("ยืนยัน", "คุณต้องการลบใช่หรือไม่", "ใช่", "ไม่ใช่");

            if (isOk)
            {
                var button = sender as MenuItem;
                var product = button.CommandParameter as Product;
                App.DbHelper.DeleteProduct(product);

                await DisplayAlert("ลบสำเร็จ", "ลบข้อมูลสินค้าเรียบร้อย", "ตกลง");
                LoadData();
            }
        }
    }
}