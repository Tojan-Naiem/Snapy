public class ModelPaths
{
    private static readonly string BaseDir = Path.GetDirectoryName(
        System.Reflection.Assembly.GetExecutingAssembly().Location
    ) ?? AppContext.BaseDirectory;
    public static readonly string ImageOnnx = Path.Combine(BaseDir, "Models", "clip_image.onnx");
    public static readonly string TextOnnx = Path.Combine(BaseDir, "Models", "clip_text.onnx");
    public static readonly string TextEmbeddings = Path.Combine(BaseDir, "Models", "text_embeddings.bin");


}