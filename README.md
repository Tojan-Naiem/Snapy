# Snapy

AI-powered screenshot manager with intelligent classification, search, and analytics.

## Overview

Snapy automatically analyzes and categorizes your screenshots using AI, extracts text for searching, and provides detailed insights about your screenshot collection. Everything runs locally on your machine with complete privacy.

## Features

- **Smart Classification**: Uses CLIP (ViT-B/32) to categorize screenshots into Person, Documents, Code, Browser, Chat, Games, and Other
- **OCR Text Search**: Extract and search text from screenshots (English + Arabic support)
- **Statistics & Analytics**: Detailed insights about your screenshot collection
- **File Metadata**: Track size, dimensions, creation time, and last organization date
- **Undo Capability**: Easily restore files to their original location
- **Offline Operation**: Works completely offline after initial setup
- **Fast Performance**: ONNX runtime for efficient CPU-based inference

## Quick Start

### Download Pre-built Release

**Linux:**
```bash
wget https://github.com/yourusername/snapy/releases/latest/download/snapy-v1.0.0-linux-x64.zip
unzip snapy-v1.0.0-linux-x64.zip
cd linux-x64
```

**Windows:**
1. Download `snapy-v1.0.0-win-x64.zip` from [Releases](https://github.com/yourusername/snapy/releases)
2. Extract to a folder
3. Install [Tesseract OCR](https://github.com/UB-Mannheim/tesseract/wiki)

### Building from Source

```bash
git clone https://github.com/yourusername/snapy.git
cd snapy
chmod +x setup.sh
./setup.sh
```

## Usage

### Organize Screenshots

Analyze and categorize your screenshots:
```bash
snapy organize ~/Screenshots
```

### Search by Text

Find screenshots containing specific text:
```bash
snapy search "invoice"
```

### View Statistics

Get detailed stats about your collection:
```bash
snapy stats ~/Screenshots
```

Example output:
```
──────────────────────────────────
Folder: /home/user/Screenshots
──────────────────────────────────
Total files: 247
Total size: 156.32MB
──────────────────────────────────
Files by category:
  Person              : 45
  Documents           : 78
  Code                : 34
  Browser             : 62
  Chat                : 18
  Games               : 7
  other               : 3
──────────────────────────────────
Last organized: Monday, February 3, 2026 2:45 PM
──────────────────────────────────
```

### File Information

View metadata for a specific screenshot:
```bash
snapy info ~/Screenshots/screenshot.png
```

### Undo Organization

Restore files to their original location:
```bash
snapy restart ~/Screenshots
```

## How It Works

### Image Classification

Snapy uses OpenAI's CLIP model to understand image content:
1. Converts images to embeddings using Vision Transformer (ViT-B/32)
2. Compares against pre-computed category embeddings
3. Assigns to the category with highest similarity score

### Text Extraction

For searchability, Snapy uses Tesseract OCR:
1. Extracts text from screenshots in multiple languages
2. Stores in SQLite database for fast lookup
3. Enables full-text search across entire collection

### Performance

- Classification: ~100-200ms per image (CPU)
- OCR: ~500ms-1s per image (cached after first run)
- Search: Near-instant (SQLite FTS)

## Architecture

```
snapy/
├── Snapy.Cli/              # Command-line interface
│   ├── Commands/           # Command implementations
│   └── Program.cs          # Entry point
├── Snapy.Core/             # Domain entities and interfaces
│   ├── Entity/
│   └── Interfaces/
├── Snapy.Infrastructure/   # Implementation layer
│   ├── Core/               # AI models (CLIP classifier)
│   ├── Repository/         # Database operations
│   └── Services/           # Business logic
└── Models/                 # ONNX models and embeddings
    ├── clip_image.onnx
    ├── clip_text.onnx
    └── text_embeddings.bin
```

## Technical Stack

- **Language**: C# (.NET 8.0)
- **AI Model**: CLIP (ViT-B/32) via ONNX
- **OCR**: Tesseract 4.x/5.x
- **Database**: SQLite with FTS5
- **Image Processing**: ImageSharp
- **Inference**: Microsoft.ML.OnnxRuntime

## Requirements

### Linux
- Ubuntu/Debian-based distribution
- Python 3.8+ (for setup only)
- .NET 8.0 SDK (if building from source)

### Windows
- Windows 10/11 (64-bit)
- Python 3.8+ (for setup only)
- Tesseract OCR
- .NET 8.0 SDK (if building from source)

## Customization

### Adding Custom Categories

1. Edit `Snapy.Infrastructure/Categories.cs`:
```csharp
public static string[] categories = { 
    "Person", "Documents", "Code", "Browser", 
    "Chat", "Games", "YourCategory", "other"
};
```

2. Regenerate embeddings:
```bash
cd Models
python3 export_text_embeddings.py
```

3. Rebuild the project

## Known Limitations

- Categories are fixed at compile-time
- No progress indicators for batch operations
- Requires Python for initial setup
- Primary testing on Ubuntu/Debian

## Roadmap

- [ ] Progress bars for long operations
- [ ] Runtime-configurable categories
- [ ] macOS support and testing
- [ ] Duplicate screenshot detection
- [ ] Web UI for browsing organized screenshots
- [ ] Multi-language support beyond English/Arabic

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

### Development Setup

```bash
git clone https://github.com/yourusername/snapy.git
cd snapy
./setup.sh
dotnet build
```

### Running Tests

```bash
dotnet test
```

## License

MIT License - see [LICENSE](LICENSE) file for details.

## Acknowledgments

- [CLIP by OpenAI](https://github.com/openai/CLIP) - Vision-language model
- [Tesseract OCR](https://github.com/tesseract-ocr/tesseract) - Text extraction
- [ONNX Runtime](https://onnxruntime.ai/) - Efficient inference


---

**Privacy Notice**: Snapy processes all data locally on your machine. No information is sent to external servers.
