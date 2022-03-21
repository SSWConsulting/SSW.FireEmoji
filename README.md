<div align="center">
<a href="https://fireemoji.io">
 
![FireEmoji Logo](https://user-images.githubusercontent.com/38869720/150602490-4beb2988-712d-472f-a916-c3eaad6d0279.png)
 
</a>

[![Gitmoji](https://img.shields.io/badge/gitmoji-%20😜%20😍-FFDD67.svg?style=flat-square)](https://gitmoji.dev)
![.NET Build + Test](https://github.com/SSWConsulting/SSW.FireEmoji/actions/workflows/dotnet.yml/badge.svg)
 
[fireemoji.io](https://fireemoji.io)

![Stats](https://repobeats.axiom.co/api/embed/1b61bc02b3fab87407aadaa0e892e129f7dbf5ba.svg)
 
</div>

<br>

---

<br>

This project was inspired by [GitMoji](https://github.com/carloscuesta/gitmoji) - a standard for use of emojis in commit messages.

# 🤷🏻‍♂️  Why?

At SSW, we love using GitMoji on our projects, but sometimes finding the right emoji is a challenge.

There are almost 70 different GitMojis, so sometimes its hard to pick the best one.

# 🥅  Our goal

Automate the selection of GitMojis using AI.

# 📦 Install VS Code extension

In VS Code:

1. Go to Extensions
2. Under `...` click `Install from VSIX`
3. Download and select [releases/latest/vscode/ssw-fireemoji-vscode-preview.vsix](https://github.com/SSWConsulting/SSW.FireEmoji/raw/main/releases/latest/vscode/ssw-fireemoji-vscode-preview.vsix)
4. Reload extension

![Install VS Code VSIX via VS Code IDE](images/install-vscode.gif)

## 👩‍💻 Usage

Go to `Source Control` tab, enter your message and press the 🔥 icon. :)

![Install VS Code VSIX via VS Code IDE](images/use-vscode.gif)

# 🏛 Architecture
Currently, there are 3 parts to this project:
 - SSW.FireEmoji.Core
 - SSW.FireEmoji.WebApp - Blazor WASM PWA that implements SSW.FireEmoji.Core
 - SSW.FireEmoji.Trainer

# 👨🏻‍💻 F5 Experience

## Prerequisites
 - [Dotnet](https://dotnet.microsoft.com/download)

## Running the project

1. Clone the project
