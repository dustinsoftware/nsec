using System;
using NSec.Cryptography.Experimental;
using Xunit;

namespace NSec.Tests.Algorithms
{
    public static class Sha3_512Tests
    {
        private static readonly string s_hashOfEmpty = "a69f73cca23a9ac5c8b567dc185a756e97c982164fe25859e0d1dcc1475c80a615b2123af1f5f94c11e3e9402c3ac558f500199d95b6d3e301758586281dcd26";

        #region Properties

        [Fact]
        public static void Properties()
        {
            var a = new Sha3_512();

            Assert.Equal(32, a.MinHashSize);
            Assert.Equal(64, a.DefaultHashSize);
            Assert.Equal(64, a.MaxHashSize);
        }

        #endregion

        #region Hash #1

        [Fact]
        public static void HashEmpty()
        {
            var a = new Sha3_512();

            var expected = s_hashOfEmpty.DecodeHex();
            var actual = a.Hash(ReadOnlySpan<byte>.Empty);

            Assert.Equal(a.DefaultHashSize, actual.Length);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Hash #2

        [Theory]
        [InlineData(32)]
        [InlineData(41)]
        [InlineData(53)]
        [InlineData(61)]
        [InlineData(64)]
        public static void HashEmptyWithSize(int hashSize)
        {
            var a = new Sha3_512();

            var expected = s_hashOfEmpty.DecodeHex().Substring(0, hashSize);
            var actual = a.Hash(ReadOnlySpan<byte>.Empty, hashSize);

            Assert.Equal(hashSize, actual.Length);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Hash #3

        [Theory]
        [InlineData(32)]
        [InlineData(41)]
        [InlineData(53)]
        [InlineData(61)]
        [InlineData(64)]
        public static void HashEmptyWithSpan(int hashSize)
        {
            var a = new Sha3_512();

            var expected = s_hashOfEmpty.DecodeHex().Substring(0, hashSize);
            var actual = new byte[hashSize];

            a.Hash(ReadOnlySpan<byte>.Empty, actual);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
