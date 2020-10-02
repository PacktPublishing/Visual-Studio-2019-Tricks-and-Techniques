import * as vscode from 'vscode';

export function activate(context: vscode.ExtensionContext) {
	const disposable = vscode.commands.registerCommand('headerinsertervscode.addCodeHeader', () => {
		// Get the active text editor
		const editor = vscode.window.activeTextEditor;

		if (editor) {
			var header = "//\r\n// Copyright (c) Visual Studio 2019 Tips and Tricks. All rights reserved.\r\n// Licensed under the Creative Commons licence. See LICENSE file in the project root for full license information.\r\n//\r\n\r\n";
			var insertPosition = new vscode.Position(0, 0);
			editor.edit(editBuilder => {
				editBuilder.insert(insertPosition, header);
			});
		}
	});

	context.subscriptions.push(disposable);
}

export function deactivate() { }
