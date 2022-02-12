![FireEmoji Logo](https://user-images.githubusercontent.com/38869720/150602490-4beb2988-712d-472f-a916-c3eaad6d0279.png)

## Why?

At SSW, we love using GitMoji on our projects, but sometimes finding the right emoji is a challenge.

There are almost 70 different GitMojis, so sometimes its hard to pick the best one.

## Features

- Automate the selection of GitMojis using AI.

![Gif Example](https://user-images.githubusercontent.com/57518417/152156252-e857b6d1-faf1-4e70-b3cf-65db16fb78f0.gif)


## Install VS Code extension

In VS Code IDE:

1. Go to Extensions
2. Under `...` click `Install from VSIX`
3. Select `releases/latest/vscode/ssw-fireemoji-vscode-preview.vsix`
4. Reload extension

![Install VS Code VSIX via VS Code IDE](https://raw.githubusercontent.com/SSWConsulting/SSW.FireEmoji/main/images/install-vscode.gif)

## Usage

Go to `Source Control` tab, enter your message and press the 🔥 icon. :)

![Install VS Code VSIX via VS Code IDE](https://raw.githubusercontent.com/SSWConsulting/SSW.FireEmoji/main/images/use-vscode.gif)

## Requirements

- Currently this will only work with Windows computers due to running an .exe under the hood. 

TODO: get it working on Macs + linux

### Install locally

Install using `code --install-extension .\ssw-fireemoji-vscode-<version-number-here>.vsix`

### Package VS Extension

1. Run `npm i -g vsce` if `vsce` is not yet installed
2. Run `vsce package` 0.1.0 --pre-release

## Extension Settings

There are currently no extension settings

**TODO:** Add model path


## Release Notes


### 0.0.1

- Initial Alpha of Fire Emoji!!

### 0.1.0

- Fixed bugs with running `gitmo.mlnet`
- Added logs
