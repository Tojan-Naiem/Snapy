import torch
import clip
import numpy as np

device = "cpu"
model, preprocess = clip.load("ViT-B/32", device=device)

labels = [
       "a screenshot of a web browser",
    "a screenshot of code editor or terminal",
    "a screenshot of a chat or messaging app",
    "a screenshot of a game",
    "a screenshot of a document or spreadsheet",
    "a screenshot of social media",
    "a screenshot of system settings",
    "a screenshot of an error message",
    "a screenshot of something else"
    ]
text = clip.tokenize(labels).to(device)

with torch.no_grad():
    text_features = model.encode_text(text)
    text_features /= text_features.norm(dim=-1, keepdim=True)  # Normalize
    text_features = text_features.cpu().numpy()

text_features.astype(np.float32).tofile("text_embeddings.bin")
print(f"Saved {text_features.shape} embeddings")