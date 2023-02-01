using System;
using System.IO;
using Cobilas.IO.Alf.Alfbt;

namespace Cobilas.Unity.Management.Translation {

    public sealed class ALFBTWriteTemp : IDisposable {
        private MemoryStream stream;
        private MemoryStream header;

        public MemoryStream Stream => stream;
        public MemoryStream Header => header;

        public ALFBTWriteTemp(ALFBTLangBase langBase) {
            using (ALFBTWriter writer = ALFBTWriter.Create(header = new MemoryStream())) {
                writer.WriteElement("lang_target", langBase.Language);
                writer.WriteElement("lang_display", string.IsNullOrEmpty(langBase.DisplayName) ? langBase.Language : langBase.DisplayName);
                foreach (TextField item in langBase.OtherFields)
                    writer.WriteElement(item.Name, item.Text);
            }
            using (ALFBTWriter writer = ALFBTWriter.Create(stream = new MemoryStream()))
                foreach (ALFBTTextFlag item in langBase.Flags)
                    foreach (TextField item2 in item.TextFields)
                        writer.WriteElement(item2.Name, item2.Text);
            stream.Seek(0, SeekOrigin.Begin);
            header.Seek(0, SeekOrigin.Begin);
        }

        public void Dispose() {
            stream.Dispose();
            header.Dispose();
            header = stream = (MemoryStream)null;
        }
    }
}
