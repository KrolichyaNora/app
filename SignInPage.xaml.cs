namespace app;

public partial class SignInPage : ContentPage
{
	// TODO: make API a global object
	IStorage storage = new RESTAPI();

	public SignInPage()
	{
		InitializeComponent();
    }

	async private void SignInClick(object sender, EventArgs e)
	{
		string login = Login.Text;
		string password = Password.Text;
		// TODO: do text cleanup
		User user = new(login, password);
		User res = await storage.Auth(user);
		if (res)
		{
			// TODO: We should store some token to keep the user session between restarts.
			// What should be stored?
			// These values must verify the user - so, simple ID or login wouldn't work.
			// Storing plaintext password is unsafe.
			// Storing hashed password isn't good, hashed part shouldn't be stored on client (or can it be, if the method is unknown?).
			// Combination of UID and password hash? Leaving the salt on the server, so the password couldn't be guessed?

			//Preferences.Set("token", Convert.ToInt32(res.id));

			// TODO: Is there a way to store object for the whole app?
			// Passing user everytime is pretty awkward.
			await Shell.Current.GoToAsync("UserInfoPage", new Dictionary<string,object>{ { "data", res } });
        }
		else
		{
			// TODO: notification "login/password is wrong"
		}
    }

	async private void RedirectSignUp(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("SignUpPage");
    }
}

