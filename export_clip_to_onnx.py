import torch
import clip

device="cpu"
model,preprocess=clip.load("ViT-B/32",device=device)

class TextEncoderWrapper(torch.nn.Module):
    def __init__(self, clip_model):
        super().__init__()
        self.model = clip_model

    def forward(self, tokens):
        return self.model.encode_text(tokens) 
    


dummy_image=torch.randn(1,3,224,224)

torch.onnx.export(
    model.visual,
    dummy_image,
    "clip_image.onnx",
    input_names=["image"],
    output_names=["image_features"],
    dynamic_shapes=[
        {0:"batch_size"}
    ],
    opset_version=14
)
print("Exported to clip_image.onnx")

dummy_text=torch.randint(0,49408,(1,7))
text_encoder = TextEncoderWrapper(model)

torch.onnx.export(
    text_encoder,
    dummy_text,
    "clip_text.onnx",
    input_names=["text"],
    output_names=["text_features"],
    dynamic_shapes=[
        {0:"batch_size"}
    ],
    opset_version=14
)
print("Exported to clip_text.onnx")
