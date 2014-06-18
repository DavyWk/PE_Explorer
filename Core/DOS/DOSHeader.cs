using System;
using System.IO;
using System.Runtime.InteropServices;


namespace PE_Explorer.Core.DOS
{
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct DOSHeader           // DOS .EXE header
    {   // winnt.h
        public ushort e_magic;              // Magic number
        public ushort e_cblp;               // Bytes on last page of file
        public ushort e_cp;                 // Pages in file
        public ushort e_crlc;               // Relocations
        public ushort e_cparhdr;            // Size of header in paragraphs
        public ushort e_minalloc;           // Minimum extra paragraphs needed
        public ushort e_maxalloc;           // Maximum extra paragraphs needed
        public ushort e_ss;                 // Initial (relative) SS value
        public ushort e_sp;                 // Initial SP value
        public ushort e_csum;               // Checksum
        public ushort e_ip;                 // Initial IP value
        public ushort e_cs;                 // Initial (relative) CS value
        public ushort e_lfarlc;             // File address of relocation table
        public ushort e_ovno;               // Overlay number
        [MarshalAs(UnmanagedType.ByValArray,SizeConst=4)]
        public ushort[] e_res;              // Reserved words
        public ushort e_oemid;              // OEM identifier (for e_oeminfo)
        public ushort e_oeminfo;            // OEM information; e_oemid specific
         [MarshalAs(UnmanagedType.ByValArray,SizeConst=10)]
        public ushort[] e_res2;               // Reserved words
        public int e_lfanew;               // File address of new exe header

        public DOSHeader(BinaryReader br)
        {
            e_magic = br.ReadUInt16();
            e_cblp = br.ReadUInt16();
            e_cp = br.ReadUInt16();
            e_crlc = br.ReadUInt16();
            e_cparhdr = br.ReadUInt16();
            e_minalloc = br.ReadUInt16();
            e_maxalloc = br.ReadUInt16();
            e_ss = br.ReadUInt16();
            e_sp = br.ReadUInt16();
            e_csum = br.ReadUInt16();
            e_ip = br.ReadUInt16();
            e_cs = br.ReadUInt16();
            e_lfarlc = br.ReadUInt16();
            e_ovno = br.ReadUInt16();
            e_res = new ushort[4] { br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16() };
            e_oemid = br.ReadUInt16();
            e_oeminfo = br.ReadUInt16();
            e_res2 = new ushort[10] { br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16() };
            e_lfanew = br.ReadInt32();

            if (!CheckSignature())
                throw new BadImageFormatException("Invalid DOS signature");
        }

        public bool CheckSignature()
        {
            return e_magic == 0x5A4D; // "MZ"
        }

        public int GetPEHeaderOffset()
        {
            return e_lfanew;
        }
    }
}
