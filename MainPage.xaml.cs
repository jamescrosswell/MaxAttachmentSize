namespace MaxAttachmentSize;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            throw new Exception("Testing 1,2,3...");
        }
        catch (Exception exception)
        {
            SentrySdk.CaptureException(exception);
        }
    }
}