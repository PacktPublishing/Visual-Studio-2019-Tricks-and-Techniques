using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace HeaderInserterMac
{
    class HeaderInsertHandler : CommandHandler
    {
        protected override void Run()
        {
            var textBuffer = IdeApp.Workbench.ActiveDocument.GetContent<ITextBuffer>();
            var header = "//\r\n// Copyright (c) Visual Studio 2019 Tips and Tricks. All rights reserved.\r\n// Licensed under the Creative Commons licence. See LICENSE file in the project root for full license information.\r\n//\r\n\r\n";
            var insertPosition = 0;
            textBuffer.Insert(insertPosition, header);
        }

        protected override void Update(CommandInfo info)
        {
            var textBuffer = IdeApp.Workbench.ActiveDocument.GetContent<ITextBuffer>();
            if (textBuffer?.AsTextContainer() is SourceTextContainer container)
                info.Enabled = container.GetTextBuffer() != null;
        }

    }
}
