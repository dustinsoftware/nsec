using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Libsodium
    {
        internal const string crypto_secretbox_PRIMITIVE = "xsalsa20poly1305";
        internal const int crypto_secretbox_xsalsa20poly1305_KEYBYTES = 32;
        internal const int crypto_secretbox_xsalsa20poly1305_MACBYTES = 16;
        internal const int crypto_secretbox_xsalsa20poly1305_NONCEBYTES = 24;

        [DllImport(Libraries.Libsodium, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_secretbox_easy(
            ref byte c,
            in byte m,
            ulong mlen,
            in NSec.Cryptography.Nonce n,
            SecureMemoryHandle k);

        [DllImport(Libraries.Libsodium, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UIntPtr crypto_secretbox_keybytes();

        [DllImport(Libraries.Libsodium, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UIntPtr crypto_secretbox_macbytes();

        [DllImport(Libraries.Libsodium, CallingConvention = CallingConvention.Cdecl)]
        internal static extern UIntPtr crypto_secretbox_noncebytes();

        [DllImport(Libraries.Libsodium, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_secretbox_open_easy(
            ref byte m,
            in byte c,
            ulong clen,
            in NSec.Cryptography.Nonce n,
            SecureMemoryHandle k);

        [DllImport(Libraries.Libsodium, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr crypto_secretbox_primitive();
    }
}
