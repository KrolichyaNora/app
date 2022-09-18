namespace app;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

	async private void RedirectSignIn(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("SignInPage");
    }

	private void SignUpClick(object sender, EventArgs e)
	{

    }
}