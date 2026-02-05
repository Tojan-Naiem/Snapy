#!/bin/bash
echo ""
echo "  ███████╗ ███╗   ██╗ █████╗  ██████╗ ██╗  ██╗ ██╗"
echo "  ██╔════╝ ████╗  ██║██╔══██║██╔═══██║██║  ██║ ██║"
echo "  ███████╗ ██╔██╗ ██║███████║██║   ██║███████║ ██║"
echo "  ╚════██║ ██║╚██╗██║██╔══██║██║   ██║██╔══██║ ██║"
echo "  ███████║ ██║ ╚███║██║  ██║╚██████╔╝██║  ██║ ██║"
echo "  ╚══════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝ ╚═╝"
echo ""
echo "  🔧 Setting up snapy..."
echo ""

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
MODELS_DIR="$SCRIPT_DIR/Models"

# ── 1. Install Tesseract ──────────────────────────────
echo "  [1/4] Installing Tesseract..."
if command -v tesseract &> /dev/null; then 
    echo "        ✓ Tesseract already installed."
else 
    sudo apt update && sudo apt install -y tesseract-ocr tesseract-ocr-ara
    echo "        ✓ Tesseract installed."
fi

# ── 2. Install Python deps ─────────────────────────────
echo ""
echo "  [2/4] Installing Python dependencies..."
pip install torch clip-by-openai numpy --break-system-packages
echo "        ✓ Python deps installed."

# ── 3. Export ONNX models ──────────────────────────────
echo ""
echo "  [3/4] Exporting CLIP models to ONNX..."
if [ -f "$MODELS_DIR/clip_image.onnx" ] && [ -f "$MODELS_DIR/clip_text.onnx" ]; then
    echo "        ✓ ONNX models already exist."
else 
    cd "$MODELS_DIR"
    python3 export_clip_to_onnx.py
    echo "        ✓ ONNX models exported."
fi

# ── 4. Generate text embeddings ────────────────────────
echo ""
echo "  [4/4] Generating text embeddings..."
if [ -f "$MODELS_DIR/text_embeddings.bin" ]; then
    echo "        ✓ Embeddings already exist."
else
    cd "$MODELS_DIR"
    python3 export_text_embeddings.py
    echo "        ✓ Embeddings generated."
fi

# ── 5. Prepare exe ─────────────────────────────────────
echo ""
echo "  [5/5] Preparing snapy..."
chmod +x "$SCRIPT_DIR/snapy"
echo "        ✓ Done."

# ── 6. Create setup marker ─────────────────────────────
touch "$SCRIPT_DIR/.setup_complete"

echo ""
echo "  ✅ Setup complete! Use snapy like this:"
echo ""
echo "      ./snapy organize <path>"
echo "      ./snapy search <text> from <path>"
echo "      ./snapy restart <path>"
echo ""