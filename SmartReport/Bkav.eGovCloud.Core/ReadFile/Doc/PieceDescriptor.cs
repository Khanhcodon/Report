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

THIS SOFTWARE IS PROVIDED ?AS IS? AND ANY EXPRESSED OR IMPLIED WARRANTIES,
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

using System.IO;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// Piece descriptor.
    /// </summary>
    internal class PieceDescriptor
    {
        const int StrSize = 8;
        byte _flags;
        byte _fn;
        uint _fc;
        short _prm;

        // public methods...
        /// <summary>
        /// Reads piece descriptor using given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader to use.</param>
        internal void Read(BinaryReader reader)
        {
            _flags = reader.ReadByte();
            _fn = reader.ReadByte();
            _fc = reader.ReadUInt32();
            _prm = reader.ReadInt16();
        }
        /// <summary>
        /// Writes piece descriptor using given binary writer.
        /// </summary>
        /// <param name="writer">The binary writer to use.</param>
        internal void Write(BinaryWriter writer)
        {
            writer.Write(_flags);
            writer.Write(_fn);
            writer.Write(_fc);
            writer.Write(_prm);
        }

        // public properties...
        /// <summary>
        /// Gets piece descriptor size.
        /// </summary>
        internal static int Size
        {
            get
            {
                return StrSize;
            }
        }
        /// <summary>
        /// Gets or sets file offset of beginning of this piece.
        /// </summary>
        internal uint Fc
        {
            get
            {
                return _fc;
            }
            set
            {
                _fc = value;
            }
        }
        /// <summary>
        /// Contains either a single sprm or else an index number of the 
        /// grpprl which contains the sprms that modify the properties of the piece.
        /// </summary>
        internal short Prm
        {
            get
            {
                return _prm;
            }
            set
            {
                _prm = value;
            }
        }
    }
}