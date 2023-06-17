namespace IPFox.Tests
{
    public class IPGeodataRepositoryTests
    {
        public IPGeodataRepositoryTests()
        {
            
        }
        [Test]
        public async Task TestGetIPLocation_USAddr()
        {
            var detail = await IPGeodataRepository.GetIPLocationAsync("8.8.8.8");
            Assert.IsNotNull(detail);
            Assert.That(detail.Country, Is.EqualTo("美国"));
            Assert.That(detail.DataProvider, Is.EqualTo("Level3"));
        }
        [Test]
        public async Task TestGetIPLocation_DEAddr()
        {
            var detail = await IPGeodataRepository.GetIPLocationAsync("104.76.204.20");
            Assert.IsNotNull(detail);
            Assert.That(detail.Country, Is.EqualTo("德国"));
            Assert.That(detail.State, Is.EqualTo("法兰克福"));
            Assert.That(detail.City, Is.EqualTo("法兰克福"));
            Assert.That(detail.DataProvider, Is.EqualTo("阿卡迈"));
        }

        [Test]
        public async Task TestGetIPLocation_CNAddr()
        {
            var detail = await IPGeodataRepository.GetIPLocationAsync("120.24.78.68");
            Assert.IsNotNull(detail);
            Assert.That(detail.Country, Is.EqualTo("中国"));
            Assert.That(detail.State, Is.EqualTo("广东省"));
            Assert.That(detail.City, Is.EqualTo("深圳市"));
            Assert.That(detail.DataProvider, Is.EqualTo("阿里云"));
        }

        [Test]
        public async Task TestGetIPv6Addr_US()
        {
            var detail = await IPGeodataRepository.GetIPLocationAsync("240e:47d:c20:1627:30a3:ba0d:a5e6:ec19");
            Assert.IsNotNull(detail);
            Assert.That(detail.Country, Is.EqualTo("美国"));
            Assert.That(detail.State, Is.EqualTo("新泽西"));
            Assert.IsNull(detail.City);
            Assert.IsNull(detail.DataProvider);
        }
    }
}