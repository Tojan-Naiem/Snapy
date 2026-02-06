# Snapy

AI-powered screenshot manager with intelligent classification, search, and analytics.

## Features

- **Smart Classification**: CLIP (ViT-B/32) automatically categorizes screenshots into Person, Documents, Code, Browser, Chat, Games, and Other
- **OCR Search**: Extract and search text from screenshots (English + Arabic)
- **Analytics**: Detailed statistics about your screenshot collection
- **Privacy-First**: Everything runs locally, completely offline after setup
- **Fast**: ONNX runtime for efficient CPU inference

## Quick Start

**Download Release** (Recommended)

```bash
# Linux
wget https://github.com/yourusername/snapy/releases/latest/download/snapy-v1.1.0-linux-x64.zip
unzip snapy-v1.1.0-linux-x64.zip
cd linux-x64
./setup.sh

# Windows
# Download snapy-v1.1.0-win-x64.zip from releases
# Extract, install Tesseract OCR, run setup.ps1
```

**Build from Source**

```bash
git clone https://github.com/yourusername/snapy.git
cd snapy
./setup.sh
```

## Usage

```bash
snapy organize ~/Screenshots    # Categorize screenshots
snapy search "invoice"           # Search by text content
snapy stats ~/Screenshots        # View statistics
snapy info screenshot.png        # File metadata
snapy restart ~/Screenshots      # Undo categorization
```

## How It Works

- **Classification**: CLIP model converts images to embeddings and matches against category embeddings
- **Search**: Tesseract OCR extracts text, stores in SQLite for fast full-text search
- **Performance**: ~100-200ms per classification, near-instant search

## Requirements

- **Linux**: Ubuntu/Debian, Python 3.8+
- **Windows**: Windows 10/11, Python 3.8+, Tesseract OCR
- .NET 8.0 SDK (if building from source)

## Technical Stack

C# (.NET 8.0) • CLIP (ONNX) • Tesseract OCR • SQLite • ImageSharp

## Architecture

```
Snapy.Cli/              # CLI interface
Snapy.Core/             # Domain entities
Snapy.Infrastructure/   # AI models, database, services
Models/                 # ONNX models and embeddings
```

## Known Limitations

- Categories fixed at compile-time
- No progress bars for batch operations
- Primary testing on Ubuntu/Debian

## Contributing

Contributions welcome! Submit issues or pull requests.

## License

MIT License - see [LICENSE](LICENSE) file for details.

## Acknowledgments

Built with [CLIP](https://github.com/openai/CLIP), [Tesseract OCR](https://github.com/tesseract-ocr/tesseract), and [ONNX Runtime](https://onnxruntime.ai/)

---

**Privacy Notice**: All processing happens locally. No data sent to external servers.
