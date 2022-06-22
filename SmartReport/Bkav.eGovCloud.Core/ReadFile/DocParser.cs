using System.IO;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// Implements loading of the text from the doc files.
    /// </summary>
    public class DocParser
    {
        readonly string _path;

        // constructors ...
        /// <summary>
        /// Creates new instance of the TextLoader.
        /// </summary>
        /// <param name="path">The path of the file to load the text</param>
        public DocParser(string path)
        {
            _path = path;
        }

        // private methods ...
        private BinaryReader GetReader(OleStream stream)
        {
            if (stream == null)
                return null;
            var streamData = stream.ReadToEnd();
            var memoryStream = new MemoryStream(streamData);
            return new BinaryReader(memoryStream);
        }

        private BinaryReader GetStreamReader(OleStorage storage, string streamName)
        {
            var stream = storage.OpenStream(streamName);
            return stream == null ? null : GetReader(stream);
        }

        private BinaryReader GetDocumentStreamReader(OleStorage storage)
        {
            return GetStreamReader(storage, "WordDocument");
        }

        private BinaryReader GetTableStreamReader(OleStorage storage, string tableName)
        {
            return GetStreamReader(storage, tableName);
        }

        private void GetDataFromFib(BinaryReader reader, out string tableName, out int pdcOffset, out uint pdcLength)
        {
            reader.BaseStream.Seek(10, SeekOrigin.Begin);
            var flags = reader.ReadUInt16();
            tableName = BitUtils.IsSet(flags, 9) ? "1Table" : "0Table";

            reader.BaseStream.Seek(418, SeekOrigin.Begin);
            pdcOffset = reader.ReadInt32();
            pdcLength = reader.ReadUInt32();
        }

        private PieceDescriptorCollection GetPieceDescriptors(BinaryReader reader, int offset, uint length)
        {
            var result = new PieceDescriptorCollection(offset, length);
            result.Read(reader);
            return result;
        }

        private string ReadString(BinaryReader reader, uint length, bool isUnicode)
        {
            if (length == 0)
                return string.Empty;

            if (isUnicode)
                length = length / 2;

            var result = string.Empty;
            for (var i = 0; i < length; i++)
            {
                if (!isUnicode)
                {
                    var ch = reader.ReadByte();
                    result += (char)ch;
                }
                else
                {
                    var ch = reader.ReadInt16();
                    result += (char)ch;
                }
            }
            return result;
        }

        private bool LoadText(OleStorage storage, out string text)
        {
            text = string.Empty;
            if (storage == null)
                return false;

            var documentReader = GetDocumentStreamReader(storage);
            if (documentReader == null)
                return false;

            int pdcOffset;
            uint pdcLength;
            string tableName;
            GetDataFromFib(documentReader, out tableName, out pdcOffset, out pdcLength);

            var tableReader = GetTableStreamReader(storage, tableName);
            if (tableReader == null)
                return false;

            var pieces = GetPieceDescriptors(tableReader, pdcOffset, pdcLength);
            if (pieces == null)
                return false;

            var count = pieces.Count;
            for (var i = 0; i < count; i++)
            {
                uint pieceStart;
                uint pieceEnd;
                var isUnicode = pieces.GetPieceFileBounds(i, out pieceStart, out pieceEnd);

                documentReader.BaseStream.Seek(pieceStart, SeekOrigin.Begin);
                text += ReadString(documentReader, pieceEnd - pieceStart, isUnicode);
            }
            return true;
        }

        // public methods ...
        /// <summary>
        /// Loads text from the file.
        /// </summary>
        /// <param name="text">The text of the file</param>
        public bool LoadText(out string text)
        {
            text = string.Empty;
            if (NativeMethods.StgIsStorageFile(_path) != 0)
                return false;

            var storage = OleStorage.CreateInstance(_path);
            try
            {
                return LoadText(storage, out text);
            }
            finally
            {
                storage.Close();
            }
        }
    }
}
