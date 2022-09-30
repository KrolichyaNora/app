namespace app;

public partial class UserInfoPage : ContentPage, IQueryAttributable
{
	User user;

	public UserInfoPage()
	{
		InitializeComponent();
	}

    async private void OnAppearing(object sender, EventArgs e)
    {
        UserInfoString.Text = user;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        user = (User)query["data"];
    }

    private async void LogoutClick(object sender, EventArgs e)
	{
        // TODO: see the SignInPage.xaml.cs; do the deletion of the token here.
		//Preferences.Remove("user_id");
		await Shell.Current.GoToAsync("SignInPage");
    }
}