namespace MauiAnimation;

public partial class MainPage : ContentPage
{
	private readonly Animation _myCustomAnimation;

	public MainPage()
	{
		InitializeComponent();

        // Create the Animation object
		_myCustomAnimation = new Animation(v => LoadingImage.Rotation = v,
			0, 360, Easing.CubicInOut);
	}

	private async void SequentialAnimationButtonOnClicked(object sender, EventArgs e)
	{
		// Hide Loading Image
		LoadingImage.Opacity = 0;

        // Show the Loading Image
        await LoadingImage.FadeTo(1, 1000, Easing.Linear);

        // Rotate the Loading Image
        await LoadingImage.RotateTo(360, 1000, Easing.CubicInOut);
        LoadingImage.Rotation = 0;

        // Hide the Loading Image
        await LoadingImage.FadeTo(0, 1000, Easing.Linear);
    }

	private async void ParallelAnimationButtonOnClicked(object sender, EventArgs e)
	{
        // Hide Loading Image
        LoadingImage.Opacity = 0;

        // Show the Loading Image
        Task<bool> fadeToAnimation = LoadingImage.FadeTo(1, 2500, Easing.Linear);

        // Rotate the Loading Image
        Task<bool> rotateToAnimation = LoadingImage.RotateTo(360, 2500, Easing.CubicInOut);

        // Start the two animations
        await Task.WhenAll(fadeToAnimation, rotateToAnimation);

        // Reset the Rotation
        LoadingImage.Rotation = 0;

        // Hide the Loading Image
        await LoadingImage.FadeTo(0, 1000, Easing.Linear);
    }

    private async void AnimationClassButtonOnClicked(object sender, EventArgs e)
    {
        // Show Loading Image
        LoadingImage.Opacity = 1;

        // Start Animation and repeat
        _myCustomAnimation.Commit(this, "animate", 16, 1000, Easing.CubicInOut,
            (v, c) => LoadingImage.Rotation = 0,
            () => true);

        // Simulate loading
        await Task.Delay(5000);

        // Stop Animation
        this.AbortAnimation("animate");
    }
}

