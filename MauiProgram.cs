using System.IO.Compression;
using Microsoft.Extensions.Logging;

namespace MaxAttachmentSize;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseSentry(options =>
            {
                // Standard options
                options.Dsn = "https://eb18e953812b41c3aeb042e666fd3b5c@o447951.ingest.sentry.io/5428537";
                options.Debug = true;
                options.SampleRate = 1.0F;

                // This should cause java.net.SocketException: Socket closed
                // for any events that get sent
                const int mb = 1024 * 1024;
                options.MaxAttachmentSize = 80 * mb;
                options.RequestBodyCompressionLevel = CompressionLevel.NoCompression;
                options.SetBeforeSend((e, h) =>
                {
                    var random = new Random();
                    var byteArray = new byte[21 * mb];
                    random.NextBytes(byteArray);

                    h.AddAttachment(byteArray, $"twentyonemb.bin");

                    return e;
                });
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}