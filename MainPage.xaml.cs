namespace CityWeatherApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted)
        {
            // You have the required permission, proceed with geolocation.
        }
        else
        {
            // Permission denied, handle it gracefully.
        }
    }
}
