namespace app;

public partial class SignUpPage : ContentPage
{
    IStorage storage = new DB();

    public SignUpPage()
	{
		InitializeComponent();
	}

	async private void RedirectSignIn(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("SignInPage");
    }

	async private void SignUpClick(object sender, EventArgs e)
	{
        string login = Login.Text;
        string password = Password.Text;
        Dictionary<string, string> res = storage.Register(login, password);
        DebugLabel.Text = string.Join(Environment.NewLine, res);
        if (res["status"] == "ok")
        {
            Preferences.Set("user_id", Convert.ToInt32(res["id"]));
            await Shell.Current.GoToAsync("UserInfoPage");
        }
    }
}