namespace app;

public partial class StartupPage : ContentPage
{
	public StartupPage()
	{
		InitializeComponent();
        Routing.RegisterRoute("SignInPage", typeof(SignInPage));
        Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
        Routing.RegisterRoute("UserInfoPage", typeof(UserInfoPage));
    }

	async private void OnAppearing(object sender, EventArgs e)
	{
		// TODO: See SignInPage.xaml.cs; do the verification here.
		//int uid = Preferences.Get("user_id", -1);
		//if (uid > 0)
		//{
		//	await Shell.Current.GoToAsync("UserInfoPage");
		//      }
		//else
		//{
		//          await Shell.Current.GoToAsync("SignInPage");
		//      }
		await Shell.Current.GoToAsync("SignInPage");
    }
}