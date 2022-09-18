namespace app;

public partial class SignInPage : ContentPage
{
	IStorage storage = new DB();

	public SignInPage()
	{
		InitializeComponent();
    }

	async private void SignInClick(object sender, EventArgs e)
	{
		string login = Login.Text;
		string password = Password.Text;
		Dictionary<string, string> res = storage.Auth(login, password);
		DebugLabel.Text = string.Join(Environment.NewLine,res);
		if (res["status"] == "ok")
		{
			Preferences.Set("user_id", Convert.ToInt32(res["id"]));
            await Shell.Current.GoToAsync("UserInfoPage");
        }
    }

	async private void RedirectSignUp(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("SignUpPage");
    }
}

