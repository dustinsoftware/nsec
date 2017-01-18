using System;
using NSec.Cryptography;
using Xunit;

namespace NSec.Tests.Core
{
    public static class PublicKeyTests
    {
        public static readonly TheoryData<Type> AsymmetricKeyAlgorithms = Registry.AsymmetricAlgorithms;
        public static readonly TheoryData<Type, KeyBlobFormat> PublicKeyBlobFormats = Registry.PublicKeyBlobFormats;

        #region Import

        [Fact]
        public static void ImportWithNullAlgorithm()
        {
            Assert.Throws<ArgumentNullException>("algorithm", () => PublicKey.Import(null, ReadOnlySpan<byte>.Empty, KeyBlobFormat.None));
        }

        [Theory]
        [MemberData(nameof(AsymmetricKeyAlgorithms))]
        public static void ImportWithFormatNone(Type algorithmType)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            Assert.Throws<ArgumentException>("format", () => PublicKey.Import(a, ReadOnlySpan<byte>.Empty, KeyBlobFormat.None));
        }

        [Theory]
        [MemberData(nameof(PublicKeyBlobFormats))]
        public static void ImportEmpty(Type algorithmType, KeyBlobFormat format)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            Assert.Throws<FormatException>(() => PublicKey.Import(a, ReadOnlySpan<byte>.Empty, format));
        }

        #endregion

        #region TryImport

        [Fact]
        public static void TryImportWithNullAlgorithm()
        {
            Assert.Throws<ArgumentNullException>("algorithm", () => PublicKey.TryImport(null, ReadOnlySpan<byte>.Empty, KeyBlobFormat.None, out PublicKey pk));
        }

        [Theory]
        [MemberData(nameof(AsymmetricKeyAlgorithms))]
        public static void TryImportWithFormatNone(Type algorithmType)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            Assert.Throws<ArgumentException>("format", () => PublicKey.TryImport(a, ReadOnlySpan<byte>.Empty, KeyBlobFormat.None, out PublicKey pk));
        }

        [Theory]
        [MemberData(nameof(PublicKeyBlobFormats))]
        public static void TryImportEmpty(Type algorithmType, KeyBlobFormat format)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            Assert.False(PublicKey.TryImport(a, ReadOnlySpan<byte>.Empty, format, out PublicKey pk));
            Assert.Null(pk);
        }

        #endregion

        #region Equals

        [Theory]
        [MemberData(nameof(AsymmetricKeyAlgorithms))]
        public static void EqualAndSame(Type algorithmType)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            using (var k = new Key(a))
            {
                Assert.Same(k.PublicKey, k.PublicKey);
                Assert.True(k.PublicKey.Equals(k.PublicKey));
                Assert.True(k.PublicKey.Equals((object)k.PublicKey));
                Assert.Equal(k.PublicKey.GetHashCode(), k.PublicKey.GetHashCode());
            }
        }

        [Theory]
        [MemberData(nameof(AsymmetricKeyAlgorithms))]
        public static void EqualButNotSame(Type algorithmType)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            using (var k = new Key(a))
            {
                var b = k.Export(KeyBlobFormat.RawPublicKey);

                var pk1 = PublicKey.Import(a, b, KeyBlobFormat.RawPublicKey);
                var pk2 = PublicKey.Import(a, b, KeyBlobFormat.RawPublicKey);

                Assert.NotSame(pk1, pk2);
                Assert.True(pk1.Equals(pk2));
                Assert.True(pk1.Equals((object)pk2));
                Assert.Equal(pk1.GetHashCode(), pk2.GetHashCode());
            }
        }

        #endregion

        #region Export #1

        [Theory]
        [MemberData(nameof(AsymmetricKeyAlgorithms))]
        public static void ExportWithFormatNone(Type algorithmType)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            using (var k = new Key(a))
            {
                Assert.Throws<ArgumentException>("format", () => k.PublicKey.Export(KeyBlobFormat.None));
            }
        }

        [Theory]
        [MemberData(nameof(PublicKeyBlobFormats))]
        public static void ExportPublicKey(Type algorithmType, KeyBlobFormat format)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            using (var k = new Key(a))
            {
                var b = k.PublicKey.Export(format);
                Assert.NotNull(b);

                var pk = PublicKey.Import(a, b, format);
                Assert.NotNull(pk);
                Assert.Equal(k.PublicKey, pk);
                Assert.Same(a, pk.Algorithm);
            }
        }

        #endregion

        #region Export #2

        [Theory]
        [MemberData(nameof(AsymmetricKeyAlgorithms))]
        public static void ExportWithSpanWithFormatNone(Type algorithmType)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            using (var k = new Key(a))
            {
                Assert.Throws<ArgumentException>("format", () => k.PublicKey.Export(KeyBlobFormat.None, Span<byte>.Empty));
            }
        }

        [Theory]
        [MemberData(nameof(PublicKeyBlobFormats))]
        public static void ExportWithSpanTooSmall(Type algorithmType, KeyBlobFormat format)
        {
            var a = (Algorithm)Activator.CreateInstance(algorithmType);

            using (var k = new Key(a))
            {
                Assert.Throws<ArgumentException>("blob", () => k.PublicKey.Export(format, Span<byte>.Empty));
            }
        }

        #endregion
    }
}
