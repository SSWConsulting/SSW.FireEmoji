# Install vsce before running the script
# npm i -g vsce

Copy-Item "Model/gitmo.mlnet" -Destination "src/Extensions/VSCode/gitmo.mlnet"

# NOTE: Console app is not necessary to copy unless new features are added.

cd src/Extensions/VSCode
npm i
vsce package 0.1.1 -o "../../../releases/latest/vscode/ssw-fireemoji-vscode-preview.vsix" --pre-release
cd ../../../
