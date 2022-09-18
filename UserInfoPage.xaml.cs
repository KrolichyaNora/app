namespace app;

public partial class UserInfoPage : ContentPage
{
	public UserInfoPage()
	{
		InitializeComponent();
	}

	private async void LogoutClick(object sender, EventArgs e)
	{
		Preferences.Remove("user_id");
		await Shell.Current.GoToAsync("SignInPage");
    }
}