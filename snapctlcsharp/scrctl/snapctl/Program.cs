using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
public class Program
{
    public static void Main(string[] args)
    {
        string imagePath = "/home/tojan/Pictures/toji.jpg";
        string imageOnnxPath = "/home/tojan/Documents/Python Projects/snapctl/py/clip_image.onnx";
        string textOnnxPath = "/home/tojan/Documents/Python Projects/snapctl/py/clip_text.onnx";
        string textEmbeddingsPath = "/home/tojan/Documents/Python Projects/snapctl/text_embeddings.bin";
        using Image<Rgb24> image = Image.Load<Rgb24>(imagePath);
        image.Mutate(x => x.Resize(new ResizeOptions { Size = new Size(224, 224), Mode = ResizeMode.Crop }));

        float[] imageData = new float[3 * 224 * 224];
        float[] mean = { 0.48145466f, 0.4578275f, 0.40821073f };
        float[] std = { 0.26862954f, 0.26130258f, 0.27577711f };

        for (int y = 0; y < 224; y++)
        {
            for (int x = 0; x < 224; x++)
            {
                Rgb24 pixel = image[x, y];
                imageData[0 * 224 * 224 + y * 224 + x] = (pixel.R / 255f - mean[0]) / std[0];
                imageData[1 * 224 * 224 + y * 224 + x] = (pixel.G / 255f - mean[1]) / std[1];
                imageData[2 * 224 * 224 + y * 224 + x] = (pixel.B / 255f - mean[2]) / std[2];
            }
        }
        // to connect with onnx 
        int[] dimensions = new int[] { 1, 3, 224, 224 };
         var inputTensor = new DenseTensor<float>(imageData, dimensions);
         
         
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("image", inputTensor)
        };
        // load image into onnx model
        using var imageSession = new InferenceSession(imageOnnxPath);
        using var results = imageSession.Run(inputs);
        var imageFeatures = results.First().AsTensor<float>().ToArray();  // ← Add .ToArray()

        float norm = 0;
        for (int i = 0; i < imageFeatures.Length; i++)
            norm += imageFeatures[i] * imageFeatures[i];
        norm = (float)Math.Sqrt(norm);
        for (int i = 0; i < imageFeatures.Length; i++)
            imageFeatures[i] /= norm;
        
        
        Console.WriteLine("Output length: " + imageFeatures.Length);
        
        string[] labels = { "a photo of a person", "code on a screen", "text on screen", "other" };
        byte[] embedBytes = File.ReadAllBytes(textEmbeddingsPath);
        float[] textFeatures = new float[embedBytes.Length / 4];
        Buffer.BlockCopy(embedBytes, 0, textFeatures, 0, embedBytes.Length);


        float[] simiaritites = new float[labels.Length];
        long dim = imageFeatures.Length;
        for (int i = 0; i < labels.Length; i++)
        {
            float dot = 0;
            for (int j = 0; j < dim; j++)
            {
                dot += imageFeatures[j] * textFeatures[i * dim + j];
            }

            simiaritites[i] = dot;
        }

        int bestIndex = Array.IndexOf(simiaritites, simiaritites.Max());
        Console.WriteLine($"📂 Classified as: {labels[bestIndex]}");


    }
    

}