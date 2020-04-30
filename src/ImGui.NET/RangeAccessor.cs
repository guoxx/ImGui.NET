using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ImGuiNET
{
    public unsafe struct RangeAccessor<T> where T : struct
    {
        private static readonly int s_sizeOfT = Unsafe.SizeOf<T>();

        public readonly void* Data;
        public readonly int Count;

        public RangeAccessor(IntPtr data, int count) : this(data.ToPointer(), count) { }
        public RangeAccessor(void* data, int count)
        {
            Data = data;
            Count = count;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }

                IntPtr ptr = (IntPtr) ((byte*) Data + s_sizeOfT * index);
                return (T)Marshal.PtrToStructure(ptr, typeof(T));
            }

            set
            {
                IntPtr ptr = (IntPtr)((byte*)Data + s_sizeOfT * index);
                Marshal.StructureToPtr(value, ptr, false);
            }
        }

    }

    public unsafe struct RangePtrAccessor<T> where T : struct
    {
        public readonly void* Data;
        public readonly int Count;

        public RangePtrAccessor(IntPtr data, int count) : this(data.ToPointer(), count) { }
        public RangePtrAccessor(void* data, int count)
        {
            Data = data;
            Count = count;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }

                int sizeOfPtr = Marshal.SizeOf(typeof(IntPtr));
                return Unsafe.Read<T>((byte*)Data + sizeOfPtr * index);

            }
        }
    }

    public static class RangeAccessorExtensions
    {
        public static unsafe string GetStringASCII(this RangeAccessor<byte> stringAccessor)
        {
            byte[] arr = new byte[stringAccessor.Count];
            Marshal.Copy((IntPtr)stringAccessor.Data, arr, 0, stringAccessor.Count);

            return Encoding.ASCII.GetString(arr);
        }
    }
}
