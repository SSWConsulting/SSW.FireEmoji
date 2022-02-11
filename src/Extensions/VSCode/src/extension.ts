import * as vscode from "vscode";

import { GitExtension, Repository } from "./api/git";

import FireEmoji from "./ssw/fireEmoji";

export function activate(context: vscode.ExtensionContext) {
    let disposable = vscode.commands.registerCommand(
        "extension.ssw-fireemoji",
        (uri?) => {
            const git = getGitExtension();

            if (!git) {
                vscode.window.showErrorMessage("Unable to load Git Extension");
                return;
            }
            if (uri) {
                const selectedRepository = git.repositories.find((repository) => {
                    return repository.rootUri.path === uri._rootUri.path;
                });
                if (selectedRepository) {
                    prefixCommit(selectedRepository);
                }
            } else {
                for (const repo of git.repositories) {
                    prefixCommit(repo);
                }
            }
        }
    );

    context.subscriptions.push(disposable);
}

async function prefixCommit(repository: Repository) {
    vscode.window.showInformationMessage('Predicting emoji for commit: ' + repository.inputBox.value);
    const emoji = (await FireEmoji(repository.inputBox.value)).trim();
    repository.inputBox.value = `${emoji} ${repository.inputBox.value}`;
}

function getGitExtension() {
    const vscodeGit =
        vscode.extensions.getExtension<GitExtension>("vscode.git");
    const gitExtension = vscodeGit && vscodeGit.exports;
    return gitExtension && gitExtension.getAPI(1);
}

export function deactivate() {}
