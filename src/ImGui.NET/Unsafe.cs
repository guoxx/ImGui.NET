using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime.InteropServices;

namespace ImGuiNET
{
    public unsafe class Unsafe
    {
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf(typeof(T));
            
            // T[] tArray = new T[2];
            //
            // var tRef0 = __makeref(tArray[0]);
            // var tRef1 = __makeref(tArray[1]);
            //
            // IntPtr ptrToT0 = *((IntPtr*)&tRef0);
            // IntPtr ptrToT1 = *((IntPtr*)&tRef1);
            //
            // return (int)(((byte*)ptrToT1) - ((byte*)ptrToT0));
        }

        //public static T Read<T>(IntPtr source, int sizeOfT)
        //{
        //    byte* bytePtr = (byte*)source;

        //    T result = default(T);
        //    TypedReference resultRef = __makeref(result);
        //    byte* resultPtr = (byte*)*((IntPtr*)&resultRef);

        //    for (int i = 0; i < sizeOfT; ++i)
        //    {
        //        resultPtr[i] = bytePtr[i];
        //    }

        //    return result;
        //}

        public static T Read<T>(void* source)
        {
            return (T)Marshal.PtrToStructure((IntPtr)source, typeof(T)); 
            
            //byte* bytePtr = (byte*)source;

            //T result = default(T);
            //TypedReference resultRef = __makeref(result);
            //byte* resultPtr = (byte*)*((IntPtr*)&resultRef);

            //for (int i = 0; i < SizeOf<T>(); ++i)
            //{
            //    resultPtr[i] = bytePtr[i];
            //}

            //return result;

            // var obj = default(T);
            // var tr = __makeref(obj);
            //
            // //This is equivalent to shooting yourself in the foot
            // //but it's the only high-perf solution in some cases
            // //it sets the first field of the TypedReference (which is a pointer)
            // //to the address you give it, then it dereferences the value.
            // //Better be 10000% sure that your type T is unmanaged/blittable...
            // unsafe { *(IntPtr*)(&tr) = (IntPtr)source; }
            //
            // return __refvalue(tr, T);
        }

        public static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
        {
            byte* valuePtr = (byte*)startAddress;

            for (int i = 0; i < byteCount; ++i)
            {
                valuePtr[i] = value;
            }
        }

        public static void CopyBlock(void* destination, void* source, uint byteCount)
        {
            byte* bytePtr = (byte*)destination;

            byte* valuePtr = (byte*)source;

            for (int i = 0; i < byteCount; ++i)
            {
                bytePtr[i] = valuePtr[i];
            }
        }

    }
}
