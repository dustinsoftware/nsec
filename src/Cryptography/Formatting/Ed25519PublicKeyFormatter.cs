using System;
using System.Diagnostics;
using static Interop.Libsodium;

namespace NSec.Cryptography.Formatting
{
    internal sealed class Ed25519PublicKeyFormatter : PublicKeyFormatter
    {
        public Ed25519PublicKeyFormatter(byte[] blobHeader) : base(
            crypto_sign_ed25519_PUBLICKEYBYTES,
            blobHeader)
        {
        }

        protected override byte[] Deserialize(
            ReadOnlySpan<byte> span)
        {
            Debug.Assert(span.Length == crypto_sign_ed25519_PUBLICKEYBYTES);

            return span.ToArray();
        }

        protected override void Serialize(
            ReadOnlySpan<byte> publicKeyBytes,
            Span<byte> span)
        {
            Debug.Assert(publicKeyBytes.Length == crypto_sign_ed25519_PUBLICKEYBYTES);
            Debug.Assert(span.Length == crypto_sign_ed25519_PUBLICKEYBYTES);

            publicKeyBytes.CopyTo(span);
        }
    }
}
