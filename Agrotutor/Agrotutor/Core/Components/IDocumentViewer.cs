using System;
using System.Collections.Generic;
using System.Text;

namespace Agrotutor.Core.Components
{
    public interface IDocumentViewer
    {
        void ShowDocumentFile(string filepath, string mimeType);
    }
}
