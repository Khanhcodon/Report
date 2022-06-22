#region Copyright (c) 2006-2008 Cellbi
/*
Cellbi Software Component Product
Copyright (c) 2006-2008 Cellbi
www.cellbi.com

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

	1.	Redistributions of source code must retain the above copyright notice,
			this list of conditions and the following disclaimer.

	2.	Redistributions in binary form must reproduce the above copyright notice,
			this list of conditions and the following disclaimer in the documentation
			and/or other materials provided with the distribution.

	3.	The names of the authors may not be used to endorse or promote products derived
			from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED “AS IS” AND ANY EXPRESSED OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL CELLBI
OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System.Collections;
using System.IO;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// Information about complex document part.
    /// </summary>
    internal class PieceDescriptorCollection
    {
        readonly int _offset;
        readonly uint _length;

        PieceDescriptor[] _descriptors;
        FileOffsetCollection _descriptorOffsets;

        // constructors ...
        /// <summary>
        /// Creates new instance of the PieceDescriptorCollection.
        /// </summary>
        /// <param name="offset">The offset of current structure in the stream</param>
        /// <param name="length">The length of current structure</param>
        internal PieceDescriptorCollection(int offset, uint length)
        {
            _offset = offset;
            _length = length;
        }

        // private methods ...
        private int GetOffsetsCount(int size, int structureSize)
        {
            return GetDescriptorsCount(size, structureSize) + 1;
        }

        private int GetDescriptorsCount(int size, int structureSize)
        {
            const int ptrSize = 4;
            return (size - ptrSize) / (structureSize + ptrSize);
        }

        private PieceDescriptor[] ReadDescriptors(BinaryReader reader, int count)
        {
            var result = new ArrayList();
            for (var i = 0; i < count; i++)
            {
                var descriptor = new PieceDescriptor();
                descriptor.Read(reader);
                result.Add(descriptor);
            }
            return (PieceDescriptor[])result.ToArray(typeof(PieceDescriptor));
        }

        // public methods ...
        /// <summary>
        /// Reads data using given reader.
        /// </summary>
        /// <param name="reader">The binary reader to use.</param>
        internal void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek(_offset, SeekOrigin.Begin);

            while (reader.BaseStream.Position < _offset + _length)
            {
                byte byteType = reader.ReadByte();

                switch (byteType)
                {
                    case 0:
                        reader.ReadByte();
                        break;
                    case 1:
                        var cbGrpprl = reader.ReadInt16();
                        reader.ReadBytes(cbGrpprl);
                        break;
                    case 2:
                        var tableLen = reader.ReadInt32();

                        _descriptorOffsets = new FileOffsetCollection();
                        _descriptorOffsets.Read(reader, GetOffsetsCount(tableLen, PieceDescriptor.Size));
                        _descriptors = ReadDescriptors(reader, GetDescriptorsCount(tableLen, PieceDescriptor.Size));
                        break;
                }
            }
        }
        /// <summary>
        /// Gets bounds of the specified piece in the file.
        /// </summary>
        /// <param name="piece">The number od the piece</param>
        /// <param name="start">The start file offset</param>
        /// <param name="end">The end file offset</param>
        internal bool GetPieceFileBounds(int piece, out uint start, out uint end)
        {
            var pd = _descriptors[piece];
            var fc = pd.Fc;
            var isUnicode = FileOffset.IsUnicode(fc);
            start = FileOffset.NormalizeFc(fc);

            var length = _descriptorOffsets[piece + 1].Value - _descriptorOffsets[piece].Value;
            end = start + length * FileOffset.GetFcDelta(isUnicode);

            return isUnicode;
        }

        // public properties ...
        /// <summary>
        /// The count of piece descriptors in the collection
        /// </summary>
        internal int Count
        {
            get
            {
                return _descriptors.Length;
            }
        }
    }
}
